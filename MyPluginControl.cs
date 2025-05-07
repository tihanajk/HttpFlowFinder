using McTools.Xrm.Connection;
using Microsoft.Identity.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using static ScintillaNET.Style;

namespace HttpFlowFinder
{
    public partial class MyPluginControl : PluginControlBase
    {
        private Settings mySettings;

        private string TOKEN_FOR_CALLBACK = "";

        private string TENANT_ID;

        private string ENV_1;

        public MyPluginControl()
        {
            InitializeComponent();
            InitializeFlowView();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }


        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            TENANT_ID = detail.TenantId.ToString();

            ENV_1 = detail.EnvironmentId.ToString();

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private async Task<string> GetValidAccessTokenAsync(string tenantId)
        {
            if (!IsTokenExpired(TOKEN_FOR_CALLBACK)) return TOKEN_FOR_CALLBACK;
            return await GetAccessTokenForListCallbackUrl(tenantId);
        }

        private bool IsTokenExpired(string token)
        {
            if (string.IsNullOrEmpty(token)) return true;

            try
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var expiry = jwtToken.ValidTo;

                // Convert to local time if necessary
                return expiry < DateTime.UtcNow;
            }
            catch
            {
                return true; // treat as expired if anything goes wrong
            }
        }

        private class AccessTokenResponse
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string scope { get; set; }
            public string refresh_token { get; set; }
            public string id_token { get; set; }
        }

        private async Task<string> GetAccessTokenForListCallbackUrl(string tenantId)
        {
            var clientId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
            var redirectUri = "app://58145B91-0C36-4500-8554-080854F2AC97";
            var audienceUrl = "https://service.flow.microsoft.com";
            var scopes = new[] { $"{audienceUrl}/.default" };

            var app = PublicClientApplicationBuilder.Create(clientId).WithRedirectUri(redirectUri).Build();

            AuthenticationResult authResult;

            try
            {
                var accounts = await app.GetAccountsAsync();
                var account = accounts.FirstOrDefault();

                authResult = await app
                    .AcquireTokenSilent(scopes, account)
                    .WithTenantId(tenantId)
                    .ExecuteAsync();

                TOKEN_FOR_CALLBACK = authResult.AccessToken;

            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    authResult = await app
                        .AcquireTokenInteractive(scopes)
                        .WithTenantId(tenantId)
                        .ExecuteAsync();

                    TOKEN_FOR_CALLBACK = authResult.AccessToken;

                    HideNotification();
                }
                catch (MsalClientException ex)
                {
                    if (ex.ErrorCode == "authentication_canceled")
                    {
                        return "";
                    }
                }
            }

            return TOKEN_FOR_CALLBACK;
        }        

        private static FlowCallbackResponse GetCallback(string envId, string flowId, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.flow.microsoft.com/providers/Microsoft.ProcessSimple/environments/{envId}/flows/{flowId}/triggers/manual/listCallbackUrl?api-version=2016-11-01");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = client.SendAsync(request).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FlowCallbackResponse>(json);
        }

        private void GetHttpFlows()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Fetching flows",
                Work = (worker, args) =>
                {
                    var fetchHttpFlows = $@"<fetch>
                                              <entity name='workflow'>
                                                <attribute name='name' />
                                                <attribute name='workflowid' />                                                
                                                <attribute name='clientdata' />
                                                <filter>
                                                  <condition attribute='category' operator='eq' value='5' />
                                                  <condition attribute='clientdata' operator='like' value='%""kind"":""Http"",%' />
                                                </filter>
                                              </entity>
                                            </fetch>";
                    var flows = Service.RetrieveMultiple(new FetchExpression(fetchHttpFlows)).Entities.ToList();

                    args.Result = flows;
                },
                PostWorkCallBack = async (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as List<Entity>;


                    var dataTable = InitializeFlowView();

                    if (result.Count == 0) return;

                    //var TOKEN_FOR_FLOWS = await GetValidAccessTokenAsync(TENANT_ID);

                    foreach (var flow in result)
                    {
                        var flowId = flow.Id;
                        var flowName = flow["name"];

                        var clientData = JsonConvert.DeserializeObject<FlowClientData>((string)flow["clientdata"]);

                        var triggers = clientData.properties.definition.triggers;

                        var triggerAuthType = triggers.manual.inputs.triggerAuthenticationType;
                        var triggerAllowedUsers = triggers.manual.inputs.triggerAllowedUsers;
                        var schema = triggers.manual.inputs.schema;

                        var schema_string = schema == null ? "" : JsonConvert.SerializeObject(schema);

                        //var flowCallback = GetCallback(ENV_1, flowId.ToString(), TOKEN_FOR_FLOWS);
                        //if (flowCallback.response == null) { MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        //var httpTrigger = flowCallback.response.value;

                        dataTable.Rows.Add(flowId, flowName, "-", triggerAuthType, triggerAllowedUsers, schema_string);
                    }

                    FlowsGrid.DataSource = dataTable;
                }
            });
        }

        private void fetchFlowsBtn_Click(object sender, EventArgs e)
        {
            ExecuteMethod(GetHttpFlows);
        }

        private DataTable InitializeFlowView()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(Guid));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Trigger", typeof(string));
            dataTable.Columns.Add("Authentication Type", typeof(string));
            dataTable.Columns.Add("Allowed Users", typeof(string));
            dataTable.Columns.Add("Schema", typeof(string));


            FlowsGrid.DataSource = dataTable;
            FlowsGrid.Columns["ID"].Visible = false;

            FlowsGrid.CellFormatting += dataGridView1_CellFormatting;
            FlowsGrid.RowTemplate.Height = 30;

            //FlowsGrid.Columns["Name"].Width = 180;
            //FlowsGrid.Columns["Trigger"].Width = 180;
            //FlowsGrid.Columns["Authentication Type"].Width = 180;
            //FlowsGrid.Columns["Allowed Users"].Width = 180;
            //FlowsGrid.Columns["Trigger"].Width = 180;

            return dataTable;
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the column is the "name" column
            if (FlowsGrid.Columns[e.ColumnIndex].HeaderText == "Authentication Type" && e.Value != null)
            {
                string cellValue = e.Value.ToString().ToLower();

                // Change cell background color based on value
                if (cellValue == "all")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (cellValue == "tenant")
                {
                    e.CellStyle.BackColor = Color.Yellow;
                    e.CellStyle.ForeColor = Color.Black;
                }
                else if (cellValue == "user")
                {
                    e.CellStyle.BackColor = Color.Green;
                    e.CellStyle.ForeColor = Color.White;
                }
            }
        }

    }
}
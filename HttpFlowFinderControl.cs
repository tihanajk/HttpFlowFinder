using HttpFlowFinder.Helpers;
using McTools.Xrm.Connection;
using Microsoft.Identity.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace HttpFlowFinder
{
    public partial class HttpFlowFinderControl : PluginControlBase
    {
        private Settings mySettings;

        private string TOKEN_FOR_CALLBACK = "";

        private Guid TENANT_ID;

        private string ENV_1 = "";

        private Dictionary<Guid, string> flowsCache = new Dictionary<Guid, string>();

        private ConnectionDetail CONNECTION;
        public HttpFlowFinderControl()
        {
            InitializeComponent();
            InitializeFlowView();            
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            if (ConnectionDetail.TenantId == Guid.Empty)
            {
                ShowInfoNotification("You are using the deprecated connection method. Please use OAuth/MFA or Client ID/Secret method if you want to see the triggers", new Uri("https://learn.microsoft.com/en-us/power-platform/admin/manage-application-users#create-an-application-user"));
            }

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

            //if (!string.IsNullOrWhiteSpace(ConnectionDetail.S2SClientSecret))
            //{
            //    ShowInfoNotification(
            //       "You are using the deprecated connection method. Please use OAuth/MFA or Client ID/Secret method.",
            //       new Uri("https://learn.microsoft.com/en-us/power-platform/admin/manage-application-users#create-an-application-user")
            //       );
            //}

            CONNECTION = detail;

            TENANT_ID = detail.TenantId;

            ENV_1 = detail?.EnvironmentId?.ToString();

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }            

            ExecuteMethod(GetSolutions);
        }

        private async Task<string> GetValidAccessTokenAsync(Guid tenantId)
        {
            if (tenantId == Guid.Empty) return "";
            return await GetAccessTokenForListCallbackUrl(tenantId);
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

        private async Task<string> GetAccessTokenForListCallbackUrl(Guid tenantId)
        {
            var clientId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
            var redirectUri = "app://58145B91-0C36-4500-8554-080854F2AC97";
            var audienceUrl = "https://service.flow.microsoft.com";
            var scopes = new[] { $"{audienceUrl}/.default" };

            var app = PublicClientApplicationBuilder.Create(clientId).WithRedirectUri(redirectUri).Build();

            Microsoft.Identity.Client.AuthenticationResult authResult;

            try
            {
                var accounts = await app.GetAccountsAsync();
                var account = accounts.FirstOrDefault();

                authResult = await app.AcquireTokenSilent(scopes, account).WithTenantId(tenantId.ToString()).ExecuteAsync();

                TOKEN_FOR_CALLBACK = authResult.AccessToken;

            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    authResult = await app
                        .AcquireTokenInteractive(scopes)
                        .WithTenantId(tenantId.ToString())
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

        private List<FlowInfo> _flows = new List<FlowInfo>();
        private string prevSolutionSelected = "";
        private void GetHttpFlows()
        {
            var selectedSolution = solutionPicker.SelectedItem as ListObject;

            if (selectedSolution == null) return;

            var solutionId = selectedSolution.Value;

            var solutionFilter = solutionId != "1" ?
                           $@"<link-entity name='solutioncomponent' from='objectid' to='workflowid' link-type='inner' alias='aa'>
                                  <filter>
                                    <condition attribute='solutionid' operator='eq' value='{solutionId}' />
                                  </filter>
                                </link-entity>" : "";

            var activeFilter = activeCheck.Checked;
            var draftFilter = draftCheck.Checked;
            var suspFilter = suspendedCheck.Checked;

            var flowFilter = "";
            if (activeFilter || draftFilter || suspFilter)
            {
                flowFilter += $@"<filter type='or'>";
                if (activeFilter) flowFilter += $@"<condition attribute='statecode' operator='eq' value='1' />";
                if (draftFilter) flowFilter += $@"<condition attribute='statecode' operator='eq' value='0' />";
                if (suspFilter) flowFilter += $@"<condition attribute='statecode' operator='eq' value='2' />";
                flowFilter += "</filter>";
            }

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
                                                <attribute name='statecode' />
                                                <filter>
                                                  <condition attribute='category' operator='eq' value='5' />
                                                  <condition attribute='clientdata' operator='like' value='%""type"":""Request"",""kind"":""Http"",%' />
                                                </filter>
                                                {flowFilter}
                                                {solutionFilter}
                                                <order attribute='name' />
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

                    var i = 0;

                    if (TOKEN_FOR_CALLBACK == "")
                    {
                        await GetValidAccessTokenAsync(TENANT_ID);
                    }

                    foreach (var flow in result)
                    {
                        i++;
                        loadingIndicator.Text = $"Loading {i}/{result.Count}";
                        var flowId = flow.Id;
                        var flowName = (string)flow["name"];
                        var flowState = ((OptionSetValue)flow["statecode"]).Value;
                        var flowState_display = flow.FormattedValues["statecode"];

                        var clientData = JsonConvert.DeserializeObject<FlowClientData>((string)flow["clientdata"]);

                        var triggers = clientData.properties.definition.triggers;

                        var triggerAuthType = triggers.manual.inputs.triggerAuthenticationType;
                        if (triggerAuthType == "" || triggerAuthType == null) triggerAuthType = "All";

                        if (triggerAuthType == "All" && !anyoneCheck.Checked) continue;
                        if (triggerAuthType == "Tenant" && !tenantCheck.Checked) continue;
                        if (triggerAuthType == "User" && !usersCheck.Checked) continue;

                        var triggerAllowedUsers = triggers.manual.inputs.triggerAllowedUsers;
                        var schema = triggers.manual.inputs.schema;

                        var schema_string = schema == null ? "" : JsonConvert.SerializeObject(schema);

                        var httpTrigger = "-";

                        if (flowsCache.ContainsKey(flowId)) httpTrigger = flowsCache[flowId];
                        else if (TOKEN_FOR_CALLBACK != "")
                        {
                            var flowCallback = GetCallback(ENV_1, flowId.ToString(), TOKEN_FOR_CALLBACK);
                            if (flowCallback.response != null)
                            {
                                httpTrigger = flowCallback.response.value;
                                flowsCache.Add(flowId, httpTrigger);
                            }
                        }

                        var link = $"https://make.powerautomate.com/environments/{ENV_1}/{(solutionId == "1" ? "" : $"solutions/{solutionId}/")}flows/{flowId}/details";
                        var flowInfo = new FlowInfo
                        {
                            name = flowName,
                            id = flowId,
                            state = flowState,
                            state_display = flowState_display,
                            trigger = httpTrigger,
                            authType = triggerAuthType,
                            users = triggerAllowedUsers,
                            schema = schema_string,
                            link = link
                        };
                        var index = _flows.FindIndex(f => f.id == flowId);
                        if (index == -1)
                            _flows.Add(flowInfo);
                        else
                            _flows[index] = flowInfo;

                        _filtered = _flows.ToList();

                        var state = flowState == 0 ? "Draft" : flowState == 1 ? "Activated" : "Suspended";
                        dataTable.Rows.Add(flowId, flowName, httpTrigger, triggerAuthType, triggerAllowedUsers, schema_string, state, link);
                    }

                    FlowsGrid.DataSource = dataTable;

                    flowsCounter.Text = dataTable.Rows.Count.ToString();

                    loadingIndicator.Text = "";
                }
            });
        }

        private void LoadFlowsBtn_Click(object sender, EventArgs e)
        {
            ExecuteMethod(GetHttpFlows);
        }



        private class ListObject
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private void GetSolutions()
        {
            var managed = managedCheck.Checked;
            var unmanaged = unmanagedCheck.Checked;

            var all = (managed && unmanaged) || (!managed && !unmanaged);

            var message = $"Fetching All {(managed && !unmanaged ? "Managed " : "")}Solutions";
            WorkAsync(new WorkAsyncInfo()
            {
                Message = message,
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var query_ismanaged = managed;
                    var query = new QueryExpression("solution");
                    query.ColumnSet.AddColumns("friendlyname", "solutionid", "uniquename");
                    query.AddOrder("friendlyname", OrderType.Ascending);

                    if (!all) query.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, query_ismanaged);

                    args.Result = Service.RetrieveMultiple(query);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;

                    solutionPicker.DataSource = new List<ListObject>();

                    if (result != null)
                    {
                        var items = new List<ListObject>();

                        items.Add(new ListObject()
                        {
                            Name = "<All Solutions>",
                            Value = "1"
                        });

                        foreach (var sol in result.Entities)
                        {
                            var friendlyName = sol.Contains("friendlyname") ? (string)sol["friendlyname"] : "";
                            var name = $"{friendlyName} ({sol["uniquename"]})";
                            var solutionId = sol.Id.ToString();

                            items.Add(new ListObject()
                            {
                                Name = name,
                                Value = solutionId,
                            });
                        }

                        solutionPicker.DataSource = items;
                        solutionPicker.DisplayMember = "Name";
                        solutionPicker.ValueMember = "Value";
                    }
                }
            });
        }

        private void LoadSolutionsBtn_Click(object sender, EventArgs e)
        {
            ExecuteMethod(GetSolutions);
        }

        private void ManagedCheck_CheckedChanged(object sender, EventArgs e)
        {
            ExecuteMethod(GetSolutions);
        }

        private void UnmanagedCheck_CheckedChanged(object sender, EventArgs e)
        {
            ExecuteMethod(GetSolutions);
        }

        private void OnSolutionSelected(object sender, EventArgs e)
        {
            var selectedSolution = solutionPicker.SelectedItem as ListObject;

            if (selectedSolution == null) return;

            var solutionId = selectedSolution.Value;
            if (prevSolutionSelected == solutionId) return;

            prevSolutionSelected = solutionId;

            ExecuteMethod(GetHttpFlows);
        }

        private List<FlowInfo> _filtered = new List<FlowInfo>();

        private void FilterFlows()
        {
            var active = activeCheck.Checked;
            var draft = draftCheck.Checked;
            var susp = suspendedCheck.Checked;

            if (!active && !draft && !susp) return;

            _filtered = _flows.Where(f =>
                                (active && f.state == 1) ||
                                (draft && f.state == 0) ||
                                (susp && f.state == 2)
                            ).ToList();

            _filtered = _filtered.Where(f =>
                (anyoneCheck.Checked && f.authType == "All") ||
                (tenantCheck.Checked && f.authType == "Tenant") ||
                (usersCheck.Checked && f.authType == "User")
            ).ToList();

            var term = searchBox.Text;
            if (!string.IsNullOrEmpty(term))
            {
                _filtered = _filtered.Where(f =>
                f.name.ToLower().Contains(term.ToLower()) || f.trigger.ToLower().Contains(term.ToLower())).ToList();
            }

            var dataTable = InitializeFlowView();

            foreach (var flow in _filtered)
            {
                var state = flow.state == 0 ? "Draft" : flow.state == 1 ? "Activated" : "Suspended";
                dataTable.Rows.Add(flow.id, flow.name, flow.trigger, flow.authType, flow.users, flow.schema, state, flow.link);
            }

            FlowsGrid.DataSource = dataTable;
            flowsCounter.Text = dataTable.Rows.Count.ToString();
        }


        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CONNECTION.S2SClientSecret))
            {
                MessageBox.Show(
                    "You are using the deprecated connection method. Please use OAuth/MFA or Client ID/Secret method.",
                    "Deprecated Connection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            GetValidAccessTokenAsync(TENANT_ID);
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (_filtered.Count == 0)
            {
                MessageBox.Show("No data in the table", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var excelHelper = new ExcelHelper();

            var fileName = "Http flows";
            excelHelper.HandleExcel(fileName, _filtered);
        }

        private void SuspendedCheck_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlows();
        }

        private void ActiveCheck_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlows();
        }

        private void DraftCheck_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlows();
        }

        private void AnyoneCheck_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlows();
        }

        private void TenantCheck_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlows();
        }

        private void UsersCheck_CheckedChanged(object sender, EventArgs e)
        {
            FilterFlows();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            FilterFlows();
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
            dataTable.Columns.Add("State", typeof(string));
            dataTable.Columns.Add("Link", typeof(string));

            FlowsGrid.DataSource = dataTable;
            FlowsGrid.Columns["ID"].Visible = false;
            FlowsGrid.Columns["Link"].Visible = false;

            FlowsGrid.CellFormatting += DataGridView1_CellFormatting;
            FlowsGrid.RowTemplate.Height = 30;

            if (FlowsGrid.Columns["LinkButton"] == null)
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "";
                buttonColumn.Name = "LinkButton";
                buttonColumn.Text = "Open in Power Automate";
                buttonColumn.UseColumnTextForButtonValue = true;
                buttonColumn.Width = 50;
                FlowsGrid.Columns.Add(buttonColumn);
            }

            FlowsGrid.CellContentClick -= HandleFlowItemClick;
            FlowsGrid.CellContentClick += HandleFlowItemClick;

            return dataTable;
        }

        private void OpenLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to open the link: " + ex.Message);
            }
        }

        private void HandleFlowItemClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == FlowsGrid.Columns["LinkButton"]?.Index && e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                var link = (string)FlowsGrid.Rows[rowIndex].Cells["Link"].Value;

                OpenLink(link);
            }
        }
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
            else if (FlowsGrid.Columns[e.ColumnIndex].HeaderText == "State" && e.Value != null)
            {
                string cellValue = e.Value.ToString().ToLower();

                // Change cell background color based on value
                if (cellValue == "draft")
                {
                    e.CellStyle.BackColor = Color.Yellow;
                    e.CellStyle.ForeColor = Color.Black;
                }
                else if (cellValue == "activated")
                {
                    e.CellStyle.BackColor = Color.Green;
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (cellValue == "suspended")
                {
                    e.CellStyle.BackColor = Color.Gray;
                    e.CellStyle.ForeColor = Color.White;
                }
            }
        }

    }
}
namespace HttpFlowFinder
{
    partial class HttpFlowFinderControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HttpFlowFinderControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.loginBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.solutionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportBtn = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.unmanagedCheck = new System.Windows.Forms.CheckBox();
            this.managedCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.solutionPicker = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.loadingIndicator = new System.Windows.Forms.Label();
            this.flowsCounter = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FlowsGrid = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.suspendedCheck = new System.Windows.Forms.CheckBox();
            this.draftCheck = new System.Windows.Forms.CheckBox();
            this.activeCheck = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.usersCheck = new System.Windows.Forms.CheckBox();
            this.tenantCheck = new System.Windows.Forms.CheckBox();
            this.anyoneCheck = new System.Windows.Forms.CheckBox();
            this.toolStripMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlowsGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginBtn,
            this.toolStripButton1,
            this.exportBtn});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(65535, 40);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // loginBtn
            // 
            this.loginBtn.Image = ((System.Drawing.Image)(resources.GetObject("loginBtn.Image")));
            this.loginBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(92, 34);
            this.loginBtn.Text = "Login";
            this.loginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solutionsToolStripMenuItem,
            this.flowsToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(103, 34);
            this.toolStripButton1.Text = "Load";
            // 
            // solutionsToolStripMenuItem
            // 
            this.solutionsToolStripMenuItem.Name = "solutionsToolStripMenuItem";
            this.solutionsToolStripMenuItem.Size = new System.Drawing.Size(315, 40);
            this.solutionsToolStripMenuItem.Text = "Solutions";
            this.solutionsToolStripMenuItem.Click += new System.EventHandler(this.LoadSolutionsBtn_Click);
            // 
            // flowsToolStripMenuItem
            // 
            this.flowsToolStripMenuItem.Name = "flowsToolStripMenuItem";
            this.flowsToolStripMenuItem.Size = new System.Drawing.Size(315, 40);
            this.flowsToolStripMenuItem.Text = "Flows";
            this.flowsToolStripMenuItem.Click += new System.EventHandler(this.LoadFlowsBtn_Click);
            // 
            // exportBtn
            // 
            this.exportBtn.Image = ((System.Drawing.Image)(resources.GetObject("exportBtn.Image")));
            this.exportBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(100, 34);
            this.exportBtn.Text = "Export";
            this.exportBtn.Click += new System.EventHandler(this.ExportBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.unmanagedCheck);
            this.groupBox1.Controls.Add(this.managedCheck);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.solutionPicker);
            this.groupBox1.Location = new System.Drawing.Point(6, 58);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(64611, 184);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solution info";
            // 
            // unmanagedCheck
            // 
            this.unmanagedCheck.AutoSize = true;
            this.unmanagedCheck.Checked = true;
            this.unmanagedCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.unmanagedCheck.Location = new System.Drawing.Point(184, 130);
            this.unmanagedCheck.Name = "unmanagedCheck";
            this.unmanagedCheck.Size = new System.Drawing.Size(145, 29);
            this.unmanagedCheck.TabIndex = 3;
            this.unmanagedCheck.Text = "Unmanaged";
            this.unmanagedCheck.UseVisualStyleBackColor = true;
            this.unmanagedCheck.CheckedChanged += new System.EventHandler(this.UnmanagedCheck_CheckedChanged);
            // 
            // managedCheck
            // 
            this.managedCheck.AutoSize = true;
            this.managedCheck.Checked = true;
            this.managedCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.managedCheck.Location = new System.Drawing.Point(26, 130);
            this.managedCheck.Name = "managedCheck";
            this.managedCheck.Size = new System.Drawing.Size(121, 29);
            this.managedCheck.TabIndex = 2;
            this.managedCheck.Text = "Managed";
            this.managedCheck.UseVisualStyleBackColor = true;
            this.managedCheck.CheckedChanged += new System.EventHandler(this.ManagedCheck_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Solution:";
            // 
            // solutionPicker
            // 
            this.solutionPicker.FormattingEnabled = true;
            this.solutionPicker.Location = new System.Drawing.Point(26, 81);
            this.solutionPicker.Name = "solutionPicker";
            this.solutionPicker.Size = new System.Drawing.Size(446, 32);
            this.solutionPicker.TabIndex = 0;
            this.solutionPicker.SelectedIndexChanged += new System.EventHandler(this.OnSolutionSelected);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.loadingIndicator);
            this.groupBox2.Controls.Add(this.flowsCounter);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.searchBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.FlowsGrid);
            this.groupBox2.Location = new System.Drawing.Point(6, 253);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(65524, 65277);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Flows";
            // 
            // loadingIndicator
            // 
            this.loadingIndicator.AutoSize = true;
            this.loadingIndicator.Location = new System.Drawing.Point(965, 40);
            this.loadingIndicator.Name = "loadingIndicator";
            this.loadingIndicator.Size = new System.Drawing.Size(97, 25);
            this.loadingIndicator.TabIndex = 9;
            this.loadingIndicator.Text = "Loading...";
            // 
            // flowsCounter
            // 
            this.flowsCounter.AutoSize = true;
            this.flowsCounter.Location = new System.Drawing.Point(862, 40);
            this.flowsCounter.Name = "flowsCounter";
            this.flowsCounter.Size = new System.Drawing.Size(23, 25);
            this.flowsCounter.TabIndex = 8;
            this.flowsCounter.Text = "0";
            this.flowsCounter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(734, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Flows count:";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(75, 36);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(614, 29);
            this.searchBox.TabIndex = 2;
            this.searchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filter:";
            // 
            // FlowsGrid
            // 
            this.FlowsGrid.AllowUserToAddRows = false;
            this.FlowsGrid.AllowUserToDeleteRows = false;
            this.FlowsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FlowsGrid.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.FlowsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FlowsGrid.GridColor = System.Drawing.SystemColors.MenuHighlight;
            this.FlowsGrid.Location = new System.Drawing.Point(11, 87);
            this.FlowsGrid.Margin = new System.Windows.Forms.Padding(6);
            this.FlowsGrid.Name = "FlowsGrid";
            this.FlowsGrid.RowHeadersWidth = 72;
            this.FlowsGrid.Size = new System.Drawing.Size(65502, 65178);
            this.FlowsGrid.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.suspendedCheck);
            this.groupBox3.Controls.Add(this.draftCheck);
            this.groupBox3.Controls.Add(this.activeCheck);
            this.groupBox3.Location = new System.Drawing.Point(767, 58);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(64291, 184);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Flow filters";
            // 
            // suspendedCheck
            // 
            this.suspendedCheck.AutoSize = true;
            this.suspendedCheck.Checked = true;
            this.suspendedCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.suspendedCheck.Location = new System.Drawing.Point(19, 118);
            this.suspendedCheck.Name = "suspendedCheck";
            this.suspendedCheck.Size = new System.Drawing.Size(139, 29);
            this.suspendedCheck.TabIndex = 2;
            this.suspendedCheck.Text = "Suspended";
            this.suspendedCheck.UseVisualStyleBackColor = true;
            this.suspendedCheck.CheckedChanged += new System.EventHandler(this.SuspendedCheck_CheckedChanged);
            // 
            // draftCheck
            // 
            this.draftCheck.AutoSize = true;
            this.draftCheck.Checked = true;
            this.draftCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.draftCheck.Location = new System.Drawing.Point(19, 83);
            this.draftCheck.Name = "draftCheck";
            this.draftCheck.Size = new System.Drawing.Size(79, 29);
            this.draftCheck.TabIndex = 1;
            this.draftCheck.Text = "Draft";
            this.draftCheck.UseVisualStyleBackColor = true;
            this.draftCheck.CheckedChanged += new System.EventHandler(this.DraftCheck_CheckedChanged);
            // 
            // activeCheck
            // 
            this.activeCheck.AutoSize = true;
            this.activeCheck.Checked = true;
            this.activeCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activeCheck.Location = new System.Drawing.Point(19, 48);
            this.activeCheck.Name = "activeCheck";
            this.activeCheck.Size = new System.Drawing.Size(103, 29);
            this.activeCheck.TabIndex = 0;
            this.activeCheck.Text = "Actived";
            this.activeCheck.UseVisualStyleBackColor = true;
            this.activeCheck.CheckedChanged += new System.EventHandler(this.ActiveCheck_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.usersCheck);
            this.groupBox4.Controls.Add(this.tenantCheck);
            this.groupBox4.Controls.Add(this.anyoneCheck);
            this.groupBox4.Location = new System.Drawing.Point(1208, 58);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox4.Size = new System.Drawing.Size(64322, 184);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Auth type";
            // 
            // usersCheck
            // 
            this.usersCheck.AutoSize = true;
            this.usersCheck.Checked = true;
            this.usersCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.usersCheck.Location = new System.Drawing.Point(18, 118);
            this.usersCheck.Name = "usersCheck";
            this.usersCheck.Size = new System.Drawing.Size(160, 29);
            this.usersCheck.TabIndex = 4;
            this.usersCheck.Text = "Specific users";
            this.usersCheck.UseVisualStyleBackColor = true;
            this.usersCheck.CheckedChanged += new System.EventHandler(this.UsersCheck_CheckedChanged);
            // 
            // tenantCheck
            // 
            this.tenantCheck.AutoSize = true;
            this.tenantCheck.Checked = true;
            this.tenantCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tenantCheck.Location = new System.Drawing.Point(18, 83);
            this.tenantCheck.Name = "tenantCheck";
            this.tenantCheck.Size = new System.Drawing.Size(190, 29);
            this.tenantCheck.TabIndex = 3;
            this.tenantCheck.Text = "Anyone in tentant";
            this.tenantCheck.UseVisualStyleBackColor = true;
            this.tenantCheck.CheckedChanged += new System.EventHandler(this.TenantCheck_CheckedChanged);
            // 
            // anyoneCheck
            // 
            this.anyoneCheck.AutoSize = true;
            this.anyoneCheck.Checked = true;
            this.anyoneCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.anyoneCheck.Location = new System.Drawing.Point(18, 48);
            this.anyoneCheck.Name = "anyoneCheck";
            this.anyoneCheck.Size = new System.Drawing.Size(106, 29);
            this.anyoneCheck.TabIndex = 2;
            this.anyoneCheck.Text = "Anyone";
            this.anyoneCheck.UseVisualStyleBackColor = true;
            this.anyoneCheck.CheckedChanged += new System.EventHandler(this.AnyoneCheck_CheckedChanged);
            // 
            // HttpFlowFinderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "HttpFlowFinderControl";
            this.Size = new System.Drawing.Size(65535, 65535);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlowsGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView FlowsGrid;
        private System.Windows.Forms.ToolStripButton loginBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox draftCheck;
        private System.Windows.Forms.CheckBox activeCheck;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox usersCheck;
        private System.Windows.Forms.CheckBox tenantCheck;
        private System.Windows.Forms.CheckBox anyoneCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox solutionPicker;
        private System.Windows.Forms.CheckBox unmanagedCheck;
        private System.Windows.Forms.CheckBox managedCheck;
        private System.Windows.Forms.CheckBox suspendedCheck;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label flowsCounter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label loadingIndicator;
        private System.Windows.Forms.ToolStripButton exportBtn;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem solutionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flowsToolStripMenuItem;
    }
}

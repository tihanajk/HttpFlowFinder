namespace HttpFlowFinder
{
    partial class MyPluginControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.loginBtn = new System.Windows.Forms.ToolStripButton();
            this.fetchFlowsBtn = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FlowsGrid = new System.Windows.Forms.DataGridView();
            this.toolStripMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlowsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginBtn,
            this.fetchFlowsBtn});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1673, 40);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // loginBtn
            // 
            this.loginBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loginBtn.Image = ((System.Drawing.Image)(resources.GetObject("loginBtn.Image")));
            this.loginBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(68, 34);
            this.loginBtn.Text = "Login";
            // 
            // fetchFlowsBtn
            // 
            this.fetchFlowsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fetchFlowsBtn.Image = ((System.Drawing.Image)(resources.GetObject("fetchFlowsBtn.Image")));
            this.fetchFlowsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fetchFlowsBtn.Name = "fetchFlowsBtn";
            this.fetchFlowsBtn.Size = new System.Drawing.Size(121, 34);
            this.fetchFlowsBtn.Text = "Fetch flows";
            this.fetchFlowsBtn.Click += new System.EventHandler(this.fetchFlowsBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(6, 52);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(1662, 190);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FlowsGrid);
            this.groupBox2.Location = new System.Drawing.Point(6, 253);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(1662, 642);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
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
            this.FlowsGrid.Location = new System.Drawing.Point(11, 35);
            this.FlowsGrid.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlowsGrid.Name = "FlowsGrid";
            this.FlowsGrid.RowHeadersWidth = 72;
            this.FlowsGrid.Size = new System.Drawing.Size(1640, 596);
            this.FlowsGrid.TabIndex = 0;
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1673, 900);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlowsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView FlowsGrid;
        private System.Windows.Forms.ToolStripButton fetchFlowsBtn;
        private System.Windows.Forms.ToolStripButton loginBtn;
    }
}

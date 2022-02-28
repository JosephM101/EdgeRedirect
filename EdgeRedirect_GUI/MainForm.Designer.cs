namespace EdgeRedirect_GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_done = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_browsers = new System.Windows.Forms.ComboBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem_about = new System.Windows.Forms.MenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_redirectSearchesSetting = new System.Windows.Forms.CheckBox();
            this.comboBox_redirectSearches_searchEngine = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_done
            // 
            this.button_done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_done.Location = new System.Drawing.Point(251, 198);
            this.button_done.Name = "button_done";
            this.button_done.Size = new System.Drawing.Size(75, 23);
            this.button_done.TabIndex = 0;
            this.button_done.Text = "Close";
            this.button_done.UseVisualStyleBackColor = true;
            this.button_done.Click += new System.EventHandler(this.button_done_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(12, 198);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 1;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBox_browsers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 54);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Browser to use";
            // 
            // comboBox_browsers
            // 
            this.comboBox_browsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_browsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_browsers.FormattingEnabled = true;
            this.comboBox_browsers.Location = new System.Drawing.Point(7, 20);
            this.comboBox_browsers.Name = "comboBox_browsers";
            this.comboBox_browsers.Size = new System.Drawing.Size(301, 21);
            this.comboBox_browsers.TabIndex = 0;
            this.comboBox_browsers.SelectedIndexChanged += new System.EventHandler(this.comboBox_browsers_SelectedIndexChanged);
            this.comboBox_browsers.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboBox_browsers_Format);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_about});
            // 
            // menuItem_about
            // 
            this.menuItem_about.Index = 0;
            this.menuItem_about.Text = "About";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBox_redirectSearchesSetting);
            this.groupBox2.Controls.Add(this.comboBox_redirectSearches_searchEngine);
            this.groupBox2.Location = new System.Drawing.Point(12, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 74);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Searches";
            // 
            // checkBox_redirectSearchesSetting
            // 
            this.checkBox_redirectSearchesSetting.AutoSize = true;
            this.checkBox_redirectSearchesSetting.Location = new System.Drawing.Point(7, 20);
            this.checkBox_redirectSearchesSetting.Name = "checkBox_redirectSearchesSetting";
            this.checkBox_redirectSearchesSetting.Size = new System.Drawing.Size(259, 17);
            this.checkBox_redirectSearchesSetting.TabIndex = 1;
            this.checkBox_redirectSearchesSetting.Text = "Redirect searches to the following search engine:";
            this.checkBox_redirectSearchesSetting.UseVisualStyleBackColor = true;
            this.checkBox_redirectSearchesSetting.CheckedChanged += new System.EventHandler(this.checkBox_redirectSearchesSetting_CheckedChanged);
            // 
            // comboBox_redirectSearches_searchEngine
            // 
            this.comboBox_redirectSearches_searchEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_redirectSearches_searchEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_redirectSearches_searchEngine.FormattingEnabled = true;
            this.comboBox_redirectSearches_searchEngine.Location = new System.Drawing.Point(6, 43);
            this.comboBox_redirectSearches_searchEngine.Name = "comboBox_redirectSearches_searchEngine";
            this.comboBox_redirectSearches_searchEngine.Size = new System.Drawing.Size(302, 21);
            this.comboBox_redirectSearches_searchEngine.TabIndex = 0;
            this.comboBox_redirectSearches_searchEngine.SelectedIndexChanged += new System.EventHandler(this.comboBox_redirectSearches_searchEngine_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AcceptButton = this.button_done;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(338, 233);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_done);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EdgeRedirect Configurator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_done;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_browsers;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem_about;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_redirectSearchesSetting;
        private System.Windows.Forms.ComboBox comboBox_redirectSearches_searchEngine;
    }
}


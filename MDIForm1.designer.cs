namespace ReSavePDFWinTool
{
	partial class MDIForm1
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
		///
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIForm1));
            this.TSMIQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.LanTradChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.LanSimpChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.LanEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnReSavePDF = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.prsBar = new System.Windows.Forms.ProgressBar();
            this.lblFolder = new System.Windows.Forms.Label();
            this.tbxFolder = new System.Windows.Forms.TextBox();
            this.btnFolder = new System.Windows.Forms.Button();
            this.lblFiles = new System.Windows.Forms.Label();
            this.tbxFiles = new System.Windows.Forms.TextBox();
            this.btnFiles = new System.Windows.Forms.Button();
            this.gbxResult = new System.Windows.Forms.GroupBox();
            this.cbxContainSubFolders = new System.Windows.Forms.CheckBox();
            this.rbnFiles = new System.Windows.Forms.RadioButton();
            this.rbnFolder = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.gbxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // TSMIQuit
            // 
            this.TSMIQuit.Name = "TSMIQuit";
            resources.ApplyResources(this.TSMIQuit, "TSMIQuit");
            // 
            // LanTradChinese
            // 
            this.LanTradChinese.Name = "LanTradChinese";
            resources.ApplyResources(this.LanTradChinese, "LanTradChinese");
            // 
            // LanSimpChinese
            // 
            this.LanSimpChinese.Name = "LanSimpChinese";
            resources.ApplyResources(this.LanSimpChinese, "LanSimpChinese");
            // 
            // LanEnglish
            // 
            this.LanEnglish.Name = "LanEnglish";
            resources.ApplyResources(this.LanEnglish, "LanEnglish");
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnReSavePDF
            // 
            resources.ApplyResources(this.btnReSavePDF, "btnReSavePDF");
            this.btnReSavePDF.Name = "btnReSavePDF";
            this.btnReSavePDF.UseVisualStyleBackColor = true;
            this.btnReSavePDF.Click += new System.EventHandler(this.btnReSavePDF_Click);
            // 
            // lblResult
            // 
            resources.ApplyResources(this.lblResult, "lblResult");
            this.lblResult.Name = "lblResult";
            // 
            // txtResult
            // 
            resources.ApplyResources(this.txtResult, "txtResult");
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.TextChanged += new System.EventHandler(this.txtResult_TextChanged);
            // 
            // prsBar
            // 
            resources.ApplyResources(this.prsBar, "prsBar");
            this.prsBar.Name = "prsBar";
            // 
            // lblFolder
            // 
            resources.ApplyResources(this.lblFolder, "lblFolder");
            this.lblFolder.Name = "lblFolder";
            // 
            // tbxFolder
            // 
            this.tbxFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.tbxFolder, "tbxFolder");
            this.tbxFolder.Name = "tbxFolder";
            // 
            // btnFolder
            // 
            resources.ApplyResources(this.btnFolder, "btnFolder");
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // lblFiles
            // 
            resources.ApplyResources(this.lblFiles, "lblFiles");
            this.lblFiles.Name = "lblFiles";
            // 
            // tbxFiles
            // 
            this.tbxFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.tbxFiles, "tbxFiles");
            this.tbxFiles.Name = "tbxFiles";
            // 
            // btnFiles
            // 
            resources.ApplyResources(this.btnFiles, "btnFiles");
            this.btnFiles.Name = "btnFiles";
            this.btnFiles.UseVisualStyleBackColor = true;
            this.btnFiles.Click += new System.EventHandler(this.btnFiles_Click);
            // 
            // gbxResult
            // 
            this.gbxResult.Controls.Add(this.button1);
            this.gbxResult.Controls.Add(this.cbxContainSubFolders);
            this.gbxResult.Controls.Add(this.rbnFiles);
            this.gbxResult.Controls.Add(this.rbnFolder);
            this.gbxResult.Controls.Add(this.btnFiles);
            this.gbxResult.Controls.Add(this.tbxFiles);
            this.gbxResult.Controls.Add(this.lblFiles);
            this.gbxResult.Controls.Add(this.btnFolder);
            this.gbxResult.Controls.Add(this.tbxFolder);
            this.gbxResult.Controls.Add(this.lblFolder);
            this.gbxResult.Controls.Add(this.prsBar);
            this.gbxResult.Controls.Add(this.txtResult);
            this.gbxResult.Controls.Add(this.lblResult);
            this.gbxResult.Controls.Add(this.btnReSavePDF);
            resources.ApplyResources(this.gbxResult, "gbxResult");
            this.gbxResult.Name = "gbxResult";
            this.gbxResult.TabStop = false;
            // 
            // cbxContainSubFolders
            // 
            resources.ApplyResources(this.cbxContainSubFolders, "cbxContainSubFolders");
            this.cbxContainSubFolders.Name = "cbxContainSubFolders";
            this.cbxContainSubFolders.UseVisualStyleBackColor = true;
            // 
            // rbnFiles
            // 
            resources.ApplyResources(this.rbnFiles, "rbnFiles");
            this.rbnFiles.Name = "rbnFiles";
            this.rbnFiles.UseVisualStyleBackColor = true;
            // 
            // rbnFolder
            // 
            resources.ApplyResources(this.rbnFolder, "rbnFolder");
            this.rbnFolder.Checked = true;
            this.rbnFolder.Name = "rbnFolder";
            this.rbnFolder.TabStop = true;
            this.rbnFolder.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MDIForm1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "MDIForm1";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.gbxResult.ResumeLayout(false);
            this.gbxResult.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ToolStripMenuItem TSMIQuit;
		private System.Windows.Forms.ToolStripMenuItem LanTradChinese;
		private System.Windows.Forms.ToolStripMenuItem LanSimpChinese;
		private System.Windows.Forms.ToolStripMenuItem LanEnglish;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox gbxResult;
        private System.Windows.Forms.RadioButton rbnFiles;
        private System.Windows.Forms.RadioButton rbnFolder;
        private System.Windows.Forms.Button btnFiles;
        private System.Windows.Forms.TextBox tbxFiles;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox tbxFolder;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.ProgressBar prsBar;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnReSavePDF;
        private System.Windows.Forms.CheckBox cbxContainSubFolders;
        private System.Windows.Forms.Button button1;
    }
}


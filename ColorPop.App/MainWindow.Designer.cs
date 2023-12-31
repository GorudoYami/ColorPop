﻿namespace ColorPop.App;

partial class MainWindow
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
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
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
		tlpMain = new TableLayoutPanel();
		pbMain = new PictureBox();
		gbImageLocation = new GroupBox();
		tlpImageLocation = new TableLayoutPanel();
		tbImageLocation = new TextBox();
		btnBrowse = new Button();
		gbColors = new GroupBox();
		dgvColors = new DataGridView();
		cnRed = new DataGridViewTextBoxColumn();
		cnGreen = new DataGridViewTextBoxColumn();
		cnBlue = new DataGridViewTextBoxColumn();
		cnColor = new DataGridViewImageColumn();
		gbMode = new GroupBox();
		tlpMode = new TableLayoutPanel();
		rbCS = new RadioButton();
		rbAssembly = new RadioButton();
		gbThreads = new GroupBox();
		tlpThreads = new TableLayoutPanel();
		rbAuto = new RadioButton();
		rbCustom = new RadioButton();
		tbThreads = new TextBox();
		gbThreshold = new GroupBox();
		tbThreshold = new TextBox();
		btnProcess = new Button();
		tlpMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)pbMain).BeginInit();
		gbImageLocation.SuspendLayout();
		tlpImageLocation.SuspendLayout();
		gbColors.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dgvColors).BeginInit();
		gbMode.SuspendLayout();
		tlpMode.SuspendLayout();
		gbThreads.SuspendLayout();
		tlpThreads.SuspendLayout();
		gbThreshold.SuspendLayout();
		SuspendLayout();
		// 
		// tlpMain
		// 
		tlpMain.ColumnCount = 2;
		tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
		tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
		tlpMain.Controls.Add(pbMain, 0, 0);
		tlpMain.Controls.Add(gbImageLocation, 1, 0);
		tlpMain.Controls.Add(gbColors, 1, 2);
		tlpMain.Controls.Add(gbMode, 1, 7);
		tlpMain.Controls.Add(gbThreads, 1, 9);
		tlpMain.Controls.Add(gbThreshold, 1, 12);
		tlpMain.Controls.Add(btnProcess, 1, 14);
		tlpMain.Dock = DockStyle.Fill;
		tlpMain.Location = new Point(0, 0);
		tlpMain.Margin = new Padding(0);
		tlpMain.Name = "tlpMain";
		tlpMain.RowCount = 15;
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		tlpMain.Size = new Size(834, 451);
		tlpMain.TabIndex = 0;
		// 
		// pbMain
		// 
		pbMain.BackColor = SystemColors.Control;
		pbMain.Dock = DockStyle.Fill;
		pbMain.Image = (Image)resources.GetObject("pbMain.Image");
		pbMain.Location = new Point(3, 3);
		pbMain.Name = "pbMain";
		tlpMain.SetRowSpan(pbMain, 15);
		pbMain.Size = new Size(577, 445);
		pbMain.SizeMode = PictureBoxSizeMode.Zoom;
		pbMain.TabIndex = 2;
		pbMain.TabStop = false;
		// 
		// gbImageLocation
		// 
		gbImageLocation.Controls.Add(tlpImageLocation);
		gbImageLocation.Dock = DockStyle.Fill;
		gbImageLocation.Location = new Point(586, 3);
		gbImageLocation.Name = "gbImageLocation";
		tlpMain.SetRowSpan(gbImageLocation, 2);
		gbImageLocation.Size = new Size(245, 54);
		gbImageLocation.TabIndex = 3;
		gbImageLocation.TabStop = false;
		gbImageLocation.Text = "Image location";
		// 
		// tlpImageLocation
		// 
		tlpImageLocation.ColumnCount = 2;
		tlpImageLocation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
		tlpImageLocation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		tlpImageLocation.Controls.Add(tbImageLocation, 0, 0);
		tlpImageLocation.Controls.Add(btnBrowse, 1, 0);
		tlpImageLocation.Dock = DockStyle.Fill;
		tlpImageLocation.Location = new Point(3, 19);
		tlpImageLocation.Margin = new Padding(0);
		tlpImageLocation.Name = "tlpImageLocation";
		tlpImageLocation.RowCount = 1;
		tlpImageLocation.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tlpImageLocation.Size = new Size(239, 32);
		tlpImageLocation.TabIndex = 0;
		// 
		// tbImageLocation
		// 
		tbImageLocation.Dock = DockStyle.Fill;
		tbImageLocation.Location = new Point(3, 3);
		tbImageLocation.Name = "tbImageLocation";
		tbImageLocation.Size = new Size(185, 23);
		tbImageLocation.TabIndex = 0;
		tbImageLocation.TextChanged += TbImageLocation_TextChanged;
		// 
		// btnBrowse
		// 
		btnBrowse.Dock = DockStyle.Fill;
		btnBrowse.Location = new Point(194, 3);
		btnBrowse.Name = "btnBrowse";
		btnBrowse.Size = new Size(42, 26);
		btnBrowse.TabIndex = 1;
		btnBrowse.Text = "...";
		btnBrowse.UseVisualStyleBackColor = true;
		btnBrowse.Click += BtnBrowse_Click;
		// 
		// gbColors
		// 
		gbColors.Controls.Add(dgvColors);
		gbColors.Dock = DockStyle.Fill;
		gbColors.Location = new Point(586, 63);
		gbColors.Name = "gbColors";
		tlpMain.SetRowSpan(gbColors, 5);
		gbColors.Size = new Size(245, 144);
		gbColors.TabIndex = 4;
		gbColors.TabStop = false;
		gbColors.Text = "Colors";
		// 
		// dgvColors
		// 
		dgvColors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvColors.BackgroundColor = SystemColors.Control;
		dgvColors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvColors.Columns.AddRange(new DataGridViewColumn[] { cnRed, cnGreen, cnBlue, cnColor });
		dgvColors.Dock = DockStyle.Fill;
		dgvColors.Location = new Point(3, 19);
		dgvColors.MultiSelect = false;
		dgvColors.Name = "dgvColors";
		dgvColors.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dgvColors.RowTemplate.Height = 25;
		dgvColors.ScrollBars = ScrollBars.Vertical;
		dgvColors.Size = new Size(239, 122);
		dgvColors.TabIndex = 0;
		// 
		// cnRed
		// 
		cnRed.HeaderText = "Red";
		cnRed.MinimumWidth = 25;
		cnRed.Name = "cnRed";
		// 
		// cnGreen
		// 
		cnGreen.HeaderText = "Green";
		cnGreen.MinimumWidth = 25;
		cnGreen.Name = "cnGreen";
		// 
		// cnBlue
		// 
		cnBlue.HeaderText = "Blue";
		cnBlue.MinimumWidth = 25;
		cnBlue.Name = "cnBlue";
		// 
		// cnColor
		// 
		cnColor.HeaderText = "Color";
		cnColor.MinimumWidth = 25;
		cnColor.Name = "cnColor";
		// 
		// gbMode
		// 
		gbMode.Controls.Add(tlpMode);
		gbMode.Dock = DockStyle.Fill;
		gbMode.Location = new Point(586, 213);
		gbMode.Name = "gbMode";
		tlpMain.SetRowSpan(gbMode, 2);
		gbMode.Size = new Size(245, 54);
		gbMode.TabIndex = 5;
		gbMode.TabStop = false;
		gbMode.Text = "Mode";
		// 
		// tlpMode
		// 
		tlpMode.BackColor = SystemColors.Control;
		tlpMode.ColumnCount = 2;
		tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tlpMode.Controls.Add(rbCS, 0, 0);
		tlpMode.Controls.Add(rbAssembly, 1, 0);
		tlpMode.Dock = DockStyle.Fill;
		tlpMode.Location = new Point(3, 19);
		tlpMode.Margin = new Padding(0);
		tlpMode.Name = "tlpMode";
		tlpMode.RowCount = 1;
		tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tlpMode.Size = new Size(239, 32);
		tlpMode.TabIndex = 0;
		// 
		// rbCS
		// 
		rbCS.AutoSize = true;
		rbCS.Checked = true;
		rbCS.Dock = DockStyle.Fill;
		rbCS.Location = new Point(3, 3);
		rbCS.Name = "rbCS";
		rbCS.Size = new Size(113, 26);
		rbCS.TabIndex = 0;
		rbCS.TabStop = true;
		rbCS.Text = "C#";
		rbCS.UseVisualStyleBackColor = true;
		// 
		// rbAssembly
		// 
		rbAssembly.AutoSize = true;
		rbAssembly.Dock = DockStyle.Fill;
		rbAssembly.Location = new Point(122, 3);
		rbAssembly.Name = "rbAssembly";
		rbAssembly.Size = new Size(114, 26);
		rbAssembly.TabIndex = 1;
		rbAssembly.Text = "Assembly";
		rbAssembly.UseVisualStyleBackColor = true;
		// 
		// gbThreads
		// 
		gbThreads.Controls.Add(tlpThreads);
		gbThreads.Dock = DockStyle.Fill;
		gbThreads.Location = new Point(586, 273);
		gbThreads.Name = "gbThreads";
		tlpMain.SetRowSpan(gbThreads, 3);
		gbThreads.Size = new Size(245, 84);
		gbThreads.TabIndex = 6;
		gbThreads.TabStop = false;
		gbThreads.Text = "Threads";
		// 
		// tlpThreads
		// 
		tlpThreads.ColumnCount = 2;
		tlpThreads.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tlpThreads.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tlpThreads.Controls.Add(rbAuto, 0, 0);
		tlpThreads.Controls.Add(rbCustom, 1, 0);
		tlpThreads.Controls.Add(tbThreads, 0, 1);
		tlpThreads.Dock = DockStyle.Fill;
		tlpThreads.Location = new Point(3, 19);
		tlpThreads.Margin = new Padding(0);
		tlpThreads.Name = "tlpThreads";
		tlpThreads.RowCount = 2;
		tlpThreads.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tlpThreads.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tlpThreads.Size = new Size(239, 62);
		tlpThreads.TabIndex = 0;
		// 
		// rbAuto
		// 
		rbAuto.AutoSize = true;
		rbAuto.Checked = true;
		rbAuto.Dock = DockStyle.Fill;
		rbAuto.Location = new Point(3, 3);
		rbAuto.Name = "rbAuto";
		rbAuto.Size = new Size(113, 25);
		rbAuto.TabIndex = 0;
		rbAuto.TabStop = true;
		rbAuto.Text = "Auto";
		rbAuto.UseVisualStyleBackColor = true;
		// 
		// rbCustom
		// 
		rbCustom.AutoSize = true;
		rbCustom.Dock = DockStyle.Fill;
		rbCustom.Location = new Point(122, 3);
		rbCustom.Name = "rbCustom";
		rbCustom.Size = new Size(114, 25);
		rbCustom.TabIndex = 1;
		rbCustom.Text = "Custom";
		rbCustom.UseVisualStyleBackColor = true;
		// 
		// tbThreads
		// 
		tlpThreads.SetColumnSpan(tbThreads, 2);
		tbThreads.Dock = DockStyle.Fill;
		tbThreads.Enabled = false;
		tbThreads.Location = new Point(3, 34);
		tbThreads.Name = "tbThreads";
		tbThreads.PlaceholderText = "Enter thread count";
		tbThreads.Size = new Size(233, 23);
		tbThreads.TabIndex = 2;
		// 
		// gbThreshold
		// 
		gbThreshold.BackColor = SystemColors.Control;
		gbThreshold.Controls.Add(tbThreshold);
		gbThreshold.Dock = DockStyle.Fill;
		gbThreshold.Location = new Point(586, 363);
		gbThreshold.Name = "gbThreshold";
		tlpMain.SetRowSpan(gbThreshold, 2);
		gbThreshold.Size = new Size(245, 54);
		gbThreshold.TabIndex = 7;
		gbThreshold.TabStop = false;
		gbThreshold.Text = "Threshold";
		// 
		// tbThreshold
		// 
		tbThreshold.Dock = DockStyle.Fill;
		tbThreshold.Location = new Point(3, 19);
		tbThreshold.Name = "tbThreshold";
		tbThreshold.PlaceholderText = "Enter threshold";
		tbThreshold.Size = new Size(239, 23);
		tbThreshold.TabIndex = 0;
		tbThreshold.Text = "0";
		// 
		// btnProcess
		// 
		btnProcess.Dock = DockStyle.Fill;
		btnProcess.Location = new Point(586, 423);
		btnProcess.Name = "btnProcess";
		btnProcess.Size = new Size(245, 25);
		btnProcess.TabIndex = 8;
		btnProcess.Text = "Process";
		btnProcess.UseVisualStyleBackColor = true;
		btnProcess.Click += BtnProcess_Click;
		// 
		// MainWindow
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(834, 451);
		Controls.Add(tlpMain);
		MaximumSize = new Size(850, 490);
		MinimumSize = new Size(850, 490);
		Name = "MainWindow";
		Text = "ColorPop";
		tlpMain.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)pbMain).EndInit();
		gbImageLocation.ResumeLayout(false);
		tlpImageLocation.ResumeLayout(false);
		tlpImageLocation.PerformLayout();
		gbColors.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)dgvColors).EndInit();
		gbMode.ResumeLayout(false);
		tlpMode.ResumeLayout(false);
		tlpMode.PerformLayout();
		gbThreads.ResumeLayout(false);
		tlpThreads.ResumeLayout(false);
		tlpThreads.PerformLayout();
		gbThreshold.ResumeLayout(false);
		gbThreshold.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private TableLayoutPanel tlpMain;
	private PictureBox pbMain;
	private GroupBox gbImageLocation;
	private GroupBox gbColors;
	private GroupBox gbMode;
	private TableLayoutPanel tlpImageLocation;
	private TextBox tbImageLocation;
	private GroupBox gbThreads;
	private GroupBox gbThreshold;
	private Button btnProcess;
	private Button btnBrowse;
	private DataGridView dgvColors;
	private DataGridViewTextBoxColumn cnRed;
	private DataGridViewTextBoxColumn cnGreen;
	private DataGridViewTextBoxColumn cnBlue;
	private DataGridViewImageColumn cnColor;
	private TableLayoutPanel tlpMode;
	private RadioButton rbCS;
	private RadioButton rbAssembly;
	private TableLayoutPanel tlpThreads;
	private RadioButton rbAuto;
	private RadioButton rbCustom;
	private TextBox tbThreads;
	private TextBox tbThreshold;
}

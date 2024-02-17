namespace ColorPop.App;

partial class ResultDialog
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
		tlpMain = new TableLayoutPanel();
		pbMain = new PictureBox();
		btnSave = new Button();
		lblProcessingTime = new Label();
		tlpMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)pbMain).BeginInit();
		SuspendLayout();
		// 
		// tlpMain
		// 
		tlpMain.ColumnCount = 2;
		tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
		tlpMain.Controls.Add(pbMain, 0, 0);
		tlpMain.Controls.Add(btnSave, 1, 1);
		tlpMain.Controls.Add(lblProcessingTime, 0, 1);
		tlpMain.Dock = DockStyle.Fill;
		tlpMain.Location = new Point(0, 0);
		tlpMain.Margin = new Padding(0);
		tlpMain.Name = "tlpMain";
		tlpMain.RowCount = 2;
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
		tlpMain.Size = new Size(739, 477);
		tlpMain.TabIndex = 0;
		// 
		// pbMain
		// 
		tlpMain.SetColumnSpan(pbMain, 2);
		pbMain.Dock = DockStyle.Fill;
		pbMain.Location = new Point(3, 3);
		pbMain.Name = "pbMain";
		pbMain.Size = new Size(733, 441);
		pbMain.SizeMode = PictureBoxSizeMode.Zoom;
		pbMain.TabIndex = 0;
		pbMain.TabStop = false;
		// 
		// btnSave
		// 
		btnSave.Dock = DockStyle.Right;
		btnSave.Location = new Point(661, 450);
		btnSave.Name = "btnSave";
		btnSave.Size = new Size(75, 24);
		btnSave.TabIndex = 1;
		btnSave.Text = "Save as...";
		btnSave.UseVisualStyleBackColor = true;
		btnSave.Click += BtnSave_Click;
		// 
		// lblProcessingTime
		// 
		lblProcessingTime.AutoSize = true;
		lblProcessingTime.Dock = DockStyle.Fill;
		lblProcessingTime.Location = new Point(3, 450);
		lblProcessingTime.Margin = new Padding(3);
		lblProcessingTime.Name = "lblProcessingTime";
		lblProcessingTime.Size = new Size(533, 24);
		lblProcessingTime.TabIndex = 2;
		lblProcessingTime.Text = "Processing time: {0} μs";
		lblProcessingTime.TextAlign = ContentAlignment.MiddleLeft;
		// 
		// ResultDialog
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(739, 477);
		Controls.Add(tlpMain);
		Name = "ResultDialog";
		Text = "Result";
		tlpMain.ResumeLayout(false);
		tlpMain.PerformLayout();
		((System.ComponentModel.ISupportInitialize)pbMain).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private TableLayoutPanel tlpMain;
	private PictureBox pbMain;
	private Button btnSave;
	private Label lblProcessingTime;
}
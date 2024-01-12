using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPop.App;

public partial class ResultDialog : Form
{
	private const string _labelFormat = "Processing time: {0} ms";
	public ResultDialog(Bitmap bitmap, long processingTimeMilliseconds)
	{
		InitializeComponent();

		pbMain.Image = bitmap;
		lblProcessingTime.Text = string.Format(_labelFormat, processingTimeMilliseconds);
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessB_UDF_Programmer
{
    public partial class frmAccbUDFProg : Form
    {
        AccessB AccB = new AccessB();
        System.IO.Stream HexReader;

        byte[] HexStreamRead;

        public frmAccbUDFProg()
        {
            InitializeComponent();
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            if (AccB.FindDevHID() == true)
            {
                btnLoadHEX.Enabled = true;
                lblDetect.Text = "Board status: Connected.";
            }
            else
            {
                lblDetect.Text = "Board status: AccessB not find.";
            }
        }

        private void btnLoadHEX_Click(object sender, EventArgs e)
        {
            opnHEXFile.Filter = "MPLABX hex file (*.hex)|*.hex";
            if (opnHEXFile.ShowDialog() == DialogResult.OK)
            {
                HexReader = opnHEXFile.OpenFile();
                lblFileName.Text = opnHEXFile.FileName.ToString();
            }
            else
            {
                return;
            }
            
            if (HexFileLoad() == true)
            {
                btnHEXProg.Enabled = true;
                lblFileName.Text = HexStreamRead[0].ToString();
            }
        }

        /// <summary>
        /// HexFileLoad seach for UDF data in the selected Hex file.
        /// 
        /// </summary>
        /// <returns>Return true if the operation was success</returns>
        private bool HexFileLoad()
        {
            HexStreamRead = new byte[HexReader.Length];
            HexReader.Read(HexStreamRead, 0, (int)HexReader.Length);
            return true;
        }

        private void btnHEXProg_Click(object sender, EventArgs e)
        {
            if (AccB.PROGRAM_UDF(0x4280, HexStreamRead) == true)
            {
                lblProgramStatus.Text = "Status: Success";
            }
            else
            {
                lblProgramStatus.Text = "Status: Fail.";
            }
        }
    }
}

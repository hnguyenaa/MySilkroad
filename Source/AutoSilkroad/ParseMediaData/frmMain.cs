using ParseMediaData.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParseMediaData
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnToSql_Click(object sender, EventArgs e)
        {
            var result = ParseSkillData.ReadFile_skilldataenc();
            var allRawData = ParseSkillData.ReadAllData(result);
            ParseSkillData.SaveToInsertSql(allRawData);
            rtfLog.AppendText("OK");
        }
    }
}

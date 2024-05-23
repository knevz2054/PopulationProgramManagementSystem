using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using DataProcessingSystem.Data;
namespace DataProcessingSystem
{
    public partial class frmViewLog : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmViewLog()
        {
            InitializeComponent();
        }

        private void frmViewLog_Load(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void chkView_CheckedChanged(object sender, EventArgs e)
        {
            dtpViewLogs.Enabled = chkView.Checked;
            lvLog.Items.Clear();
            if (chkView.Checked)
            {
                DateTime date = dtpViewLogs.Value.Date;
                foreach (var v in db.tblLogs.Where(x => DbFunctions.TruncateTime(x.DateTime) == dtpViewLogs.Value.Date).OrderByDescending(x => x.DateTime))
                {
                    ListViewItem lvi = new ListViewItem(v.DateTime.ToString());
                    lvi.SubItems.Add(v.ActivityLog);

                    lvLog.Items.Add(lvi);
                }
            }
            else
            {
                foreach (var v in db.tblLogs.OrderByDescending(x => x.DateTime))
                {
                    ListViewItem lvi = new ListViewItem(v.DateTime.ToString());
                    lvi.SubItems.Add(v.ActivityLog);

                    lvLog.Items.Add(lvi);
                }
            }
           
        }

        private void dtpViewLogs_ValueChanged(object sender, EventArgs e)
        {
            lvLog.Items.Clear();
            foreach (var v in db.tblLogs.Where(x => DbFunctions.TruncateTime(x.DateTime) == dtpViewLogs.Value.Date).OrderByDescending(x => x.DateTime))
            {
                ListViewItem lvi = new ListViewItem(v.DateTime.ToString());
                lvi.SubItems.Add(v.ActivityLog);

                lvLog.Items.Add(lvi);
            }
        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            frmDeleteLog log = new frmDeleteLog();
            log.ShowDialog();
        }

        public void LoadLogs()
        {
            lvLog.Items.Clear();
            foreach (var v in db.tblLogs.OrderByDescending(x => x.DateTime))
            {
                ListViewItem lvi = new ListViewItem(v.DateTime.ToString());
                lvi.SubItems.Add(v.ActivityLog);

                lvLog.Items.Add(lvi);
            }
        }
    }
}

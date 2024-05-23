using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataProcessingSystem.Data;
namespace DataProcessingSystem
{
    public partial class frmHome : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmHome()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            
        }
    }
}

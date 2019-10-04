using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalApp
{
    public partial class FrmMain : Form
    {
        public Patient ActiveUser { get; set; }
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var frmLogin = new FrmLogin();
            frmLogin.MdiParent = this;
            frmLogin.Show();
        }

        private void RandevularımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FrmMyAppointments();
            frm.MdiParent = this;
            frm.Show();
        }

        private void HastanelerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FrmHospitals();
            frm.MdiParent = this;
            frm.Show();
        }

        private void BölümlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FrmDepartments();
            frm.MdiParent = this;
            frm.Show();
        }

        private void DoktorlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FrmDoctors();
            frm.MdiParent = this;
            frm.Show();
        }

        private void HastalarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new FrmPatients();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}

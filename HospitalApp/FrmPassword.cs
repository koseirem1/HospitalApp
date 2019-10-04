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
    public partial class FrmPassword : Form
    {
        public FrmPatients MasterForm { get; set; }
        private readonly int Id;
        public FrmPassword()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            if (Email.Text == "")
            {
                MessageBox.Show("Mail gereklidir.");
                return;
            }
            if (txtTC.Text == "")
            {
                MessageBox.Show("tc gereklidir.");
                return;
            }

            using (var db=new ApplicationDbContext())
            {
                var password = db.Patients.Where(p => p.IdentityNumber ==txtTC.Text && p.Email==Email.Text ).FirstOrDefault();
                if (password != null)
                {
                    var frm = new FrmSifre(password.Id);
                    frm.MdiParent = this.MdiParent;
                    frm.Show();
                }

               else
                {
                    MessageBox.Show("Geçersiz TC Kimlik No veya Email!");
                }
            }

            if (MasterForm != null)
            {
                MasterForm.LoadPatients();
            }
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

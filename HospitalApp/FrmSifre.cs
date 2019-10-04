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
    public partial class FrmSifre : Form
    {
        public readonly int Id;
        public FrmSifre(int id)
        {
            this.Id = id;
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Şifre gereklidir.");
                return;
            }
            if (txtNewPassword.Text == "")
            {
                MessageBox.Show("Şifre doğrulama gereklidir.");
                return;
            }

            using(var db=new ApplicationDbContext())
            {
                var password = db.Patients.Where(p => p.Id==this.Id).FirstOrDefault();
                if (password != null)
                {

                    password.Password = txtPassword.Text;
                   
                    db.SaveChanges();
                }

                this.Close();
            }
        }
    }
}

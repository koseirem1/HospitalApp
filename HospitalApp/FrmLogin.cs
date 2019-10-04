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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // SELECT İŞLEMİ: TEK KAYIT GETİRME
            using (var db = new ApplicationDbContext())
            {
                var user = db.Patients.Where(p => p.IdentityNumber == txtIdentityNumberLogin.Text && p.Password == txtPasswordLogin.Text).FirstOrDefault();

                if (user != null)
                {
                    string hitap = "Bey";
                    if (user.Gender == Gender.Female)
                    {
                        hitap = "Hanım";
                    }
                    string mesaj = "Hoşgeldiniz " + user.FirstName + " " + hitap + " :)";               
                    MessageBox.Show(mesaj);
                    ((FrmMain)this.MdiParent).ActiveUser = user;
                    ((FrmMain)this.MdiParent).menuStrip1.Enabled = true;
                    this.Close();
                } else
                {
                    MessageBox.Show("Geçersiz TC Kimlik No veya şifre!");
                }
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // INSERT İŞLEMİ
            using (var db = new ApplicationDbContext())
            {
                var patient = new Patient();
                patient.FirstName = txtFirstName.Text;
                patient.LastName = txtLastName.Text;
                patient.IdentityNumber = txtIdentityNumber.Text;
                patient.Password = txtPassword.Text;
                patient.Gender = (rbtMale.Checked ? Gender.Male : Gender.Female);
                patient.Phone = txtPhone.Text;
                patient.Email = txtEmail.Text;

                db.Patients.Add(patient);
                db.SaveChanges();
                MessageBox.Show("Kullanıcı başarıyla eklendi.");
            }
            tabControl1.Show();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            ((FrmMain)this.MdiParent).menuStrip1.Enabled = false;
        }

        private void YeniSifre_Click(object sender, EventArgs e)
        {
            var frm = new FrmPassword();
            frm.MdiParent=this.MdiParent;
            frm.Show();
        }
    }
}

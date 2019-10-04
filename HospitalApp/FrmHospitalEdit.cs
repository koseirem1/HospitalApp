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
    public partial class FrmHospitalEdit : Form
    {
        public FrmHospitals MasterForm { get; set; }
        private readonly int Id;
        public FrmHospitalEdit(int id)
        {
            this.Id = id;
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validasyonlar
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Hastane adı gereklidir.");
                return;
            } else if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Hastane adresi gereklidir.");
                return;
            } else if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Hastane telefonu gereklidir.");
                return;
            }

            // Veritabanında güncelleme işlemi
            using (var db = new ApplicationDbContext())
            {
                var hospital = db.Hospitals.Where(x => x.Id == this.Id).FirstOrDefault();
                if (hospital != null) { 
                    hospital.Name = txtName.Text;
                    hospital.Address = txtAddress.Text;
                    hospital.Phone = txtPhone.Text;
                    db.SaveChanges();
                }
            }
            // Grid yenilenir
            if (MasterForm != null)
            {
                MasterForm.LoadHospitals();
            }
        }

        private void FrmHospitalEdit_Load(object sender, EventArgs e)
        {
            // id'si verilen hastaneyi veritabanın getir
            using (var db = new ApplicationDbContext())
            {
                var hospital = db.Hospitals.Where(x => x.Id == this.Id).FirstOrDefault();
                if (hospital != null)
                {
                    txtName.Text = hospital.Name;
                    txtAddress.Text = hospital.Address;
                    txtPhone.Text = hospital.Phone;
                } else
                {
                    MessageBox.Show("Kayıt bulunamadı.");
                }
            }
        }
    }
}

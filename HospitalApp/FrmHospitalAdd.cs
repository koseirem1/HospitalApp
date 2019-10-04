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
    public partial class FrmHospitalAdd : Form
    {
        public FrmHospitals MasterForm { get; set; }
        public FrmHospitalAdd()
        {
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

            // Veritabanına ekleme işlemi
            using (var db = new ApplicationDbContext())
            {
                var hospital = new Hospital();
                hospital.Name = txtName.Text;
                hospital.Address = txtAddress.Text;
                hospital.Phone = txtPhone.Text;
                db.Hospitals.Add(hospital);
                db.SaveChanges();
            }
            // Grid yenilenir
            if (MasterForm != null)
            {
                MasterForm.LoadHospitals();
            }
        }

        private void FrmHospitalAdd_Load(object sender, EventArgs e)
        {

        }
    }
}

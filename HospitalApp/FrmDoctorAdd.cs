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
    public partial class FrmDoctorAdd : Form
    {
        public FrmDoctors MasterForm { get; set; }
        public FrmDoctorAdd()
        {
            InitializeComponent();
        }

        private void LoadHospitals()
        {
            using (var db = new ApplicationDbContext())
            {
                var hospitals = db.Hospitals.OrderBy(o => o.Name).ToList();
                cmbHospital.Items.Clear();
                cmbHospital.Items.Add(new Hospital() { Name = "Hastane seçiniz", Id = 0 });
                cmbHospital.DisplayMember = "Name";
                cmbHospital.ValueMember = "Id";
                foreach (var item in hospitals)
                {
                    cmbHospital.Items.Add(item);
                }
                cmbHospital.SelectedIndex = 0;
            }
        }

        private void LoadDepartments()
        {
            using (var db = new ApplicationDbContext())
            {
                int hospitalId = ((Hospital)cmbHospital.SelectedItem).Id;
                var departments = db.Departments.Where(x => x.HospitalId == hospitalId).OrderBy(o => o.Name).ToList();
                cmbbolum.Items.Clear();
                cmbbolum.Items.Add(new Department() { Name = " Bölüm seçiniz", Id = 0 });
                cmbbolum.DisplayMember = "Name";
                cmbbolum.ValueMember = "Id";
                foreach (var item in departments)
                {
                    cmbbolum.Items.Add(item);
                }
                cmbbolum.SelectedIndex = 0;
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Doktor adı gereklidir.");
                return;
            }

            else if (txtSurname.Text == "")
            {
                MessageBox.Show("Soyadı gereklidir.");
                return;
            }

            else if ((rdoMale.Checked == false) && (rdoFemale.Checked == false))
            {
                MessageBox.Show("Cinsiyet gereklidir.");
                return;
            }

            else if (((Hospital)cmbHospital.SelectedItem).Id == 0)
            {
                MessageBox.Show("Hastane Gereklidir.");
                return;
            }



            else if (((Department)cmbbolum.SelectedItem).Id == 0)
            {
                MessageBox.Show("Bölüm adı gereklidir.");
                return;
            }


            using (var db = new ApplicationDbContext())
            {
                var doctor = new Doctor();
                doctor.FirstName = txtName.Text;
                doctor.LastName = txtSurname.Text;
                if (rdoFemale.Checked == true)
                {
                    doctor.Gender = Gender.Female;
                }
                else
                {
                    doctor.Gender = Gender.Male;
                }

                doctor.HospitalId = ((Hospital)cmbHospital.SelectedItem).Id;

                doctor.DepartmentId = ((Department)cmbbolum.SelectedItem).Id;
                db.Doctors.Add(doctor);
                db.SaveChanges();
            }

            if (MasterForm != null)
            {
                MasterForm.LoadDoctors();
            }

            this.Close();
        }

        private void FrmDoctorAdd_Load(object sender, EventArgs e)
        {
           
        }

        private void CmbHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDoctorAdd_Load_1(object sender, EventArgs e)
        {
            LoadHospitals();
        }

        private void CmbHospital_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadDepartments();
        }
    }
}




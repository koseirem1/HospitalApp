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
    public partial class FrmDoctorEdit : Form
    {
        private readonly int Id;
        public FrmDoctors MasterForm { get; set; }
        public FrmDoctorEdit(int id)
        {
            InitializeComponent();
            this.Id = id;
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
                var doctor = db.Doctors.Where(x => x.Id == this.Id).FirstOrDefault();
                //databaseden bilgileri getirdik
                if (doctor != null)
                {
                   /* doctor.FirstName = txtName.Text;
                    doctor.LastName = txtSurname.Text;
                    if (rdoFemale.Checked == true)
                    {
                        doctor.Gender = Gender.Female;
                    }
                    else
                    {
                        doctor.Gender = Gender.Male;
                    }*/
                    doctor.HospitalId = ((Hospital)cmbHospital.SelectedItem).Id;

                    if (((Department)cmbbolum.SelectedItem).Id > 0)
                    {
                        doctor.DepartmentId = ((Department)
                            cmbbolum.SelectedItem).Id;

                    }
                    else
                    {
                        doctor.DepartmentId = 1;
                    }

                    db.SaveChanges();
                }

            }
            if (MasterForm != null)
            {
                MasterForm.LoadDoctors();
            }
            this.Close();

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmbHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDepartments();
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
        private void FrmDoctorEdit_Load(object sender, EventArgs e)
        {
            LoadHospitals();
            using (var db = new ApplicationDbContext())
            {
                var doctor = db.Doctors.Where(x => x.Id == this.Id).FirstOrDefault();
                txtName.Text = doctor.FirstName;
                txtSurname.Text = doctor.LastName;
                if (doctor.Gender==Gender.Female)
                {
                    rdoFemale.Checked=true;
                }
                else
                {
                    rdoMale.Checked=true;
                }

                int index = 0;

                foreach (var item in cmbHospital.Items)
                {
                    if (((Hospital)item).Id == doctor.HospitalId)
                    {
                        break;
                    }
                    index++;

                }
                if (index >= cmbHospital.Items.Count)
                {
                    index = 0;
                }

                cmbHospital.SelectedIndex = index;
                LoadDepartments();
                index = 0;

                foreach (var item in cmbbolum.Items)
                {
                    if (((Department)item).Id == doctor.DepartmentId)
                    {
                        break;
                    }
                    index++;
                }

                if (index >= cmbbolum.Items.Count)
                {
                    index = 0;
                }
                cmbbolum.SelectedIndex = index;

            }


        }
    }
}

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
    public partial class FrmDepartmentAdd : Form
    {
        public FrmDepartments MasterForm { get; set; }
        public FrmDepartmentAdd()
        {
            InitializeComponent();
        }

        private void FrmDepartmentAdd_Load(object sender, EventArgs e)
        {
            LoadHospitals();
            // LoadDepartments();
        }

        private void LoadHospitals()
        {
            using (var db = new ApplicationDbContext())
            {
                var hospitals = db.Hospitals.OrderBy(o => o.Name).ToList();
                cmbHospital.Items.Clear();
                cmbHospital.Items.Add(new Hospital() { Name = "Hastane seçiniz",Id=0 });
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
                var departments = db.Departments.Where(x=>x.HospitalId==hospitalId).OrderBy(o => o.Name).ToList();
                cmbParentDepartment.Items.Clear();
                cmbParentDepartment.Items.Add(new Department() { Name = "Üst bölüm seçiniz" ,Id=0});
                cmbParentDepartment.DisplayMember = "Name";
                cmbParentDepartment.ValueMember = "Id";
                foreach (var item in departments)
                {
                    cmbParentDepartment.Items.Add(item);
                }
                cmbParentDepartment.SelectedIndex = 0;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text==" ")
            {
                MessageBox.Show("Bölüm adı gereklidir.");
                return;
            }
            if (((Hospital)cmbHospital.SelectedItem).Id == 0)
            {
                MessageBox.Show("Hastane Gereklidir.");
                return; 
            }

            using (var db = new ApplicationDbContext())
            {
                var department = new Department();
                department.Name = txtName.Text;
                department.HospitalId = ((Hospital)cmbHospital.SelectedItem).Id;

                if (((Department)cmbParentDepartment.SelectedItem).Id > 0)
                {
                    department.ParentDepartmentId = ((Department)
                        cmbParentDepartment.SelectedItem).Id;
                }
                db.Departments.Add(department);
                db.SaveChanges();


            }

            if (MasterForm != null)
            {
                MasterForm.LoadDepartments();
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
    }
}

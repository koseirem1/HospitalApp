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
    public partial class FrmDepartmentEdit : Form
    {
        public FrmDepartments MasterForm { get; set; }
        private readonly int Id;
        public FrmDepartmentEdit(int id)
        {
            this.Id = id;
            InitializeComponent();
        }

        private void FrmDepartmentEdit_Load(object sender, EventArgs e)
        {
           
            LoadHospitals();
            using (var db=new ApplicationDbContext())
            {
                var department = db.Departments.Where(x => x.Id == this.Id).FirstOrDefault();
                txtName.Text = department.Name;
                int index = 0;

                foreach (var item in cmbHospital.Items)
                {
                    if(((Hospital)item).Id==department.HospitalId)
                    {
                        break;
                    } index++;

                }
                if (index >= cmbHospital.Items.Count)
                {
                    index = 0;
                }

                cmbHospital.SelectedIndex = index;
                LoadDepartments();
                index = 0;

                foreach (var item in cmbParentDepartment.Items)
                {
                    if (((Department)item).Id == department.ParentDepartmentId)
                    {
                        break;
                    }
                    index++;
                }

                if (index >= cmbParentDepartment.Items.Count)
                {
                    index = 0;
                }
                cmbParentDepartment.SelectedIndex = index;

            }
            
            
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
                cmbParentDepartment.Items.Clear();
                cmbParentDepartment.Items.Add(new Department() { Name = "Üst bölüm seçiniz", Id = 0 });
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
          
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void BtnSave_Click_1(object sender, EventArgs e)
        {
            if (txtName.Text == " ")
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
                var department = db.Departments.Where(x => x.Id == this.Id).FirstOrDefault();
                if (department != null)
                {
                    department.Name = txtName.Text;
                    department.HospitalId = ((Hospital)cmbHospital.SelectedItem).Id;

                    if (((Department)cmbParentDepartment.SelectedItem).Id > 0)
                    {
                        department.ParentDepartmentId = ((Department)
                            cmbParentDepartment.SelectedItem).Id;

                    }
                    else
                    {
                        department.ParentDepartmentId = null;
                    }

                    db.SaveChanges();
                }
               
            }

            if (MasterForm != null)
            {
                MasterForm.LoadDepartments();
            }
            this.Close();
        }
        private void CmbHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void BtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

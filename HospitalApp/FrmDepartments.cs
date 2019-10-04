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
    public partial class FrmDepartments : Form
    {
        public FrmDepartments()
        {
            InitializeComponent();
        }

        private void FrmDepartments_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            
        }

        public void LoadDepartments()
        {
            using (var db = new ApplicationDbContext())
            {
                var departments = db.Departments.OrderBy(x => x.Name).Select(s => new { s.Id, s.Name, ParentName = (s.ParentDepartment!=null?s.ParentDepartment.Name:""), HospitalName = s.Hospital.Name }).ToList();
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = departments;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var frm = new FrmDepartmentAdd();
            frm.MdiParent = this.MdiParent;
            frm.MasterForm = this;
            frm.Show();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {

           
        }

        private void BtnEdit_Click_1(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int departmentId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                var frm = new FrmDepartmentEdit(departmentId);
                frm.MdiParent = this.MdiParent;
                frm.MasterForm = this;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz bölümü seçiniz.");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Seçili departmanı silmek istediğinize emin misiniz?", "Departman Sil", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        int departmentId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        var department = db.Departments.Where(x => x.Id == departmentId).FirstOrDefault();
                        try
                        {
                            if (department != null)
                            {
                                db.Departments.Remove(department);
                                db.SaveChanges();
                                LoadDepartments();
                            }
                            else
                            {
                                MessageBox.Show("Silinecek kayıt bulunamadı");
                            }
                        }
                        catch(Exception ex)
                        { MessageBox.Show("Kaydı silemezsiniz"+ex); }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz departmanı seçiniz");
            }
        }
    }
    
}

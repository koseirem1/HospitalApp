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
    public partial class FrmDoctors : Form
    {
        public FrmDoctors()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

            var frm = new FrmDoctorAdd();
            frm.MdiParent = this.MdiParent;
            frm.MasterForm = this;
            frm.Show();
        }

        private void FrmDoctors_Load(object sender, EventArgs e)
        {
            LoadDoctors();
        }

        public void LoadDoctors()
        {
            using (var db = new ApplicationDbContext())
            {
                var doctors = db.Doctors.OrderBy(x => x.FirstName).Select(s => new { s.Id, s.FirstName,s.LastName,GenderName= s.Gender==Gender.Male?"Erkek":"Kadın", HospitalName = s.Hospital.Name,
                    DepartmentName=s.Department.Name}).ToList();
               
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = doctors;
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int doctorId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                var frm = new FrmDoctorEdit(doctorId);
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
                var result = MessageBox.Show("Seçili doktoru silmek istediğinize emin misiniz?", "Doktor Sil", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        int doctorId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        var doctor = db.Doctors.Where(x => x.Id == doctorId).FirstOrDefault();
                        try
                        {
                            if (doctor != null)
                            {
                                db.Doctors.Remove(doctor);
                                db.SaveChanges();
                                LoadDoctors();
                            }
                            else
                            {
                                MessageBox.Show("Silinecek kayıt bulunamadı");
                            }
                        }
                        catch (Exception ex)
                        { MessageBox.Show("Kaydı silemezsiniz" + ex); }
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

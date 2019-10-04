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
    public partial class FrmMyAppointments : Form
    {
        
        public FrmMyAppointments()
        {
            InitializeComponent();
        }

        private void FrmMyAppointments_Load(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        public void LoadAppointments()
        {
            var userId = ((FrmMain)this.MdiParent).ActiveUser.Id;

            using (var db = new ApplicationDbContext())
            {
                var myappointments = db.Appointments.Include("Hospital").Include("Department").Include("Doctor").Where(a => a.PatientId == userId).OrderBy(o => o.Hour).AsEnumerable().Select(x => new { Id = x.Id, Hour = x.Hour, HospitalName = x.Hospital.Name, DepartmentName = x.Department.Name, DoctorName = x.Doctor.FullName, IsCancelled = x.IsCancelled }).ToList();
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = myappointments;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var frm = new FrmAppointment(this);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int selectedAppointmentId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                var result = MessageBox.Show("Seçtiğiniz randevuyu iptal etmek istediğinize emin misiniz?", "Randevu İptal İşlemi", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) { 
                    using (var db = new ApplicationDbContext())
                    {
                        var appointment = db.Appointments.FirstOrDefault(x => x.Id == selectedAppointmentId);
                        if (appointment != null)
                        {
                            appointment.IsCancelled = true;
                            db.SaveChanges();
                            MessageBox.Show("Seçtiğiniz randevu iptal edildi.");
                            LoadAppointments();
                        } else
                        {
                            MessageBox.Show("Seçili randevu bulanamadı");
                        }
                    }
                }
            } else
            {
                MessageBox.Show("Lütfen iptal etmek istediğiniz randevuyu seçiniz.");
            }
        }
    }
}

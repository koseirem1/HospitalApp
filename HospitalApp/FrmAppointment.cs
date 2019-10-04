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
    public partial class FrmAppointment : Form
    {
        private FrmMyAppointments gridForm;
        public FrmAppointment(FrmMyAppointments gridForm)
        {
            this.gridForm = gridForm;
            InitializeComponent();
        }

        private void FrmAppointment_Load(object sender, EventArgs e)
        {
            // form yüklendiğinde hastaneleri yükle
            using (var db = new ApplicationDbContext())
            {
                var hospitals = db.Hospitals.OrderBy(o => o.Name).ToList();                
                cmbHospital.DisplayMember = "Name";
                cmbHospital.ValueMember = "Id";
                cmbHospital.DataSource = hospitals;
                cmbHospital.SelectedIndex = -1;
                cmbHospital.Text = "(Hastane Seçiniz)";
                LoadDepartments();
            }

            // randevu saati combosunu temizle ve randevu saatlerini ekle
            cmbHour.Items.Clear();
            for (int i = 9; i<=17; i++)
            {
                cmbHour.Items.Add(string.Format("{0:00}:00", i));
                if (i<17) {
                    cmbHour.Items.Add(string.Format("{0:00}:15", i));
                    cmbHour.Items.Add(string.Format("{0:00}:30", i));
                    cmbHour.Items.Add(string.Format("{0:00}:45", i));
                }
            }
        }

        // hastane combosundaki seçili öğe değiştiğinde
        private void CmbHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            using (var db = new ApplicationDbContext())
            {
                // hastane combosunda aktif öğe null mı kontrol et
                // null değilse o hastanenin departmanlarını yükle
                int hospitalId = 0;
                if (cmbHospital.SelectedValue != null)
                {
                    hospitalId = (int)cmbHospital.SelectedValue;
                }
                var departments = db.Departments.Where(d => d.HospitalId == hospitalId).OrderBy(o => o.Name).ToList();
                cmbDepartment.DisplayMember = "Name";
                cmbDepartment.ValueMember = "Id";
                cmbDepartment.DataSource = departments;

                cmbDepartment.SelectedIndex = -1;
                cmbDepartment.Text = "(Bölüm Seçiniz)";
                LoadDoctors();
            }
        }

        private void CmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            using (var db = new ApplicationDbContext())
            {
                // seçili departman varsa doktorları yükle
                int departmentId = 0;
                if (cmbDepartment.SelectedValue != null)
                {
                    departmentId = (int)cmbDepartment.SelectedValue;
                }
                var doctors = db.Doctors.Where(d => d.DepartmentId == departmentId).OrderBy(o => o.FirstName).ThenBy(t => t.LastName).ToList();
                cmbDoctor.DisplayMember = "FullName";
                cmbDoctor.ValueMember = "Id";
                cmbDoctor.DataSource = doctors;

                cmbDoctor.SelectedIndex = -1;
                cmbDoctor.Text = "(Doktor Seçiniz)";
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            // form validasyonları
            if (cmbHospital.SelectedValue == null)
            {
                MessageBox.Show("Hastane seçmelisiniz");
                return;
            }
            else if (cmbDepartment.SelectedValue == null)
            {
                MessageBox.Show("Bölüm seçmelisiniz");
                return;
            }
            else if (cmbDoctor.SelectedValue == null)
            {
                MessageBox.Show("Doktor seçmelisiniz");
                return;
            }
            else if (string.IsNullOrEmpty(cmbHour.Text))
            {
                MessageBox.Show("Randevu saati seçmelisiniz");
                return;
            }
            using (var db = new ApplicationDbContext())
            {
                var appointment = new Appointment();
                appointment.HospitalId = (int)cmbHospital.SelectedValue;
                appointment.DepartmentId = (int)cmbDepartment.SelectedValue;
                appointment.DoctorId = (int)cmbDoctor.SelectedValue;
                appointment.PatientId = ((FrmMain)this.MdiParent).ActiveUser.Id;
                appointment.Hour = Convert.ToDateTime(dtpHour.Text + cmbHour.Text);
                appointment.IsCancelled = false;
                db.Appointments.Add(appointment);
                db.SaveChanges();
                MessageBox.Show("Randevu başarıyla kaydedildi");
                gridForm.LoadAppointments();
                this.Close();
            }
        }
    }
}

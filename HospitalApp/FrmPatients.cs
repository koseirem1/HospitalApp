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
    public partial class FrmPatients : Form
    {
        public FrmPatients()
        {
            InitializeComponent();
        }

        private void FrmPatients_Load(object sender, EventArgs e)
        {
            LoadPatients();
        }

        public void LoadPatients()
        {
            using(var db = new ApplicationDbContext())
            {
                var patients = db.Patients.OrderBy(x => x.FirstName).Select(s => new
                {
                    s.Id,
                    s.FirstName,
                    s.LastName,
                    s.IdentityNumber,
                    Gender = s.Gender,
                    s.Phone,
                    s.Email,
                    s.Password
                }).ToList();


                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = patients;
            }
        }

        private void YeniSifre_Click(object sender, EventArgs e)
        {

            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int patientId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                var frm = new FrmPassword();
                frm.MdiParent = this.MdiParent;
                frm.Show();
                frm.MasterForm = this;

            }
            else
            {
                MessageBox.Show("Lütfen şifresini düzenlemek istediğiniz bölümü seçiniz.");
            }
         
        }
    }
}

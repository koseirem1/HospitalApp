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
    public partial class FrmHospitals : Form
    {
        public FrmHospitals()
        {
            InitializeComponent();
        }

        private void FrmHospitals_Load(object sender, EventArgs e)
        {
            LoadHospitals();
        }

        public void LoadHospitals()
        {
            using (var db = new ApplicationDbContext())
            {
                var hospitals = db.Hospitals.OrderBy(o => o.Name).ToList();
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = hospitals;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var frm = new FrmHospitalAdd();
            frm.MasterForm = this;
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int hospitalId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                var frm = new FrmHospitalEdit(hospitalId);
                frm.MasterForm = this;
                frm.MdiParent = this.MdiParent;
                frm.Show();
            } else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz hastaneyi seçiniz.");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Seçili hasteneyi silmek istediğinize emin misiniz?", "Hastane Sil", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {                
                    using (var db = new ApplicationDbContext())
                    {
                        int hospitalId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        var hospital = db.Hospitals.Where(x => x.Id == hospitalId).FirstOrDefault();
                        if (hospital != null)
                        {
                            db.Hospitals.Remove(hospital);
                            db.SaveChanges();
                            LoadHospitals();
                        } else
                        {
                            MessageBox.Show("Silinecek kayıt bulunamadı");
                        }
                    }
                }
            } else
            {
                MessageBox.Show("Lütfen silmek istediğiniz hastaneyi seçiniz");
            }
        }
    }
}

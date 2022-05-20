using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOKO_ABC
{
    public partial class FormMaster : Form
    {
        public FormMaster()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var newForm = new FormSupplier();
            newForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var newForm = new FormPembeli();
            newForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newForm = new FormBarang();
            newForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var newForm = new FormPenjualan();
            newForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Apakah anda yakin ingin keluar?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }

        private void FormMaster_Load(object sender, EventArgs e)
        {
            label3.Text = "Logged in as : " + storedUser.nama;
            label4.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy, HH:mm:ss");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy, HH:mm:ss");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var newForm = new FormBantuan();
            newForm.Show();
        }
    }
}

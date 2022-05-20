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
    public partial class FormPembeli : Form
    {
        public bool modeSearch;
        StokDataContext context = new StokDataContext();
        public FormPembeli()
        {
            InitializeComponent();
        }

        private void FormPembeli_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stok_dbDataSet.pembeli' table. You can move, or remove it, as needed.
            this.pembeliTableAdapter.Fill(this.stok_dbDataSet.pembeli);
            dataGridView1.ClearSelection();

        }

        void refreshData()
        {
            this.pembeliTableAdapter.Fill(this.stok_dbDataSet.pembeli);
        }

        void clearComponent()
        {
            storedPembeli.id = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            textBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (storedPembeli.id == 0)
            {
                if (!String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(richTextBox1.Text) && !String.IsNullOrEmpty(textBox3.Text))
                {
                    pembeli newPembeli = new pembeli();
                    newPembeli.nama = textBox2.Text;
                    newPembeli.alamat = richTextBox1.Text;
                    newPembeli.no_telp = textBox3.Text;
                    context.pembelis.InsertOnSubmit(newPembeli);
                    context.SubmitChanges();
                    MessageBox.Show("Data pembeli dengan ID : " + newPembeli.id_pembeli + " berhasil ditambahkan!");
                    clearComponent();
                    refreshData();
                }
                else
                {
                    MessageBox.Show("Setiap input harus diisi!", "Error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Kosongkan input terlebih dahulu (Reset)", "Error");
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            storedPembeli.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            var p = context.pembelis.Where(q => q.id_pembeli == storedPembeli.id).FirstOrDefault();
            storedPembeli.nama = p.nama;
            storedPembeli.alamat = p.alamat;
            storedPembeli.noTelp = p.no_telp;
            textBox1.Text = storedPembeli.id.ToString();
            textBox2.Text = storedPembeli.nama;
            richTextBox1.Text = storedPembeli.alamat;
            textBox3.Text = storedPembeli.noTelp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (storedPembeli.id > 0)
            {
                if (MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    pembeli deletePembeli = context.pembelis.Single(p => p.id_pembeli == storedPembeli.id);
                    context.pembelis.DeleteOnSubmit(deletePembeli);
                    context.SubmitChanges();
                    MessageBox.Show("Data pembeli dengan ID : " + storedPembeli.id + " berhasil dihapus!");
                    clearComponent();
                    refreshData();
                }
                else return;
            }
            else
            {
                MessageBox.Show("Pilih salah satu data pembeli yang akan dihapus!", "Error");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (storedPembeli.id > 0)
            {
                if (!String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(richTextBox1.Text) && !String.IsNullOrEmpty(textBox3.Text))
                {
                    if (textBox2.Text != storedPembeli.nama
                    || richTextBox1.Text != storedPembeli.alamat
                    || textBox3.Text != storedPembeli.noTelp)
                    {
                        pembeli updatePembeli = context.pembelis.Single(p => p.id_pembeli == storedPembeli.id);
                        updatePembeli.nama = textBox2.Text;
                        updatePembeli.alamat = richTextBox1.Text;
                        updatePembeli.no_telp = textBox3.Text;
                        context.SubmitChanges();
                        MessageBox.Show("Data pembeli dengan ID : " + storedPembeli.id + " berhasil diupdate!");
                        refreshData();
                    }
                    else
                    {
                        MessageBox.Show("Anda belum mengubah apapun dari data pembeli ini!", "Error");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Setiap input harus diisi!", "Error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Pilih salah satu data pembeli yang akan diupdate!", "Error");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox4.Text))
            {
                dataGridView1.DataSource = context.pembelis.Where(p => p.nama.Contains(textBox4.Text)).ToList();
                dataGridView1.Refresh();
            }
            else
            {
                MessageBox.Show("Masukkan nama pembeli yang ingin dicari!", "Error");
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.pembelis.ToList();
            dataGridView1.Refresh();
            clearComponent();
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (modeSearch == false)
            {
                modeSearch = true;
                textBox4.Enabled = true;
                button6.Enabled = true;
            }
            else
            {
                modeSearch = false;
                textBox4.Enabled = false;
                button6.Enabled = false;
            }
        }
    }
}

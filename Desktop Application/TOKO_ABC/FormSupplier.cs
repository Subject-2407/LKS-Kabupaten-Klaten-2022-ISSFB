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
    public partial class FormSupplier : Form
    {
        public bool modeSearch;
        StokDataContext context = new StokDataContext();    
        public FormSupplier()
        {
            InitializeComponent();
        }

        private void FormSupplier_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stok_dbDataSet.supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter.Fill(this.stok_dbDataSet.supplier);
            dataGridView1.ClearSelection();

        }

        void refreshData()
        {
            this.supplierTableAdapter.Fill(this.stok_dbDataSet.supplier);
        }

        void clearComponent()
        {
            storedSupplier.id = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            textBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (storedSupplier.id == 0)
            {
                if (!String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(richTextBox1.Text) && !String.IsNullOrEmpty(textBox3.Text))
                {
                    supplier newSupplier = new supplier();
                    newSupplier.nama = textBox2.Text;
                    newSupplier.alamat = richTextBox1.Text;
                    newSupplier.no_telp = textBox3.Text;
                    context.suppliers.InsertOnSubmit(newSupplier);
                    context.SubmitChanges();
                    MessageBox.Show("Data supplier dengan ID : " + newSupplier.id_supplier + " berhasil ditambahkan!");
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
            storedSupplier.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            var p = context.suppliers.Where(q => q.id_supplier == storedSupplier.id).FirstOrDefault();
            storedSupplier.nama = p.nama;
            storedSupplier.alamat = p.alamat;
            storedSupplier.noTelp = p.no_telp;
            textBox1.Text = storedSupplier.id.ToString();
            textBox2.Text = storedSupplier.nama;
            richTextBox1.Text = storedSupplier.alamat;
            textBox3.Text = storedSupplier.noTelp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (storedSupplier.id > 0)
            {
                if (MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    supplier deleteSupplier = context.suppliers.Single(p => p.id_supplier == storedSupplier.id);
                    context.suppliers.DeleteOnSubmit(deleteSupplier);
                    context.SubmitChanges();
                    MessageBox.Show("Data supplier dengan ID : " + storedSupplier.id + " berhasil dihapus!");
                    clearComponent();
                    refreshData();
                }
                else return;
            }
            else
            {
                MessageBox.Show("Pilih salah satu data supplier yang akan dihapus!", "Error");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(storedSupplier.id > 0)
            {
                if (!String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(richTextBox1.Text) && !String.IsNullOrEmpty(textBox3.Text))
                {
                    if (textBox2.Text != storedSupplier.nama
                    || richTextBox1.Text != storedSupplier.alamat
                    || textBox3.Text != storedSupplier.noTelp)
                    {
                        supplier updateSupplier = context.suppliers.Single(p => p.id_supplier == storedSupplier.id);
                        updateSupplier.nama = textBox2.Text;
                        updateSupplier.alamat = richTextBox1.Text;
                        updateSupplier.no_telp = textBox3.Text;
                        context.SubmitChanges();
                        MessageBox.Show("Data supplier dengan ID : " + storedSupplier.id + " berhasil diupdate!");
                        refreshData();
                    }
                    else
                    {
                        MessageBox.Show("Anda belum mengubah apapun dari data supplier ini!", "Error");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Setiap input tidak boleh dibiarkan kosong!", "Error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Pilih salah satu data supplier yang akan diupdate!", "Error");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBox4.Text))
            {
                dataGridView1.DataSource = context.suppliers.Where(p => p.nama.Contains(textBox4.Text)).ToList();
                dataGridView1.Refresh();
            }
            else
            {
                MessageBox.Show("Masukkan nama supplier yang ingin dicari!", "Error");
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.suppliers.ToList();
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

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
    public partial class FormBarang : Form
    {
        StokDataContext context = new StokDataContext();
        public bool modeSearch;

        public FormBarang()
        {
            InitializeComponent();
        }

        private void FormBarang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stok_dbDataSet.supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter.Fill(this.stok_dbDataSet.supplier);
            // TODO: This line of code loads data into the 'stok_dbDataSet.barang' table. You can move, or remove it, as needed.
            this.barangTableAdapter.Fill(this.stok_dbDataSet.barang);
            modeSearch = false;
            storedBarang.id = 0;
            

            var x = (from p in context.barangs
                     join q in context.suppliers
                     on p.id_supplier equals q.id_supplier
                     select new
                     {
                         id_barang = p.id_barang,
                         nama_barang = p.nama_barang,
                         harg = p.harga,
                         id_supplier = q.nama,
                         jns_barang = p.jns_barang,
                         jumlah = p.jumlah,
                         keterangan = p.keterangan
                     }).ToList();
            dataGridView1.DataSource = x;
            dataGridView1.ClearSelection();

        }

        void refreshData()
        {
            this.supplierTableAdapter.Fill(this.stok_dbDataSet.supplier);
            this.barangTableAdapter.Fill(this.stok_dbDataSet.barang);
            var x = (from p in context.barangs
                     join q in context.suppliers
                     on p.id_supplier equals q.id_supplier
                     select new
                     {
                         id_barang = p.id_barang,
                         nama_barang = p.nama_barang,
                         harg = p.harga,
                         id_supplier = q.nama,
                         jns_barang = p.jns_barang,
                         jumlah = p.jumlah,
                         keterangan = p.keterangan
                     }).ToList();
            dataGridView1.DataSource = x;
            dataGridView1.Refresh();
        }

        void clearComponent()
        {
            storedBarang.id = 0;
            dataGridView1.ClearSelection();
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (storedBarang.id == 0)
            {
                if (!String.IsNullOrEmpty(textBox2.Text)
                    && !String.IsNullOrEmpty(richTextBox1.Text)
                    && !String.IsNullOrEmpty(textBox3.Text)
                    && !String.IsNullOrEmpty(textBox5.Text)
                    && !String.IsNullOrEmpty(textBox6.Text))
                {
                    int isNumber;
                    if (int.TryParse(textBox2.Text, out isNumber) && int.TryParse(textBox3.Text, out isNumber))
                    {
                        barang newBarang = new barang();
                        newBarang.nama_barang = richTextBox1.Text;
                        newBarang.harga = Convert.ToInt32(textBox2.Text);
                        newBarang.id_supplier = Convert.ToInt32(comboBox1.SelectedValue);
                        newBarang.jns_barang = textBox5.Text;
                        newBarang.jumlah = Convert.ToInt32(textBox3.Text);
                        newBarang.keterangan = textBox6.Text;

                        context.barangs.InsertOnSubmit(newBarang);
                        context.SubmitChanges();
                        MessageBox.Show("Data barang dengan ID : " + newBarang.id_barang + " berhasil ditambahkan!");
                        clearComponent();
                        refreshData();
                    }
                    else
                    {
                        MessageBox.Show("Harga / jumlah harus bernilai angka!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Setiap entry harus diisi!", "Error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Kosongkan input terlebih dahulu (Reset)", "Error");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            storedBarang.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            var p = context.barangs.Where(q => q.id_barang == storedBarang.id).FirstOrDefault();
            storedBarang.nama = p.nama_barang;
            storedBarang.harga = Convert.ToInt32(p.harga);
            storedBarang.supplierID = Convert.ToInt32(p.id_supplier);
            storedBarang.jenisBarang = p.jns_barang;
            storedBarang.jumlah = Convert.ToInt32(p.jumlah);
            storedBarang.keterangan = p.keterangan;
            textBox1.Text = storedBarang.id.ToString();
            richTextBox1.Text = storedBarang.nama;
            comboBox1.SelectedValue = storedBarang.supplierID;
            textBox2.Text = storedBarang.harga.ToString();
            textBox5.Text = storedBarang.jenisBarang;
            textBox3.Text = storedBarang.jumlah.ToString();
            textBox6.Text = storedBarang.keterangan;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (storedBarang.id > 0)
            {
                if (MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    barang deleteBarang = context.barangs.Single(p => p.id_barang == storedBarang.id);
                    context.barangs.DeleteOnSubmit(deleteBarang);
                    context.SubmitChanges();
                    MessageBox.Show("Data barang dengan ID : " + storedBarang.id + " berhasil dihapus!");
                    clearComponent();
                    refreshData();
                }
                else return;
            }
            else
            {
                MessageBox.Show("Pilih salah satu data barang yang akan dihapus!", "Error");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (storedBarang.id > 0)
            {
                if (!String.IsNullOrEmpty(textBox2.Text)
                    && !String.IsNullOrEmpty(richTextBox1.Text)
                    && !String.IsNullOrEmpty(textBox3.Text)
                    && !String.IsNullOrEmpty(textBox5.Text)
                    && !String.IsNullOrEmpty(textBox6.Text))
                {
                    if (richTextBox1.Text != storedBarang.nama
                    || textBox2.Text != storedBarang.harga.ToString()
                    || Convert.ToInt32(comboBox1.SelectedValue) != storedBarang.supplierID
                    || textBox5.Text != storedBarang.jenisBarang
                    || textBox3.Text != storedBarang.jumlah.ToString()
                    || textBox6.Text != storedBarang.keterangan)
                    {
                        int isNumber;
                        if (int.TryParse(textBox2.Text, out isNumber) && int.TryParse(textBox3.Text, out isNumber))
                        {
                            barang updateBarang = context.barangs.Single(p => p.id_barang == storedBarang.id);
                            updateBarang.nama_barang = richTextBox1.Text;
                            updateBarang.harga = Convert.ToInt32(textBox2.Text);
                            updateBarang.id_supplier = Convert.ToInt32(comboBox1.SelectedValue);
                            updateBarang.jns_barang = textBox5.Text;
                            updateBarang.jumlah = Convert.ToInt32(textBox3.Text);
                            updateBarang.keterangan = textBox6.Text;
                            context.SubmitChanges();
                            MessageBox.Show("Data barang dengan ID : " + storedBarang.id + " berhasil diupdate!");
                            refreshData();
                        }
                        else
                        {
                            MessageBox.Show("Harga / jumlah harus bernilai angka!");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Anda belum mengubah apapun dari data barang ini!", "Error");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Setiap entry harus diisi!", "Error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Pilih salah satu data barang yang akan diupdate!", "Error");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox4.Text))
            {
                var x = (from p in context.barangs
                         join q in context.suppliers
                         on p.id_supplier equals q.id_supplier
                         select new
                         {
                             id_barang = p.id_barang,
                             nama_barang = p.nama_barang,
                             harg = p.harga,
                             id_supplier = q.nama,
                             jns_barang = p.jns_barang,
                             jumlah = p.jumlah,
                             keterangan = p.keterangan
                         }).Where(abc => abc.nama_barang.Contains(textBox4.Text)).ToList();
                dataGridView1.DataSource = x;
                dataGridView1.Refresh();
            }
            else
            {
                MessageBox.Show("Masukkan nama barang yang ingin dicari!", "Error");
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var x = (from p in context.barangs
                     join q in context.suppliers
                     on p.id_supplier equals q.id_supplier
                     select new
                     {
                         id_barang = p.id_barang,
                         nama_barang = p.nama_barang,
                         harg = p.harga,
                         id_supplier = q.nama,
                         jns_barang = p.jns_barang,
                         jumlah = p.jumlah,
                         keterangan = p.keterangan
                     }).ToList();
            dataGridView1.DataSource = x;
            dataGridView1.Refresh();
            clearComponent();
            dataGridView1.ClearSelection();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(textBox4.Text == "")
            {
                var x = (from p in context.barangs
                         join q in context.suppliers
                         on p.id_supplier equals q.id_supplier
                         select new
                         {
                             id_barang = p.id_barang,
                             nama_barang = p.nama_barang,
                             harg = p.harga,
                             id_supplier = q.nama,
                             jns_barang = p.jns_barang,
                             jumlah = p.jumlah,
                             keterangan = p.keterangan
                         }).ToList();
                dataGridView1.DataSource = x;
                dataGridView1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(modeSearch == false)
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

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
    public partial class FormPenjualan : Form
    {

        StokDataContext context = new StokDataContext();
        public FormPenjualan()
        {
            InitializeComponent();
        }

        private void FormPenjualan_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stok_dbDataSet.pembeli' table. You can move, or remove it, as needed.
            this.pembeliTableAdapter.Fill(this.stok_dbDataSet.pembeli);
            // TODO: This line of code loads data into the 'stok_dbDataSet.pembelian_barang' table. You can move, or remove it, as needed.
            this.pembelian_barangTableAdapter.Fill(this.stok_dbDataSet.pembelian_barang);
            // TODO: This line of code loads data into the 'stok_dbDataSet.penjualan_barang' table. You can move, or remove it, as needed.
            this.penjualan_barangTableAdapter.Fill(this.stok_dbDataSet.penjualan_barang);
            // TODO: This line of code loads data into the 'stok_dbDataSet.barang' table. You can move, or remove it, as needed.
            this.barangTableAdapter.Fill(this.stok_dbDataSet.barang);
            dataGridView1.ClearSelection();
            textBox6.Text = storedUser.nama;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            storedBarang.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            var p = context.barangs.Where(q => q.id_barang == storedBarang.id).FirstOrDefault();
            storedBarang.nama = p.nama_barang;
            storedBarang.harga = Convert.ToInt32(p.harga);
            storedBarang.jenisBarang = p.jns_barang;
            storedBarang.jumlah = Convert.ToInt32(p.jumlah);
            storedBarang.keterangan = p.keterangan;
            textBox2.Text = storedBarang.id.ToString();
            textBox3.Text = storedBarang.nama;
            textBox4.Text = storedBarang.harga.ToString();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox11.Text))
            {
                int isNumber;
                if (int.TryParse(textBox11.Text, out isNumber))
                {
                    storedPenjualan.quantity = Convert.ToInt32(textBox11.Text);
                    if (storedPenjualan.quantity > 0 && storedBarang.id > 0)
                    {
                        storedPenjualan.subTotalBarang = storedBarang.harga * storedPenjualan.quantity;
                        textBox12.Text = storedPenjualan.subTotalBarang.ToString();
                    }
                    else textBox12.Text = "";
                }
                else
                {
                    MessageBox.Show("Input kuantitas harus bernilai angka!", "Error");
                    return;
                }
            }
            else textBox12.Text = ""; return;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(storedBarang.id > 0)
            {
                if (storedPenjualan.quantity > 0 && storedPenjualan.subTotalBarang > 0)
                {

                }
                else
                {
                    MessageBox.Show("Tentukan jumlah barang yang akan dibeli!", "Error");
                    return;
                }
            }    
            else
            {
                MessageBox.Show("Pilih barang pembelian yang akan ditambahkan!", "Error");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var newForm = new FormPenjualan();
            newForm.Close();
        }
    }
}

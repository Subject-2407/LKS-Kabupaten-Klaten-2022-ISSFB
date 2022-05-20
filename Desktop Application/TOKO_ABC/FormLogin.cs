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
    public partial class FormLogin : Form
    {
        StokDataContext context = new StokDataContext();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                int isNumber;
                if (int.TryParse(textBox1.Text, out isNumber))
                {
                    var p = context.users.Where(x => x.id_user == Convert.ToInt32(textBox1.Text)).FirstOrDefault();
                    if (p == null)
                    {
                        MessageBox.Show("ID tidak ada!", "Gagal Login");
                        return;
                    }
                    else if (p.password != textBox2.Text)
                    {
                        MessageBox.Show("Password salah!", "Gagal Login");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Berhasil login!");
                        storedUser.nama = p.nama;
                        this.Hide();
                        var newForm = new FormMaster();
                        newForm.Closed += (s, args) => this.Close();
                        newForm.Show();
                    }

                }
                else
                {
                    MessageBox.Show("ID harus berisi angka!", "Gagal Login");
                    return;
                }
            }
            else
            {
                MessageBox.Show("ID dan Password harus diisi!", "Gagal Login");
                return;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}

using InternetCafeApp.Common;
using InternetCafeApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetCafeApp
{
    public partial class FrmKullanici : Form
    {
        DBOperations dBOperations = new DBOperations();
        public FrmKullanici()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            TBLKullanici getKullanici = dBOperations.GetUser(txtKullaniciAdi.Text, txtSifre.Text);
            if (getKullanici != null)
            {
                frmSatıs frm = new frmSatıs();
                frm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Kullanıcı adı veya şifrenizi kontrol ediniz.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FrmKullanici_Load(object sender, EventArgs e)
        {
            txtKullaniciAdi.Focus();
        }

        private void FrmKullanici_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

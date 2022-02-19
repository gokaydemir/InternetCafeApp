using InternetCafeApp.Common;
using InternetCafeApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetCafeApp
{
    public partial class frmSatıs : Form
    {
        #region Değişkenler
        DBOperations dBOperations = new DBOperations();
        RadioButton radio;
        #endregion

        public frmSatıs()
        {
            InitializeComponent();
        }

        private void frmSatıs_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'internetCafeDataSet.TBLSaatUcreti' table. You can move, or remove it, as needed.
            this.tBLSaatUcretiTableAdapter.Fill(this.internetCafeDataSet.TBLSaatUcreti);

            radioButtonSuresiz.Checked = true;
            Yenile();
            comboBosMasalar.Text = "";
        }

        private void Yenile()
        {
            dataGridView1.DataSource = dBOperations.GetSepetList();
            DgvHareketler.DataSource = dBOperations.GetHareketlerList();

            /*false ise Boş, true ise Dolu Masalar*/
            comboBosMasalar.DataSource = dBOperations.GetMasaList(false);
            comboBosMasalar.DisplayMember = "Masalar";
            comboBosMasalar.ValueMember = "MasalarID";

            MasalariGuncelle();
        }

        private void MasalariGuncelle()
        {
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    if (item.Name != btnMasaAc.Name)
                    {
                        List<TBLMasa> GetMasalar = dBOperations.GetMasaList();
                        foreach (TBLMasa masa in GetMasalar)
                        {
                            if (!masa.Durumu && masa.Masalar == item.Text)
                                item.BackColor = Color.LightGreen;
                            if (masa.Durumu && masa.Masalar == item.Text)
                                item.BackColor = Color.OrangeRed;
                        }
                    }
                }
            }
        }

        private void SecileneGore(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor == Color.OrangeRed)
            {
                süreliMasaAçmaİsteğiToolStripMenuItem.Visible = false;
                süresizMasaAçmaİsteğiToolStripMenuItem.Visible = false;
            }
            if (btn.BackColor == Color.LightGreen)
            {
                süreliMasaAçmaİsteğiToolStripMenuItem.Visible = true;
                süresizMasaAçmaİsteğiToolStripMenuItem.Visible = true;
            }
        }

        private void RadioButtonSeciliyeGore(object sender, EventArgs e)
        {
            radio = sender as RadioButton;
        }

        private void btnMasaAc_Click(object sender, EventArgs e)
        {
            if (Helper.tBLKullanici.Gorevi == Helper.KullaniciGorevi.Yonetici)
            {
                TBLSepet newSepet = new TBLSepet()
                {
                    MasalarID = int.Parse(comboBosMasalar.SelectedValue.ToString()),
                    Masa = comboBosMasalar.Text,
                    AcilisTuru = radio.Text,
                    Baslangic = DateTime.Now,
                    Tarih = DateTime.Now
                };
                bool IsAdded = dBOperations.InsertTBLSepet(newSepet);

                if (IsAdded)
                {
                    TBLMasa updateMasa = new TBLMasa()
                    {
                        MasalarID = newSepet.MasalarID,
                        Durumu = true
                    };
                    dBOperations.UpdateMasa(updateMasa);
                    MessageBox.Show(comboBosMasalar.Text + " masa açıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(comboBosMasalar.Text + " masa açılamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Yenile();
                radioButtonSuresiz.Checked = true;
            }
            else
                MessageBox.Show("Böyle bir yetkiniz bulunmuyor", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void frmSatıs_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmKullanici frmKullanici = new FrmKullanici();
            frmKullanici.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["frmMasaKapat"].Index)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex] as DataGridViewRow;
                int SelectedRow = int.Parse(row.Cells[dataGridView1.Columns["Masalar_ID"].Index].Value.ToString());

                TBLMasa updateMasa = new TBLMasa()
                {
                    MasalarID = SelectedRow,
                    Durumu = false
                };
                bool IsUpdated = dBOperations.UpdateMasa(updateMasa);
                if (IsUpdated)
                    MessageBox.Show("Masa kapatıldı.", "Masa Kapatma Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Masa kapatılırken hata oluştu", "Masa Kapatma Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Yenile();
                radioButtonSuresiz.Checked = true;
            }
        }
    }
}
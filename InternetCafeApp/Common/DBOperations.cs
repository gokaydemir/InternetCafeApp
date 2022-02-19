using InternetCafeApp.Model;
using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCafeApp.Common
{
    public class DBOperations
    {
        /// <summary>
        /// Sql Bağlantı Cümleciği
        /// </summary>
        public static SqlConnection Conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=InternetCafe;Integrated Security=True;Pooling=False");

        #region TBLKullanici İşlemleri
        /// <summary>
        /// Kullanıcı adı ve şifreye göre kullanıcı döndüren metod
        /// </summary>
        /// <param name="KullaniciAdi">Kullanıcı adı parametresi</param>
        /// <param name="Sifre">Şifre parametresi</param>
        /// <returns></returns>
        public TBLKullanici GetUser(string KullaniciAdi, string Sifre)
        {
            TBLKullanici tBLKullanici = new TBLKullanici();
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string SqlQuery = @"SELECT * FROM TBLKullanici WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";
                SqlCommand Cmd = new SqlCommand(SqlQuery, Conn);
                Cmd.Parameters.AddWithValue("@KullaniciAdi", KullaniciAdi);
                Cmd.Parameters.AddWithValue("@Sifre", Sifre);
                SqlDataReader Reader = Cmd.ExecuteReader();
                if (Reader.Read())
                {
                    tBLKullanici.AdiSoyadi = Reader[1].ToString();
                    tBLKullanici.Telefon = Reader[2].ToString();
                    tBLKullanici.Adres = Reader[3].ToString();
                    tBLKullanici.Email = Reader[4].ToString();
                    tBLKullanici.KullaniciAdi = Reader[5].ToString();
                    tBLKullanici.Sifre = Reader[6].ToString();
                    tBLKullanici.Gorevi = (Helper.KullaniciGorevi)int.Parse(Reader[7].ToString());
                    tBLKullanici.Tarih = DateTime.Parse(Reader[8].ToString());
                    Helper.tBLKullanici = tBLKullanici;
                }
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Hata Detayı: " + exc.Message, "Hata Oluştu!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return tBLKullanici;
        }
        #endregion

        #region TBLSepet İşlemleri
        /// <summary>
        /// Sepete yeni kayıt ekleyen metod
        /// </summary>
        /// <param name="tBLSepet">TBLSepet Nesnesi</param>
        /// <returns></returns>
        public bool InsertTBLSepet(TBLSepet tBLSepet)
        {
            bool IsAdded = false;
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string SqlQuery = @"INSERT INTO TBLSepet VALUES (@MasalarID, @Masa, @AcilisTuru, @Baslangic, @Tarih)";
                SqlCommand Cmd = new SqlCommand(SqlQuery, Conn);
                Cmd.Parameters.AddWithValue("@MasalarID", tBLSepet.MasalarID);
                Cmd.Parameters.AddWithValue("@Masa", tBLSepet.Masa);
                Cmd.Parameters.AddWithValue("@AcilisTuru", tBLSepet.AcilisTuru);
                Cmd.Parameters.AddWithValue("@Baslangic", tBLSepet.Baslangic);
                Cmd.Parameters.AddWithValue("@Tarih", tBLSepet.Tarih);
                int AddedRow = Cmd.ExecuteNonQuery();
                if (AddedRow > 0)
                    IsAdded = true;
                else
                    IsAdded = false;
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Hata Detayı: " + exc.Message, "Hata Oluştu!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return IsAdded;
        }

        /// <summary>
        /// Sepetteki bilgileri liste olarak döndüren metod
        /// </summary>
        /// <returns></returns>
        public List<TBLSepet> GetSepetList()
        {
            List<TBLSepet> SepetListesi = new List<TBLSepet>();
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string SqlQuery = @"SELECT * FROM TBLSepet";
                SqlCommand Cmd = new SqlCommand(SqlQuery, Conn);
                SqlDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    TBLSepet getSepet = new TBLSepet();
                    getSepet.MasalarID = int.Parse(Reader[1].ToString());
                    getSepet.Masa = Reader[2].ToString();
                    getSepet.AcilisTuru = Reader[3].ToString();
                    getSepet.Baslangic = DateTime.Parse(Reader[4].ToString());
                    getSepet.Tarih = DateTime.Parse(Reader[5].ToString());
                    SepetListesi.Add(getSepet);
                }
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Hata Detayı: " + exc.Message, "Hata Oluştu!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return SepetListesi;
        }
        #endregion

        #region TBLMasa İşlemleri
        /// <summary>
        /// Masaların tamamını gelen parametreye göre listeler
        /// </summary>
        /// <param name="Durumu">Masanın dolu veya boş olduğunu belirten parametre</param>
        /// <returns></returns>
        public List<TBLMasa> GetMasaList(bool Durumu)
        {
            List<TBLMasa> MasaListesi = new List<TBLMasa>();
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string SqlQuery = @"SELECT * FROM TBLMasa WHERE Durumu = @Durumu";
                SqlCommand Cmd = new SqlCommand(SqlQuery, Conn);
                Cmd.Parameters.AddWithValue("@Durumu", Durumu);
                SqlDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    TBLMasa getMasa = new TBLMasa();
                    getMasa.MasalarID = int.Parse(Reader[0].ToString());
                    getMasa.Masalar = Reader[1].ToString();
                    getMasa.Durumu = bool.Parse(Reader[2].ToString());
                    getMasa.Aciklama = Reader[3].ToString();
                    MasaListesi.Add(getMasa);
                }
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Hata Detayı: " + exc.Message, "Hata Oluştu!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return MasaListesi;
        }

        /// <summary>
        /// Masaların tamamını listeler
        /// </summary>
        /// <returns></returns>
        public List<TBLMasa> GetMasaList()
        {
            List<TBLMasa> MasaListesi = new List<TBLMasa>();
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string SqlQuery = @"SELECT * FROM TBLMasa";
                SqlCommand Cmd = new SqlCommand(SqlQuery, Conn);
                SqlDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    TBLMasa getMasa = new TBLMasa();
                    getMasa.MasalarID = int.Parse(Reader[0].ToString());
                    getMasa.Masalar = Reader[1].ToString();
                    getMasa.Durumu = bool.Parse(Reader[2].ToString());
                    getMasa.Aciklama = Reader[3].ToString();
                    MasaListesi.Add(getMasa);
                }
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Hata Detayı: " + exc.Message, "Hata Oluştu!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return MasaListesi;
        }

        public bool UpdateMasa(TBLMasa tblMasa)
        {
            bool IsUpdated = false;
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string SqlQuery = @"UPDATE TBLMasa SET Durumu = @Durumu WHERE MasalarID = @MasalarID";
                SqlCommand Cmd = new SqlCommand(SqlQuery, Conn);
                Cmd.Parameters.AddWithValue("@Durumu", tblMasa.Durumu);
                Cmd.Parameters.AddWithValue("@MasalarID", tblMasa.MasalarID);
                int IsAffectedRow = Cmd.ExecuteNonQuery();
                if (IsAffectedRow > 0)
                    IsUpdated = true;
                else
                    IsUpdated = false;
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Hata Detayı: " + exc.Message, "Hata Oluştu!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return IsUpdated;
        }
        #endregion

        #region TBLHareketler İşlemleri
        /// <summary>
        /// Hareketleri Listeleyen Metod
        /// </summary>
        /// <returns></returns>
        public List<TBLHareketler> GetHareketlerList()
        {
            List<TBLHareketler> HareketListesi = new List<TBLHareketler>();
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                string SqlQuery = @"SELECT * FROM TBLHareketler";
                SqlCommand Cmd = new SqlCommand(SqlQuery, Conn);
                SqlDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    TBLHareketler getHareket = new TBLHareketler();
                    getHareket.KullanıcıID = int.Parse(Reader[1].ToString());
                    getHareket.MasalarID = int.Parse(Reader[2].ToString());
                    getHareket.Masa = Reader[3].ToString();
                    getHareket.IstekTuru = Reader[4].ToString();
                    getHareket.Aciklama = Reader[5].ToString();
                    getHareket.Tarih = DateTime.Parse(Reader[6].ToString());
                    HareketListesi.Add(getHareket);
                }
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Hata Detayı: " + exc.Message, "Hata Oluştu!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return HareketListesi;
        }
        #endregion
    }
}
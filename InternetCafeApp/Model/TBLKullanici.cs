using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InternetCafeApp.Common.Helper;

namespace InternetCafeApp.Model
{
    public class TBLKullanici
    {
        public int KullaniciID { get; set; }
        public string AdiSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public KullaniciGorevi Gorevi { get; set; }
        public DateTime Tarih { get; set; }
    }
}

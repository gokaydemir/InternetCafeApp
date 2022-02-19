using InternetCafeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCafeApp.Common
{
    public class Helper
    {
        /// <summary>
        /// Oturum Nesnesi
        /// </summary>
        public static TBLKullanici tBLKullanici;

        /// <summary>
        /// Oturum açan kullanıcı görevi nesnesi
        /// </summary>
        public enum KullaniciGorevi : int
        { 
            Yonetici = 1,
            Kullanici = 2
        }
    }
}

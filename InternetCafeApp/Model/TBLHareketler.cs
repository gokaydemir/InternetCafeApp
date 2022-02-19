using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCafeApp.Model
{
    public class TBLHareketler
    {
        public int HareketID { get; set; }
        public int KullanıcıID { get; set; }
        public int MasalarID { get; set; }
        public string Masa { get; set; }
        public string IstekTuru { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}

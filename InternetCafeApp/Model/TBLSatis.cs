using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCafeApp.Model
{
    public class TBLSatis
    {
        public int SatisID { get; set; }
        public int KullaniciID { get; set; }
        public int MasalarID { get; set; }
        public DateTime BaslangicSaati { get; set; }
        public DateTime BitisSaati { get; set; }
        public decimal Sure { get; set; }
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }

    }
}

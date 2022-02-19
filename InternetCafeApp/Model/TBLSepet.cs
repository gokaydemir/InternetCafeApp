using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCafeApp.Model
{
    public class TBLSepet
    {
        public int SepetID { get; set; }
        public int MasalarID { get; set; }
        public string Masa { get; set; }
        public string AcilisTuru { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Tarih { get; set; }
    }
}

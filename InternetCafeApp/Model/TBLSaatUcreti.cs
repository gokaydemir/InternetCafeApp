using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCafeApp.Model
{
    public class TBLSaatUcreti
    {
        public int SaatUcretiID { get; set; }
        public decimal SaatUcreti { get; set; }
        public string UcretTuru { get; set; }
        public string Aciklama { get; set; }
    }
}

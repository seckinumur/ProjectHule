using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VM
{
   public class VMKullanici
    {
        public int KullanicilarID { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSifre { get; set; }
        public string Gorev { get; set; }
        public bool Admin { get; set; }
    }
}

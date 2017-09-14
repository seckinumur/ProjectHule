using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
   public class Siparis
    {
        public int SiparisID { get; set; }
        public int SepetID { get; set; }
        public int KullanicilarID { get; set; }
        public string SiparisTarihi { get; set; }
        public string GonderimTarihi { get; set; }
        public string Not { get; set; }
        public bool Onaylandimi { get; set; }
        public bool Gonderildimi { get; set; }
        public bool İptal { get; set; }
        public string Yil { get; set; }
        public string Ay { get; set; }
        public string Gun { get; set; }

        public virtual Sepet Sepet { get; set; }
        public virtual Kullanicilar Kullanicilar { get; set; }
    }
}

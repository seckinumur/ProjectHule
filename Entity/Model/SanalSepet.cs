using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{ 
   public class SanalSepet
    {
        public int SanalSepetID { get; set; }
        public int KullanicilarID { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string SinifKodu { get; set; }
        public string SinifTanimi { get; set; }
        public string MalzemeKodu { get; set; }
        public double Fiyat { get; set; }
        public int Adet { get; set; }

        public virtual Kullanicilar Kullanicilar { get; set; }
    }
}

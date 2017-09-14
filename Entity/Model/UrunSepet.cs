using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
   public class UrunSepet
    {
        public int UrunSepetID { get; set; }
        public int KullanicilarID { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string SinifKodu { get; set; }
        public string SinifTanimi { get; set; }
        public string MalzemeKodu { get; set; }
        public double Fiyat { get; set; }
        public int Adet { get; set; }
        public int UrunStokID { get; set; }

        public virtual UrunStok UrunStok { get; set; }
    }
}

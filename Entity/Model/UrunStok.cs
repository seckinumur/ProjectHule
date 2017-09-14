using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
   public class UrunStok
    {
        public int UrunStokID { get; set; }
        public double Fiyati { get; set; }
        public int Adedi { get; set; }
        public string MalzemeKodu { get; set; }
    }
}

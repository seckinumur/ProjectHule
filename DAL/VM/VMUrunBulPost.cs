using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VM
{
   public class VMUrunBulPost
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public string SinifKodu { get; set; }
        public string SinifTanimi { get; set; }
        public string MalzemeKodu { get; set; }
        public int Stok { get; set; }
        public double Fiyat { get; set; }
    }
}

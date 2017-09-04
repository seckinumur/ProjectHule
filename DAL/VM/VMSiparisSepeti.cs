using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VM
{
   public class VMSiparisSepeti
    {
        public List<VMUrun> Urun { get; set; }
        public Musteri Musteri { get; set; }
        public double ToplamFiyat { get; set; }
    }
}

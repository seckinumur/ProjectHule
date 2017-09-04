using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
   public class EnCokSatan
    {
        public int EncokSatanID { get; set; }
        public int UrunID { get; set; }
        public int Adet { get; set; }

        public virtual Urun Urun { get; set; }
    }
}

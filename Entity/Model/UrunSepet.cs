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
        public int UrunID { get; set; }
        public int Adedi { get; set; }
        public int UrunStokID { get; set; }

        public virtual Urun Urun { get; set; }
        public virtual UrunStok UrunStok { get; set; }
    }
}

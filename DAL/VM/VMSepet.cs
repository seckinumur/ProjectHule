using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VM
{
   public class VMSepet
    {
        public int SepetID { get; set; }
        public int MusterilerID { get; set; }
        public bool SiparisTamamlandimi { get; set; }
        public bool Manuel { get; set; }

        public virtual List<UrunSepet> UrunSepet { get; set; }
    }
}

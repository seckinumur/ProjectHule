﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
   public class Sepet
    {
        public int SepetID { get; set; }
        public int MusteriID { get; set; }
        public bool SiparisTamamlandimi { get; set; }
        public bool Aktifmi { get; set; }
        public bool Manuel { get; set; }

        public virtual List<UrunSepet> UrunSepet { get; set; }
        public virtual Musteri Musteri { get; set; }
    }
}

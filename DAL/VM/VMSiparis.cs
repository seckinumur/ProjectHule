﻿using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VM
{
    public class VMSiparis
    {
        public int SiparisID { get; set; }
        public int SepetID { get; set; }
        public Musteri Musteri { get; set; }
        public string SiparisTarihi { get; set; }
        public string GonderimTarihi { get; set; }
        public string Gorev { get; set; }
        public string Durum { get; set; }
        public string Sonuc { get; set; }
        public string Not { get; set; }
        public int ToplamAdet { get; set; }
        public double ToplamFiyat { get; set; }
        public bool Onaylandimi { get; set; }
        public bool Gonderildimi { get; set; }
        public bool İptal { get; set; }
    }
}

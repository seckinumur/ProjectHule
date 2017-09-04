﻿using DAL.VM;
using Entity.Context;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class SiparisRepo
    {
        public static bool SiparisKaydet(Sepet Data) //sepet olarak gelen datayı siparişe ekledik
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    db.Siparis.Add(new Siparis()
                    {
                        Gonderildimi = false,
                        Onaylandimi = true,
                        SepetID = Data.SepetID,
                        SiparisTarihi = DateTime.Now.ToShortDateString(),
                        İptal = false
                    });
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool SiparisKaydetUye(Sepet Data) //sepet olarak gelen datayı siparişe ekledik
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    db.Siparis.Add(new Siparis()
                    {
                        Gonderildimi = false,
                        Onaylandimi = false,
                        SepetID = Data.SepetID,
                        SiparisTarihi = DateTime.Now.ToShortDateString(),
                        İptal = false
                    });
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool SiparisOnayla(int ID)
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Siparis = db.Siparis.FirstOrDefault(p => p.SiparisID == ID && p.Onaylandimi == false);
                    Siparis.Onaylandimi = true;
                    Siparis.İptal = false;
                    Siparis.Gonderildimi = false;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool SiparisİptalEt(int ID)
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Siparis = db.Siparis.FirstOrDefault(p => p.SiparisID == ID && p.İptal != true);
                    Siparis.Onaylandimi = true;
                    Siparis.İptal = true;
                    Siparis.Gonderildimi = false;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool SiparisİptalEtme(int ID) //iptal siparişi tekrar iptallikten çıkarma
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Siparis = db.Siparis.FirstOrDefault(p => p.SiparisID == ID && p.İptal == true);
                    Siparis.Onaylandimi = true;
                    Siparis.İptal = false;
                    Siparis.Gonderildimi = false;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static void SiparisSil(int ID)
        {
            using (PHDB db = new PHDB())
            {
                var Siparis = db.Siparis.FirstOrDefault(p => p.SiparisID == ID);
                var Sepet = db.Sepet.FirstOrDefault(p => p.SepetID == Siparis.SepetID);
                foreach (var item in Sepet.UrunSepet)
                {
                    var bul = db.UrunSepet.FirstOrDefault(p => p.UrunSepetID == item.UrunSepetID);
                    db.UrunSepet.Remove(bul);
                    db.SaveChanges();
                }
                db.Siparis.Remove(Siparis);
                db.Sepet.Remove(Sepet);
                db.SaveChanges();
            }
        }
        public static bool SiparisGonder(int ID)
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Siparis = db.Siparis.FirstOrDefault(p => p.SiparisID == ID && p.İptal != true && p.Onaylandimi == true);
                    string gun = DateTime.Now.Day.ToString(), ay = DateTime.Now.Month.ToString(), yil = DateTime.Now.Year.ToString();
                    Siparis.Onaylandimi = true;
                    Siparis.İptal = false;
                    Siparis.Gonderildimi = true;
                    Siparis.GonderimTarihi = DateTime.Now.ToShortDateString();
                    foreach (var item in Siparis.Sepet.UrunSepet)
                    {
                        item.UrunStok.Adedi -= item.Adedi;
                        db.SaveChanges();
                    }
                    foreach (var item in Siparis.Sepet.UrunSepet)
                    {
                        try
                        {
                            var Bul = db.AylikCiro.FirstOrDefault(p => p.Yil == yil && p.Ay == ay);
                            Bul.ToplamAdet += item.Adedi;
                            Bul.ToplamSatis += item.UrunStok.Fiyati * item.Adedi;
                            db.SaveChanges();
                            try
                            {
                                var kontrol = db.GunlukCiro.FirstOrDefault(p => p.Yil == yil && p.Ay == ay && p.Gun == gun);
                                kontrol.ToplamAdet += item.Adedi;
                                kontrol.ToplamSatis += item.UrunStok.Fiyati * item.Adedi;
                                db.SaveChanges();
                            }
                            catch
                            {
                                db.GunlukCiro.Add(new GunlukCiro
                                {
                                    Ay = ay,
                                    Gun = gun,
                                    Yil = yil,
                                    ToplamAdet = item.Adedi,
                                    ToplamSatis = item.UrunStok.Fiyati * item.Adedi
                                });
                                db.SaveChanges();
                            }
                        }
                        catch
                        {
                            db.AylikCiro.Add(new AylikCiro
                            {
                                Ay = ay,
                                Yil = yil,
                                ToplamAdet = item.Adedi,
                                ToplamSatis = item.UrunStok.Fiyati * item.Adedi
                            });
                            db.SaveChanges();

                            db.GunlukCiro.Add(new GunlukCiro
                            {
                                Ay = ay,
                                Gun = gun,
                                Yil = yil,
                                ToplamAdet = item.Adedi,
                                ToplamSatis = item.UrunStok.Fiyati * item.Adedi
                            });
                            db.SaveChanges();
                        }
                        try
                        {
                            var al = db.EnCokSatan.FirstOrDefault(p => p.UrunID == item.UrunID);
                            al.Adet += item.Adedi;
                            db.SaveChanges();
                        }
                        catch
                        {
                            db.EnCokSatan.Add(new EnCokSatan
                            {
                                UrunID = item.UrunID,
                                Adet = item.Adedi
                            });
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static List<VMSiparis> OnayBEkleyenSiparisler()
        {
            using (PHDB db = new PHDB())
            {
                return db.Siparis.Where(p => p.Onaylandimi == false && p.İptal != true && p.Gonderildimi != true).Select(p => new VMSiparis
                {
                    SepetID = p.SepetID,
                    SiparisID = p.SiparisID,
                    SiparisTarihi = p.SiparisTarihi,
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adedi),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adedi),
                    Musteri = db.Musteri.FirstOrDefault(w => w.MusteriID == p.Sepet.MusteriID),
                    Not = p.Not
                }).ToList();
            }
        }
        public static List<VMSiparis> OnaylananSiparisler()
        {
            using (PHDB db = new PHDB())
            {
                return db.Siparis.Where(p => p.Onaylandimi == true && p.İptal != true && p.Gonderildimi != true).Select(p => new VMSiparis
                {
                    SepetID = p.SepetID,
                    SiparisID = p.SiparisID,
                    SiparisTarihi = p.SiparisTarihi,
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adedi),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adedi),
                    Musteri = db.Musteri.FirstOrDefault(w => w.MusteriID == p.Sepet.MusteriID),
                    Not = p.Not
                }).ToList();
            }
        }
        public static List<VMSiparis> IptalEdilenSiparisler()
        {
            using (PHDB db = new PHDB())
            {
                return db.Siparis.Where(p => p.İptal == true && p.Gonderildimi != true).Select(p => new VMSiparis
                {
                    SepetID = p.SepetID,
                    SiparisID = p.SiparisID,
                    SiparisTarihi = p.SiparisTarihi,
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adedi),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adedi),
                    Musteri = db.Musteri.FirstOrDefault(w => w.MusteriID == p.Sepet.MusteriID),
                    Not = p.Not
                }).ToList();
            }
        }
        public static List<VMSiparis> GonderilenSiparisler()
        {
            using (PHDB db = new PHDB())
            {
                return db.Siparis.Where(p => p.Gonderildimi == true && p.İptal != true).Select(p => new VMSiparis
                {
                    GonderimTarihi = p.GonderimTarihi,
                    SepetID = p.SepetID,
                    SiparisID = p.SiparisID,
                    SiparisTarihi = p.SiparisTarihi,
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adedi),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adedi),
                    Musteri = db.Musteri.FirstOrDefault(w => w.MusteriID == p.Sepet.MusteriID),
                    Not = p.Not
                }).ToList();
            }
        }
        public static List<VMSiparis> GonderilmeyenSiparisler()
        {
            using (PHDB db = new PHDB())
            {
                return db.Siparis.Where(p => p.Gonderildimi == false && p.İptal != true && p.Onaylandimi == true).Select(p => new VMSiparis
                {
                    SepetID = p.SepetID,
                    SiparisID = p.SiparisID,
                    SiparisTarihi = p.SiparisTarihi,
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adedi),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adedi),
                    Musteri = db.Musteri.FirstOrDefault(w => w.MusteriID == p.Sepet.MusteriID),
                    Not = p.Not
                }).ToList();
            }
        }
        public static List<VMSiparis> UyeSiparisleri(int ID)
        {
            using (PHDB db = new PHDB())
            {
                return db.Siparis.Where(p => p.Sepet.MusteriID == ID).Select(p => new VMSiparis
                {
                    SepetID = p.SepetID,
                    Gonderildimi = p.Gonderildimi,
                    Onaylandimi = p.Onaylandimi,
                    İptal = p.İptal,
                    GonderimTarihi = p.GonderimTarihi,
                    SiparisID = p.SiparisID,
                    SiparisTarihi = p.SiparisTarihi,
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adedi),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adedi),
                    Musteri = db.Musteri.FirstOrDefault(w => w.MusteriID == p.Sepet.MusteriID),
                    Not = p.Not
                }).ToList();
            }
        }
        public static List<VMSiparis> UyeSiparisListele(string id)
        {
            using (PHDB db = new PHDB())
            {
                int ID = int.Parse(id);

                return db.Siparis.Where(p => p.Sepet.MusteriID == ID).Select(p => new VMSiparis
                {
                    SepetID = p.SepetID,
                    GonderimTarihi = p.GonderimTarihi,
                    Durum = p.Gonderildimi == false ? "label label-danger" : "label label-primary",
                    Sonuc = p.Gonderildimi == false ? "Sipariş Hazırlanıyor" : "Sipariş Gönderildi",
                    SiparisID = p.SiparisID,
                    Gonderildimi = p.Gonderildimi,
                    SiparisTarihi = p.SiparisTarihi,
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adedi),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adedi),
                    Musteri = db.Musteri.FirstOrDefault(w => w.MusteriID == p.Sepet.MusteriID),
                    Not = p.Not
                }).ToList();
            }
        }
        public static List<VMSiparisUrun> UrunSepeti(int ID)
        {
            using (PHDB db = new PHDB())
            {
                List<VMSiparisUrun> liste = new List<VMSiparisUrun>();
                var ayir = db.Siparis.FirstOrDefault(p => p.SiparisID == ID);
                foreach (var item in ayir.Sepet.UrunSepet)
                {
                    liste.Add(new VMSiparisUrun
                    {
                        Marka = item.Urun.Marka,
                        Model = item.Urun.Model,
                        SinifKodu = item.Urun.SinifKodu,
                        SinifTanimi = item.Urun.SinifTanimi,
                        MalzemeKodu = item.Urun.MalzemeKodu,
                        Section1 = item.Urun.Section1,
                        Section2 = item.Urun.Section2,
                        Section3 = item.Urun.Section3,
                        Section4 = item.Urun.Section4,
                        Section5 = item.Urun.Section5,
                        Section6 = item.Urun.Section6,
                        Section7 = item.Urun.Section7,
                        Section8 = item.Urun.Section8,
                        Section9 = item.Urun.Section9,
                        Section10 = item.Urun.Section10,
                        Section11 = item.Urun.Section11,
                        Section12 = item.Urun.Section12,
                        Section13 = item.Urun.Section13,
                        Section14 = item.Urun.Section14,
                        Section15 = item.Urun.Section15,
                        Adedi = item.Adedi,
                        Fiyat = item.UrunStok.Fiyati * item.Adedi
                    });
                }
                return liste;
            }
        }
        public static List<VMSiparisUrun> UrunSepetiKullanici(int ID)
        {
            using (PHDB db = new PHDB())
            {
                List<VMSiparisUrun> liste = new List<VMSiparisUrun>();
                var ayir = db.Siparis.FirstOrDefault(p => p.SiparisID == ID);
                foreach (var item in ayir.Sepet.UrunSepet)
                {
                    liste.Add(new VMSiparisUrun
                    {
                        Marka = item.Urun.Marka,
                        Model = item.Urun.Model,
                        SinifKodu = item.Urun.SinifKodu,
                        SinifTanimi = item.Urun.SinifTanimi,
                        MalzemeKodu = item.Urun.MalzemeKodu,
                        Section1 = item.Urun.Section1,
                        Section2 = item.Urun.Section2,
                        Section3 = item.Urun.Section3,
                        Section4 = item.Urun.Section4,
                        Section5 = item.Urun.Section5,
                        Section6 = item.Urun.Section6,
                        Section7 = item.Urun.Section7,
                        Section8 = item.Urun.Section8,
                        Section9 = item.Urun.Section9,
                        Section10 = item.Urun.Section10,
                        Section11 = item.Urun.Section11,
                        Section12 = item.Urun.Section12,
                        Section13 = item.Urun.Section13,
                        Section14 = item.Urun.Section14,
                        Section15 = item.Urun.Section15,
                        Adedi = item.Adedi,
                        Fiyat = item.UrunStok.Fiyati * item.Adedi
                    });
                }
                return liste;
            }
        }
    }
}

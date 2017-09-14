using DAL.VM;
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
                        Onaylandimi = false,
                        SepetID = Data.SepetID,
                        KullanicilarID=Data.KullanicilarID,
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
                    Siparis.Ay = ay;
                    Siparis.Gun = gun;
                    Siparis.Yil = yil;
                    foreach (var item in Siparis.Sepet.UrunSepet)
                    {
                        item.UrunStok.Adedi -= item.Adet;
                        db.SaveChanges();
                    }
                    foreach (var item in Siparis.Sepet.UrunSepet)
                    {
                        try
                        {
                            var Bul = db.AylikCiro.FirstOrDefault(p => p.Yil == yil && p.Ay == ay);
                            Bul.ToplamAdet += item.Adet;
                            Bul.ToplamSatis += item.UrunStok.Fiyati * item.Adet;
                            db.SaveChanges();
                            try
                            {
                                var kontrol = db.GunlukCiro.FirstOrDefault(p => p.Yil == yil && p.Ay == ay && p.Gun == gun);
                                kontrol.ToplamAdet += item.Adet;
                                kontrol.ToplamSatis += item.UrunStok.Fiyati * item.Adet;
                                db.SaveChanges();
                            }
                            catch
                            {
                                db.GunlukCiro.Add(new GunlukCiro
                                {
                                    Ay = ay,
                                    Gun = gun,
                                    Yil = yil,
                                    ToplamAdet = item.Adet,
                                    ToplamSatis = item.UrunStok.Fiyati * item.Adet
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
                                ToplamAdet = item.Adet,
                                ToplamSatis = item.UrunStok.Fiyati * item.Adet
                            });
                            db.SaveChanges();

                            db.GunlukCiro.Add(new GunlukCiro
                            {
                                Ay = ay,
                                Gun = gun,
                                Yil = yil,
                                ToplamAdet = item.Adet,
                                ToplamSatis = item.UrunStok.Fiyati * item.Adet
                            });
                            db.SaveChanges();
                        }
                        try
                        {
                            var al = db.EnCokSatan.FirstOrDefault(p => p.MalzemeKodu == item.MalzemeKodu);
                            al.Adet += item.Adet;
                            db.SaveChanges();
                        }
                        catch
                        {
                            db.EnCokSatan.Add(new EnCokSatan
                            {
                                MalzemeKodu = item.MalzemeKodu,
                                Marka=item.Marka,
                                Model=item.Model,
                                SinifKodu=item.SinifKodu,
                                SinifTanimi=item.SinifTanimi,
                                Adet = item.Adet
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
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adet),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adet),
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
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adet),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adet),
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
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adet),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adet),
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
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adet),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adet),
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
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adet),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adet),
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
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adet),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adet),
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
                    ToplamAdet = p.Sepet.UrunSepet.Sum(n => n.Adet),
                    ToplamFiyat = p.Sepet.UrunSepet.Sum(n => n.UrunStok.Fiyati * n.Adet),
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
                        Marka = item.Marka,
                        Model = item.Model,
                        SinifKodu = item.SinifKodu,
                        SinifTanimi = item.SinifTanimi,
                        MalzemeKodu = item.MalzemeKodu,
                        Adedi = item.Adet,
                        Fiyat = item.UrunStok.Fiyati * item.Adet
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
                        Marka = item.Marka,
                        Model = item.Model,
                        SinifKodu = item.SinifKodu,
                        SinifTanimi = item.SinifTanimi,
                        MalzemeKodu = item.MalzemeKodu,
                        Adedi = item.Adet,
                        Fiyat = item.UrunStok.Fiyati * item.Adet
                    });
                }
                return liste;
            }
        }
    }
}


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
    public class SepetRepo
    {
        public static bool SanalSepeteEkle(int kullanici, string malzemekodu, int adet, double Fiyat) //Sanal sepet
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var bulUrun = db.Urun.FirstOrDefault(p => p.MalzemeKodu == malzemekodu);
                    try
                    {
                        var bul = db.SanalSepet.FirstOrDefault(p => p.KullanicilarID == kullanici && p.MalzemeKodu == malzemekodu);
                        bul.Adet += adet;
                        bul.Fiyat = Fiyat;
                        db.SaveChanges();
                        return true;
                    }
                    catch
                    {
                        db.SanalSepet.Add(new SanalSepet()
                        {
                            Adet = adet,
                            KullanicilarID = kullanici,
                            MalzemeKodu = malzemekodu,
                            Fiyat = Fiyat,
                            Marka = bulUrun.Marka,
                            Model = bulUrun.Model,
                            SinifKodu = bulUrun.SinifKodu,
                            SinifTanimi = bulUrun.SinifTanimi
                        });
                        db.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool SanalSepetUrunGuncelle(int kullanici, string malzemekodu, int adet, double Fiyat) //Sanal sepet
        {
            using (PHDB db = new PHDB())
            {
                var bulUrun = db.Urun.FirstOrDefault(p => p.MalzemeKodu == malzemekodu);
                try
                {
                    var bul = db.SanalSepet.FirstOrDefault(p => p.KullanicilarID == kullanici && p.MalzemeKodu == malzemekodu);
                    bul.Adet = adet;
                    bul.Fiyat = Fiyat;
                    db.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool SanalSepetKontrol(int ID) //Sanal sepet
        {
            using (PHDB db = new PHDB())
            {
                bool varmi = db.SanalSepet.Any(p => p.KullanicilarID == ID);
                return varmi;
            }
        }
        public static List<VMUrun> SanalSepeteListe(int kullanici) //Sanal sepet
        {
            using (PHDB db = new PHDB())
            {
                return db.SanalSepet.Where(p => p.KullanicilarID == kullanici).Select(p => new VMUrun
                {
                    Marka = p.Marka,
                    Model = p.Model,
                    SinifKodu = p.SinifKodu,
                    SinifTanimi = p.SinifTanimi,
                    MalzemeKodu = p.MalzemeKodu,
                    Adet = p.Adet,
                    Fiyat = p.Fiyat
                }).ToList();
            }
        }
        public static bool SanalSepeteCikar(int kullanici, string Malzemekodu) //Sanal sepet
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var bul = db.SanalSepet.FirstOrDefault(p => p.KullanicilarID == kullanici && p.MalzemeKodu == Malzemekodu);
                    db.SanalSepet.Remove(bul);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool SepetiKaydetKullanici(int KullaniciID, VMSanalSiparis data) //Kullanıcı Modunda Manuel Sepeti Ekle
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var bul = db.SanalSepet.Where(p => p.KullanicilarID == KullaniciID).ToList();
                    if (bul.Count != 0)
                    {
                        var liste = bul.Select(p => new UrunSepet
                        {
                            Adet = p.Adet,
                            Fiyat = p.Fiyat,
                            KullanicilarID = p.KullanicilarID,
                            MalzemeKodu = p.MalzemeKodu,
                            Marka = p.Marka,
                            Model = p.Model,
                            SinifKodu = p.SinifKodu,
                            SinifTanimi = p.SinifTanimi,
                            UrunStokID = db.UrunStok.FirstOrDefault(e => e.MalzemeKodu == p.MalzemeKodu).UrunStokID
                        }).ToList();

                        int Uye;
                        try
                        {
                            Uye = db.Musteri.FirstOrDefault(p => p.AdiSoyadi == data.AdiSoyadi.Trim().ToUpper()).MusteriID;
                        }
                        catch
                        {
                            db.Musteri.Add(new Musteri
                            {
                                AdiSoyadi = data.AdiSoyadi.Trim().ToUpper(),
                                Adres = data.Adres.Trim().ToUpper(),
                                MailAdresi = data.MailAdresi,
                                not = data.not.Trim().ToUpper(),
                                Tarih = DateTime.Now.ToShortDateString(),
                                Telefon = data.Telefon.Trim().ToUpper()
                            });
                            db.SaveChanges();

                            Uye = db.Musteri.FirstOrDefault(p => p.AdiSoyadi == data.AdiSoyadi.Trim().ToUpper()).MusteriID;
                        }
                        db.Sepet.Add(new Sepet()
                        {
                            SiparisTamamlandimi = true,
                            MusteriID = Uye,
                            KullanicilarID = KullaniciID,
                            UrunSepet = liste,
                            Manuel = true,
                            Aktifmi = true,
                            ToplamAdet= db.SanalSepet.Where(p => p.KullanicilarID == KullaniciID).Sum(P => P.Adet),
                            ToplamFiyat=data.ToplamFiyat,
                            IndirimliFiyat=data.IndirimliFiyat
                        });
                        db.SaveChanges();

                        var bulsepet = db.Sepet.FirstOrDefault(p => p.Aktifmi == true);
                        bool sonuc = SiparisRepo.SiparisKaydet(bulsepet);
                        if (sonuc == true)
                        {
                            bulsepet.Aktifmi = false;
                        }
                        db.SanalSepet.RemoveRange(bul);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool SepetiSilKullanici(int KullaniciID) //Kullanıcı Modunda Manuel Sepeti Sil
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var sil = db.SanalSepet.Where(p => p.KullanicilarID == KullaniciID).ToList();
                    db.SanalSepet.RemoveRange(sil);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}


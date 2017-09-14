using DAL.VM;
using Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
   public class AnalizRepo
    {
        public static VMAnaliz Analiz() //Toplam
        {
            using (PHDB db = new PHDB())
            {
                string gun = DateTime.Now.Day.ToString(), ay = DateTime.Now.Month.ToString(), yil = DateTime.Now.Year.ToString();

                int ToplamUrun = db.Urun.Count();
                int Kullanicilar = db.Kullanicilar.Where(p => p.Admin == false).ToList().Count();
                int Gonderilenurunler = db.Siparis.Where(p => p.Gonderildimi == true && p.İptal == false).Count();
                int Gonderilmeyenurunler = db.Siparis.Where(p => p.Gonderildimi == false && p.İptal == false && p.Onaylandimi == true).Count();
                int IptalEdilen = db.Siparis.Where(p => p.İptal == true && p.Gonderildimi == false).Count();
                int OnayBekleyenler = db.Siparis.Where(p => p.Onaylandimi == false && p.Gonderildimi == false && p.İptal == false).Count();
                double ciroay = 0;
                int urunindex = 0;
                bool kontrol = db.AylikCiro.Any(d => d.Yil == yil && d.Ay == ay);
                if (Gonderilenurunler != 0 && kontrol == true)
                {
                    ciroay = db.AylikCiro.Where(P => P.Yil == yil && P.Ay == ay).Sum(P => P.ToplamSatis);
                    urunindex = db.AylikCiro.Where(p => p.Yil == yil && p.Ay == ay).Sum(p => p.ToplamAdet);
                }
                VMAnaliz Analiz = new VMAnaliz
                {
                    Gönderilen = Gonderilenurunler,
                    Kullanıcılar = Kullanicilar,
                    OnayBekleyen = OnayBekleyenler,
                    ToplamUrun = ToplamUrun,
                    Gonderilmeyen = Gonderilmeyenurunler,
                    Iptal = IptalEdilen,
                    Ciro = ciroay,
                    UrunEndeks = urunindex
                };
                return Analiz;
            }
        }
        public static VMAnaliz AnalizPersonel(int id) //Toplam
        {
            using (PHDB db = new PHDB())
            {
                int Gonderilenurunler = db.Siparis.Where(p => p.Gonderildimi == true && p.İptal == false && p.KullanicilarID==id).Count();
                int Gonderilmeyenurunler = db.Siparis.Where(p => p.Gonderildimi == false && p.İptal == false && p.Onaylandimi == true && p.KullanicilarID==id).Count();
                int IptalEdilen = db.Siparis.Where(p => p.İptal == true && p.Gonderildimi == false && p.KullanicilarID==id).Count();
                int OnayBekleyenler = db.Siparis.Where(p => p.Onaylandimi == false && p.Gonderildimi == false && p.İptal == false && p.KullanicilarID==id).Count();
                VMAnaliz Analiz = new VMAnaliz
                {
                    Gönderilen = Gonderilenurunler,
                    OnayBekleyen = OnayBekleyenler,
                    Gonderilmeyen = Gonderilmeyenurunler,
                    Iptal = IptalEdilen,
                };
                return Analiz;
            }
        }

        public static List<VMKullanici> KullaniciListele() //Kullanıcı Listele
        {
            using (PHDB db = new PHDB())
            {
                var Bul = db.Kullanicilar.Select(t=> new VMKullanici { Admin=t.Admin,KullaniciAdi=t.KullaniciAdi,KullanicilarID=t.KullanicilarID,KullaniciSifre=t.KullaniciSifre}).ToList();
                return Bul;
            }
        }

        public static List<VMGunlukToplam> CiroAylik() //Ciro/Toplam Ürün Aylık Listele
        {
            using (PHDB db = new PHDB())
            {
                return db.AylikCiro.OrderByDescending(p => p.Yil).Select(p => new VMGunlukToplam
                {
                    Ay = p.Ay,
                    Yil = p.Yil,
                    ToplamAdet = p.ToplamAdet,
                    ToplamSatis = p.ToplamSatis
                }).ToList();
            }
        }

        public static List<VMGunlukToplam> CiroGunluk(string yil, string ay) //Ciro gunuk Listele
        {
            using (PHDB db = new PHDB())
            {
                return db.GunlukCiro.Where(p => p.Yil == yil && p.Ay == ay).OrderBy(p => p.Gun).Select(p => new VMGunlukToplam
                {
                    Yil = p.Yil,
                    Ay = p.Ay,
                    Gun = p.Gun,
                    ToplamSatis = p.ToplamSatis
                }).ToList();
            }
        }

        public static List<VMGunlukToplam> ToplamGunluk(string yil, string ay) //Toplam Ürün gunluk Listele
        {
            using (PHDB db = new PHDB())
            {
                return db.GunlukCiro.Where(p => p.Yil == yil && p.Ay == ay).OrderBy(p => p.Gun).Select(p => new VMGunlukToplam
                {
                    Yil = p.Yil,
                    Ay = p.Ay,
                    Gun = p.Gun,
                    ToplamAdet = p.ToplamAdet
                }).ToList();
            }
        }
    }
}

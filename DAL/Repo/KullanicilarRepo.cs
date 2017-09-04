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
    public class KullanicilarRepo
    {
        public static bool KullaniciKaydet(VMKullanici Al) //Kullanıcı Kaydet
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    bool Control = db.Kullanicilar.Any(p => p.KullaniciAdi == Al.KullaniciAdi && p.KullaniciSifre == Al.KullaniciSifre);
                    if (Control != true)
                    {
                        db.Kullanicilar.Add(new Kullanicilar()
                        {
                            KullaniciAdi = Al.KullaniciAdi.Trim(),
                            KullaniciSifre = Al.KullaniciSifre.Trim(),
                            Admin = Al.Admin
                        });
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
        
        public static bool KullaniciGuncelle(VMKullanici Al) //Kullanıcı Güncelle
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Bul = db.Kullanicilar.FirstOrDefault(p => p.KullanicilarID == Al.KullanicilarID);
                    Bul.KullaniciAdi = Al.KullaniciAdi.Trim();
                    Bul.KullaniciSifre = Al.KullaniciSifre.Trim();
                    Bul.Admin = Al.Admin;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        
        public static bool KullaniciSil(int ID) //Kullanıcı Sil
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Bul = db.Kullanicilar.FirstOrDefault(p => p.KullanicilarID == ID && p.KullanicilarID != 1);
                    db.Kullanicilar.Remove(Bul);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool AdminYap(int ID) //Admin Yap
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Bul = db.Kullanicilar.FirstOrDefault(p => p.KullanicilarID == ID && p.Admin != true);
                    Bul.Admin = true;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static List<VMKullanici> Kullanicilar() //Kullanıcı Listele
        {
            using (PHDB db = new PHDB())
            {
                return db.Kullanicilar.Select(p => new VMKullanici
                {
                    KullaniciAdi = p.KullaniciAdi,
                    KullanicilarID = p.KullanicilarID,
                    KullaniciSifre = p.KullaniciSifre,
                    Admin = p.Admin
                }).ToList();
            }
        }
    }
}


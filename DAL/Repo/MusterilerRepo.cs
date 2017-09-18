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
    public class MusterilerRepo
    {
        public static bool UyeKaydet(VMMusteri Al)
        {
            using (PHDB db = new PHDB())
            {
                bool Control = db.Musteri.Any(p => p.MailAdresi == Al.MailAdresi && p.AdiSoyadi==Al.AdiSoyadi);
                if (Control != true)
                {
                    db.Musteri.Add(new Musteri
                    {
                        Adres = Al.Adres.Trim(),
                        MailAdresi = Al.MailAdresi.Trim(),
                        AdiSoyadi = Al.AdiSoyadi.Trim(),
                        Tarih = DateTime.Now.ToShortDateString(),
                        Telefon = Al.Telefon.Trim()
                    });
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool UyeGuncelle(VMMusteri Al)
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Bul = db.Musteri.FirstOrDefault(p => p.MusteriID == Al.MusteriID);

                    Bul.Adres = Al.Adres.Trim();
                    Bul.MailAdresi = Al.MailAdresi.Trim();
                    Bul.AdiSoyadi = Al.AdiSoyadi.Trim();
                    Bul.Telefon = Al.Telefon.Trim();
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool UyeSil(int ID)
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var Bul = db.Musteri.FirstOrDefault(p => p.MusteriID == ID);
                    db.Musteri.Remove(Bul);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static VMMusteri UyeListele(string ID)
        {
            int id = int.Parse(ID);
            using (PHDB db = new PHDB())
            {
                return db.Musteri.Where(p => p.MusteriID == id).Select(p => new VMMusteri
                {
                    Adres = p.Adres,
                    MailAdresi = p.MailAdresi,
                    AdiSoyadi = p.AdiSoyadi,
                    Telefon = p.Telefon,
                    MusteriID = p.MusteriID,
                }).FirstOrDefault();
            }
        }
        public static VMMusteri MusteriAjax(string Name)
        {
            using (PHDB db = new PHDB())
            {
                return db.Musteri.Where(p => p.AdiSoyadi == Name).Select(t => new VMMusteri
                {
                    AdiSoyadi = t.AdiSoyadi,
                    Adres = t.Adres,
                    MailAdresi = t.MailAdresi,
                    Telefon = t.Telefon,
                    Not=t.not
                }).FirstOrDefault();
            }
        }
        public static List<VMMusteri> TumUyeler()
        {
            using (PHDB db = new PHDB())
            {
                return db.Musteri.Select(p => new VMMusteri
                {
                    Adres = p.Adres,
                    MailAdresi = p.MailAdresi,
                    AdiSoyadi = p.AdiSoyadi,
                    Telefon = p.Telefon,
                    MusteriID = p.MusteriID
                }).ToList();
            }
        }
        public static List<string> TumUyelerAjax()
        {
            using (PHDB db = new PHDB())
            {
                return db.Musteri.Select(p => p.AdiSoyadi).ToList();
            }
        }
    }
}

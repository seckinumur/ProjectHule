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
                bool Control = db.Musteri.Any(p => p.MailAdresi == Al.MailAdresi && p.Sifre == Al.Sifre);
                if (Control != true)
                {
                    db.Musteri.Add(new Musteri
                    {
                        Adres = Al.Adres.Trim(),
                        MailAdresi = Al.MailAdresi.Trim(),
                        AdiSoyadi = Al.AdiSoyadi.Trim(),
                        Sifre = Al.Sifre.Trim(),
                        Tarih = DateTime.Now.ToShortDateString(),
                        Telefon = Al.Telefon.Trim(),
                        Il = Al.Il
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
                    Bul.Sifre = Al.Sifre.Trim();
                    Bul.Telefon = Al.Telefon.Trim();
                    Bul.Il = Al.Il;
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
                    Sifre = p.Sifre,
                    Tarih = p.Tarih,
                    Telefon = p.Telefon,
                    MusteriID = p.MusteriID,
                    Il = p.Il
                }).FirstOrDefault();
            }
        }
        public static string UyeIsmi(string ID)
        {
            using (PHDB db = new PHDB())
            {
                int id = int.Parse(ID);
                return db.Musteri.FirstOrDefault(p => p.MusteriID == id).AdiSoyadi;
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
                    Sifre = p.Sifre,
                    Tarih = p.Tarih,
                    Telefon = p.Telefon,
                    MusteriID = p.MusteriID,
                    Il = p.Il
                }).ToList();
            }
        }
    }
}

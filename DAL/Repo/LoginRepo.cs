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
    public class LoginRepo
    {
        public static VMKullanici Login(VMKullanici Data)
        {
            using(PHDB db = new PHDB())
            {
                return db.Kullanicilar.Where(p => p.KullaniciAdi == Data.KullaniciAdi && p.KullaniciSifre == Data.KullaniciSifre).Select(t => new VMKullanici { Admin = t.Admin, KullaniciAdi = t.KullaniciAdi, KullanicilarID = t.KullanicilarID }).FirstOrDefault();
            }
        }
        public static bool Kontrol(VMKullanici Data)
        {
            using (PHDB db = new PHDB())
            {
                return db.Kullanicilar.Any(p => p.KullaniciAdi == Data.KullaniciAdi && p.KullaniciSifre == Data.KullaniciSifre);
            }
        }
    }
}

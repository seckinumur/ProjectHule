using DAL.VM;
using Entity.Context;
using Entity.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class UrunRepo
    {
        public static bool ExcelKaydet(List<Urun> Data)
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    var silurun = db.Urun.ToList();
                    db.Urun.RemoveRange(silurun);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Uruns', RESEED, 0)");
                    db.Urun.AddRange(Data);
                    db.SaveChanges();

                    var UrunData = Data;

                    var silmarka = db.Markalar.ToList();
                    db.Markalar.RemoveRange(silmarka);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Markalars', RESEED, 0)");
                    List<Markalar> eklemarkalar = new List<Markalar>();
                    foreach (var item in UrunData.Select(p => p.Marka).Distinct())
                    {
                        eklemarkalar.Add(new Markalar { Marka = item });
                    }
                    db.Markalar.AddRange(eklemarkalar);

                    var silmodel = db.Modeller.ToList();
                    db.Modeller.RemoveRange(silmodel);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Modellers', RESEED, 0)");
                    List<Modeller> ekleModeller = new List<Modeller>();
                    foreach (var item in UrunData.Select(p => p.Model).Distinct())
                    {
                        ekleModeller.Add(new Modeller { Model = item });
                    }
                    db.Modeller.AddRange(ekleModeller);

                    var silsinifkodlari = db.SinifKodlari.ToList();
                    db.SinifKodlari.RemoveRange(silsinifkodlari);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.SinifKodlaris', RESEED, 0)");
                    List<SinifKodlari> ekleSinifKodlari = new List<SinifKodlari>();
                    foreach (var item in UrunData.Select(p => p.SinifKodu).Distinct())
                    {
                        ekleSinifKodlari.Add(new SinifKodlari { SinifKodu = item });
                    }
                    db.SinifKodlari.AddRange(ekleSinifKodlari);

                    var silsiniftamlari = db.SinifTanimlari.ToList();
                    db.SinifTanimlari.RemoveRange(silsiniftamlari);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.SinifTanimlaris', RESEED, 0)");
                    List<SinifTanimlari> ekleSinifTanimlari = new List<SinifTanimlari>();
                    foreach (var item in UrunData.Select(p => p.SinifTanimi).Distinct())
                    {
                        ekleSinifTanimlari.Add(new SinifTanimlari { SinifTanimi = item });
                    }
                    db.SinifTanimlari.AddRange(ekleSinifTanimlari);

                    var SS1 = db.Section1.ToList();
                    db.Section1.RemoveRange(SS1);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section1', RESEED, 0)");
                    db.Section1.AddRange(UrunData.Select(n => n.Section1).Distinct().Select(p => new Section1 { section1 = p }));
                    db.SaveChanges();

                    var SS2 = db.Section2.ToList();
                    db.Section2.RemoveRange(SS2);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section2', RESEED, 0)");
                    db.Section2.AddRange(UrunData.Select(n => n.Section2).Distinct().Select(p => new Section2 { section2 = p }));
                    db.SaveChanges();

                    var SS3 = db.Section3.ToList();
                    db.Section3.RemoveRange(SS3);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section3', RESEED, 0)");
                    db.Section3.AddRange(UrunData.Select(n => n.Section3).Distinct().Select(p => new Section3 { section3 = p }));
                    db.SaveChanges();

                    var SS4 = db.Section4.ToList();
                    db.Section4.RemoveRange(SS4);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section4', RESEED, 0)");
                    db.Section4.AddRange(UrunData.Select(n => n.Section4).Distinct().Select(p => new Section4 { section4 = p }));
                    db.SaveChanges();

                    var SS5 = db.Section5.ToList();
                    db.Section5.RemoveRange(SS5);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section5', RESEED, 0)");
                    db.Section5.AddRange(UrunData.Select(n => n.Section5).Distinct().Select(p => new Section5 { section5 = p }));
                    db.SaveChanges();

                    var SS6 = db.Section6.ToList();
                    db.Section6.RemoveRange(SS6);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section6', RESEED, 0)");
                    db.Section6.AddRange(UrunData.Select(n => n.Section6).Distinct().Select(p => new Section6 { section6 = p }));
                    db.SaveChanges();

                    var SS7 = db.Section7.ToList();
                    db.Section7.RemoveRange(SS7);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section7', RESEED, 0)");
                    db.Section7.AddRange(UrunData.Select(n => n.Section7).Distinct().Select(p => new Section7 { section7 = p }));
                    db.SaveChanges();

                    var SS8 = db.Section8.ToList();
                    db.Section8.RemoveRange(SS8);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section8', RESEED, 0)");
                    db.Section8.AddRange(UrunData.Select(n => n.Section8).Distinct().Select(p => new Section8 { section8 = p }));
                    db.SaveChanges();

                    var SS9 = db.Section9.ToList();
                    db.Section9.RemoveRange(SS9);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section9', RESEED, 0)");
                    db.Section9.AddRange(UrunData.Select(n => n.Section9).Distinct().Select(p => new Section9 { section9 = p }));
                    db.SaveChanges();

                    var SS10 = db.Section10.ToList();
                    db.Section10.RemoveRange(SS10);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section10', RESEED, 0)");
                    db.Section10.AddRange(UrunData.Select(n => n.Section10).Distinct().Select(p => new Section10 { section10 = p }));
                    db.SaveChanges();

                    var SS11 = db.Section11.ToList();
                    db.Section11.RemoveRange(SS11);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section11', RESEED, 0)");
                    db.Section11.AddRange(UrunData.Select(n => n.Section11).Distinct().Select(p => new Section11 { section11 = p }));
                    db.SaveChanges();

                    var SS12 = db.Section12.ToList();
                    db.Section12.RemoveRange(SS12);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section12', RESEED, 0)");
                    db.Section12.AddRange(UrunData.Select(n => n.Section12).Distinct().Select(p => new Section12 { section12 = p }));
                    db.SaveChanges();

                    var SS13 = db.Section13.ToList();
                    db.Section13.RemoveRange(SS13);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section13', RESEED, 0)");
                    db.Section13.AddRange(UrunData.Select(n => n.Section13).Distinct().Select(p => new Section13 { section13 = p }));
                    db.SaveChanges();

                    var SS14 = db.Section14.ToList();
                    db.Section14.RemoveRange(SS14);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section14', RESEED, 0)");
                    db.Section14.AddRange(UrunData.Select(n => n.Section14).Distinct().Select(p => new Section14 { section14 = p }));
                    db.SaveChanges();

                    var SS15 = db.Section15.ToList();
                    db.Section15.RemoveRange(SS15);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.Section15', RESEED, 0)");
                    db.Section15.AddRange(UrunData.Select(n => n.Section15).Distinct().Select(p => new Section15 { section15 = p }));
                    db.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static List<Urun> Liste()
        {
            using (PHDB db = new PHDB())
            {
                return db.Urun.ToList();
            }
        }
        public static string[] malzemekodubul(string data)
        {
            using (PHDB db = new PHDB())
            {
                string[] Data = new string[20];
                try
                {
                    var Liste = db.Urun.FirstOrDefault(p => p.MalzemeKodu == data);
                    Data[0] = Liste.Marka;
                    Data[1] = Liste.Model;
                    Data[2] = Liste.SinifKodu;
                    Data[3] = Liste.SinifTanimi;
                    Data[4] = Liste.MalzemeKodu;
                    Data[5] = Liste.Section1;
                    Data[6] = Liste.Section2;
                    Data[7] = Liste.Section3;
                    Data[8] = Liste.Section4;
                    Data[9] = Liste.Section5;
                    Data[10] = Liste.Section6;
                    Data[11] = Liste.Section7;
                    Data[12] = Liste.Section8;
                    Data[13] = Liste.Section9;
                    Data[14] = Liste.Section10;
                    Data[15] = Liste.Section11;
                    Data[16] = Liste.Section12;
                    Data[17] = Liste.Section13;
                    Data[18] = Liste.Section14;
                    Data[19] = Liste.Section15;
                    return Data;
                }
                catch
                {
                    return Data;
                }
            }
        }
        public static VMStok StokListe()
        {
            using (PHDB db = new PHDB())
            {
                var silmarka = db.Markalar.ToList();
                var silmodel = db.Modeller.ToList();
                var silsinifkodlari = db.SinifKodlari.ToList();
                var silsiniftamlari = db.SinifTanimlari.ToList();
                return new VMStok { Markalar = silmarka, Modeller = silmodel };
            }
        }
        public static List<string> SinifKoduGet()
        {
            using (PHDB db = new PHDB())
            {
                return db.SinifKodlari.Select(P => P.SinifKodu).ToList();
            }
        }
        public static List<string> SinifTanimiGet()
        {
            using (PHDB db = new PHDB())
            {
                return db.SinifTanimlari.Select(P => P.SinifTanimi).ToList();
            }
        }
        public static VMSection SectionGet()
        {
            using (PHDB db = new PHDB())
            {
                VMSection s = new VMSection();
                s.Section1 = db.Section1.Select(p => p.section1).ToList();
                s.Section2 = db.Section2.Select(p => p.section2).ToList();
                s.Section3 = db.Section3.Select(p => p.section3).ToList();
                s.Section4 = db.Section4.Select(p => p.section4).ToList();
                s.Section5 = db.Section5.Select(p => p.section5).ToList();
                s.Section6 = db.Section6.Select(p => p.section6).ToList();
                s.Section7 = db.Section7.Select(p => p.section7).ToList();
                s.Section8 = db.Section8.Select(p => p.section8).ToList();
                s.Section9 = db.Section9.Select(p => p.section9).ToList();
                s.Section10 = db.Section10.Select(p => p.section10).ToList();
                s.Section11 = db.Section11.Select(p => p.section11).ToList();
                s.Section12 = db.Section12.Select(p => p.section12).ToList();
                s.Section13 = db.Section13.Select(p => p.section13).ToList();
                s.Section14 = db.Section14.Select(p => p.section14).ToList();
                s.Section15 = db.Section15.Select(p => p.section15).ToList();
                return s;
            }
        }
        public static List<VMUrunBulPost> UrunBul(VMUrunBulPost Data)
        {
            using (PHDB db = new PHDB())
            {
                return db.Urun.Where(p => (p.Marka == Data.Marka && p.Model == Data.Model) && (p.SinifKodu == Data.SinifKodu || p.SinifTanimi == Data.SinifTanimi)).Select(e => new VMUrunBulPost
                {
                    MalzemeKodu = e.MalzemeKodu,
                    Marka = e.Marka,
                    Model = e.Model,
                    SinifKodu = e.SinifKodu,
                    SinifTanimi = e.SinifTanimi,
                    Fiyat = db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == e.MalzemeKodu) == null ? 0 : db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == e.MalzemeKodu).Fiyati,
                    Stok = db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == e.MalzemeKodu) == null ? 0 : db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == e.MalzemeKodu).Adedi
                }).ToList();
            }
        }
        public static List<VMUrunBulPost> UrunBulDetay(VMUrunBulPost Data)
        {
            using (PHDB db = new PHDB())
            {
                return db.Urun.Where(p => (p.Marka==Data.Marka && p.Model== Data.Model && p.SinifKodu == Data.SinifKodu && p.SinifTanimi == Data.SinifTanimi) && ( p.Section1==Data.Section1 || p.Section10==Data.Section10 || p.Section11==Data.Section11 || p.Section12==Data.Section12 || p.Section13==Data.Section13 || p.Section14==Data.Section14 || p.Section15== Data.Section15 || p.Section2== Data.Section2 || p.Section3== Data.Section3 || p.Section4== Data.Section4 || p.Section5==Data.Section5 || p.Section6==Data.Section6 || p.Section7==Data.Section7 || p.Section8== Data.Section8 || p.Section9== Data.Section9)).Select(e => new VMUrunBulPost
                {
                    MalzemeKodu = e.MalzemeKodu,
                    Marka = e.Marka,
                    Model = e.Model,
                    SinifKodu = e.SinifKodu,
                    SinifTanimi = e.SinifTanimi,
                    Fiyat = db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == e.MalzemeKodu) == null ? 0 : db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == e.MalzemeKodu).Fiyati,
                    Stok = db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == e.MalzemeKodu) == null ? 0 : db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == e.MalzemeKodu).Adedi
                }).ToList();
            }
        }
        public static bool StokEkleAjax(VMStokEkle data)
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    try
                    {
                        var bul = db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == data.MalzemeKodu);
                        bul.Adedi = data.Stok;
                        bul.Fiyati = data.Fiyati;
                        db.SaveChanges();
                        return true;
                    }
                    catch
                    {
                        db.UrunStok.Add(new UrunStok
                        {
                            Fiyati = data.Fiyati,
                            Adedi = data.Stok,
                            MalzemeKodu=data.MalzemeKodu
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
        public static bool StokEkleExcel(List<VMStokEkle> data)
        {
            using (PHDB db = new PHDB())
            {
                try
                {
                    foreach (var item in data)
                    {
                        try
                        {
                            var bul = db.UrunStok.FirstOrDefault(p => p.MalzemeKodu == item.MalzemeKodu);
                            bul.Adedi = item.Stok;
                            bul.Fiyati = item.Fiyati;
                            db.SaveChanges();
                        }
                        catch
                        {
                            db.UrunStok.Add(new UrunStok { Adedi = item.Stok, Fiyati = item.Fiyati, MalzemeKodu = item.MalzemeKodu });
                            db.SaveChanges();
                        }
                    }
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

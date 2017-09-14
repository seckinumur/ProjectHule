namespace Entity.Context
{
    using Entity.Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PHDB : DbContext
    {
        public PHDB()
            : base("name=PHDB")
        {
            Database.SetInitializer(new PHDBInitializer());
        }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilar { get; set; }
        public virtual DbSet<Sepet> Sepet { get; set; }
        public virtual DbSet<Siparis> Siparis { get; set; }
        public virtual DbSet<UrunSepet> UrunSepet { get; set; }
        public virtual DbSet<SanalSepet> SanalSepet { get; set; }
        public virtual DbSet<AylikCiro> AylikCiro { get; set; }
        public virtual DbSet<GunlukCiro> GunlukCiro { get; set; }
        public virtual DbSet<EnCokSatan> EnCokSatan { get; set; }
        public virtual DbSet<Musteri> Musteri { get; set; }
        public virtual DbSet<Markalar> Markalar { get; set; }
        public virtual DbSet<Modeller> Modeller { get; set; }
        public virtual DbSet<SinifKodlari> SinifKodlari { get; set; }
        public virtual DbSet<SinifTanimlari> SinifTanimlari { get; set; }
        public virtual DbSet<UrunStok> UrunStok { get; set; }
        public virtual DbSet<Section1> Section1 { get; set; }
        public virtual DbSet<Section2> Section2 { get; set; }
        public virtual DbSet<Section3> Section3 { get; set; }
        public virtual DbSet<Section4> Section4 { get; set; }
        public virtual DbSet<Section5> Section5 { get; set; }
        public virtual DbSet<Section6> Section6 { get; set; }
        public virtual DbSet<Section7> Section7 { get; set; }
        public virtual DbSet<Section8> Section8 { get; set; }
        public virtual DbSet<Section9> Section9 { get; set; }
        public virtual DbSet<Section10> Section10 { get; set; }
        public virtual DbSet<Section11> Section11 { get; set; }
        public virtual DbSet<Section12> Section12 { get; set; }
        public virtual DbSet<Section13> Section13 { get; set; }
        public virtual DbSet<Section14> Section14 { get; set; }
        public virtual DbSet<Section15> Section15 { get; set; }
    }
    public class PHDBInitializer : CreateDatabaseIfNotExists<PHDB> //Otomatik database Oluþturma
    {
        protected override void Seed(PHDB db)
        {
            db.Urun.Add(new Urun
            {
                Marka = "Bellona",
                Model = "ALESTA",
                SinifKodu = "20ABK3E",
                SinifTanimi = "B2 ALESTA BYZ KUMAS 3LU KANEPE",
                MalzemeKodu = "20ABK3E00500003",
                Section1 = "00ML-STEFON TRKZ-MOR-MVI-TGRI-YSL-PMB A",
                Section2 = "00JW-ESTA TURKUAZ KOMBIN",
                Section3 = "00JW-ESTA TURKUAZ KOMBIN",
                Section4 = "00JW-ESTA TURKUAZ KOMBIN",
                Section5 = "002C-REAL BEYAZ DERI",
                Section6 = null,
                Section7 = null,
                Section8 = null,
                Section9 = null,
                Section10 = null,
                Section11 = null,
                Section12 = null,
                Section13 = null,
                Section14 = null,
                Section15 = null
            });
            db.SaveChanges();

            db.Kullanicilar.Add(new Kullanicilar { Admin = true, KullaniciAdi = "Admin", KullaniciSifre = "9916" });
            db.SaveChanges();

            db.Kullanicilar.Add(new Kullanicilar { Admin = false, KullaniciAdi = "Demo", KullaniciSifre = "9916" });
            db.SaveChanges();

            //db.UrunStok.Add(new UrunStok { Adedi = 10, Fiyati = 800, MalzemeKodu= "20ABK3E00500003" });
            //db.SaveChanges();
        }
    }
}
using DAL.Repo;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analiz
{
    class Program
    {
        public static bool End;

        public static async void AnaEkran()
        {
            Task<int[]> analiz = new Task<int[]>(Analiz);
            analiz.Start();
            while (End == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine("\n LimonLAB Console Kontrol Sistemi V.0.2 Alpha");
                Console.WriteLine("\r Bu Test Programı Multi Task Özelliğine Sahiptir.\n Şuanda Database Bağlanma Ve Veri Alma İşlemi Başladı.");
                Console.WriteLine("\n 1-) Database Analizi");
                Console.WriteLine("\n 2-) Malzeme Kodu Ara");
                Console.WriteLine("\n 3-) Çıkış");
                Console.Write("\n Seçiminiz : ");
                int Tus;
                if (!int.TryParse(Console.ReadLine(), out Tus))
                {
                    Console.WriteLine("\n Harf Girişi Yapılamaz!");
                    Console.WriteLine("\r Devam Etmek İçin Bir Tuşa Basın...");
                    Console.ReadKey();
                }
                else if (Tus <= 0 || Tus >= 4)
                {
                    Console.WriteLine("\n Yanlış Seçim!");
                    Console.WriteLine("\r Devam Etmek İçin Bir Tuşa Basın...");
                    Console.ReadKey();
                }
                else
                {
                    switch (Tus)
                    {
                        case 1:
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n Database ten Veriler Çekiliyor...");
                                Thread.Sleep(2000);
                                var data = await analiz;
                                AnalizEkran(data);
                                Console.WriteLine("\n Ana Menüye Dönmek İçin Bir Tuşa Basın...");
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n Malzeme Koduna Göre Veri Arama");
                                Console.Write("\r Malzeme Kodunu Gir: ");
                                string word = Console.ReadLine();
                                if(word != null)
                                {
                                    MalzemkoduBulEkran(word);
                                    Console.WriteLine("\n Ana Menüye Dönmek İçin Bir Tuşa Basın...");
                                    Console.ReadKey();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\n Malzeme Kodu Bulunamadı!");
                                    Console.WriteLine("\n Ana Menüye Dönmek İçin Bir Tuşa Basın...");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                        case 3:
                            {
                                End = true;
                                return;
                            }
                    }
                }
            }
        }

        private static int[] Analiz()
        {
            int[] Toplam = new int[20];
            var liste = UrunRepo.Liste();
            var markalar = liste.Select(p => p.Marka).Distinct().ToList();
            var Modeller = liste.Select(p => p.Model).Distinct().ToList();
            var sinifkodlari = liste.Select(p => p.SinifKodu).Distinct().ToList();
            var siniftanimlari = liste.Select(p => p.SinifTanimi).Distinct().ToList();
            var malzemekodlari = liste.Select(p => p.MalzemeKodu).Distinct().ToList();
            var Section1 = liste.Select(p => p.Section1).Distinct().ToList();
            var Section2 = liste.Select(p => p.Section2).Distinct().ToList();
            var Section3 = liste.Select(p => p.Section3).Distinct().ToList();
            var Section4 = liste.Select(p => p.Section4).Distinct().ToList();
            var Section5 = liste.Select(p => p.Section5).Distinct().ToList();
            var Section6 = liste.Select(p => p.Section6).Distinct().ToList();
            var Section7 = liste.Select(p => p.Section7).Distinct().ToList();
            var Section8 = liste.Select(p => p.Section8).Distinct().ToList();
            var Section9 = liste.Select(p => p.Section9).Distinct().ToList();
            var Section10 = liste.Select(p => p.Section10).Distinct().ToList();
            var Section11 = liste.Select(p => p.Section11).Distinct().ToList();
            var Section12 = liste.Select(p => p.Section12).Distinct().ToList();
            var Section13 = liste.Select(p => p.Section13).Distinct().ToList();
            var Section14 = liste.Select(p => p.Section14).Distinct().ToList();
            var Section15 = liste.Select(p => p.Section15).Distinct().ToList();
            Toplam[0] = markalar.Count;
            Toplam[1] = Modeller.Count;
            Toplam[2] = sinifkodlari.Count;
            Toplam[3] = siniftanimlari.Count;
            Toplam[4] = malzemekodlari.Count;
            Toplam[5] = Section1.Count();
            Toplam[6] = Section2.Count();
            Toplam[7] = Section3.Count();
            Toplam[8] = Section4.Count();
            Toplam[9] = Section5.Count();
            Toplam[10] = Section6.Count();
            Toplam[11] = Section7.Count();
            Toplam[12] = Section8.Count();
            Toplam[13] = Section9.Count();
            Toplam[14] = Section10.Count();
            Toplam[15] = Section11.Count();
            Toplam[16] = Section12.Count();
            Toplam[17] = Section13.Count();
            Toplam[18] = Section14.Count();
            Toplam[19] = Section15.Count();
            return Toplam;

        }
        public static void AnalizEkran(int[] Data)
        {
            Console.WriteLine("\nMarkalar : " + Data[0] + "\nModeller : " + Data[1] + "\nSinif Kodları : " + Data[2] + "\nSınıf Tanımları : " + Data[3] + "\nMalzeme Kodları : " + Data[4]);
            Console.WriteLine("\rSection1 " + " : " + Data[5]);
            Console.WriteLine("\rSection2 " + " : " + Data[6]);
            Console.WriteLine("\rSection3 " + " : " + Data[7]);
            Console.WriteLine("\rSection4 " + " : " + Data[8]);
            Console.WriteLine("\rSection5 " + " : " + Data[9]);
            Console.WriteLine("\rSection6 " + " : " + Data[10]);
            Console.WriteLine("\rSection7 " + " : " + Data[11]);
            Console.WriteLine("\rSection8 " + " : " + Data[12]);
            Console.WriteLine("\rSection9 " + " : " + Data[13]);
            Console.WriteLine("\rSection10" + " : " + Data[14]);
            Console.WriteLine("\rSection11" + " : " + Data[15]);
            Console.WriteLine("\rSection12" + " : " + Data[16]);
            Console.WriteLine("\rSection13" + " : " + Data[17]);
            Console.WriteLine("\rSection14" + " : " + Data[18]);
            Console.WriteLine("\rSection15" + " : " + Data[19]);
        }
        public static void MalzemkoduBulEkran(string data)
        {
            var Data = UrunRepo.malzemekodubul(data);
            Console.WriteLine("\nMarkalar : " + Data[0] + "\nModeller : " + Data[1] + "\nSinif Kodları : " + Data[2] + "\nSınıf Tanımları : " + Data[3] + "\nMalzeme Kodları : " + Data[4]);
            Console.WriteLine("\rSection1 " + " : " + Data[5]);
            Console.WriteLine("\rSection2 " + " : " + Data[6]);
            Console.WriteLine("\rSection3 " + " : " + Data[7]);
            Console.WriteLine("\rSection4 " + " : " + Data[8]);
            Console.WriteLine("\rSection5 " + " : " + Data[9]);
            Console.WriteLine("\rSection6 " + " : " + Data[10]);
            Console.WriteLine("\rSection7 " + " : " + Data[11]);
            Console.WriteLine("\rSection8 " + " : " + Data[12]);
            Console.WriteLine("\rSection9 " + " : " + Data[13]);
            Console.WriteLine("\rSection10" + " : " + Data[14]);
            Console.WriteLine("\rSection11" + " : " + Data[15]);
            Console.WriteLine("\rSection12" + " : " + Data[16]);
            Console.WriteLine("\rSection13" + " : " + Data[17]);
            Console.WriteLine("\rSection14" + " : " + Data[18]);
            Console.WriteLine("\rSection15" + " : " + Data[19]);
        }
        static void Main(string[] args)
        {
            AnaEkran();
        }
    }
}

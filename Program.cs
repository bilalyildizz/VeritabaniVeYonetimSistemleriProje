using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeritabanıProjesi
{
    class Program
    {   
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432;" +
               "Database=ProjeDeneme; user ID=postgres; password=fetih1453");
        
      

        public void icMenü(long tc) 
        {
            while (true)
            {
                string secim;
                Console.Write("\n 1- Hesaplarım \n 2- Para Transferi \n 3- Kartlarım \n 4- Ödemeler \n 5- Yatırım  \n 6- Kredi Alın \n 7- Sigorta \n 8- Kişisel Bilgilerim \n 9- Emeklilik \n 10- Çıkış \n" +
                    " \n Lütfen seçiminizi yapın: ");

                secim = Console.ReadLine();

                if (secim == "1") { hesaplarım(tc); }
                if (secim == "2") { paraTransferi(tc);  }
                if (secim == "3") { kartolustur(tc); }
                if (secim == "4") { odemeler(tc); }
                if (secim == "5") { yatirimYap(tc);  }
                if (secim == "6") { krediAlimi(tc); }
                if (secim == "7") { sigortaYap(tc);  }
                if (secim == "8") { kisiselbilgiler(tc);   }
                if (secim == "9") { emeklilik(tc); }
                if (secim == "10") { Environment.Exit(0); }
            }

        } 
        public void sigortaYap(long tc) 
        {
            Console.Write("\n 1- Ev Sigortası \n 2- Araba Sigortası \n 3- Hayat Sigartası \n  Lütfen seçiminizi yapın: ");
            int secim = Convert.ToInt32(Console.ReadLine());

            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("insert into kisiselyatirim(tc,yatirimid) values(@p1,@p2)", baglanti);
            cmd.Parameters.AddWithValue("@p1", tc);
            cmd.Parameters.AddWithValue("@p2", secim);
           
            cmd.ExecuteNonQuery();
            baglanti.Close();
            Console.Clear();
            Console.WriteLine("\n Sigortanız yapıldı. \n ");

        }
        public void yatirimYap(long tc) 
        {
            Console.Write("\n 1- Hisse Alım \n 2- Fon Alım \n 3- Elüs Alım \n  Lütfen seçiminizi yapın: ");
            int secim = Convert.ToInt32(Console.ReadLine());

            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("insert into kisiselyatirim(tc,yatirimid,miktar) values(@p1,@p2,@p3)", baglanti);
            cmd.Parameters.AddWithValue("@p1", tc);
            cmd.Parameters.AddWithValue("@p2", secim);
            cmd.Parameters.AddWithValue("@p3", 1000);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            Console.Clear();
            Console.WriteLine("\n 1000 TL yatırım yaptınız. \n ");

        }
        public void krediAlimi(long tc) 
        {
            Console.Write("\n 1- Dijital Kredi \n 2- Bireysel Kredi\n 3- Toki Kredisi\n 4-Bireysel kredi Gecikme Faiz Tutarı Hesapla \n Lütfen seçiminizi yapın: ");
            int secim = Convert.ToInt32(Console.ReadLine());
            if (secim ==4) 
            { 
                Console.Clear();
                Console.Write( " \n Lütfen bireysel kredi ödemesinin kaç gün geciktiğini yazınız: ");
                int gecikmeSure = Convert.ToInt32(Console.ReadLine());


                baglanti.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from faizhesapla(@p1,@p2,@p3)", baglanti);
                cmd.Parameters.AddWithValue("@p1", tc);
                cmd.Parameters.AddWithValue("@p2", 2);
                cmd.Parameters.AddWithValue("@p3", gecikmeSure);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Console.WriteLine("\n Ödemenize eklenicek faiz tutarı: " + reader.GetInt32(0));

                }
                baglanti.Close();

            }
           
            else
            {
                baglanti.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("insert into kisiselkredi(tc,krediid,miktar) values(@p1,@p2,@p3)", baglanti);
                cmd.Parameters.AddWithValue("@p1", tc);
                cmd.Parameters.AddWithValue("@p2", secim);
                cmd.Parameters.AddWithValue("@p3", 50000);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                Console.Clear();
                Console.WriteLine("\n 50000 TL kredi çektiniz. Para hesabınıza aktarıldı \n ");
            }
        }
        public void odemeler(long tc) 
        {
            Console.Write("\n 1- Fatura Ödemesi\n 2- SGK Ödemesi\n 3- Vergi Ödemesi\n 4- Trafik Ödemesi\n 5- Eğitim Ödemesi\n  \n Lütfen seçiminizi yapın: ");
            int secim = Convert.ToInt32(Console.ReadLine());
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("insert into kisiselodeme(tc,odemeid,miktar) values(@p1,@p2,@p3)", baglanti);
            cmd.Parameters.AddWithValue("@p1", tc);
            cmd.Parameters.AddWithValue("@p2", secim);
            cmd.Parameters.AddWithValue("@p3", 500);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            Console.Clear();
            Console.WriteLine(  "\n 500 TL ödeme yapıldı. Hesabınızı kontrol edebilirsiniz. \n ");
        }
        public void kartolustur(long tc) 
        {
            Console.Clear();
            Console.Write(" 1-Kredi Kartı\n 2-Banka Kartı\n 3-Sanal Kart\n\n Lütfen Oluşturmak istediğiniz kartı seçiniz: ");
            int secim = Convert.ToInt32( Console.ReadLine());

            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("insert into kisiselkart(tc,kartid) values(@p1,@p2)", baglanti);
            cmd.Parameters.AddWithValue("@p1", tc);
            cmd.Parameters.AddWithValue("@p2", secim);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            Console.Clear();
            Console.WriteLine("\n Kartınız oluşturuldu\n ");
        }
        public void hesaplarım(long tc) 
        {
            Console.Clear();
            Console.WriteLine("\n 1-Vadesiz hesap bakiyesi \n 2-Vadeli hesap bakiyesi \n 3-Altın hesap bakiyesi \n 4-Bakiyemin dolar karşılığı \n 5-Hesap aç \n 6-Hesap sil ");
            Console.Write("\n Lütfen seçiminizi yapınız: "); 
            string secim = Console.ReadLine();
            if (secim == "1") 
            { 
                Console.Clear();
                baglanti.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from hesapara(@p1,@p2)", baglanti);
                cmd.Parameters.AddWithValue("@p1", 1);
                cmd.Parameters.AddWithValue("@p2", tc);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\n Hesap Bakiyesi: " +reader.GetInt32(0) + "TL");
                }
                baglanti.Close();

            }
            if (secim == "2") { }
            if (secim == "3") { }
            if (secim == "4") 
            {
                baglanti.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from  dolarhesapla(@p1,@p2)", baglanti);
                cmd.Parameters.AddWithValue("@p1", 1);
                cmd.Parameters.AddWithValue("@p2" ,tc);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\n Dolar Bakiyesi: " + reader.GetFloat(0) + "$");
                }
                baglanti.Close();

            }
            if( secim == "5")
            {
                
                Console.Write("\n 1- Vadeli hesap aç \n 2- Altın hesabı aç \n\nLütfen seçiminizi yapınız: ");
                string sonuc = Console.ReadLine();
                if (sonuc == "1") 
                {
                    Console.Clear();
                    baglanti.Open();
                    NpgsqlCommand ekleme = new NpgsqlCommand("insert into kisiselhesap( tc,hesapid,miktar) values(@p1,@p2,@p3)", baglanti);
                    ekleme.Parameters.AddWithValue("@p1", tc);
                    ekleme.Parameters.AddWithValue("@p2", 2);
                    ekleme.Parameters.AddWithValue("@p3", 0);
                    ekleme.ExecuteNonQuery();
                    baglanti.Close();
                    Console.Clear();
                    Console.WriteLine("\n Vadeli  hesabınız oluşturuldu. Güncel bakiyeniz : 0 TL\n");
                }
                else if (sonuc == "2")
                {
                    Console.Clear();
                    baglanti.Open();
                    NpgsqlCommand ekleme = new NpgsqlCommand("insert into kisiselhesap( tc,hesapid,miktar) values(@p1,@p2,@p3)", baglanti);
                    ekleme.Parameters.AddWithValue("@p1", tc);
                    ekleme.Parameters.AddWithValue("@p2", 3);
                    ekleme.Parameters.AddWithValue("@p3", 0);
                    ekleme.ExecuteNonQuery();
                    baglanti.Close();
                    Console.Clear();
                    Console.WriteLine("\nAltın  hesabınız oluşturuldu. Güncel bakiyeniz : 0 TL\n");
                }
            }
            if (secim == "6") 
            {
                Console.Clear();
                Console.Write("\n 1- Vadeli hesap sil \n 2- Altın hesabı sil \n\nLütfen seçiminizi yapınız: ");
                string sonuc = Console.ReadLine();
                if (sonuc == "1")
                {
                    Console.Clear();
                    baglanti.Open();
                    NpgsqlCommand ekleme = new NpgsqlCommand("Delete from kisiselhesap where tc=@p1 AND hesapid=@p2", baglanti);
                    ekleme.Parameters.AddWithValue("@p1", tc);
                    ekleme.Parameters.AddWithValue("@p2", 2);
                    ekleme.ExecuteNonQuery();
                    baglanti.Close();
                    Console.WriteLine("\n Vadeli hesabınız silindi\n");
                }
                else if (sonuc == "2")
                {
                    Console.Clear();
                    baglanti.Open();
                    NpgsqlCommand ekleme = new NpgsqlCommand("Delete from kisiselhesap where tc=@p1 AND hesapid=@p2", baglanti);
                    ekleme.Parameters.AddWithValue("@p1", tc);
                    ekleme.Parameters.AddWithValue("@p2", 3);
                    ekleme.ExecuteNonQuery();
                    baglanti.Close();
                    Console.WriteLine("\n Altın hesabınız silindi\n");
                }

            }

        }
        public void emeklilik(long tc) 
        {
            Console.Write("\n Emekliliği onaylıyor musunuz?(e,h)");
            string secim = Console.ReadLine();
            if (secim == "e") 
            {
                Console.Clear();
                baglanti.Open();
                string evet = "Onaylı";

                NpgsqlCommand cmd = new NpgsqlCommand("update kullanici set emekli=@p2  where tc=@p1", baglanti);

                cmd.Parameters.AddWithValue("@p1", tc);
                cmd.Parameters.AddWithValue("@p2", evet);
                cmd.ExecuteNonQuery();
                

                Console.WriteLine("\n Emekliliğiniz onaylandı");
                baglanti.Close();

            }

        }
        public void paraTransferi(long tc) 
        {
            baglanti.Open();


            Console.Clear();
            Console.Write("Alıcı hesap TC: ");
            long aliciHesap = Convert.ToInt64(Console.ReadLine());
            Console.Write("Gönderilecek Miktar: ");
            int miktar = Convert.ToInt32(Console.ReadLine());
            NpgsqlCommand ekleme = new NpgsqlCommand("insert into paratransfer( miktar,alicikullanicitc,gonderenkullanicitc) values(@p2,@p3,@p4)", baglanti);
            
            ekleme.Parameters.AddWithValue("@p2", miktar);
            ekleme.Parameters.AddWithValue("@p3", aliciHesap);
            ekleme.Parameters.AddWithValue("@p4", tc);
            ekleme.ExecuteNonQuery();

            Console.WriteLine("\n Gönderim Başarılı...\n");

            string kullanicisorgu = "SELECT * FROM kisiselhesap";
            NpgsqlCommand kullanicicommand = new NpgsqlCommand(kullanicisorgu, baglanti);
            NpgsqlDataReader reader = kullanicicommand.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetInt64(0) == tc )
                {
                    Console.WriteLine("Hesabınızda Kalan Bakiye: " + reader.GetInt32(2)+ "\n");

                }
            }
            baglanti.Close();

        }
        public void girisMenu() 
        {

            long tc;
            int sifre;
            String secenek;
            Console.Write(" BMH Bankasına Hoşgeldiniz... ");
            while (true) 
            {

             Console.Write(" \n\n1- Kullanıcı Girişi \n2- Kayıt Ol \n\nLütfen Seçeneklerden Birini Seçiniz: " );
            secenek = Console.ReadLine();

            if (secenek == "1")
            {
                baglanti.Open();
                 string kullanicisorgu = "SELECT * FROM kullanici";
                NpgsqlCommand kullanicicommand = new NpgsqlCommand(kullanicisorgu, baglanti);
                NpgsqlDataReader reader = kullanicicommand.ExecuteReader();





                    while (true)
                    {

                    Console.Write("TC (Lütfen 11 haneli giriniz): ");
                    tc = Convert.ToInt64(Console.ReadLine());
                    Console.Write("Şifre (Lütfen 4 adet rakam giriniz): ");
                    sifre = Convert.ToInt32(Console.ReadLine());

                    while (reader.Read()) {
                        if (reader.GetInt64(0) == tc && reader.GetInt32(1) == sifre)
                        {
                                baglanti.Close();

                            icMenü(tc);

                        }
                    }
                      
                       Console.WriteLine("Tc veya Şifre Hatalı");

                    }

            }

            else 
            {
                kullaniciEkle();
                    baglanti.Close();
            }
            }
           
        }
        public void kisiselbilgiler(long kimlik) 
        {
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("Select * from kisiselbilgiler(\'"+kimlik+"\')", baglanti);            
            NpgsqlDataReader reader = cmd.ExecuteReader();
            Console.Clear();
            while (reader.Read())
            {
                Console.WriteLine("Kisisel Bilgileriniz \n");
                Console.WriteLine(" TC: " + reader.GetInt64(0));
                Console.WriteLine(" Sifre: " + reader.GetInt32(1));
                Console.WriteLine(" Ad: " + reader.GetString(2));
                Console.WriteLine(" Soyad: " + reader.GetString(3));
                Console.WriteLine(" Tel No: " + reader.GetString(4));
                Console.WriteLine(" E-posta: " + reader.GetString(5));
                Console.WriteLine(" Meslek: " + reader.GetString(6)+"\n\n");
               
            }
            baglanti.Close();
        }
        public void kullaniciEkle()
        {
            baglanti.Open();

           Console.WriteLine("Lütfen Gerekli Bilgileri Giriniz...");
            Console.Write(" TC(11 rakamdan oluşmalı): ");
            long tc = Convert.ToInt64(Console.ReadLine());
            Console.Write(" Şifre(4 rakamdan oluşmalı): ");
            int sifre = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Ad: ");
            String ad = Console.ReadLine();
            Console.Write(" Soyad: ");
            String soyad = (Console.ReadLine());
            Console.Write(" Telefon No: ");
            String telNo =Console.ReadLine();
            Console.Write(" E-posta: ");
            String eposta =Console.ReadLine();
            Console.Write(" Meslek: ");
            String meslek = Console.ReadLine();



            NpgsqlCommand ekleme = new NpgsqlCommand("insert into kullanici( tc,sifre,ad,soyad,telefon,eposta,meslek) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
            ekleme.Parameters.AddWithValue("@p1", tc);
            ekleme.Parameters.AddWithValue("@p2", sifre);
            ekleme.Parameters.AddWithValue("@p3", ad);
            ekleme.Parameters.AddWithValue("@p4", soyad);
            ekleme.Parameters.AddWithValue("@p5", telNo);
            ekleme.Parameters.AddWithValue("@p6", eposta);
            ekleme.Parameters.AddWithValue("@p7", meslek);
            ekleme.ExecuteNonQuery();

            Console.WriteLine(" \nKayıt Başarılı \n");
            Console.WriteLine("Otomatik Vadesiz Hesabınız Oluşturuldu \n");
        }
        static void Main(string[] args)

        {

            Program program = new Program();
            program.girisMenu();



        }
    }
}

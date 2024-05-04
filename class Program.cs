using System;

class Program
{
    static void Main(string[] args)
    {
        Garaz g1 = new Garaz();
        g1.Adres = "ul. Testowa 123";
        g1.Pojemnosc = 3;

        Samochod s1 = new Samochod("Fiat", "126p", 2, 650, 6.0);
        Samochod s2 = new Samochod("Syrena", "105", 2, 800, 7.6);
        Samochod s3 = new Samochod("Polonez", "Caro", 4, 1500, 8.5);

        g1.WprowadzSamochod(s1);
        g1.WprowadzSamochod(s2);
        g1.WprowadzSamochod(s3);

        g1.WypiszInfo();

        Console.WriteLine("Wyprowadzono samochód: ");
        Samochod wyprowadzonySamochod = g1.WyprowadzSamochod();
        wyprowadzonySamochod.WypiszInfo();

        Console.ReadKey();
    }
}

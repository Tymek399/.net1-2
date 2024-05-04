using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Wybierz opcje:");
        Console.WriteLine("1. Zapisz dane w pliku");
        Console.WriteLine("2. Odczytaj dane z pliku");

        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 1:
                ZapiszDaneDoPliku();
                break;
            case 2:
                OdczytajDaneZPliku();
                break;
            default:
                Console.WriteLine("Nieprawidłowy wybor.");
                break;
        }
    }

    static void ZapiszDaneDoPliku()
    {
        Console.WriteLine("Podaj imię:");
        string imie = Console.ReadLine();
        Console.WriteLine("Podaj wiek:");
        int wiek = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj adres:");
        string adres = Console.ReadLine();

        using (FileStream fileStream = new FileStream("dane.bin", FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fileStream))
        {
            writer.Write(imie);
            writer.Write(wiek);
            writer.Write(adres);
        }

        Console.WriteLine("Dane zostały zapisane do pliku.");
    }

    static void OdczytajDaneZPliku()
    {
        if (File.Exists("dane.bin"))
        {
            using (FileStream fileStream = new FileStream("dane.bin", FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                string imie = reader.ReadString();
                int wiek = reader.ReadInt32();
                string adres = reader.ReadString();

                Console.WriteLine("Imię: " + imie);
                Console.WriteLine("Wiek: " + wiek);
                Console.WriteLine("Adres: " + adres);
            }
        }
        else
        {
            Console.WriteLine("Taki plik nie istnieje.");
        }
    }
}

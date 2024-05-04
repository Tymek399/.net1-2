using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ZarzadzanieZd
{
    public class Zadanie
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public DateTime DataZakonczenia { get; set; }
        public bool CzyWykonane { get; set; }

        public Zadanie() { }

        public Zadanie(int id, string nazwa, string opis, DateTime dataZakonczenia, bool czyWykonane)
        {
            Id = id;
            Nazwa = nazwa;
            Opis = opis;
            DataZakonczenia = dataZakonczenia;
            CzyWykonane = czyWykonane;
        }
    }

    public class ManagerZadan
    {
        private List<Zadanie> listaZadan;

        public ManagerZadan()
        {
            listaZadan = new List<Zadanie>();
        }

        public void DodajZadanie(Zadanie zadanie)
        {
            listaZadan.Add(zadanie);
        }

        public void UsunZadanie(int id)
        {
            listaZadan.RemoveAll(z => z.Id == id);
        }

        public void WyswietlZadania()
        {
            foreach (var zadanie in listaZadan)
            {
                Console.WriteLine($"Id: {zadanie.Id}, Nazwa: {zadanie.Nazwa}, Opis: {zadanie.Opis}, DataZakonczenia: {zadanie.DataZakonczenia}, CzyWykonane: {zadanie.CzyWykonane}");
            }
        }

        public void ZapiszDoPliku(string sciezka)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Zadanie>));
            using (TextWriter writer = new StreamWriter(sciezka))
            {
                serializer.Serialize(writer, listaZadan);
            }
        }

        public void WczytajZPliku(string sciezka)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Zadanie>));
            using (TextReader reader = new StreamReader(sciezka))
            {
                listaZadan = (List<Zadanie>)serializer.Deserialize(reader);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ManagerZadan manager = new ManagerZadan();

            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj zadanie");
                Console.WriteLine("2. Usuń zadanie");
                Console.WriteLine("3. Wyświetl zadania");
                Console.WriteLine("4. Zapisz zadania do pliku");
                Console.WriteLine("5. Wczytaj zadania z pliku");
                Console.WriteLine("6. Wyjdź");

                string opcja = Console.ReadLine();

                switch (opcja)
                {
                    case "1":
                        Console.WriteLine("Podaj nazwe zadania:");
                        string nazwa = Console.ReadLine();
                        Console.WriteLine("Podaj opis zadania:");
                        string opis = Console.ReadLine();
                        Console.WriteLine("Podaj date zakończenia zadania np 1998-12-11:");
                        DateTime dataZakonczenia = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Czy zadanie jest wykonane? (T/N)");
                        bool czyWykonane = Console.ReadLine().ToUpper() == "T";
                        Zadanie noweZadanie = new Zadanie(manager.listaZadan.Count + 1, nazwa, opis, dataZakonczenia, czyWykonane);
                        manager.DodajZadanie(noweZadanie);
                        break;
                    case "2":
                        Console.WriteLine("Podaj Id zadania do usunięcia:");
                        int idUsun = int.Parse(Console.ReadLine());
                        manager.UsunZadanie(idUsun);
                        break;
                    case "3":
                        Console.WriteLine("Lista zadań:");
                        manager.WyswietlZadania();
                        break;
                    case "4":
                        Console.WriteLine("Podaj nazwę pliku do zapisu:");
                        string plikZapis = Console.ReadLine();
                        manager.ZapiszDoPliku(plikZapis);
                        Console.WriteLine("Zadania zostały zapisane do pliku.");
                        break;
                    case "5":
                        Console.WriteLine("Podaj nazwę pliku do odczytu:");
                        string plikOdczyt = Console.ReadLine();
                        manager.WczytajZPliku(plikOdczyt);
                        Console.WriteLine("Zadania zostały wczytane z pliku.");
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Wybierz ponownie.");
                        break;
                }
            }
        }
    }
}

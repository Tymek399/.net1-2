using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Ścieżka do pliku tekstowego
        string filePath = "sciezka/do/pliku.txt";

        // Sprawdzenie, czy plik istnieje
        if (File.Exists(filePath))
        {
            // Otwarcie pliku za pomocą FileStream
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Utworzenie czytnika StreamReader
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    // Odczytanie zawartości pliku i wyświetlenie na konsoli
                    string content = reader.ReadToEnd();
                    Console.WriteLine("Zawartość pliku:");
                    Console.WriteLine(content);
                }
            }
        }
        else
        {
            Console.WriteLine("Plik nie istnieje.");
        }
    }
}

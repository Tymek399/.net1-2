using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "sciezka/do/pliku.txt";

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Odwrócenie kolejności znaków w linii i wyświetlenie na konsoli
                    char[] charArray = line.ToCharArray();
                    Array.Reverse(charArray);
                    Console.WriteLine(new string(charArray));
                }
            }
        }
        else
        {
            Console.WriteLine("Plik nie istnieje.");
        }
    }
}

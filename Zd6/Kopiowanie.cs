using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        KopiujPlik("plik_wejsciowy.txt", "plik_wyjsciowy.txt");
        Console.WriteLine("Kopiowanie zakonczone.");
    }

    static void KopiujPlik(string sciezkaWejsciowa, string sciezkaWyjsciowa)
    {
        using (FileStream inputStream = new FileStream(sciezkaWejsciowa, FileMode.Open))
        using (FileStream outputStream = new FileStream(sciezkaWyjsciowa, FileMode.Create))
        {
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
            }
        }
    }
}

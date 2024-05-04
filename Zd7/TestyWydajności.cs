using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Generowanie plików testowych o rozmiarze 300MB
        Console.WriteLine("Generowanie plików testowych...");
        string plikWejsciowy = "plik_wejsciowy.bin";
        string plikWyjsciowy = "plik_wyjsciowy.bin";
        GenerujPlikTestowy(plikWejsciowy, 300);

        // Pomiar czasu kopiowania z użyciem FileStream
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        KopiujPlik(plikWejsciowy, plikWyjsciowy);
        stopwatch.Stop();
        Console.WriteLine($"Czas kopiowania z użyciem FileStream: {stopwatch.Elapsed}");

        // Pomiar czasu kopiowania z użyciem metody File
        stopwatch.Restart();
        File.Copy(plikWejsciowy, plikWyjsciowy);
        stopwatch.Stop();
        Console.WriteLine($"Czas kopiowania z użyciem metody File: {stopwatch.Elapsed}");
    }

    static void GenerujPlikTestowy(string nazwaPliku, int rozmiarMB)
    {
        using (FileStream fileStream = new FileStream(nazwaPliku, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fileStream))
        {
            byte[] buffer = new byte[1024];
            Random random = new Random();
            for (int i = 0; i < rozmiarMB * 1024; i++)
            {
                random.NextBytes(buffer);
                writer.Write(buffer);
            }
        }
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

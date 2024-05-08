class Program
{
    static void Main()
    {
        int option = -1;

        while (option != 0)
        {
            Console.Clear();

            Console.WriteLine("Tugas Kriptografi");
            Console.WriteLine("XOR Encryption Decryption");
            Console.WriteLine("Marzuki Ahmad Syarif (20200801352)");
            Console.WriteLine("");
            Console.WriteLine("=== Menu ===");
            Console.WriteLine("1. Enkrip Teks File");
            Console.WriteLine("2. Enkrip Teks Input");
            Console.WriteLine("3. Dekrip Teks");
            Console.WriteLine("0. Keluar (Exit Program)");
            Console.WriteLine("");
            Console.Write("Pilihan: ");
            try
            {
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Enkrip(true);
                        break;

                    case 2:
                        Enkrip(false);
                        break;

                    case 3:
                        Dekrip();
                        break;
                }
            }
            catch { continue; }
        }
    }

    static void Enkrip(bool byFile)
    {
        Console.Clear();

        Console.WriteLine("Enkripsi");
        Console.WriteLine("");

        string rawText;

        if (byFile)
        {
            rawText = GetFileRaw("plain.txt");

            Console.WriteLine("[plain.txt]");
            Console.WriteLine(rawText);
        }
        else
        {
            Console.Write("Masukan teks untuk di enkripsi: ");
            rawText = Console.ReadLine();
        }

        Console.WriteLine("");

        Console.Write("Kata Kunci Enkripsi: ");
        var kataKunci = Console.ReadLine();

        string encryptedText = "";
        for (int i = 0, j = 0; i < rawText.Length; i++)
        {
            encryptedText += ((char)(rawText[i] ^ (uint)kataKunci[j])).ToString();

            j++; if (j > kataKunci.Length - 1) j = 0;
        }

        Console.WriteLine("");
        Console.WriteLine("Hasil Enkripsi: ");
        Console.WriteLine(encryptedText);
        Console.WriteLine("");

        SaveFile("cipher.txt", encryptedText);
        Console.WriteLine("Hasil Enkripsi disimpan pada file cipher.txt");

        Console.ReadLine();
    }

    static void Dekrip()
    {
        Console.Clear();

        Console.WriteLine("Dekripsi");
        Console.WriteLine("");

        //Console.WriteLine("Masukan path file yang akan di enkrip");
        //Console.Write("File: ");
        //var filePath = Console.ReadLine();

        Console.WriteLine("[cipher.txt]");

        var rawText = GetFileRaw("cipher.txt");

        Console.WriteLine(rawText);
        Console.WriteLine("");

        Console.Write("Kata Kunci: ");
        var kataKunci = Console.ReadLine();

        string decryptedText = "";
        for (int i = 0, j = 0; i < rawText.Length; i++)
        {
            decryptedText += ((char)(rawText[i] ^ (uint)kataKunci[j])).ToString();

            j++; if (j > kataKunci.Length - 1) j = 0;
        }

        Console.WriteLine("");
        Console.WriteLine("Hasil Deskripsi: ");
        Console.WriteLine(decryptedText);

        Console.WriteLine("");
        SaveFile("decrypted.txt", decryptedText);
        Console.WriteLine("Hasil Deskripsi disimpan pada file decrypted.txt");

        Console.ReadLine();
    }

    static string GetFileRaw(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.Clear();
            Console.WriteLine($"File {filePath} tidak ditemukan");
            Console.WriteLine("Tekan apa saja untuk melanjutkan program");
            Console.ReadLine();
            throw new FileLoadException("File Not Found");
        }

        return File.ReadAllText(filePath, Encoding.ASCII);
    }

    static void SaveFile(string filePath, string text)
    {
        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
        using (var sw = new StreamWriter(fs, Encoding.ASCII))
            sw.Write(text);
    }
}

using System;
using System.IO;
using KrasilnikovaAlina_CatFramework;

class Program
{
    static Random random = new Random();

    static Cat[] GenerateRandomCats(uint count)
    {
        Cat[] cats = new Cat[count];
        for (int i = 0; i < count; i++)
        {
            try
            {
                if (random.Next(2) == 0)
                {
                    double weight = random.Next(50, 161);
                    int fluffiness = random.Next(-20, 121);
                    cats[i] = new Tiger(weight, fluffiness);
                }
                else
                {
                    int fluffiness = random.Next(-20, 121);
                    cats[i] = new CuteCat(fluffiness);
                }
            }
            catch (CatException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                i--;
            }
        }
        return cats;
    }

    static void DisplayCatInfo(Cat[] catsArr, string path)
    {
        using (StreamWriter writer = File.CreateText(path))
        {
            foreach (var cat in catsArr)
            {
                Console.WriteLine($"Fluffiness Check: {cat.FluffinessCheck()}");
                Console.WriteLine(cat);
                Console.WriteLine();

                writer.WriteLine($"Fluffiness Check: {cat.FluffinessCheck()}");
                writer.WriteLine(cat);
                writer.WriteLine();
            }
        }
    }

    static void Main(string[] args)
    {
        Console.Write("Enter the number of cats to generate: ");
        uint catCount;
        if (uint.TryParse(Console.ReadLine(), out catCount))
        {
            Cat[] cats = GenerateRandomCats(catCount);

            Console.Write("Enter the path to save cat info: ");
            string path = Console.ReadLine();

            DisplayCatInfo(cats, path);
            Console.WriteLine("Cat info has been saved.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

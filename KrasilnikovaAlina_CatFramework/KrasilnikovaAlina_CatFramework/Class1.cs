namespace KrasilnikovaAlina_CatFramework;

using System;

public class CatException : ArgumentException
{
    public CatException(string message) : base(message)
    {
    }
}

public abstract class Cat
{
    public abstract int Fluffiness { get; }

    public abstract string FluffinessCheck();

    public override string ToString()
    {
        return $"A cat with fluffiness: {Fluffiness}";
    }
}

public class Tiger : Cat
{
    private double weight;
    private int fluffiness;

    public override int Fluffiness
    {
        get { return fluffiness; }
    }

    public double Weight
    {
        get { return weight; }
    }

    public Tiger(double weight = 50, int fluffiness = 50)
    {
        if ((weight < 75.0 || weight > 140.0) & (fluffiness < 0 || fluffiness > 100))
            throw new CatException($"Unable to create a tiger with weight: {weight} and fluffiness: {fluffiness}");

        if (fluffiness < 0 || fluffiness > 100)
            throw new CatException($"Unable to create a tiger with fluffiness: {fluffiness}");
        
        if (weight < 75.0 || weight > 140.0)
            throw new CatException($"Unable to create a tiger with weight: {weight}");

        this.weight = weight;
    }

    public override string FluffinessCheck()
    {
        return "Kycь!";
    }

    public override string ToString()
    {
        return $"A tiger with weight: {Weight} fluffiness: {Fluffiness}";
    }
}

public class CuteCat : Cat
{
    private int fluffiness;

    public override int Fluffiness
    {
        get { return fluffiness; }
    }

    public CuteCat(int fluffiness = 50)
    {
        if (fluffiness < 0 || fluffiness > 140)
            throw new CatException($"Unable to create a cute cat with fluffiness: {fluffiness}");

        this.fluffiness = fluffiness;
    }

    public override string FluffinessCheck()
    {
        if (fluffiness == 0)
            return "Sphynx";
        else if (fluffiness <= 20)
            return "Slightly";
        else if (fluffiness <= 50)
            return "Medium";
        else if (fluffiness <= 75)
            return "Heavy";
        else
            return "OwO";
    }

    public override string ToString()
    {
        return $"A cute cat with fluffiness: {Fluffiness}";
    }
}
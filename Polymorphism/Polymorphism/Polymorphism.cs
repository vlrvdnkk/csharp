namespace Polymorphism
{
    abstract class Property
    {
        protected double worth;

        public Property(double worth)
        {
            this.worth = worth;
        }

        public virtual double CalculateTax()
        {
            return 0;
        }
    }

    class Immovable : Property
    {
        protected double area;

        public Immovable(double worth, double area) : base(worth)
        {
            this.area = area;
        }

        public double CostPerSquareMeter()
        {
            return worth / area;
        }

        public override double CalculateTax()
        {
            if (area < 100)
                return 1 / 500.0 * worth;
            else if (area >= 100 && area < 300)
                return 1 / 350.0 * worth;
            else
                return 1 / 250.0 * worth;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: Стоимость - {worth}, налог - {CalculateTax()}, площадь - {area} кв.м";
        }
    }

    class Vehicle : Property
    {
        protected double engineVolume;

        public Vehicle(double worth, double engineVolume) : base(worth)
        {
            this.engineVolume = engineVolume;
        }

        public override double CalculateTax()
        {
            return worth * engineVolume / 3000.0;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: Стоимость - {worth}, налог - {CalculateTax()}, объем двигателя - {engineVolume} см.куб";
        }
    }


    class Appartment : Immovable
    {
        public Appartment(double worth, double area) : base(worth, area)
        {
        }
    }

    class CountryHouse : Immovable
    {
        public CountryHouse(double worth, double area) : base(worth, area)
        {
        }
    }

    class Car : Vehicle
    {
        public Car(double worth, double engineVolume) : base(worth, engineVolume)
        {
        }
    }

    class Boat : Vehicle
    {
        public Boat(double worth, double engineVolume) : base(worth, engineVolume)
        {
        }
    }

    class Polymorphism
    {
        static void Main()
        {
            Property[] properties = new Property[10];

            properties[0] = new Appartment(500000, 80);
            properties[1] = new Appartment(700000, 120);
            properties[2] = new Appartment(1000000, 250);

            properties[3] = new Car(30000, 1500);
            properties[4] = new Car(40000, 1800);
            properties[5] = new Car(60000, 2200);

            properties[6] = new Boat(20000, 950);
            properties[7] = new Boat(25000, 1200);

            properties[8] = new CountryHouse(1200000, 400);
            properties[9] = new CountryHouse(1800000, 600);

            foreach (var property in properties)
            {
                Console.WriteLine(property.ToString());
            }
        }
    }
}
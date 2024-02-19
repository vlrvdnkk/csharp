using GeneralizedClass;

namespace DefaultValues
{
    internal class DefaultValues
    {
        class GenericClass<T>
        {
            private T value;
            public T Value 
            { 
                get { return value; } 
                set { this.value = value; } 
            }

            public GenericClass(T value)
            {
                this.value = value;
            }

            public void Reset()
            {
                value = default(T);
            }
        }

        class Program
        {
            static void Main()
            {
                GenericClass<int> intInstance = new GenericClass<int>(42);
                Console.WriteLine($"Значение intInstance.Value: {intInstance.Value}");

                Book<Guid> book = new Book<Guid> { Id = Guid.NewGuid(), Name = "Недоросль", Author = "Денис Фонвизин", PagesCount = 100 };
                GenericClass<Book<Guid>> bookInstance = new GenericClass<Book<Guid>>(book);
                Console.WriteLine($"Значение bookInstance.Value: {bookInstance.Value}");

                intInstance.Reset();
                bookInstance.Reset();

                Console.WriteLine($"Значение intInstance.Value после вызова Reset: {intInstance.Value}");
                Console.WriteLine($"Значение bookInstance.Value после вызова Reset: {bookInstance.Value}");
            }
        }
    }
}
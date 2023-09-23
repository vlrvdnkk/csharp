using System;

namespace динамический_массив
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        public class IntArrayList
        {
            private int[] _buffer; // Закрытый массив чисел типа int - буфер для хранения чисел в массиве.
            private int _count; // Текущее хранимое количество элементов.
            private int _capacity; // Реальный текущий размер буфера.

            // Закрытое поле только для чтения, обозначающее размер буфера по умолчанию, равно 2;
            private readonly int _defaultCapacity = 2;

            // Свойство только для чтения Count, возвращающее размер.
            public int Count => _count;

            // Свойство только для чтения Capacity, возвращающее реальный размер буфера.
            public int Capacity => _capacity;

            public IntArrayList() // Конструктор без параметров, создающий буфер размера по умолчанию.
            {
                _buffer = new int[_defaultCapacity];
                _capacity = _defaultCapacity;
            }

            public IntArrayList(int initialCapacity) // Конструктор с 1 параметром типа int, явно задающим размер создаваемого буфера.
            {
                if (initialCapacity < 0)
                    throw new ArgumentException("Размер буфера не может быть меньше нуля");

                _buffer = new int[initialCapacity];
                _capacity = initialCapacity;
            }

            public int this[int index] // Открытый индексатор, позволяющий записать/считать значение буфера по индексу без проверки диапазона.
            {
                get
                {
                    return _buffer[index];
                }
                set
                {
                    _buffer[index] = value;
                }
            }

            public void PushBack(int value) // Метод void PushBack(int value), добавляющий число в конец списка.
            {
                if (_count == _capacity)
                {
                    // Если буфер оказывается слишком мал, чтобы добавить в него элемент, пересоздаем буфер в 2 раза большего размера.
                    int newCapacity = _capacity * 2;
                    Array.Resize(ref _buffer, newCapacity);
                    _capacity = newCapacity;
                }

                _buffer[_count] = value;
                _count++;
            }

            public void PopBack() // Метод void PopBack(), удаляющий последний элемент из буфера.
            {
                if (_count > 0)
                {
                    _count--;
                    _buffer[_count] = 0; //Удаляю через обнуление
                }
            }

            public bool TryInsert(int index, int value) // Метод bool TryInsert(int index, int value), вставляет значение value на позицию index.
            {
                if (index < 0 || index > _count)
                    return false;

                if (index == _capacity)
                {
                    int newCapacity = _capacity * 2;
                    Array.Resize(ref _buffer, newCapacity);
                    _capacity = newCapacity;
                }

                for (int i = _count; i > index; i--) // Сдвигаем элементы вправо, чтобы освободить место для нового элемента.
                {
                    _buffer[i] = _buffer[i - 1];
                }

                _buffer[index] = value;
                _count++;
                return true;
            }

            public bool TryErase(int index) // Метод bool TryErase(int index), удаляющий из массива элемент с указанным индексом.
            {
                if (index < 0 || index >= _count)
                    return false;

                for (int i = index; i < _count - 1; i++) // Сдвигаем элементы влево, чтобы удалить элемент.
                {
                    _buffer[i] = _buffer[i + 1];
                }

                _count--;
                _buffer[_count] = 0; // Опять обнуляю последний элемент.

                return true;
            }

            public bool TryGetAt(int index, out int result) // Метод bool TryGetAt(int index, out int result), пытающийся получить значение по индексу.
            {
                if (index < 0 || index >= _count)
                {
                    result = 0;
                    return false;
                }

                result = _buffer[index];
                return true;
            }

            public void Clear() // Открытый метод void Clear(), обнуляющий количество хранимых элементов буфера.
            {
                _count = 0;
            }

            public bool TryForceCapacity(int newCapacity) // Открытый метод bool TryForceCapacity(int newCapacity), явно изменяющий размер буфера на заданное значение.
            {
                if (newCapacity < 0)
                    return false;

                if (newCapacity < _count)
                    return false;

                Array.Resize(ref _buffer, newCapacity);
                _capacity = newCapacity;
                return true;
            }

            public int Find(int value) // Открытый метод int Find(int value), который выполняет поиск индекса первого элемента, равного value.
            {
                for (int i = 0; i < _count; i++)
                {
                    if (_buffer[i] == value)
                        return i;
                }

                return -1; // Если такого элемента нет, метод возвращает -1.
            }
        }
    }
}
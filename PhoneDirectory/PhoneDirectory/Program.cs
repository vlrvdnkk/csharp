namespace PhoneDirectory
{
    internal class Program
    {
        static void Main()
        {
            PhoneBook phoneBook = new PhoneBook();
            string filePath = "contacts.txt"; // Имя файла для сохранения/загрузки

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить контакт");
                Console.WriteLine("2. Удалить контакт");
                Console.WriteLine("3. Найти контакт по имени");
                Console.WriteLine("4. Вывести все контакты");
                Console.WriteLine("5. Сохранить в файл");
                Console.WriteLine("6. Загрузить из файла");
                Console.WriteLine("7. Выход");

                try
                {
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Введите имя: ");
                                string name = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(name))
                                    throw new ArgumentException("Имя не может быть пустым");
                                Console.Write("Введите телефонный номер: ");
                                string phoneNumber = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(phoneNumber))
                                    throw new ArgumentException("Номер не может быть пустым");
                                Contact newContact = new Contact(name, phoneNumber);
                                phoneBook.AddContact(newContact);
                                break;

                            case 2:
                                Console.Write("Введите Id контакта для удаления: ");
                                if (Guid.TryParse(Console.ReadLine(), out Guid idToRemove))
                                    phoneBook.RemoveContact(idToRemove);
                                else
                                    throw new Exception("Некорректный ввод Id контакта.");
                                break;

                            case 3:
                                Console.Write("Введите имя для поиска: ");
                                string searchName = Console.ReadLine();
                                phoneBook.SearchByName(searchName);
                                break;

                            case 4:
                                phoneBook.DisplayAllContacts();
                                break;

                            case 5:
                                phoneBook.SaveToFile(filePath);
                                break;

                            case 6:
                                phoneBook.LoadFromFile(filePath);
                                break;

                            case 7:
                                Environment.Exit(0);
                                break;

                            default:
                                throw new Exception("Некорректный ввод. Пожалуйста, выберите действие от 1 до 7.");
                        }
                    }
                    else
                    {
                        throw new Exception("Некорректный ввод. Пожалуйста, выберите действие от 1 до 7.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
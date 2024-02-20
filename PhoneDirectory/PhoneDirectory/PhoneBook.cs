namespace PhoneDirectory
{
    public class PhoneBook
    {
        private List<Contact> contacts;

        public PhoneBook()
        {
            contacts = new List<Contact>();
        }

        public void AddContact(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact), "Контакт не может быть null.");
            else
                contacts.Add(contact);
        }

        public void RemoveContact(Guid id)
        {
            Contact contactToRemove = contacts.FirstOrDefault(c => c.Id == id);
            if (contactToRemove != null)
                contacts.Remove(contactToRemove);
            else
                throw new Exception("Контакт не найден.");
        }

        public void SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя для поиска не может быть пустым.", nameof(name));

            var matchingContacts = contacts
                .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchingContacts.Count > 0)
            {
                Console.WriteLine("Найденные контакты:");
                foreach (var contact in matchingContacts)
                {
                    Console.WriteLine($"Id: {contact.Id}, Имя: {contact.Name}, Телефон: {contact.PhoneNumber}");
                }
            }
            else
            {
                throw new Exception("Контакты не найдены.");
            }
        }

        public void DisplayAllContacts()
        {
            if (contacts.Count > 0)
            {
                Console.WriteLine("Все контакты:");
                foreach (var contact in contacts)
                {
                    Console.WriteLine($"Id: {contact.Id}, Имя: {contact.Name}, Телефон: {contact.PhoneNumber}");
                }
            }
            else
            {
                Console.WriteLine("Контактов нет.");
            }
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var contact in contacts)
                    {
                        writer.WriteLine($"{contact.Id},{contact.Name},{contact.PhoneNumber}");
                    }
                }
                Console.WriteLine("Данные сохранены в файл.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при сохранении в файл: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    contacts.Clear();
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var parts = line.Split(',');
                            if (parts.Length == 3 && Guid.TryParse(parts[0], out Guid id))
                            {
                                var contact = new Contact(parts[1], parts[2]);
                                contacts.Add(contact);
                            }
                        }
                    }
                    Console.WriteLine("Данные загружены из файла.");
                }
                else
                {
                    throw new Exception("Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке из файла: {ex.Message}");
            }
        }
    }
}
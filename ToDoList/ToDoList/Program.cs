namespace ToDoListApp
    {
        public class TaskItem
        {
            public string Description { get; set; }
            public string Priority { get; set; }

            public TaskItem(string description, string priority)
            {
                Description = description;
                Priority = priority;
            }

            public override string ToString()
            {
                return $"[{Priority}] {Description}";
            }
        }

        public class TaskManager
        {
            private List<TaskItem> tasks = new List<TaskItem>();
            private const string FileName = "tasks.txt";

            public void LoadTasks()
            {
                if (File.Exists(FileName))
                {
                    var lines = File.ReadAllLines(FileName);
                    foreach (var line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            tasks.Add(new TaskItem(parts[0], parts[1]));
                        }
                    }
                }
            }

            public void SaveTasks()
            {
                var lines = tasks.Select(task => $"{task.Description}|{task.Priority}");
                File.WriteAllLines(FileName, lines);
            }

            public void AddTask(string description, string priority)
            {
                tasks.Add(new TaskItem(description, priority));
            }

            public void DeleteTask(int index)
            {
                if (index >= 0 && index < tasks.Count)
                {
                    tasks.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("Неверный номер задачи.");
                }
            }

            public void ShowTasks()
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("Список задач пуст.");
                    return;
                }

                Console.WriteLine("Список задач:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
            }

            public void FilterTasksByPriority(string priority)
            {
                var filteredTasks = tasks.Where(t => t.Priority == priority).ToList();
                if (filteredTasks.Count == 0)
                {
                    Console.WriteLine("Задачи с выбранным приоритетом не найдены.");
                    return;
                }

                Console.WriteLine("Задачи с выбранным приоритетом:");
                foreach (var task in filteredTasks)
                {
                    Console.WriteLine(task);
                }
            }

            public void SearchTasks(string keyword)
            {
                var foundTasks = tasks.Where(t => t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                if (foundTasks.Count == 0)
                {
                    Console.WriteLine("Задачи с таким ключевым словом не найдены.");
                    return;
                }

                Console.WriteLine("Найденные задачи:");
                foreach (var task in foundTasks)
                {
                    Console.WriteLine(task);
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                var taskManager = new TaskManager();
                taskManager.LoadTasks();

                bool exit = false;

                var actions = new Dictionary<string, Action>
            {
                { "1", () =>
                    {
                        Console.Write("Введите описание задачи: ");
                        string description = Console.ReadLine();
                        Console.Write("Укажите приоритет (1 - Высокий, 2 - Средний, 3 - Низкий): ");
                        string priorityChoice = Console.ReadLine();

                        string priority = priorityChoice switch
                        {
                            "1" => "Высокий",
                            "2" => "Средний",
                            "3" => "Низкий",
                            _ => "Низкий"
                        };

                        taskManager.AddTask(description, priority);
                    }
                },
                { "2", () =>
                    {
                        taskManager.ShowTasks();
                        Console.Write("Введите номер задачи для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int taskNumber))
                        {
                            taskManager.DeleteTask(taskNumber - 1);
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод.");
                        }
                    }
                },
                { "3", taskManager.ShowTasks },
                { "4", () =>
                    {
                        Console.Write("Введите приоритет для фильтрации (1 - Высокий, 2 - Средний, 3 - Низкий): ");
                        string filterPriorityChoice = Console.ReadLine();
                        string filterPriority = filterPriorityChoice switch
                        {
                            "1" => "Высокий",
                            "2" => "Средний",
                            "3" => "Низкий",
                            _ => "Низкий"
                        };

                        taskManager.FilterTasksByPriority(filterPriority);
                    }
                },
                { "5", () =>
                    {
                        Console.Write("Введите ключевое слово для поиска: ");
                        string keyword = Console.ReadLine();
                        taskManager.SearchTasks(keyword);
                    }
                },
                { "6", () =>
                    {
                        taskManager.SaveTasks();
                        exit = true;
                    }
                }
            };

                while (!exit)
                {
                    Console.WriteLine("\nВыберите действие:");
                    Console.WriteLine("1. Добавить задачу");
                    Console.WriteLine("2. Удалить задачу");
                    Console.WriteLine("3. Просмотреть список задач");
                    Console.WriteLine("4. Фильтрация задач по приоритету");
                    Console.WriteLine("5. Поиск задач по ключевому слову");
                    Console.WriteLine("6. Выйти и сохранить");

                    Console.Write("Введите номер действия: ");
                    string choice = Console.ReadLine();

                    if (actions.TryGetValue(choice, out var action))
                    {
                        action();
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор.");
                    }
                }
            }
        }
}
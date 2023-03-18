using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FProject
{
    

    class AdminMethod
    {
        User user = new User();
        public string FlightNumber { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Airline { get; set; }
        public string Terminal { get; set; }
        public string Status { get; set; }
        public string Gate { get; set; }
        public int TotalOccupiedSeats { get; set; }
        public int Money { get; set; }
        public int SeatsLeft
        {
            get { return 24 - TotalOccupiedSeats; }
        }
        

        public static List<AdminMethod> Flights = new List<AdminMethod>(); // список рейсов
        // Поля
        // Свойства
        // Остальные свойства и методы класса


        public AdminMethod(string flightNumber, DateTime date, string city, string airline, string terminal, string status, string gate, int totalOccupiedSeats, int money)
        {
            FlightNumber = flightNumber;
            Date = date;
            City = city;
            Airline = airline;
            Terminal = terminal;
            Status = status;
            Gate = gate;
            TotalOccupiedSeats = totalOccupiedSeats;
            Money = money;
        }
        public AdminMethod()
        {
            
        }
        public  void ViewFlightsByCity()//поиск по городу
        {
            Console.Write("Введите город: ");
            string city = Console.ReadLine();

            // Ищем все рейсы, у которых город соответствует заданному
            var flightsByCity = Flights.Where(x => x.City == city).OrderBy(x => x.FlightNumber).ToList();

            if (flightsByCity.Count == 0)
            {
                Console.WriteLine($"Рейсы в город {city} не найдены.");
                return;
            }
            
            

            // Выводим список рейсов
            for (int i = 0; i < flightsByCity.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {flightsByCity[i].FlightNumber}");
            }

            Console.Write("Введите номер рейса, чтобы просмотреть информацию (или введите 00, чтобы вернуться к главному меню): ");
            string input = Console.ReadLine();

            if (input == "00")
            {
                // возврат к общему меню
                return;
            }
            int index = int.Parse(input) - 1;

            if (index < 0 || index >= flightsByCity.Count)
            {
                Console.WriteLine("Неверный номер рейса.");
                return;
            }

            if (index < 0 || index >= flightsByCity.Count)
            {
                Console.WriteLine("Неверный номер рейса.");
                return;
            }

            AdminMethod flight = flightsByCity[index];

            Console.WriteLine($"Информация о рейсе {flight.FlightNumber}:");
            Console.WriteLine($"Город: {flight.City}");
            Console.WriteLine($"Дата: {flight.Date.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Авиалиния: {flight.Airline}");
            Console.WriteLine($"Терминал: {flight.Terminal}");
            Console.WriteLine($"Статус: {flight.Status}");
            Console.WriteLine($"Гейт: {flight.Gate}");
            Console.WriteLine($"Занятые места: {flight.TotalOccupiedSeats}");
            Console.WriteLine($"Стоимость: {flight.Money}$");


            Console.WriteLine("Хотите купить билет на этот рейс? (Да/Нет)");
            string answer = Console.ReadLine();

            if (answer.ToLower() == "да")
            {
                ByTicket(flight);
            }
        }
        public void ByTicket(AdminMethod flight)//покупка билета
        {
            if (flight.SeatsLeft <= 0)
            {
                Console.WriteLine("Места на этот рейс закончились.");
                return;
            }

            Console.Write("Введите количество билетов: ");
            int count;
            if (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Неверное количество билетов.");
                return;
            }

            if (count > flight.SeatsLeft)
            {
                Console.WriteLine("На рейсе нет столько свободных мест.");
                return;
            }

            Console.Write("Выберите класс (business/economy): ");
            string travelClass = Console.ReadLine().ToLower();
            double coefficient = 1.0;
            int Price = (int)(coefficient * Money);

            while (true)
            {
                if (travelClass == "business")
                {
                    coefficient = 1.7;
                    break;
                }
                else if (travelClass == "economy")
                {
                    coefficient = 1.3;
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный класс перелета.");
                    Console.Write("Выберите класс (business/economy): ");
                    travelClass = Console.ReadLine().ToLower();
                }
            }

            Price = (int)(coefficient * flight.Money);
            Console.WriteLine($"Стоимость билетов: {Price * count}");
            Console.Write("Хотите купить билеты? (Да/Нет) ");
            string answer2 = Console.ReadLine();

            if (answer2.ToLower() == "да")
            {
                Console.WriteLine("Оплатите, прикладывайте карту");
                Thread.Sleep(7000);
                Console.WriteLine("Оплата прошла, приятного полёта");
                int TotalPrice = (int)(coefficient * flight.Money * count);
                flight.TotalOccupiedSeats += count;
                Thread.Sleep(700);
                Console.WriteLine($"Успешно куплено {count} билетов на рейс {flight.FlightNumber}. Осталось мест: {flight.SeatsLeft}");
                Thread.Sleep(700);
                Console.WriteLine($"Стоимость билетов: {TotalPrice}");
                Console.ReadLine();
            }
        }
        public void ViewAllFlights()//вывод всех рейсов
        {
            if (Flights.Count == 0)// проверка, есть ли зарегистрированные рейсы
            {
                Console.WriteLine("Рейсы не зарегистрированы.");
                return;
            }

            Console.WriteLine("Все рейсы:");
            for (int i = 0; i < Flights.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Flights[i].FlightNumber} - {Flights[i].City} - {Flights[i].Date} - {Flights[i].Status}");
            }

            while (true)
            {
                Console.Write("Введите номер рейса, который хотите просмотреть (или 00 для выхода):");
                string input = Console.ReadLine();

                if (input == "00")
                {
                    return;
                }

                if (int.TryParse(input, out int selectedFlight))
                {
                    if (selectedFlight > 0 && selectedFlight <= Flights.Count)
                    {
                        Console.WriteLine($"Информация о рейсе {Flights[selectedFlight - 1].FlightNumber}:");
                        Console.WriteLine($"Город: {Flights[selectedFlight - 1].City}");
                        Console.WriteLine($"Дата: {Flights[selectedFlight - 1].Date}");
                        Console.WriteLine($"Авиалиния: {Flights[selectedFlight - 1].Airline}");
                        Console.WriteLine($"Терминал: {Flights[selectedFlight - 1].Terminal}");
                        Console.WriteLine($"Статус: {Flights[selectedFlight - 1].Status}");
                        Console.WriteLine($"Гейт: {Flights[selectedFlight - 1].Gate}");
                        Console.WriteLine($"Занятые места: {Flights[selectedFlight - 1].TotalOccupiedSeats}");
                        Console.WriteLine($"Стоимость: {Flights[selectedFlight - 1].Money}$");
                        // вывод остальной информации о рейсе
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор.");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный Ввод. Пожалуйста, введите правильный номер или 00 для выхода.");
                }
            }
        }
        public void AddNewFlight()//добавление рейса
        {
            Console.WriteLine("Введите информацию о рейсе: ");

            string flightNumber;
            do
            {
            Console.Write("Номер рейса: ");
            flightNumber = Console.ReadLine();
                if (string.IsNullOrEmpty(flightNumber))
                {
                    Console.WriteLine("Имя является обязательным полем. Пожалуйста, введите Имя.");
                }
            } while (string.IsNullOrEmpty(flightNumber));

            Console.WriteLine("Введите дату и время: (формат: dd/MM/yyyy HH:mm\nПример: 16/02/2003 16:00)");
            DateTime date;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Console.WriteLine("Ошибка ввода даты и времени, попробуйте еще раз.");
            }

            
            string Places;
            do
            {
            Console.Write("Places:");
                Places = Console.ReadLine();
                if (string.IsNullOrEmpty(Places))
                {
                    Console.WriteLine("Имя является обязательным полем. Пожалуйста, введите Имя.");
                }
            } while (string.IsNullOrEmpty(Places));

            string city;
            do
            {
                Console.Write("Город:");
                city = Console.ReadLine();
                if (string.IsNullOrEmpty(city))
                {
                    Console.WriteLine("Город является обязательным для заполнения. Пожалуйста, введите значение.");
                }
            } while (string.IsNullOrEmpty(city));


            string airline;
            do
            {
                Console.Write("Авиалиния:");
                airline = Console.ReadLine();
                if (string.IsNullOrEmpty(airline))
                {
                    Console.WriteLine("Авиакомпания является обязательным полем. Пожалуйста, введите значение.");
                }
            } while (string.IsNullOrEmpty(airline));

            string terminal;
            do
            {
                Console.Write("Терминал:");
                terminal = Console.ReadLine();
                if (string.IsNullOrEmpty(terminal))
                {
                    Console.WriteLine("Терминал - обязательное поле. Пожалуйста, введите значение.");
                }
            } while (string.IsNullOrEmpty(terminal));

            string status = "Положение состояния";
            do
            {
                Console.Write("Status: 1 - Задерживается, 2 - Уже прибыл: ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Статус є обов’язковим для заповнення. Будь ласка, введіть значення.");
                }
                else if (!(input == "1" || input == "2"))
                {
                    Console.WriteLine("Недійсне введення. Введіть \"1\" або \"2\".");
                }
                else
                {
                    status = input == "1" ? "Задерживается" : "Уже прибыл";
                }
            } while (string.IsNullOrEmpty(status));


            string gate;
            do
            {
                Console.Write("Gate:");
                gate = Console.ReadLine();
                if (string.IsNullOrEmpty(gate))
                {
                    Console.WriteLine("Gate - обязательное поле. Пожалуйста, введите значение..");
                }
            } while (string.IsNullOrEmpty(gate));
            Console.Write("Занятые места:");
            int totalOccupiedSeats;
            do
            {
                Console.Write("Введите количество всего занятых мест, всего 24 места:");
                if (!int.TryParse(Console.ReadLine(), out totalOccupiedSeats) || totalOccupiedSeats < 0 || totalOccupiedSeats > 24)
                {
                    Console.WriteLine("Общее количество занятых мест должно быть числом от 0 до 24.");
                }
            } while (totalOccupiedSeats < 0 || totalOccupiedSeats > 24);
            int money;
            do
            {
                Console.Write("Стоимость билета:");
                if (!int.TryParse(Console.ReadLine(), out money) || money == 0)
                {
                    Console.WriteLine("Деньги - обязательное поле. Пожалуйста, введите действительный номер.");
                }
            } while (money == 0);

            // Создаем новый объект рейса с переданными параметрами
            AdminMethod newFlight = new AdminMethod(flightNumber, date, city, airline, terminal, status, gate, totalOccupiedSeats,money);

            // Добавляем новый объект рейса в список рейсов
            Flights.Add(newFlight);
        }
        public void DeleteFlight()//удаление рейса
        {
            if (Flights.Count == 0)
            {
                Console.WriteLine("Нет доступных рейсов для удаления.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Выберите рейс для удаления:");
            int index = 1;
            foreach (var flight in Flights)
            {
                Console.WriteLine($"{index}. {flight.FlightNumber} - {flight.City} - {flight.Date} - {flight.Status}");
                index++;
            }

            Console.Write("Введите номер рейса для удаления: ");
            int flightNumber;
            while (!int.TryParse(Console.ReadLine(), out flightNumber) || flightNumber < 1 || flightNumber > Flights.Count)
            {
                Console.WriteLine($"Неверный Ввод. Пожалуйста, введите число от 1 до {Flights.Count}.");
                Console.Write("Введите номер рейса для удаления: ");
            }

            Console.Write($"Вы действительно хотите удалить рейс {Flights[flightNumber - 1].FlightNumber}? (Y/N): ");
            string confirmation = Console.ReadLine();
            if (confirmation.ToUpper() == "Y")
            {
                Flights.RemoveAt(flightNumber - 1);
                Console.WriteLine($"Номер рейса {flightNumber} был удален.");
            }
            else
            {
                Console.WriteLine($"Удаление рейса {Flights[flightNumber - 1].FlightNumber} отменено.");
            }

            Console.ReadLine();
        }

        public void EditFlight()//изменение рейса 
        {
            if (Flights.Count == 0)
            {
                Console.WriteLine("Нет доступных рейсов для редактирования.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Выберите рейс для редактирования:");
            int index = 1;
            foreach (var flight in Flights)
            {
                Console.WriteLine($"{index}. {flight.FlightNumber} - {flight.City} - {flight.Date} - {flight.Status}");
                index++;
            }

            Console.Write("Введите номер рейса для редактирования: ");
            int flightNumber;
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "00")
                {
                    Console.WriteLine("Выход из метода.");
                    return;
                }
                else if (int.TryParse(input, out flightNumber) && flightNumber >= 1 && flightNumber <= Flights.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный ввод. Пожалуйста, введите число от 1 до {Flights.Count} (или 00 для выхода).");
                    Console.Write("Введите номер рейса для редактирования: ");
                }
            }

            var flightToEdit = Flights[flightNumber - 1];

            Console.WriteLine($"Редактирование полета {flightToEdit.FlightNumber}:");
            Console.WriteLine($"1. Город: {flightToEdit.City}");
            Console.WriteLine($"2. Дата: {flightToEdit.Date}");
            Console.WriteLine($"3. Авиалиния : {flightToEdit.Airline}");
            Console.WriteLine($"4. Терминал: {flightToEdit.Terminal}");
            Console.WriteLine($"5. Статус: {flightToEdit.Status}");
            Console.WriteLine($"6. Гейт: {flightToEdit.Gate}");
            Console.WriteLine($"7 Всего занятых мест: {flightToEdit.TotalOccupiedSeats}");
            Console.WriteLine($"8. Цена билета: {flightToEdit.Money}");

            Console.Write("Введите номер поля для редактирования: ");
            int fieldNumber;
            while (!int.TryParse(Console.ReadLine(), out fieldNumber) || fieldNumber < 1 || fieldNumber > 8)
            {
                Console.WriteLine("Неверный Ввод. Пожалуйста, введите число от 1 до 8.");
                Console.Write("Введите номер поля для редактирования: ");
            }

            Console.Write("Введите новое значение: ");
            string newValue = Console.ReadLine();

            switch (fieldNumber)
            {
                case 1:
                    flightToEdit.City = newValue;
                    break;
                case 2:
                    flightToEdit.Date = DateTime.Parse(newValue);
                    break;
                case 3:
                    flightToEdit.Airline = newValue;
                    break;
                case 4:
                    flightToEdit.Terminal = newValue;
                    break;
                case 5:
                    string status = "";
                    do
                    {
                        Console.Write("Status: 1 - Задерживается, 2 - Уже прибыл: ");
                        var input = Console.ReadLine();
                        if (string.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Статус - обязательное поле. Пожалуйста, введите значение.");
                        }
                        else if (!(input == "1" || input == "2"))
                        {
                            Console.WriteLine("Неверный ввод. Пожалуйста, введите либо \"1\", либо \"2\".");
                        }
                        else
                        {
                            status = input == "1" ? "Задерживается" : "Уже прибыл";
                        }
                    } while (string.IsNullOrEmpty(status));

                    flightToEdit.Status = status;
                    Console.WriteLine($"Статус рейса изменен на {status}.");
                    break;
                case 6:
                    flightToEdit.Gate = newValue;
                    break;
                case 7:
                    flightToEdit.TotalOccupiedSeats = int.Parse(newValue);
                    break;
                case 8:
                    flightToEdit.Money = Convert.ToInt32(decimal.Parse(newValue));
                    break;
            }

            Console.WriteLine($"Номер рейса {flightToEdit.FlightNumber} был обновлен.");
            Console.ReadLine();
        }
        public void EmergencyInformation()//Экстренная информация
        {
            Console.WriteLine($"В случае чрезвычайной ситуации, пожалуйста, следуйте указателям ближайшего выхода и слушайте указания бортпроводников.");
        }
        public void FindFlightByNumber()//Найти по номеру рейса
        {
            Console.Write("Введите номер рейса: ");
            int flightNumber;
            while (!int.TryParse(Console.ReadLine(), out flightNumber))
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите номер рейса.");
                Console.Write("Введите номер рейса: ");
            }

            var flight = Flights.FirstOrDefault(f => f.FlightNumber.ToString() == flightNumber.ToString());
            if (flight != null)
            {
                Console.WriteLine($"{flight.FlightNumber} - {flight.City} - {flight.Date} - {flight.Status}");

                Console.WriteLine($"Информация о рейсе {flight.FlightNumber}:");
                Console.WriteLine($"Город: {flight.City}");
                Console.WriteLine($"Дата: {flight.Date}");
                Console.WriteLine($"Авиалиния: {flight.Airline}");
                Console.WriteLine($"Терминал: {flight.Terminal}");
                Console.WriteLine($"Статус: {flight.Status}");
                Console.WriteLine($"Гейт: {flight.Gate}");
                Console.WriteLine($"Занятые места: {flight.TotalOccupiedSeats}");
                Console.WriteLine($"Стоимость: {flight.Money}$");

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Рейс с номером {flightNumber} не найден.");
                Console.ReadLine();
            }
        }




























    }
}

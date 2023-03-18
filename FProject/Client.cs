using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FProject
{
    public class Client
    {
        public void Client_User(string login,string password)
        {
            UserManager user = new UserManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Добрый день {login}");
                Console.WriteLine("1.Просмотреть информацию рейчах по городу");
                Console.WriteLine("2.Купить билет");
                Console.WriteLine("3.Просмотр информации о рейсах");
                Console.WriteLine("4. Найти по номеру рейса");
                Console.WriteLine("5.Экстренная информация");
                Console.WriteLine("6. Выход");

                Console.Write("\nВведите свой выбор: ");
                int choice;
                bool validInput = false;
                do
                {
                    Console.Write("Введите свой выбор (1-9): ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out choice) && choice >= 1 && choice <= 6)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Пожалуйста, введите число от 1 до 6");
                    }
                } while (!validInput);

                AdminMethod flight = new AdminMethod();

                Console.WriteLine("Если вы ошиблись при выборе напишите \"exit\" ");
                string a = Console.ReadLine();
                switch (choice)
                {
                    case 1:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                            flight.ViewFlightsByCity();
                        break;

                    case 2:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                            flight.ViewFlightsByCity();
                        break;

                    case 3:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                        flight.ViewAllFlights();
                            break;
                    case 4:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                        flight.FindFlightByNumber();
                        break;

                    case 5:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                        flight.EmergencyInformation();
                        break;
                    
                    case 6:
                        Console.WriteLine("Exiting...");
                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                Console.ReadKey();
            }
        }
    }
}

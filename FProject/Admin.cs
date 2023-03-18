using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FProject
{
    public class Admin
    {
        public enum MenuChoice
        {
            AddNewFlight = 1,
            ViewFlightsByCity,
            ShowAllUsers,
            ViewAllFlights,
            DeleteFlight,
            EditFlight,
            FindFlightByNumber,
            EmergencyInformation,
            Exit
        }

        public void Admin_User()
        {

            User user = new User();
            AdminMethod flight = new AdminMethod();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добрый день Администратор)");
                Console.WriteLine("1.Добавить новый рейс");
                Console.WriteLine("2.Просмотреть информацию рейчах по городу");
                Console.WriteLine("3.Вывести весь список ак");
                Console.WriteLine("4.Показать все рейсы");
                Console.WriteLine("5.Удалить рейс");
                Console.WriteLine("6.Изминение рейса");
                Console.WriteLine("7.Найти по номеру рейса");
                Console.WriteLine("8.Экстренная информация");
                Console.WriteLine("9.Выход");

                Console.Write("\nВведите свой выбор: ");
                int choice;
                bool validInput = false;
                do
                {
                    Console.Write("Введите свой выбор (1-9): ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out choice) && choice >= 1 && choice <= 9)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Пожалуйста, введите число от 1 до 9");
                    }
                } while (!validInput);

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
                            flight.AddNewFlight();
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
                            user.ShowAllUsers();
                        break;
                    case 4:
                        if (a == "exit")
                        {
                            break;
                        }
                        flight.ViewAllFlights();
                        break;


                    case 5:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                            flight.DeleteFlight();
                            break;

                    case 6:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                        flight.EditFlight();
                            break;

                    case 7:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                        flight.FindFlightByNumber();
                            break;
                    case 8:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                        flight.EmergencyInformation();
                            break;

                    case 9:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                        Console.WriteLine("Exiting...");
                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}

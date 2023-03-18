using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace FProject
{
    internal class Program
    {
        public interface IFlightManagement
        {
            void AddNewFlight();
            void DeleteFlight();
            void ViewAllFlights();
            void EditFlight();
            void FindFlightByNumber();
        }

        static void Main(string[] args)
        {

            string connectionString = @"Data Source=DESKTOP-540U1OH;Database=Airport;Integrated Security=True";



            AdminMethod adminMethod = new AdminMethod();
            UserManager user_2 = new UserManager();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM AirUser", connection))
            {
                // Открытие соединения
                connection.Open();

                // Создание листа
                List<string> users = new List<string>();

                // Исполнение команды и получение объекта SqlDataReader
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Чтение строк из объекта SqlDataReader и добавление их в лист
                    while (reader.Read())
                    {
                        users.Add(reader.GetString(1));
                    }
                }

                // Вывод списка в консоль
                foreach (string name in users)
                {
                    Console.WriteLine(name);
                }
                connection.Close();
            }

            


            do
            {
            User user = new User();
                
                Console.WriteLine($"1.Войти в аккаунт\n" +
                                  $"2.Создать аккаунт\n" +
                                  $"3.Я администратор\n" +
                                  $"4.Выход");

                Console.Write("\nВведите свой выбор: ");
                int choice;
                bool validInput = false;
                do
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out choice) && choice >= 1 && choice <= 5)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Пожалуйста, введите число от 1 до 9.");
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
                        user.UserAuthentication();
                        break;
                    case 2:if (a == "exit")
                        {
                        break;
                        }
                        else
                        user.AddUser();
                        User.ShowUsersFromDatabase();
                        break;
                    case 3:
                        if (a == "exit")
                        {
                            break;
                        }
                        else
                        user.UserAdmin();
                        break;
                    case 4:
                        return; // выход из программы
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }
            } while (true);

            


                
                 
        }
    }
}

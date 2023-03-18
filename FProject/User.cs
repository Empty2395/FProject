using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FProject
{
    public class UserManager
    {
        public static List<User> users = new List<User>();

        public static void AddUser(User user)
        {
            users.Add(user);
        }

        public static bool AuthenticateUser(string login, string password)
        {
            
            return users.Exists(user => user.Login == login && user.Password == password);
        }

        public static void ShowAllUsers()
        {
            Console.Clear();
            if (users.Count == 0)
            {
                Console.WriteLine("Пользователи не найдены.");
                return;
            }

            users = users.OrderBy(u => u.Login).ToList(); // Сортируем пользователей по логину

            for (int i = 0; i < users.Count; i++) // Используем цикл for для вывода порядкового номера каждого пользователя
            {
                Console.WriteLine($"{i + 1}. Логин: {users[i].Login}");
            }

            Console.WriteLine("Введите номер пользователя, чтобы узнать подробную информацию или нажмите Enter, чтобы вернуться к запросам");

            string input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                if (int.TryParse(input, out int index))
                {
                    if (index > 0 && index <= users.Count)
                    {
                        User user = users[index - 1];
                        Console.WriteLine($"Логин: {user.Login}");
                        Console.WriteLine($"Пароль: {user.Password}");
                        Console.WriteLine($"Имя: {user.Name}");
                        Console.WriteLine($"Фамилия: {user.SurName}");
                        Console.WriteLine($"Отчество: {user.LastName}");
                        Console.WriteLine($"Номер паспорта: {user.Passport_ID}");
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                }
            }

            Console.ReadLine();
            Console.Clear();
        }
    }



    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Passport_ID { get; set; }


            Admin admin = new Admin();



        public User(string login,string password, string name, string surName, string lastName, string passport_ID)
        {
            this.Login = login;
            this.Password = password;
            this.Name = name;
            this.SurName = surName;
            this.LastName = lastName;
            this.Passport_ID = passport_ID;
        }
        public User()
        {
        }
        

        public void UserAdmin()
        {
            Console.Clear();
            User adminUser = new User("admin","admin","","","","");
            UserManager.AddUser(adminUser);
            // здесь надо использовать статический метод AuthenticateUser
            Console.WriteLine("Введите логин для авторизации: ");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль для авторизации: ");
            string password = Console.ReadLine();

            // здесь надо использовать статический метод AuthenticateUser
            bool isAuthenticated = UserManager.AuthenticateUser(login, password);

            if (isAuthenticated)
            {
                Console.WriteLine($"Вы успешно зашли в свой аккаунт {login}");
                Admin admin = new Admin();
                admin.Admin_User();
            }
            else
            {
                Console.WriteLine("Неправильный логин или пароль!");
            }
        }


        public static void AddUserToDatabase(User user)
        {
            string connectionString = @"Data Source=DESKTOP-540U1OH;Database=Airport;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO AirUser(Login, Password, Name, SurName, LastName, Passport_ID) VALUES(@Login, @Password, @Name, @SurName, @LastName, @Passport_ID)", connection))
                {
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@SurName", user.SurName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Passport_ID", user.Passport_ID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ShowUsersFromDatabase()
        {
            string connectionString = @"Data Source=DESKTOP-540U1OH;Database=Airport;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM AirUser", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Список пользователей:");

                        while (reader.Read())
                        {
                            string login = reader.GetString(1);
                            string password = reader.GetString(2);
                            string name = reader.GetString(3);
                            string surname = reader.GetString(4);
                            string lastname = reader.GetString(5);
                            string passportId = reader.GetString(6);

                            Console.WriteLine($"Логин: {login}, Пароль: {password} Имя: {name}, Фамилия: {surname}, Отчество: {lastname}, Номер паспорта: {passportId}");
                        }
                    }
                }
            }
        }

        public void AddUser()
        {
            Console.Clear();
            Console.WriteLine("Введите логин для регистрации: ");
            string login = Console.ReadLine();

            
            while (UserManager.AuthenticateUser(login, ""))
            {
                Console.WriteLine("Такой пользователь уже зарегистрирован, введите другой логин: ");
                login = Console.ReadLine();
            }

            Console.WriteLine("Введите пароль для регистрации: ");
            string password = Console.ReadLine();
            Console.WriteLine("С \"*\" обязательно: ");
            do
            {
                Console.WriteLine("Введите Имя*: ");
                Name = Console.ReadLine();
                if (string.IsNullOrEmpty(Name))
                {
                    Console.WriteLine("Имя является обязательным полем. Пожалуйста, введите Имя.");
                }
            } while (string.IsNullOrEmpty(Name));
            do
            {
                Console.WriteLine("Введите Фамилию*: ");
                SurName = Console.ReadLine();
                if (string.IsNullOrEmpty(SurName))
                {
                    Console.WriteLine("Фамилия является обязательным полем. Пожалуйста, введите Фамилию.");
                }
            } while (string.IsNullOrEmpty(SurName));
            do
            {
                Console.WriteLine("Введите Отчество*: ");
                LastName = Console.ReadLine();
                if (string.IsNullOrEmpty(LastName))
                {
                    Console.WriteLine("Отчество является обязательным полем. Пожалуйста, введите Отчество.");
                }
            } while (string.IsNullOrEmpty(LastName));
            string Passport_ID = "";
            while (string.IsNullOrEmpty(Passport_ID))
            {
                Console.WriteLine("Введите номер паспорта*: ");
                Passport_ID = Console.ReadLine();
                if (string.IsNullOrEmpty(Passport_ID))
                {
                    Console.WriteLine("Поле номер паспорта обязательно для заполнения.");
                }
                else if (UserManager.users.Exists(u => u.Passport_ID == Passport_ID))
                {
                    Console.WriteLine($"Пользователь с номером паспорта {Passport_ID} уже зарегистрирован.");
                    Passport_ID = "";
                }
            }
            User newUser = new User(login, password,Name,SurName,LastName,Passport_ID);
            UserManager.AddUser(newUser);
            Console.WriteLine($"Поздравляю вы успешно создали аккаунт {login}");
            Console.Clear();

            AddUserToDatabaseSQL(newUser);
        }
        public void AddUserToDatabaseSQL(User user)
        {
            // Код для добавления пользователя в базу данных
        string query = $"INSERT INTO users (login, password, name, surname, lastname, passport_id) VALUES ('{user.Login}', '{user.Password}', '{user.Name}', '{user.SurName}', '{user.LastName}', '{user.Passport_ID}')";
        }


        public void UserAuthentication()
        {
            Console.Clear();
            Client client = new Client();
            Console.WriteLine("Введите логин для авторизации: ");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль для авторизации: ");
            string password = Console.ReadLine();
            bool isAuthenticated = UserManager.AuthenticateUser(login, password);

            if (isAuthenticated)
            {
                Console.WriteLine($"Вы успешно зашли в свой аккаунт {login}");
                client.Client_User(login,password);
            }
            else
            {
                Console.WriteLine("Неправильный логин или пароль!");
            }
        }

        public  void ShowAllUsers()
        {
            
            UserManager.ShowAllUsers();
        }

    }

}

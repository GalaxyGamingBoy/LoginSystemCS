using LoginSystem.LoginSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.LoginSystem
{
    class Terminal
    {
#pragma warning disable IDE0044 // Remove readonly modifier
        List<User> RegisterUsers = new();
#pragma warning restore IDE0044 // Enable readonly modifier

        User CurrentUser { get; set; }
        string CourrentCommand { get; set; }
        bool IsRunning { get; set; }
        bool IsDebug { get; set; }

        int RegisterUser()
        {
            string Username, Password, Country, loginAfterRegistering;

            Console.WriteLine("----- Register User Form -----");

            // Ask User
            Console.Write("Username: ");
            Username = Console.ReadLine();
            Console.Write("Password: ");
            Password = Console.ReadLine();
            Console.Write("Country: ");
            Country = Console.ReadLine();
            Console.Write("Login after registering? (y/N)");
            loginAfterRegistering = Console.ReadLine();

            User newUser = new(Username, Password, Country);
            this.RegisterUsers.Add(newUser);

            if(loginAfterRegistering == "y")
            {
                this.CurrentUser = newUser;
            }

            return 200;
        }

        string LoginUser()
        {
            string requestedUsername, requestedPassword = "";
            bool UsernameFound = false;
            User userFound = new();

            Console.WriteLine("----- Login User Form -----");

            Console.Write("Username: ");
            requestedUsername = Console.ReadLine();

            this.RegisterUsers.ForEach(x =>
            {
                if (x.Username == requestedUsername)
                {
                    Console.WriteLine("Found Username!");
                    UsernameFound = true;
                    userFound = x;
                }
            });

            if (!UsernameFound)
            {
                return "404 - Username Not Found!";
            }

            Console.Write("Password: ");
            requestedPassword = Console.ReadLine();

            if(requestedPassword == userFound.Username)
            {
                this.CurrentUser = userFound;
                return "Logged In!";
            }

            return "Unauthorized - 400";
        }

        int SearchCommand()
        {
            string command = this.CourrentCommand;
            switch (command)
            {
                case "ping":
                    Console.WriteLine("Pong!");
                    return 200;
                case "debugOn":
                    this.IsDebug = true;
                    return 200;
                case "debugOff":
                    this.IsDebug = false;
                    return 200;
                case "registeredUsers":
                    this.RegisterUsers.ForEach(user => { Console.WriteLine(user.Username); });
                    return 200;
                case "users":
                    this.RegisterUsers.ForEach(user => { Console.WriteLine(user.Username); });
                    return 200;
                case "register":
                    return this.RegisterUser();
                case "login":
                    Console.WriteLine(this.LoginUser());
                    return 200;
                case "exit":
                    this.IsRunning = false;
                    return 200;
                case "e":
                    this.IsRunning = false;
                    return 200;
                default:
                    return 404;
            }
        }

        void AskForInput()
        {
            while (IsRunning)
            {
                Console.Write($"{this.CurrentUser.Username} ~ System ..:> ");
                this.CourrentCommand = Console.ReadLine();
                if (this.IsDebug) { Console.WriteLine(this.SearchCommand()); } else { this.SearchCommand(); } ;
            }
        }

        public void StartTerminal(bool debug)
        {
            this.IsDebug = debug;

            User guest = new("guest", "guest", "None");

            this.RegisterUsers.Add(guest);
            this.CurrentUser = RegisterUsers[0];

            this.IsRunning = true;
            this.AskForInput();
        }
    }
}

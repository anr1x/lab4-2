using System;
using System.Collections.Generic;

// Базовий клас "Комп'ютер"
public class Computer
{
    public string IPAddress { get; set; }
    public int Power { get; set; }
    public string OperatingSystem { get; set; }

    public Computer(string ipAddress, int power, string operatingSystem)
    {
        IPAddress = ipAddress;
        Power = power;
        OperatingSystem = operatingSystem;
    }
}

// Інтерфейс для з'єднання та відключення
public interface IConnectable
{
    void Connect(Computer target);
    void Disconnect(Computer target);
}

// Клас "Сервер" як спадкоємницький від "Комп'ютер" з додатковими властивостями та реалізацією інтерфейсу
public class Server : Computer, IConnectable
{
    public int MaxConnections { get; set; }

    public Server(string ipAddress, int power, string operatingSystem, int maxConnections)
        : base(ipAddress, power, operatingSystem)
    {
        MaxConnections = maxConnections;
    }

    public void Connect(Computer target)
    {
        Console.WriteLine($"Server {IPAddress} is connecting to {target.IPAddress}");
        // Логіка для передачі даних
    }

    public void Disconnect(Computer target)
    {
        Console.WriteLine($"Server {IPAddress} is disconnecting from {target.IPAddress}");
        // Логіка для відключення
    }
}

// Клас "Робоча станція" як спадкоємницький від "Комп'ютер" з додатковими властивостями та реалізацією інтерфейсу
public class Workstation : Computer, IConnectable
{
    public string UserName { get; set; }

    public Workstation(string ipAddress, int power, string operatingSystem, string userName)
        : base(ipAddress, power, operatingSystem)
    {
        UserName = userName;
    }

    public void Connect(Computer target)
    {
        Console.WriteLine($"Workstation {IPAddress} is connecting to {target.IPAddress}");
        // Логіка для передачі даних
    }

    public void Disconnect(Computer target)
    {
        Console.WriteLine($"Workstation {IPAddress} is disconnecting from {target.IPAddress}");
        // Логіка для відключення
    }
}

// Клас "Маршрутизатор" як спадкоємницький від "Комп'ютер" з додатковими властивостями та реалізацією інтерфейсу
public class Router : Computer, IConnectable
{
    public List<Computer> ConnectedComputers { get; set; }

    public Router(string ipAddress, int power, string operatingSystem)
        : base(ipAddress, power, operatingSystem)
    {
        ConnectedComputers = new List<Computer>();
    }

    public void Connect(Computer target)
    {
        Console.WriteLine($"Router {IPAddress} is connecting to {target.IPAddress}");
        ConnectedComputers.Add(target);
        // Логіка для маршрутизації та передачі даних
    }

    public void Disconnect(Computer target)
    {
        Console.WriteLine($"Router {IPAddress} is disconnecting from {target.IPAddress}");
        ConnectedComputers.Remove(target);
        // Логіка для відключення
    }
}

// Клас "Мережа" для моделювання взаємодії між комп'ютерами
public class Network
{
    public List<Computer> Computers { get; set; }

    public Network()
    {
        Computers = new List<Computer>();
    }

    public void SimulateNetwork()
    {
        foreach (var computer in Computers)
        {
            if (computer is IConnectable connectable)
            {
                // Приклад з'єднання комп'ютерів у мережі
                if (Computers.Count > 1)
                {
                    var targetComputer = Computers[0]; // Виберіть конкретний комп'ютер для прикладу
                    connectable.Connect(targetComputer);
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Приклад створення та моделювання умовної комп'ютерної мережі
        Network network = new Network();

        Server server = new Server("192.168.1.1", 1000, "Windows Server", 100);
        Workstation workstation = new Workstation("192.168.1.2", 500, "Windows 10", "User1");
        Router router = new Router("192.168.1.3", 200, "Embedded OS");

        network.Computers.Add(server);
        network.Computers.Add(workstation);
        network.Computers.Add(router);

        network.SimulateNetwork();
    }
}

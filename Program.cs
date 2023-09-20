//Proyecto Maquina Expendedora de Alimentos - Oscar Mora Cohorte 12
using System;
using System.Collections.Generic;
using System.Linq;

class Consumable
{
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public int Cantidad { get; set; }
}

interface IStoreItem
{
    string Nombre { get; }
    int Precio { get; }
}

class VendingMachine
{
    private List<Consumable> consumables;
    private List<int> availableCoins = new List<int> { 500, 200, 100, 50 };

    public VendingMachine()
    {
        consumables = new List<Consumable>();
    }

    public void AddConsumable(Consumable consumable)
    {
        
        var existingConsumable = consumables.FirstOrDefault(c => c.Nombre.Equals(consumable.Nombre));

        if (existingConsumable != null)
        {
            
            Console.WriteLine("El consumible ya existe. ¿Desea rellenar el inventario? (S/N)");
            if (Console.ReadLine().Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                existingConsumable.Cantidad += consumable.Cantidad;
                Console.WriteLine("Inventario rellenado con éxito.");
            }
        }
        else
        {
            
            consumables.Add(consumable);
            Console.WriteLine("Nuevo consumible agregado.");
        }
    }

    public void ShowConsumables()
    {
        Console.WriteLine("Consumibles disponibles:");
        foreach (var consumable in consumables)
        {
            Console.WriteLine($"{consumable.Nombre} - ${consumable.Precio} - Cantidad: {consumable.Cantidad}");
        }
    }

    public void Purchase(string itemName, int insertedMoney)
    {
        
        var consumable = consumables.FirstOrDefault(c => c.Nombre.Equals(itemName));

        if (consumable != null)
        {
            if (insertedMoney >= consumable.Precio)
            {
                
                int change = insertedMoney - consumable.Precio;
                Console.WriteLine($"Compra exitosa. Cambio: ${change}");

                
                consumable.Cantidad--;

                
                GiveChange(change);
            }
            else
            {
                Console.WriteLine("Dinero insuficiente para comprar este consumible.");
            }
        }
        else
        {
            Console.WriteLine("El consumible no está disponible en la máquina.");
        }
    }

    private void GiveChange(int change)
    {
        Console.WriteLine("Cambio entregado:");

        foreach (var coin in availableCoins)
        {
            while (change >= coin)
            {
                Console.WriteLine($"{coin}");
                change -= coin;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("¡Bienvenido a la Máquina Expendedora!");
        VendingMachine vendingMachine = new VendingMachine();

        while (true)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Cliente");
            Console.WriteLine("2. Proveedor");
            Console.WriteLine("3. Salir");

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    vendingMachine.ShowConsumables();
                    Console.WriteLine("Ingrese el nombre del consumible que desea comprar:");
                    string itemName = Console.ReadLine();
                    Console.WriteLine("Ingrese la cantidad de dinero:");
                    int money = int.Parse(Console.ReadLine());
                    vendingMachine.Purchase(itemName, money);
                    break;
                case 2:
                    Console.WriteLine("Ingrese el nombre del consumible:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Ingrese el precio del consumible:");
                    int price = int.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese la cantidad en inventario:");
                    int quantity = int.Parse(Console.ReadLine());
                    Consumable newConsumable = new Consumable { Nombre = name, Precio = price, Cantidad = quantity };
                    vendingMachine.AddConsumable(newConsumable);
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}

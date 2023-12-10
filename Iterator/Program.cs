using System;
using System.Collections.Generic;

public class MenuItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Vegetarian { get; set; }
    public double Price { get; set; }

    public MenuItem(string name, string description, bool vegetarian, double price)
    {
        Name = name;
        Description = description;
        Vegetarian = vegetarian;
        Price = price;
    }
}

public interface IIterator<T>
{
    bool HasNext();
    T Next();
}

public class ListIterator<T> : IIterator<T>
{
    private List<T> items;
    private int position = 0;

    public ListIterator(List<T> items)
    {
        this.items = items;
    }

    public bool HasNext()
    {
        return position < items.Count;
    }

    public T Next()
    {
        T item = items[position];
        position++;
        return item;
    }
}

public interface IMenu
{
    IIterator<MenuItem> CreateIterator();
}

public class PancakeHouseMenu : IMenu
{
    private List<MenuItem> menuItems;

    public PancakeHouseMenu()
    {
        menuItems = new List<MenuItem>();

        AddItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs, and toast", true, 2.99);
        AddItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", false, 2.99);
        AddItem("Blueberry Pancakes", "Pancakes made with fresh blueberries, and blueberry syrup", true, 3.49);
        AddItem("Waffles", "Waffles, with your choice of blueberries or strawberries", true, 3.59);
    }

    public void AddItem(string name, string description, bool vegetarian, double price)
    {
        menuItems.Add(new MenuItem(name, description, vegetarian, price));
    }

    public IIterator<MenuItem> CreateIterator()
    {
        return new ListIterator<MenuItem>(menuItems);
    }
}

public class DinerMenu : IMenu
{
    private List<MenuItem> menuItems;

    public DinerMenu()
    {
        menuItems = new List<MenuItem>();

        AddItem("Vegetarian BLT", "(Fakin') Bacon with lettuce & tomato on whole wheat", true, 2.99);
        AddItem("BLT", "Bacon with lettuce & tomato on whole wheat", false, 2.99);
        AddItem("Soup of the day", "Soup of the day, with a side of potato salad", false, 3.29);
        AddItem("Hotdog", "A hot dog, with saurkraut, relish, onions, topped with cheese", false, 3.05);
        AddItem("Steamed Veggies and Brown Rice", "Steamed vegetables over brown rice", true, 3.99);
        AddItem("Pasta", "Spaghetti with Marinara Sauce, and a slice of sourdough bread", true, 3.89);
    }

    public void AddItem(string name, string description, bool vegetarian, double price)
    {
        menuItems.Add(new MenuItem(name, description, vegetarian, price));
    }

    public IIterator<MenuItem> CreateIterator()
    {
        return new ListIterator<MenuItem>(menuItems);
    }
}

public class Waitress
{
    public void PrintMenu(IMenu menu)
    {
        Console.WriteLine("MENU");
        Console.WriteLine("----");

        var iterator = menu.CreateIterator();
        while (iterator.HasNext())
        {
            var menuItem = iterator.Next();
            Console.WriteLine($"{menuItem.Name}, {menuItem.Price} -- {menuItem.Description}");
        }
    }
}

class Program
{
    static void Main()
    {
        var pancakeMenu = new PancakeHouseMenu();
        var dinerMenu = new DinerMenu();

        var waitress = new Waitress();
        waitress.PrintMenu(pancakeMenu);
        waitress.PrintMenu(dinerMenu);
    }
}

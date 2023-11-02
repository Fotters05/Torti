class Program
{

    static Dictionary<string, Dictionary<string, double>> prices = new Dictionary<string, Dictionary<string, double>>
{
        { "форма", new Dictionary<string, double> { { "круглый 100 Рублей", 100 }, { "квадратный 120 Рублей", 120 }, { "шар 150 Рублей", 150 } } },
        { "размер", new Dictionary<string, double> { { "маленький 500 Рублей ", 500 }, { "средний 800 Рублей", 800 }, { "большой 1000 Рублей", 1000 } } },
        { "вкус", new Dictionary<string, double> { { "персиковый  300 Рублей", 300 }, { "банановый 200 Рублей", 200 }, { "вишневый 400 Рублей", 400 } } },
        { "глазурь", new Dictionary<string, double> { { "персиковая  200 Рублей", 200 }, { "шоколадная 300 Рублей", 300 }, { "вишневая 200 Рублей", 200 } } },
        { "декор", new Dictionary<string, double> { { "звезда  50 Рублей", 50 }, { "бабочка  40 Рублей", 40 }, { "вишня 200 Рублей", 30 } } }
};

    static Dictionary<string, string> order = new Dictionary<string, string>
    {
        { "форма", "" },
        { "размер", "" },
        { "вкус", "" },
        { "глазурь", "" },
        { "декор", "" }
    };


    static void Main()
    {
        bool continueOrdering = true;
        while (continueOrdering)
        {
            Console.Clear();
            Console.WriteLine("Меню заказа тортов:");
            Console.WriteLine("Выберите опцию и нажмите Enter:");

            string selectedOption = ShowMenuAndGetSelection(order.Keys);
            if (selectedOption == "Выход")
            {
                continueOrdering = false;
                continue;
            }

            order[selectedOption] = ShowSubMenuAndGetSelection(prices[selectedOption].Keys);
        }

        double totalCost = CalculateTotalCost(order, prices);
        SaveOrderToHistory(order, totalCost);

        Console.WriteLine("Заказ сохранен в истории заказов.");
        Console.WriteLine($"Итоговая стоимость заказа: {totalCost} рублей");

        Console.WriteLine("Хотите сделать еще один заказ? (Да/Нет)");
        string anotherOrder = Console.ReadLine().ToLower();
        if (anotherOrder == "да")
        {
            Main();
        }
    }

    static string ShowMenuAndGetSelection(ICollection<string> options)
    {
        int selectedIndex = 0;
        string[] optionsArray = options.ToArray();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выберите опцию:");
            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                    Console.WriteLine($"> {optionsArray[i]}");
                else
                    Console.WriteLine($"  {optionsArray[i]}");
            }

            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
                return optionsArray[selectedIndex];
            else if (key == ConsoleKey.Escape)
                return "Выход";
            else if (key == ConsoleKey.UpArrow)
                selectedIndex = Math.Max(0, selectedIndex - 1);
            else if (key == ConsoleKey.DownArrow)
                selectedIndex = Math.Min(options.Count - 1, selectedIndex + 1);
        }
    }

    static string ShowSubMenuAndGetSelection(ICollection<string> options)
    {
        int selectedIndex = 0;
        string[] optionsArray = options.ToArray();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выберите опцию:");
            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                    Console.WriteLine($"> {optionsArray[i]}");
                else
                    Console.WriteLine($"  {optionsArray[i]}");
            }

            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
                return optionsArray[selectedIndex];
            else if (key == ConsoleKey.Escape)
                return "Назад";
            else if (key == ConsoleKey.UpArrow)
                selectedIndex = Math.Max(0, selectedIndex - 1);
            else if (key == ConsoleKey.DownArrow)
                selectedIndex = Math.Min(options.Count - 1, selectedIndex + 1);
        }
    }

    static double CalculateTotalCost(Dictionary<string, string> order, Dictionary<string, Dictionary<string, double>> prices)
    {
        double totalCost = 0;
        foreach (var item in order)
        {
            string optionCategory = item.Key;
            string selectedOption = item.Value;

            if (prices.ContainsKey(optionCategory) && prices[optionCategory].ContainsKey(selectedOption))
            {
                double itemCost = prices[optionCategory][selectedOption];
                totalCost += itemCost;
            }
        }
        return totalCost;
    }

    static void Cost()
    {

        double totalCost = CalculateTotalCost(order, prices);
        SaveOrderToHistory(order, totalCost);

        Console.WriteLine("Заказ сохранен в истории заказов.");
        Console.WriteLine($"Итоговая стоимость заказа: {totalCost} рублей");

        Console.WriteLine("Хотите сделать еще один заказ? (Да/Нет)");
        string anotherOrder = Console.ReadLine().ToLower();
        if (anotherOrder == "да")
        {
        }
    }

    static void SaveOrderToHistory(Dictionary<string, string> order, double totalCost)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "История заказов.txt");

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Заказ:");
            foreach (var item in order)
            {
                writer.WriteLine($"{item.Key}: {item.Value}");
            }
            writer.WriteLine($"Сумма заказа: {totalCost} руб.");
            writer.WriteLine();
        }
    }
}
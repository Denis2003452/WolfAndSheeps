using System.Diagnostics;

namespace WolfAndSheeps;

internal class Program
{
    static Random rnd = new Random();
    static List<Animal> animals = new List<Animal>();
    static List<Animal> bufferlist = new List<Animal>();
    static Field field = new Field(15, 15);
    static void Main()
    {
        Wolf w = new Wolf(5, 5);
        Sheep s1 = new Sheep(rnd.Next(0, 15), rnd.Next(0, 15));
        Sheep s2 = new Sheep(rnd.Next(0, 15), rnd.Next(0, 15));
        Sheep s3 = new Sheep(rnd.Next(0, 15), rnd.Next(0, 15));
        animals.Add(s1);
        animals.Add(s2);
        animals.Add(s3);
        animals.Add(w);

        while (true)
        {
            field.InitializeField();

            ConsoleKeyInfo pushedKey = Console.ReadKey();
            int dirWolf = 0;
            if (pushedKey.Key == ConsoleKey.RightArrow)
                dirWolf = 0;
            else if (pushedKey.Key == ConsoleKey.DownArrow)
                dirWolf = 1;
            else if (pushedKey.Key == ConsoleKey.LeftArrow)
                dirWolf = 2;
            else if (pushedKey.Key == ConsoleKey.UpArrow)
                dirWolf = 3;

            // Логика
            foreach (Animal s in animals)
            {
                if (s.Name == "Sheep")
                {
                    s.Move(rnd.Next(0, 4));
                    if (s.X >= 0 & s.Y == 0)
                        s.Move(1);
                    else if (s.X == 0 & s.Y >= 0)
                        s.Move(0);
                    else if (s.X >= 0 & s.Y == 15)
                        s.Move(3);
                    else if (s.X == 15 & s.Y >= 0)
                        s.Move(2);                                            
                }
                else
                {
                    w.Move(dirWolf);
                }
            }
            foreach (Animal s in animals)
            {
                if (s.Name == "Sheep")
                {
                    if (s.X == w.X & s.Y == w.Y)
                        bufferlist.Add(s);
                }         
            }                    
            foreach (Animal buffer in bufferlist)
            {
                animals.Remove(buffer);
            }
            PlaceObjects();
            // Отрисовка
            RenderField();
        }
    }
    static void PlaceObjects()
    {
        foreach (Animal a in animals)
        {
            char sprite = '\0';
            switch (a.Name)
            {
                case "Wolf":
                    {
                        sprite = 'w';
                        break;
                    }
                case "Sheep":
                    {
                        sprite = 's';
                        break;
                    }
            }
            field.Space[a.X, a.Y] = sprite;
        }

    }
    static void RenderField()
    {
        Console.Clear();
        for (int y = 0; y < field.Space.GetLength(1); y++)
        {
            for (int x = 0; x < field.Space.GetLength(0); x++)
            {
                if (field.Space[x, y] == 'w')
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(field.Space[x, y]);
                    Console.ResetColor();
                }
                else if(field.Space[x, y] == 's')
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(field.Space[x, y]);
                    Console.ResetColor();
                }
                else if (field.Space[x, y] == '\0')
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
    }
}
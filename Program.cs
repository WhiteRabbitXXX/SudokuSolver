using System;

namespace mainspace
{
    class main
    {
        public static bool Solved = false;
        public static List<int> Possible = new List<int>();
        public static int[,] field = new int[9,9];
        public static int[,] fieldRestore = new int[9,9];
        public static bool RowCheck(int number, int Row)
        {
            for (int x = 0; x < 9; x++)
            {
                if (field[Row, x] == number)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ColumnCheck(int number, int Column)
        {
            for (int y = 0; y < 9; y++)
            {
                if (field[y, Column] == number)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool SquareCheck(int number, int Column, int Row)
        {
            bool process = true;
            while (process&((Column)%3 != 0)|(Column == 8))
            {
                if (Column != 0)
                {
                    Column--;
                }
                else
                {
                    process = false;
                }
            }
            int SquareX = Column;
            process = true;
            while (process&((Row)%3 != 0)|(Row == 8))
            {
                if (Row != 0)
                {
                    Row--;
                }
                else
                {
                    process = false;
                }
            }
            int SquareY = Row;
            //Console.WriteLine($"Y {SquareY} X {SquareX} num {number}");
            for (int y = SquareY; y < (SquareY + 3); y++)
            {
                for (int x = SquareX; x < (SquareX + 3); x++)
                {
                    if (field[y,x] == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static void Solve()
        {
            Random random = new Random(); 
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (field[y,x] == 0)
                    {
                        for (int i = random.Next(1,9); (SquareCheck(i, x, y)&&ColumnCheck(i, x)&&RowCheck(i, y));)
                        {
                            field[y,x] = i;
                        }
                    }
                }   
            }
        }
        public static void PreSolve()
        {
            bool PreSolved = true;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (field[y,x] == 0)
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            if (SquareCheck(i, x, y)&&ColumnCheck(i, x)&&RowCheck(i, y))
                            {
                                Possible.Add(i);
                            }
                        }
                        if (Possible.Count == 1)
                        {
                            field[y,x] = Possible[0];
                            Possible.Clear();
                        }
                        else
                        {
                            PreSolved = false;
                            Possible.Clear();
                        }
                    }
                }   
            }
        }
        public static void Resolve()
        {
            Random random = new Random(); 
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (field[y,x] == 0)
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            if ((SquareCheck(i, x, y)&&ColumnCheck(i, x)&&RowCheck(i, y)))
                            {
                                field[y,x] = i;
                            }
                        }
                    }
                }   
            }
        }
        public static void Input()
        {
            bool work = true;
            string input;
            for (int i = 0; work&&(i < 9); i++)
            {
                for (int x = 0; work&&(x < 9); x++)
                {
                    input = Console.ReadLine();
                    if (input == "solve")
                    {
                        work = false;
                    }
                    else
                    {
                        field[i,x] = Convert.ToInt32(input);
                        fieldRestore[i,x] = Convert.ToInt32(input);
                        Output(field);
                    }
                }
            }
        }
        public static void Output(int[,] matrix)
        {
            {
                Console.Clear();
                for (int i = 0; i < 9; i++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        Console.Write(String.Format("{0, -5}", matrix[i,x]));
                        if (((x+1) % 3) == 0)
                        {
                            Console.Write(String.Format("{0, -5}","||"));
                        }
                    }
                    if (((i+1) % 3) == 0)
                    {
                        Console.WriteLine();
                        for (int y = 0; y < 56; y++)
                        {
                            Console.Write("=");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
            }
        }
        public static void Main()
        {
            Output(field);
            Input();
            do
            {
                PreSolve();
                Solve();
                Resolve();
                Output(field);
                string UserCommand = Console.ReadLine();
                if (UserCommand == "yes")
                {
                    Solved = true;
                }
                if (UserCommand == "look restore")
                {
                    Output(fieldRestore);
                    Console.WriteLine("Restored");
                    Console.ReadLine();
                }
            } while (!Solved);
        }
    }
}

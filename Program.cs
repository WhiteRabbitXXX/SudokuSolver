using System;

namespace mainspace
{
    class main
    {
        public static List<int> Possible = new List<int>();
        public static bool Solved = false;
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

        public static bool Eraser()
        {
            bool process = true;
            List<int> ProblemPointY = new List<int>();
            List<int> ProblemPointX = new List<int>();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (field[y, x] == 0)
                    {
                        ProblemPointY.Add(y);
                        ProblemPointX.Add(x);
                    }
                }
            }
            if ((ProblemPointY.Count == 0)&(ProblemPointX.Count == 0))
            {
                return true;
            }
            foreach (int Y in ProblemPointY)
            {
                for (int X = 0; X < 9; X++)
                {
                    field[Y, X] = 0;
                }
            }
            foreach (int X in ProblemPointX)
            {
                for (int Y = 0; Y < 9; Y++)
                {
                    field[Y, X] = 0;
                }
            }
            ProblemPointX.Clear();
            ProblemPointY.Clear();
            return false;
        }

        public static void Restore()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (field[y, x] == 0)
                    {
                        field[y, x] = fieldRestore[y, x];
                    }
                }
            }
        }

        public static void Solve()
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 8; j > 3; j--)
                {
                    for (int y = (8 - j), x = 0; (y < 9)&&(x < (9)); x++, y++)
                    {
                        //Console.Write($"j {j} y {y} x {x}");
                        //Console.WriteLine();
                        if ((field[y,x] == 0)&(SquareCheck(i, x, y)&&ColumnCheck(i, x)&&RowCheck(i, y)))
                        {
                            field[y,x] = i;
                            //Console.WriteLine($"accept y {y} x {x} num {i}");
                            PreSolve();
                        }
                    }
                }
                for (int u = 1; u < 5; u++)
                {
                    for (int x = (u), y = 0; (y < (6-u))&&(x < 6); x++, y++)
                    {
                        //Console.WriteLine($"u {u} y {y} x {x}");
                        if ((field[y,x] == 0)&(SquareCheck(i, x, y)&&ColumnCheck(i, x)&&RowCheck(i, y)))
                        {
                            field[y,x] = i;
                            //Console.WriteLine($"accept y {y} x {x} num {i}");
                            PreSolve();
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
                        for (int i = random.Next(1,9); (SquareCheck(i, x, y)&&ColumnCheck(i, x)&&RowCheck(i, y));)
                        {
                            field[y,x] = i;
                            PreSolve();
                        }
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
                Resolve();
                Solve();
                PreSolve();
                if (Eraser())
                {
                    Solved = true;
                }
                else
                {
                    Restore();
                }
                for (int i = 0; i < 6; i++)
                {
                    Resolve();
                }
                Solve();
                Output(field);
                
            } while (!Solved);
            Output(field);
            Console.WriteLine("Solved");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{/// <summary>
/// کلاس بازی مار
/// </summary>
    public class Snake
    {
        private static List<int[]> Point { get; set; }
        private static int[] Prize { get; set; }
        private static int Speed { get; set; }
        private static EnDirectionX DirectionX { get; set; } = EnDirectionX.Stable;
        private static EnDirectionY DirectionY { get; set; } = EnDirectionY.Stable;
        private static bool End { get; set; } = true;
        /// <summary>
        /// شروع بازی
        /// </summary>
        /// <param name="speed">سرعت بازی</param>
        /// <param name="snakeLength">طول مار</param>
        public static void SnakeRun(int speed = 50, int snakeLength = 8)
        {
            Point = new List<int[]>();
            Speed = speed;
            CreateSnake(30, 10, snakeLength);
            CreatePrize();
            Draw();

            Thread th = new Thread(Show);
            Thread th2 = new Thread(MoveSnake);

            th.Start();
            th2.Start();
            EndGame();
        }
        /// <summary>
        /// ایجاد کردن مار
        /// </summary>
        private static void CreateSnake(int x,int y, int snakeLengh)
        {
            for (int i = snakeLengh-1 ; i >= 0; i--)
            {
                Point.Add(new int[] { x + i, y });
            }
        }
        /// <summary>
        /// ایجاد جایزه به صورت رندوم
        /// </summary>
        private static void CreatePrize()
        {
            var x = new Random().Next(11, 110);
            var y = new Random().Next(4, 23);
            Prize = new int[] { x, y };
        }
        /// <summary>
        /// ایجاد شمایل بازی
        /// </summary>
        private static void Draw()
        {
            Console.CursorVisible = false;
            Console.Clear();
            for (int i = 10; i < 111; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                if (i%2 ==0)
                {
                    Console.SetCursorPosition(i, 3);
                    Console.Write("■");
                    Console.SetCursorPosition(i, 23);
                    Console.Write("■");
                }
            }
            for (int i = 3; i < 24; i++)
            {
                Console.SetCursorPosition(10, i);
                Console.Write("■");
                Console.SetCursorPosition(110, i);
                Console.Write("■");
            }
        }
        /// <summary>
        /// تنظیم کردن سرعت
        /// </summary>
        /// <param name="speed"></param>
        private static void SetSpeed(int speed)
        {
            if (Point[0][0] - Point[1][0] != 0)
            {
                Thread.Sleep(speed);
            }
            else
            {
                Thread.Sleep(speed * 2);
            }
        }
        /// <summary>
        /// افزایش طول و ایجاد دوباره جایزه
        /// </summary>
        private static void IsGetPrize()
        {
            var check = Point[0];

            if (check[0] == Prize[0] && check[1] == Prize[1])
            {
                var x = Point[0][0] - Point[1][0];
                var y = Point[0][1] - Point[1][1];
                Point.Add(new int[] { Point[Point.Count - 1][0] + x, Point[Point.Count - 1][1] + y });
                CreatePrize();
                ShowScore();
            }
        }
        /// <summary>
        /// کنترل مار
        /// </summary>
        private static void MoveSnake()
        {
            while (End)
            {
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        DirectionY = EnDirectionY.Stable;
                        DirectionX = EnDirectionX.Right;
                        break;
                    case ConsoleKey.LeftArrow:
                        DirectionY = EnDirectionY.Stable;
                        DirectionX = EnDirectionX.Left;
                        break;
                    case ConsoleKey.UpArrow:
                        DirectionX = EnDirectionX.Stable;
                        DirectionY = EnDirectionY.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        DirectionX = EnDirectionX.Stable;
                        DirectionY = EnDirectionY.Down;
                        break;
                }
            }
        }
        /// <summary>
        /// تغییر مختصات جاری مار
        /// </summary>
        private static void ChangePoint()
        {
            var last = Point[Point.Count - 1];
            var first = Point[0];
            var second = Point[1];

            if (DirectionX != EnDirectionX.Stable)
            {
                last[0] = first[0] - second[0] == 0 ? first[0] - second[0] + first[0] + (int)DirectionX : first[0] - second[0] + first[0];
                last[1] = first[1];
                Point.Remove(last);
                Point.Insert(0, last);
                DirectionX = EnDirectionX.Stable;
            }
            else if (DirectionY != EnDirectionY.Stable)
            {
                last[0] = first[0];
                last[1] = first[1] - second[1] == 0 ? first[1] - second[1] + first[1] + (int)DirectionY : first[1] - second[1] + first[1];
                Point.Remove(last);
                Point.Insert(0, last);
                DirectionY = EnDirectionY.Stable;
            }
            else
            {
                last[0] = first[0] - second[0] + first[0];
                last[1] = first[1] - second[1] + first[1];
                Point.Remove(last);
                Point.Insert(0, last);
            }
        }
        /// <summary>
        /// نمایش مار و جایزه
        /// </summary>
        private static void Show()
        {
            while (End)
            {
                Console.SetCursorPosition(Point[Point.Count-1][0], Point[Point.Count-1][1]);
                Console.Write(" ");

                ChangePoint();
                IsGetPrize();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(Prize[0], Prize[1]);
                Console.Write("■");

                for (int i = 0; i < Point.Count; i++)
                    {
                        var xy = Point[i];
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(xy[0], xy[1]);
                        Console.Write("*");
                    }
                SetSpeed(Speed);
            }

            
        }
        /// <summary>
        ///چک کردن پایان بازی
        /// </summary>
        private static void EndGame()
        {
            while (End)
            {
                Thread.Sleep(1);
                    for (int i = 3; i < Point.Count; i++)
                    {
                        var check1 = Point[0];
                        var check2 = Point[i];

                        if (check1[0] == check2[0] && check1[1]==check2[1])
                        {
                            End = false;
                            GameOverShow();
                        }
                    }

                if (Point[0][1] == 3 || Point[0][1] == 23 || Point[0][0] == 10 || Point[0][0] == 110)
                {
                    End = false;
                    GameOverShow();
                }
            }
        }
        /// <summary>
        /// نمایش پایان بازی 
        /// </summary>
        private static void GameOverShow()
        {
            Thread.Sleep(500);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(47, 4);
                Console.Write("Press Enter To Play Again");
                Console.SetCursorPosition(51, 6);
                Console.Write("Press ESC To Exit");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0,10);
                Console.WriteLine(@"                     _______  _______  _______  _______        _______           _______  _______ ");
                Thread.Sleep(400);
                Console.WriteLine(@"                    (  ____ \(  ___  )(       )(  ____ \      (  ___  )|\     /|(  ____ \(  ____ )");
                Thread.Sleep(400);
                Console.WriteLine(@"                    | (    \/| (   ) || () () || (    \/      | (   ) || )   ( || (    \/| (    )|");
                Thread.Sleep(400);
                Console.WriteLine(@"                    | |      | (___) || || || || (__          | |   | || |   | || (__    | (____)|");
                Thread.Sleep(400);
                Console.WriteLine(@"                    | | ____ |  ___  || |(_)| ||  __)         | |   | |( (   ) )|  __)   |     __)");
                Thread.Sleep(400);
                Console.WriteLine(@"                    | | \_  )| (   ) || |   | || (            | |   | | \ \_/ / | (      | (\ (   ");
                Thread.Sleep(400);
                Console.WriteLine(@"                    | (___) || )   ( || )   ( || (____/\      | (___) |  \   /  | (____/\| ) \ \__");
                Thread.Sleep(400);
                Console.WriteLine(@"                    (_______)|/     \||/     \|(_______/      (_______)   \_/   (_______/|/   \__/");
            ExitOrContinue();
        }
        /// <summary>
        /// خروج یا ادامه بازی
        /// </summary>
        private static void ExitOrContinue()
        {
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.Escape:
                    System.Environment.Exit(0);
                    break;
                case ConsoleKey.Enter:
                    End = true;
                    SnakeRun();
                    break;
            }
        }
        /// <summary>
        /// نمایش امتیاز بازیکن
        /// </summary>
        private static void ShowScore()
        {
            Console.SetCursorPosition(55,2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Score: {Point.Count}");
        }
    }
}

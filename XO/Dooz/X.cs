using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooz
{
    class X
    {
        int cursorT = 4;
        int cursorL = 35;
        int xo = 1;
        
        public void x(int t,int l,int num)
        {
           

            finalxo.CT[num - 1] = t;
            finalxo.CL[num - 1] = l;
           


                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
            for (int i = 1; i < num; i += 2)
            {
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i]);
                Console.Write(@"██     ██");
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i] + 1);
                Console.Write(@"  █   █  ");
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i] + 2);
                Console.Write(@"    █    ");
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i] + 3);
                Console.Write(@"  █   █  ");
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i] + 4);
                Console.Write(@"██     ██");
            }
            for (int i = 0; i < num; i += 2)
            {
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i]);
                Console.Write(@"    █████ ");
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i] + 1);
                Console.Write(@"  ██     ██");
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i] + 2);
                Console.Write(@"  ██     ██");
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i] + 3);
                Console.Write(@"  ██     ██");
                Console.SetCursorPosition(finalxo.CL[i], finalxo.CT[i] + 4);
                Console.Write(@"    █████ ");
            }
            for (int i = 29; i <= 89; i++)
                {
                    Console.SetCursorPosition(i, 2);
                    Console.WriteLine("0");
                    Console.SetCursorPosition(i, 10);
                    Console.WriteLine("0");
                    Console.SetCursorPosition(i, 18);
                    Console.WriteLine("0");
                    Console.SetCursorPosition(i, 26);
                    Console.WriteLine("0");
                }
                for (int i = 2; i <= 26; i++)
                {
                    Console.SetCursorPosition(29, i);
                    Console.WriteLine("0");
                    Console.SetCursorPosition(49, i);
                    Console.WriteLine("0");
                    Console.SetCursorPosition(69, i);
                    Console.WriteLine("0");
                    Console.SetCursorPosition(89, i);
                    Console.WriteLine("0");
                }
     
            Console.SetCursorPosition(cursorL, cursorT);
            Console.Write(@"██     ██");
            Console.SetCursorPosition(cursorL, cursorT+1);
            Console.Write(@"  █   █  ");
            Console.SetCursorPosition(cursorL, cursorT+2);
            Console.Write(@"    █    ");
            Console.SetCursorPosition(cursorL, cursorT+3);
            Console.Write(@"  █   █  ");
            Console.SetCursorPosition(cursorL, cursorT+4);
            Console.Write(@"██     ██");

            finalxo.CL[num] = Console.CursorLeft - 10;
            finalxo.CT[num] = Console.CursorTop - 4;

            ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                    O o = new O();
                    o.o(finalxo.CT[num], finalxo.CL[num], num+1);
                   
                    break;
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop == 8)
                        {
                            cursorT = 20;
                        }
                        else
                        {
                            cursorT -= 8;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop == 24)
                        {
                            cursorT = 4;
                        }
                        else
                        {
                            cursorT += 8;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Console.CursorLeft == 45)
                        {
                            cursorL = 75;
                        }
                        else
                        {
                            cursorL -= 20;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (Console.CursorLeft == 85)
                        {
                            cursorL = 35;
                        }
                        else
                        {
                            cursorL += 20;
                        }
                        break;
                }
            x(t,l , num);
            }
    }
}

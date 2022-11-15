using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooz
{
    class O
    {
        int cursorT = 4;
        int cursorL = 35;
 
        public void o(int t,int l, int num)
        {
           
            if (num > 0)
            {
                finalxo.CT[num - 1] = t;
                finalxo.CL[num - 1] = l;
            }


            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
           
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
                Console.Write(Console.CursorLeft);
                Console.Write(Console.CursorTop);
            }
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
                Console.Write(Console.CursorLeft);
                Console.Write(Console.CursorTop);
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
        
                
            Console.SetCursorPosition(cursorL, cursorT );
            Console.Write(@"  █████ ");
            Console.SetCursorPosition(cursorL, cursorT+1);
            Console.Write(@"██     ██");
            Console.SetCursorPosition(cursorL, cursorT+2);
            Console.Write(@"██     ██");
            Console.SetCursorPosition(cursorL, cursorT+3);
            Console.Write(@"██     ██");
            Console.SetCursorPosition(cursorL, cursorT+4);
            Console.Write(@"  █████ ");
            
            finalxo.CL[num] = Console.CursorLeft - 10;
            finalxo.CT[num] = Console.CursorTop - 4;

            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.Enter:
                    X x = new X();
                    x.x(finalxo.CT[num], finalxo.
                        CL[num], num + 1);
                    
                   
                    
                    break;
                case ConsoleKey.UpArrow:
                    if (Console.CursorTop == 8)
                    {
                        cursorT = 20;
                    }
                    if (finalxo.ReserveUp(Console.CursorTop, Console.CursorLeft))
                    {
                        cursorT -= 16;
                        o(t,l,num);
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
                    if (finalxo.ReserveDown(Console.CursorTop, Console.CursorLeft))
                    {
                        cursorT += 16;
                        o(t, l, num);
                    }
                    else
                    {
                        cursorT += 8;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (Console.CursorLeft == 44)
                    {
                        cursorL = 75;
                    }
                    if (finalxo.ReserveLeft(Console.CursorTop, Console.CursorLeft))
                    {
                        cursorL -= 40;
                        o(t, l, num);
                    }
                    else
                    {
                        cursorL -= 20;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (Console.CursorLeft == 84)
                    {
                        cursorL = 35;
                    }
                    if (finalxo.ReserveRight(Console.CursorTop, Console.CursorLeft))
                    {
                        cursorL += 40;
                        o(t, l, num);
                    }
                    else
                    {
                        cursorL += 20;
                    }
                    break;
            }
            o(t,l,num);
        }
    }
}

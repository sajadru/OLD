using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooz
{
    class finalxo
    {
      public static int[] CT = new int[10];
      public static int[] CL = new int[10];
        public static bool ReserveUp(int top , int left)
        {
            for (int i = 0; i < CT.Length; i++)
            {
                if (CT[i] == top-8 && CL[i] == left)
                {
                    return false;
                }
            }
            
            return true;
        }
        public static bool ReserveDown(int top, int left)
        {
            for (int i = 0; i < CT.Length; i++)
            {
                if (CT[i] == top + 8 && CL[i] == left)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool ReserveLeft(int top, int left)
        {
            for (int i = 0; i < CT.Length; i++)
            {
                if (CT[i] == top  && CL[i] == left-20)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool ReserveRight(int top, int left)
        {
            for (int i = 0; i < CT.Length; i++)
            {
                if (CT[i] == top && CL[i] == left + 20)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

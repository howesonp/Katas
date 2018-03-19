using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public class TriangleKata
    {
        public static bool IsTriangle(int a, int b, int c)
        {
            return !(a <= 0 || b <= 0 || c <= 0) && (a + b > c && b + c > a && c + a > b);
        }
    }
}

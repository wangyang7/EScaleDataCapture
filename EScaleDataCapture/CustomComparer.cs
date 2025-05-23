using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EScaleDataCapture
{
    public class CustomComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string s1 = x;
            string s2 = y;

            if (s1.Length > s2.Length) return 1;
            if (s1.Length < s2.Length) return -1;
            return s1.CompareTo(s2);
        }
    }
}

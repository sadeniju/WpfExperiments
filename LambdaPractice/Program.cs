using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LambdaPractice
{
    /// <summary>
    /// Lambda: 
    /// - (Opt.) Input params on the left side of =>
    /// - Expression/Statement block on the right side
    /// </summary>
    class Program
    {
        delegate Boolean comp(string s, string t);
        delegate List<int> primTest(int[] n);

        static void Main(string[] args)
        {
            comp c = (string s, string t) => s.Equals(t);
            Console.WriteLine(c("hello world", "hallo welt"));
            Console.WriteLine(c("hello world", "hello world"));

            // input does not have to be specified (optional)
            // int[] n works as well
            primTest pt = n =>
                {
                    List<int> prims = new List<int>();
                    Boolean prim;

                    foreach (int no in n)
                    {
                        prim = true;
                        for (int i = 2; i <= Math.Sqrt(no); i++)
                        {
                            if (no % i == 0)
                            {
                                prim = false;
                            }
                        }
                        if (prim) { prims.Add(no); };
                    }
                    return prims;
                };

            List<int> testedPrims = pt(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23 });
            foreach(int primNo in testedPrims){
                Console.WriteLine(primNo);
            }
            Console.Read();
        }
    }
}

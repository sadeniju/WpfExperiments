using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegatesAndAnonymousMethodsPractice
{
    class Program
    {
        delegate int MyDelegate(int x); // defining a delegate
        delegate void PrintMe(string s);

        static void Main(string[] args)
        {
            MyDelegate a = delegate(int x) { return x * 2; };   // Instentiating delegate using a anonymous method

            // Anonymous method
            PrintMe pm = delegate(string s) { Console.WriteLine(s); };
            pm("Hello");
            pm("World");

            // Multicast delegate
            MyDelegate b = Repeat;
            b += delegate(int x) { return x * 100; };
            
            Console.WriteLine("a: "+a(10));
            Console.WriteLine("b: "+b(10));
            DoSomething(delegate(int x) { return x * 10; });

            Console.Read();
        }

        static void DoSomething(Func<int,int> DoSth) 
        {
            Console.WriteLine("DoSomething: "+DoSth(10));
        }

        // a method which has the same signature as the delegate 
        static int Repeat(int x)
        {
            Console.WriteLine("DoNothing: "+x);
            return x;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharptp
{
    class Program
    {
        static void Main(string[] args)
        {
            Droid droid = new C3PO();

            droid.Work();
            droid.Init();
            droid.Shutdown();

        }
    }

    class C3PO : Droid
    {
        public override void Init()
        {
            base.Init();
            Console.Write("Coucou");
        }


    }
}

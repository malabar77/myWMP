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

            Droid Bdroid = new R2D2();

            Bdroid.Work();
            Bdroid.Init();
            Bdroid.Shutdown();

            Droid Cdroid = new Astromech();

            Cdroid.Work();
            Cdroid.Init();
            Cdroid.Shutdown();

            Droid Ddroid = new BattleDroid();

            Ddroid.Work();
            Ddroid.Init();
            Ddroid.Shutdown();

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

    class R2D2 : Droid
    {
        public override void Init()
        {
            base.Init();
            Console.Write("Bip");
        }


    }
    class Astromech : Droid
    {
        public override void Init()
        {
            base.Init();
            Console.Write("Astromech");
        }


    }
    class BattleDroid : Droid
    {
        public override void Init()
        {
            base.Init();
            Console.Write("BattleDroid");
        }


    }
}

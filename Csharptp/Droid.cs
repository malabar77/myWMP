
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharptp
{
    public abstract class Droid
    {
        private bool isInit = false;
        public virtual void Init() {
            isInit = true;
        }
        public bool Work() {
            if (isInit == false)
                return false;
            return true;
                }
        public void Shutdown()
        {
            if (isInit == false)
                return;
        }
    }

    class C3PO : Droid
    {
        public override void Init()
        {
            base.Init();
            Console.WriteLine("Coucou");
        }
    }

    class R2D2 : Droid
    {
        public override void Init()
        {
            base.Init();
            Console.WriteLine("Bip");
        }


    }

    class Astromech : Droid
    {
        public override void Init()
        {
            base.Init();
            Console.WriteLine("Astromech");
        }


    }

    class BattleDroid : Droid
    {
        public override void Init()
        {
            base.Init();
            Console.WriteLine("BattleDroid");
        }

    }
}

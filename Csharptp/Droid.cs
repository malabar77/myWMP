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
            Console.Write("Coucou");
            Console.ReadLine();
        }
        public bool Work() {
            if (isInit == false)
                return false;
            Console.Write("Coucou");
            Console.ReadLine();
            return true;
                }
        public void Shutdown()
        {
            if (isInit == false)
                return;
            Console.ReadLine();
            Console.Write("Coucou");
        }
    }
}

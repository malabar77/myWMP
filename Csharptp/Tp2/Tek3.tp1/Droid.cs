// -----------------------------------------------------------------------
// <copyright file="Droid.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Tek3.tp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Timers;
    using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class Droid
    {
        private bool isInit = false;

        public virtual void Init()
        {
            this.isInit = true;
            Console.WriteLine("Droid init");
        }

        public bool Work()
        {
            if (!this.isInit)
                throw new MethodAccessException("le droid n'est pas initialisé!");

            Console.WriteLine("Droid work");
            return true;
        }

        public void Shutdown()
        {
            if (!this.isInit)
                throw new MethodAccessException("le droid n'est pas initialisé!");

            Console.WriteLine("Droid shutdown");
        }
    }
}

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
        public string Name = "Droid";

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
        public bool isinit()
        {
            return isInit;
        }
    }


    public class C3PO : Droid
    {
        public override void Init()
        {
            Console.WriteLine("C3PO Init");
            Name = "C3P0";
            base.Init();
        }
    }

    public class R2D2 : Droid
    {
        public override void Init()
        {
            Console.WriteLine("R2D2 Init");
            Name = "R2D2";
            base.Init();
        }
    }

    public class AstroMech : Droid
    {
        public override void Init()
        {
            Console.WriteLine("AstroMech Init");
            Name = "AstroMech";
            base.Init();
        }
    }

    public class BattleDroid : Droid
    {
        public override void Init()
        {
            Console.WriteLine("BattleDroid Init");
            Name = "BattleDroid";
            base.Init();
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="DroidCreatedArg.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Tek3.tp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DroidCreatedArg : EventArgs
    {
        public DroidCreatedArg(Droid droid)
        {
            this.Droid = droid;
        }

        public Droid Droid { get; private set; }
    }
}

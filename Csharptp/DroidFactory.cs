// -----------------------------------------------------------------------
// <copyright file="DroidFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Csharptp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Timers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DroidFactory // la class est static car n'est seulement là pour fournir des droids
    {
        private Timer timer;
        private Random random;
        private List<Type> typeOfDroids;

        public event EventHandler<DroidCreatedArg> DroidCreated;

        public DroidFactory(List<Type> typeOfDroids)
        {
            if (typeOfDroids == null || !typeOfDroids.Any())
                throw new ArgumentNullException("la liste de droid doit en contenir au moins un pour que l'usine tourne");

            this.typeOfDroids = typeOfDroids;
            this.random = new Random();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public void Start(double delay)
        {
            this.timer = new Timer();
            this.timer.Interval = TimeSpan.FromSeconds(delay).TotalMilliseconds;
            this.timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            this.timer.Start();

            Console.WriteLine("========================================================================");
            Console.WriteLine("                       Démarrage de l'usine!                        ");
            Console.WriteLine("========================================================================");
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int idx = this.random.Next(this.typeOfDroids.Count);
            Type type = this.typeOfDroids[idx];

            Console.WriteLine(string.Format("Construction d'un droid de type: {0}", type.Name));

            // !! Reflection !!
            // Cette partie du code permet par reflection (exploration du code, au runtime, une sorte de metaprog au runtime donc) de generer la methode generic pour
            // le type de droid que l'on veut créer à la volé, la reflection est un outil très puissant
            MethodInfo method = this.GetType().GetMethod("Create");
            MethodInfo generic = method.MakeGenericMethod(type);
            var droid = generic.Invoke(this, null) as Droid;
            this.Test(droid);
        }

        private void Test(Droid droid)
        {
            Console.WriteLine("=================================");
            droid.Init();
            droid.Work();
            droid.Shutdown();
            Console.WriteLine("=================================");

            if (this.DroidCreated != null)
                this.DroidCreated(this, new DroidCreatedArg(droid));
        }

        public T Create<T>() // méthode generic static permettant de faire un droid de type T et le retourner
            where T : Droid, new() // contrainte sur la generic pour forcer que T soit un Droid, et une class instanciable
        {
            return new T(); // et...et oui c'est tout :).
        }
    }
}

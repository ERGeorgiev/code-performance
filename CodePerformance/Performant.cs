using System;
using System.Diagnostics;
using System.Reflection;

namespace CodePerformance
{
    public class Performant
    {
        public string Name { get; set; }
        public MethodBase ActionMethod { get; set; }
        public object Obj { get; set; }

        /// <summary>
        /// Used for performance testing.
        /// </summary>
        /// <param name="actionMethod">MethodBase of a method with no parameters.</param>
        /// <param name="obj">The object that hosts the method.</param>
        public Performant(string name, MethodBase actionMethod, object obj)
        {
            Name = name;
            ActionMethod = actionMethod;
            Obj = obj;
        }

        public void Log(int iterations, int executions, bool preparation)
        {
            Result.Title(Name);

            if (preparation == true)
            {
                Result.Print(
                    "Prepare",
                    $"{Prepare().TotalSeconds}",
                    "s",
                    preparation: true);
            }

            for (int i = 0; i < executions; i++)
            {
                Result.Print(
                    "Execute",
                    $"{GetExecutionTime(iterations).TotalSeconds}",
                    "s");
            }

            Result.End();
        }

        /// <summary>
        /// Executes a method only once to put it into cache.
        /// </summary>
        /// <returns>Timespan equal to the time elapsed for the method to fully execute.</returns>
        public TimeSpan Prepare()
        {
            var timer = TimeSpan.Zero;
            timer += GetExecutionTime();
            return new TimeSpan(timer.Ticks);
        }

        /// <summary>
        /// Executes a method several times.
        /// </summary>
        /// <returns>Average timespan equal to the time elapsed for the method to fully execute.</returns>
        public TimeSpan GetExecutionTime(int iterations)
        {
            var timer = TimeSpan.Zero;
            for (int i = 0; i < iterations; i++)
                timer += GetExecutionTime();
            return new TimeSpan(timer.Ticks / iterations);
        }

        /// <summary>
        /// Executes a method once.
        /// </summary>
        /// <returns>Timespan equal to the time elapsed for the method to fully execute.</returns>
        public TimeSpan GetExecutionTime()
        {
            var sw = new Stopwatch();
            sw.Start();
            ActionMethod.Invoke(Obj, null);
            sw.Stop();
            return sw.Elapsed;
        }
    }
}

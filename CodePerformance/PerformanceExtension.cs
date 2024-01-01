using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace CodePerformance
{
    public class Performance
    {
        public int Iterations { get; set; } = 10;
        public int Executions { get; set; } = 3;
        public bool Preparation { get; set; } = true;

        public Performance(int iterations, int executions, bool preparation)
        {
            this.Iterations = iterations;
            this.Executions = executions;
            this.Preparation = preparation;
        }

        ///// <summary>
        ///// Executes an action only once to put it into cache.
        ///// </summary>
        ///// <returns>Timespan equal to the time elapsed for the action to fully execute.</returns>
        //public static TimeSpan Prepare(Action action)
        //{
        //    var timer = TimeSpan.Zero;
        //    timer += GetExecutionTime(action);
        //    return new TimeSpan(timer.Ticks);
        //}

        ///// <summary>
        ///// Executes an action several times.
        ///// </summary>
        ///// <returns>Average timespan equal to the time elapsed for the action to fully execute.</returns>
        //public static TimeSpan GetExecutionTime(Action action, int iterations, bool preparation = true)
        //{
        //    if (preparation)
        //        Prepare(action);
        //    var timer = TimeSpan.Zero;
        //    for (int i = 0; i < iterations; i++)
        //        timer += GetExecutionTime(action);
        //    return new TimeSpan(timer.Ticks / iterations);
        //}

        ///// <summary>
        ///// Executes an action once.
        ///// </summary>
        ///// <returns>Timespan equal to the time elapsed for the action to fully execute.</returns>
        //public static TimeSpan GetExecutionTime(Action action)
        //{
        //    var sw = new Stopwatch();
        //    sw.Start();
        //    action();
        //    sw.Stop();
        //    return sw.Elapsed;
        //}
    }
}
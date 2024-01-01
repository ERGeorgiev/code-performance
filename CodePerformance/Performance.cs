using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodePerformance
{
    public static class Performance
    {
        public static void Log<T>(int iterations = 10000, int executions = 1, bool preparation = true) where T : class
        {
            T instance = Activator.CreateInstance<T>();
            string instanceName = instance.ToString().Split('.').Last();
            MethodInfo[] methods = typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public);
            MethodInfo[] actions = methods.Where(m => m.ReturnType == typeof(void)).ToArray();

            char key = 'r';
            while (key == 'r')
            {
                // Start
                Console.Clear();
                Result.MainTitle("Performance Test: " + instanceName);

                // Test
                if (actions.Length == 0)
                {
                    Console.WriteLine("No actions detected in class " + nameof(T));
                }
                else
                {
                    foreach (var method in actions)
                    {
                        Performant performant = new Performant(method.Name, method, instance);
                        performant.Log(iterations, executions, preparation);
                        Result.Summary(preparation, "s");
                    }
                }

                // End
                Console.Write("Press 'r' to repeat or any other key to exit...");
                key = Console.ReadKey().KeyChar;
            }
        }

        public static void DisplayTimerProperties()
        {
            Result.Title("Timer Properties");

            Result.Print(
                "High-resolution",
                Stopwatch.IsHighResolution);

            var frequency = Stopwatch.Frequency;
            Result.Print(
                "Ticks per Second",
                frequency);

            var secondsPerTick = 1d / frequency;
            Result.Print(
                "Precision",
                secondsPerTick,
                "s");

            Result.End();
        }

        /// <summary>
        /// Executes an action only once to put it into cache.
        /// </summary>
        /// <returns>Timespan equal to the time elapsed for the action to fully execute.</returns>
        public static TimeSpan Prepare(Action action)
        {
            var timer = TimeSpan.Zero;
            timer += GetExecutionTime(action);
            return new TimeSpan(timer.Ticks);
        }

        /// <summary>
        /// Executes an action several times.
        /// </summary>
        /// <returns>Average timespan equal to the time elapsed for the action to fully execute.</returns>
        public static TimeSpan GetExecutionTime(Action action, int iterations, bool preparation = true)
        {
            if (preparation)
                Prepare(action);
            var timer = TimeSpan.Zero;
            for (int i = 0; i < iterations; i++)
                timer += GetExecutionTime(action);
            return new TimeSpan(timer.Ticks / iterations);
        }

        /// <summary>
        /// Executes an action once.
        /// </summary>
        /// <returns>Timespan equal to the time elapsed for the action to fully execute.</returns>
        public static TimeSpan GetExecutionTime(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Executes a method only once to put it into cache.
        /// </summary>
        /// <returns>Timespan equal to the time elapsed for the method to fully execute.</returns>
        public static TimeSpan Prepare(MethodBase actionMethod, object instance)
        {
            var timer = TimeSpan.Zero;
            timer += GetExecutionTime(actionMethod, instance);
            return new TimeSpan(timer.Ticks);
        }

        /// <summary>
        /// Executes a method several times.
        /// </summary>
        /// <returns>Average timespan equal to the time elapsed for the method to fully execute.</returns>
        public static TimeSpan GetExecutionTime(MethodBase actionMethod, object instance, int iterations)
        {
            var timer = TimeSpan.Zero;
            for (int i = 0; i < iterations; i++)
                timer += GetExecutionTime(actionMethod, instance);
            return new TimeSpan(timer.Ticks / iterations);
        }

        /// <summary>
        /// Executes a method once.
        /// </summary>
        /// <returns>Timespan equal to the time elapsed for the method to fully execute.</returns>
        public static TimeSpan GetExecutionTime(MethodBase actionMethod, object instance)
        {
            var sw = new Stopwatch();
            sw.Start();
            actionMethod.Invoke(instance, null);
            sw.Stop();
            return sw.Elapsed;
        }
    }
}

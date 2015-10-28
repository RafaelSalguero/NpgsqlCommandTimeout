using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpgsqlCommandTimeout
{
    class Program
    {
        static string ExceptionToString(Exception Ex)
        {
            if (Ex == null) return "";
            return Ex.Message + " --> " + Environment.NewLine
                + ExceptionToString(Ex.InnerException);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Conecting...");
            //Create the database if doesn't exist and test the connection
            using (var M = new Model())
            {
                int C = M.table1.Count();
                Console.WriteLine("table1 count: " + C.ToString());
            }

            Console.WriteLine("Deleting table data...");
            //Delete the table data avoiding any postgre cache
            using (var M = new Model())
            {
                //Disable console output:
                M.Database.Log = null;

                M.table1.RemoveRange(M.table1);
                M.SaveChanges();
            }

            Console.WriteLine("Writing table data...");

            //Recreate the table with random data:
            var Ran = new Random(unchecked((int)DateTime.Now.Ticks));
            int dataCount = 10000;
            using (var M = new Model())
            {
                //Disable console output:
                M.Database.Log = null;

                for (int i = 0; i < dataCount; i++)
                {
                    M.table1.Add(new table1 { key = i, value1 = Ran.NextDouble(), value2 = Ran.NextDouble(), value3 = Ran.NextDouble() });
                }
                M.SaveChanges();
            }

            using (var M = new Model())
                Console.WriteLine("table1 count: " + M.table1.Count());


            //A long running query:
            var st = new Stopwatch();

            try
            {
                using (var M = new Model())
                {
                    var Q = M.table1.Select(x => M.table1.Where(y => y.value1 > x.value1).Sum(y =>
                       M.table1.Where(z => z.value2 > y.value2).Sum(z => (double?)z.value2 + y.value1)
                    ));


                    //Use this query instead if Q is not slow enough
                    var Q2 = M.table1.Select(x => M.table1.Where(y => y.value1 > x.value1).Sum(y =>
                      M.table1.Where(z => z.value2 > y.value2).Sum(z =>
                      M.table1.Where(w => w.value3 > x.value3).Sum(w => (double?)w.value3 + z.value2 + y.value1)
                      )));


                    Console.WriteLine(Q.ToString());

                    Console.WriteLine("Executing at " + DateTime.Now.TimeOfDay.ToString());

                    st.Restart();
                    var result = Q.ToArray();
                    st.Stop();

                    Console.WriteLine("Output: " + result.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ExceptionToString(ex));
                st.Stop();

            }

            st.Stop();
            Console.WriteLine("Now: " + DateTime.Now.TimeOfDay.ToString());

            Console.WriteLine("Time in ms: " + st.ElapsedMilliseconds);

            Console.WriteLine("Test finished");
            Console.ReadKey();
        }
    }
}

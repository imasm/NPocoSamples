using System;
using System.Collections.Generic;

namespace NPocoSamples.Common
{
    public abstract class BaseTests
    {
        protected void Output(string message)
        {
            Console.WriteLine(message);
        }

        protected void Output<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Console.WriteLine(item);
        }
    }
}

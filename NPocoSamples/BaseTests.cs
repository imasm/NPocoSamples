using System;
using System.Collections.Generic;

namespace NPocoSamples
{
    public abstract class BaseTests
    {
        protected void Output<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Console.WriteLine(item);
        }
    }
}

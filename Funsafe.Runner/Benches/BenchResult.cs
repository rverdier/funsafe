using System;

namespace Funsafe.Runner.Benches
{
    public class BenchResult
    {
        public string BenchName { get; private set; }
        public TimeSpan Elapsed { get; private set; }
        public int GCCount { get; private set; }
        public double OperationPerSecond { get; private set; }

        public BenchResult(string benchName, TimeSpan elapsed, int gcCount, double operationPerSecond)
        {
            BenchName = benchName;
            Elapsed = elapsed;
            GCCount = gcCount;
            OperationPerSecond = operationPerSecond;
        }

        public override string ToString()
        {
            return string.Format("{0,50}\t{1,10:N0} ms\t{2,12:N0} ops/s\t{3} GC", BenchName, Elapsed.TotalMilliseconds, OperationPerSecond, GCCount);
        }
    }
}
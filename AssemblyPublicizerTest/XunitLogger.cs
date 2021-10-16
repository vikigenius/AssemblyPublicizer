using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using Xunit.Abstractions;

namespace AssemblyPublicizerTest
{
    public class XunitLogger: ConsoleLogger
    {
        public XunitLogger(ITestOutputHelper testOutputHelper)
        {
            base.WriteHandler = testOutputHelper.WriteLine;
        }
    }
}
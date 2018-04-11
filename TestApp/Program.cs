using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AzureDistributedTesting;
using Xunit.Runners;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFrom("AzureDistributedTesting.dll");
            using (var runner = AssemblyRunner.WithAppDomain(assembly.Location))
            {
                runner.OnDiscoveryComplete = OnDiscoveryComplete;
                runner.OnExecutionComplete = OnExecutionComplete;
                runner.OnTestFailed = OnTestFailed;
                runner.OnTestSkipped = OnTestSkipped;
                runner.OnErrorMessage = OnErrorMessage;
                runner.OnTestPassed = OnTestPassed;

                Console.WriteLine("Discovering...");
                
                runner.Start();

                Console.ReadLine();
            }
        }

        private static void OnTestPassed(TestPassedInfo obj)
        {
            Console.WriteLine("Test Passed: " + obj.TestDisplayName);
        }

        private static void OnErrorMessage(ErrorMessageInfo obj)
        {
            Console.WriteLine("Test failure: " + obj.ExceptionMessage);
        }

        private static void OnTestSkipped(TestSkippedInfo obj)
        {
            Console.WriteLine("Test skipped: " + obj.MethodName + " : " + obj.SkipReason);
        }

        private static void OnTestFailed(TestFailedInfo obj)
        {
            Console.WriteLine("Test failure: " + obj.ExceptionMessage);
        }

        private static void OnExecutionComplete(ExecutionCompleteInfo obj)
        {
            Console.WriteLine("Execution Complete:");
            Console.WriteLine("Total tests: " + obj.TotalTests);
            Console.WriteLine("Total test successes: " + (obj.TotalTests - obj.TestsFailed - obj.TestsSkipped));
            Console.WriteLine("Total tests skipped: " + obj.TestsSkipped);
            Console.WriteLine("Total tests failed: " + obj.TestsFailed);
        }

        private static void OnDiscoveryComplete(DiscoveryCompleteInfo obj)
        {
            Console.WriteLine("Discovery complete: ");
            Console.WriteLine("Tests discovered: " + obj.TestCasesDiscovered);
        }
    }
}

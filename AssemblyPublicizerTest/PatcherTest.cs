using System;
using Xunit;
using System.IO;
using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using Microsoft.Build.Framework;
using Xunit.Abstractions;


namespace AssemblyPublicizerTest
{
    public class PatcherTest
    {
        private readonly XunitLogger _xunitLogger;

        public PatcherTest(ITestOutputHelper testOutputHelper)
        {
            _xunitLogger = new XunitLogger(testOutputHelper);
        }

        [Fact]
        public void TestFullProjectBuild()
        {
            List<ILogger> loggers = new() { _xunitLogger };
            var projectCollection = new ProjectCollection();
            projectCollection.RegisterLoggers(loggers);
            string fixtureDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fixtures");
            string projPath = Path.Combine(fixtureDir, "TestProject.csproj.xml");
            var project = projectCollection.LoadProject(projPath);
            var ok = project.Build();
            projectCollection.UnregisterAllLoggers();
            Assert.True(ok);
        }

        [Fact]
        public void TestFullProjectBuild_MissingOutputDir()
        {
            List<ILogger> loggers = new() { _xunitLogger };
            var projectCollection = new ProjectCollection();
            projectCollection.RegisterLoggers(loggers);
            string fixtureDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fixtures");
            string projPath = Path.Combine(fixtureDir, "TestProjectMissingOutDir.csproj.xml");
            var project = projectCollection.LoadProject(projPath);
            var ok = project.Build();
            projectCollection.UnregisterAllLoggers();
            Assert.True(ok);
        }
    }
}
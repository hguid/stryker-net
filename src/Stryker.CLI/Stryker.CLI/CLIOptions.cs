﻿using Stryker.Core.Options;
using Stryker.Core.Reporters;
using Stryker.Core.TestRunners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stryker.CLI
{
    public static class CLIOptions
    {
        private static StrykerOptions _defaultOptions = new StrykerOptions();

        public static readonly CLIOption<string> ConfigFilePath = new CLIOption<string>
        {
            ArgumentName = "--config-file-path",
            ArgumentShortName = "-cp <path>",
            ArgumentDescription = "Sets the config-file-path relative to current workingDirectory | stryker-config.json (default)",
            DefaultValue = "stryker-config.json"
        };

        public static readonly CLIOption<string[]> Reporters = new CLIOption<string[]>
        {
            ArgumentName = "--reporters",
            ArgumentShortName = "-r <reporters>",
            ArgumentDescription = $@"Sets the reporter | { FormatOptionsString(_defaultOptions.Reporters.First(), (IEnumerable<Reporter>)Enum.GetValues(_defaultOptions.Reporters.First().GetType())) }]
                                                This argument takes a json array as a value. Example: ['{ Reporter.ConsoleProgressDots }', '{ Reporter.Json }']",
            DefaultValue = _defaultOptions.Reporters.Select(r => r.ToString()).ToArray(),
            JsonKey = "reporters"
        };

        public static readonly CLIOption<string> LogLevel = new CLIOption<string>
        {
            ArgumentName = "--log-console",
            ArgumentShortName = "-l <logLevel>",
            ArgumentDescription = "Sets the logging level | Options [error, warning, info (default), debug, trace]",
            DefaultValue = "info",
            JsonKey = "log-level"
        };

        public static readonly CLIOption<bool> LogToFile = new CLIOption<bool>
        {
            ArgumentName = "--log-file",
            ArgumentShortName = "-f",
            ArgumentDescription = "Makes the logger write to a file",
            DefaultValue = _defaultOptions.LogOptions.LogToFile,
            ValueType = Microsoft.Extensions.CommandLineUtils.CommandOptionType.NoValue,
            JsonKey = "log-file"
        };

        public static readonly CLIOption<bool> DevMode = new CLIOption<bool>
        {
            ArgumentName = "--dev-mode",
            ArgumentShortName = "-dm",
            ArgumentDescription = @"Stryker automatically removes all mutations from a method if a failed mutation could not be rolled back
                                                Setting this flag makes stryker not remove the mutations but rather break on failed rollbacks",
            DefaultValue = _defaultOptions.DevMode,
            ValueType = Microsoft.Extensions.CommandLineUtils.CommandOptionType.NoValue,
            JsonKey = "dev-mode"
        };

        public static readonly CLIOption<int> AdditionalTimeoutMS = new CLIOption<int>
        {
            ArgumentName = "--timeout-ms",
            ArgumentShortName = "-t <ms>",
            ArgumentDescription = $"Stryker calculates a timeout based on the time the testrun takes before the mutations| Options {_defaultOptions.AdditionalTimeoutMS}",
            DefaultValue = _defaultOptions.AdditionalTimeoutMS,
            JsonKey = "timeout-ms"
        };

        public static readonly CLIOption<string[]> ExcludedMutations = new CLIOption<string[]>
        {
            ArgumentName = "--excluded-mutations",
            ArgumentShortName = "-em <mutators>",
            ArgumentDescription = @"The given mutators will be excluded for this mutation testrun.
                                                This argument takes a json array as value. Example: ['string', 'logical']",
            JsonKey = "excluded-mutations"
        };

        public static readonly CLIOption<string> ProjectFileName = new CLIOption<string>
        {
            ArgumentName = "--project-file",
            ArgumentShortName = "-p <projectFileName>",
            ArgumentDescription = @"Used for matching the project references when finding the project to mutate. Example: ""ExampleProject.csproj""",
            JsonKey = "project-file"
        };

        public static readonly CLIOption<int> MaxConcurrentTestRunners = new CLIOption<int>
        {
            ArgumentName = "--max-concurrent-test-runners",
            ArgumentShortName = "-m <integer>",
            ArgumentDescription = @"Mutation testing is time consuming. 
                                                By default Stryker tries to make the most of your CPU, by spawning as many test runners as you have CPU cores.
                                                This setting allows you to override this default behavior.

                                                Reasons you might want to lower this setting:
                                                                 
                                                    - Your test runner starts a browser (another CPU-intensive process)
                                                    - You're running on a shared server
                                                    - You are running stryker in the background while doing other work
                                ",
            DefaultValue = _defaultOptions.ConcurrentTestrunners,
            JsonKey = "max-concurrent-test-runners"
        };

        public static readonly CLIOption<int> ThresholdBreak = new CLIOption<int>
        {
            ArgumentName = "--threshold-break",
            ArgumentShortName = "-tb <thresholdBreak>",
            ArgumentDescription = $"Set the minimum mutation score threshold. Anything below this score will return a non-zero exit code. | {_defaultOptions.ThresholdOptions.ThresholdBreak} (default)",
            DefaultValue = _defaultOptions.ThresholdOptions.ThresholdBreak,
            JsonKey = "threshold-break"
        };

        public static readonly CLIOption<int> ThresholdLow = new CLIOption<int>
        {
            ArgumentName = "--threshold-low",
            ArgumentShortName = "-tl <thresholdLow>",
            ArgumentDescription = $"Set the lower bound of the mutation score threshold. It will not fail the test. | {_defaultOptions.ThresholdOptions.ThresholdLow} (default)",
            DefaultValue = _defaultOptions.ThresholdOptions.ThresholdLow,
            JsonKey = "threshold-low"
        };

        public static readonly CLIOption<int> ThresholdHigh = new CLIOption<int>
        {
            ArgumentName = "--threshold-high",
            ArgumentShortName = "-th <thresholdHigh>",
            ArgumentDescription = $"Set the prefered mutation score threshold. | {_defaultOptions.ThresholdOptions.ThresholdHigh} (default)",
            DefaultValue = _defaultOptions.ThresholdOptions.ThresholdHigh,
            JsonKey = "threshold-high"
        };

        public static readonly CLIOption<string[]> FilesToExclude = new CLIOption<string[]>
        {
            ArgumentName = "--files-to-exclude",
            ArgumentShortName = "-fte <files-to-exclude>",
            ArgumentDescription = "Set files to exclude for mutation. Example: ['C:\\ExampleProject\\Example.cs','C:\\ExampleProject\\Example2.cs']",
            DefaultValue = null,
            JsonKey = "files-to-exclude"
        };

        public static readonly CLIOption<string> TestRunner = new CLIOption<string>
        {
            ArgumentName = "--test-runner",
            ArgumentShortName = "-tr",
            ArgumentDescription = $"Choose which testrunner should be used to run your tests. | { FormatOptionsString(_defaultOptions.TestRunner, (IEnumerable<TestRunner>)Enum.GetValues(_defaultOptions.TestRunner.GetType())) }",
            DefaultValue = _defaultOptions.TestRunner.ToString(),
            JsonKey = "test-runner"
        };

        private static string FormatOptionsString<T>(T @default, IEnumerable<T> options)
        {
            return $"Options[{@default.ToString()} (default), {string.Join(",", options.Where(o => o.ToString() != @default.ToString()))}]";
        }
    }
}
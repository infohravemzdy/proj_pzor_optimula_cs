using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;
using HraveMzdy.Legalios.Service;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service;
using HraveMzdy.Procezor.Optimula.Registry.Constants;
using HraveMzdy.Procezor.Optimula.Service;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Optimula.Registry.Providers;
using Procezor.OptimulaTest.Examples;
using System.IO;

namespace Procezor.OptimulaTest.Service
{
    public class ServiceTestPamicaExamples2022 : ServiceTestPamicaExampleTemplate
    {
        private static IPeriod TestPeriod = new Period(2022, 1);
        private static Int32 TestPeriodCode = 202201;
        private static Int32 PrevPeriodCode = 202101;

        public static IEnumerable<object[]> GenTestData => GetGenTestDecData(_genTests, TestPeriod, TestPeriodCode, PrevPeriodCode);

        public ServiceTestPamicaExamples2022(ITestOutputHelper output) : base(output)
        {
        }
        [Fact]
        public void ServiceExamples_1_OPTOptTestMonthTestCase101Test()
        {
            OptimulaGenerator example = Example_1_OPTOptTestMonthTestCase101();

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_1_OPTOptTestHourTestCase111Test()
        {
            OptimulaGenerator example = Example_1_OPTOptTestHourTestCase111();

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_1_OPTOptTestHourTestCase161Test()
        {
            OptimulaGenerator example = Example_1_OPTOptTestHourTestCase161();

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_1_OPTOptTestHourTestCase276Test()
        {
            OptimulaGenerator example = Example_1_OPTOptTestHourTestCase276();

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_1_OPTOptTestEpsTestCase126Test()
        {
            OptimulaGenerator example = Example_1_OPTOptTestEpsTestCase126();

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_101_FullTime_OverTimeZeroHolidaysZeroTest()
        {
            OptimulaGenerator example = Example_101_FullTime_OverTimeZeroHolidaysZero();

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_111_WorkTime_OverTimeZeroHolidaysZeroTest()
        {
            OptimulaGenerator example = Example_111_WorkTime_OverTimeZeroHolidaysZero();

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_102_FullTime_MinimumWageTest()
        {
            OptimulaGenerator example = Example_102_FullTime_MinimumWage();

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_CreateImport()
        {
            ServiceExamplesCreateImport(_genTests, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Theory]
        [MemberData(nameof(GenTestData))]
        public void ServiceExamplesTest(OptimulaGenerator example)
        {
            ServiceTemplateExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
    }

}

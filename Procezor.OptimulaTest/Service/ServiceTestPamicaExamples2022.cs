using System;
using System.Collections.Generic;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Generator;
using Xunit;
using Xunit.Abstractions;

namespace Procezor.OptimulaTest.Service
{
    [Collection("Non-Parallel")]
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
            OptimulaGenerator example = Example_1_OPTOptTestHourTestCase161(TestPeriod);

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_1_OPTOptTestHourTestCase276Test()
        {
            OptimulaGenerator example = Example_1_OPTOptTestHourTestCase276(TestPeriod);

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_1_OPTOptTestEpsTestCase126Test()
        {
            OptimulaGenerator example = Example_1_OPTOptTestEpsTestCase126(TestPeriod);

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_1_OPTOptTestEpsTestCase233Test()
        {
            OptimulaGenerator example = Example_1_OPTOptTestEpsTestCase233(TestPeriod);

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_101_FullTime_OverTimeZeroHolidaysZeroTest()
        {
            OptimulaGenerator example = Example_101_FullTime_OverTimeZeroHolidaysZero(TestPeriod);

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_111_WorkTime_OverTimeZeroHolidaysZeroTest()
        {
            OptimulaGenerator example = Example_111_WorkTime_OverTimeZeroHolidaysZero(TestPeriod);

            ServiceExampleTest(example, TestPeriod, TestPeriodCode, PrevPeriodCode);
        }
        [Fact]
        public void ServiceExamples_102_FullTime_MinimumWageTest()
        {
            OptimulaGenerator example = Example_102_FullTime_MinimumWage(TestPeriod);

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

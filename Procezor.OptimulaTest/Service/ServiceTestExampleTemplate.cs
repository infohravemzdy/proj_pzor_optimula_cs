using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using HraveMzdy.Legalios.Service;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service;
using HraveMzdy.Procezor.Optimula.Registry.Constants;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Optimula.Registry.Providers;
using HraveMzdy.Procezor.Generator;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;
using ResultMonad;
using HraveMzdy.Procezor.Service.Errors;

namespace Procezor.OptimulaTest.Service
{
    [CollectionDefinition("Non-Parallel", DisableParallelization = true)]
    public class NonParallelCollectionDefinitionClass
    {
    }

    public abstract class ServiceTestExampleTemplate
    {
#if __MACOS__
        public const string PROTOKOL_TEST_FOLDER = "../../../test_import";
#else
        public const string PROTOKOL_TEST_FOLDER = "..\\..\\..\\test_import";
#endif
        public const string PROTOKOL_FOLDER_NAME = "test_import";
        public const string PARENT_PROTOKOL_FOLDER_NAME = "Procezor.OptimulaTest";

        protected const string TestFolder = PROTOKOL_TEST_FOLDER;

        protected readonly ITestOutputHelper output;

        protected string TestCaseName = "";
        protected readonly IServiceProcezor _sut;
        protected readonly IServiceLegalios _leg;

        public static IEnumerable<object[]> GetGenTestDecData(IEnumerable<OptimulaGenerator> tests, IPeriod testPeriod, Int32 testPeriodCode, Int32 prevPeriodCode)
        {
            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);

            return tests.Select((tt) => (new object[] { tt }));
        }

        public ServiceTestExampleTemplate(ITestOutputHelper output, IServiceProcezor sut, string testCaseName)
        {
            this.output = output;
            this.TestCaseName = testCaseName;

            this._sut = sut;
            this._leg = new ServiceLegalios();
        }
        public static IPeriod PrevYear(IPeriod period)
        {
            return new Period(Math.Max(2010, period.Year - 1), period.Month);
        }
        public static IBundleProps CurrYearBundle(IServiceLegalios legSvc, IPeriod period)
        {
            var legResult = legSvc.GetBundle(period);
            return legResult.Value;
        }
        public static IBundleProps PrevYearBundle(IServiceLegalios legSvc, IPeriod period)
        {
            var legResult = legSvc.GetBundle(PrevYear(period));
            return legResult.Value;
        }
        protected static StreamWriter CreateProtokolFile(string fileName)
        {
            string filePath = Path.GetFullPath(Path.Combine(TestFolder, fileName));

            string currPath = Path.GetFullPath(".");
            int nameCount = currPath.Split(Path.DirectorySeparatorChar).Length;

            while (!currPath.EndsWith(PARENT_PROTOKOL_FOLDER_NAME) && nameCount != 1)
            {
                currPath = Path.GetDirectoryName(currPath);
            }
            string basePath = Path.Combine(currPath, PROTOKOL_FOLDER_NAME);
            if (nameCount <= 1)
            {
                basePath = Path.Combine(Path.GetFullPath("."), PROTOKOL_FOLDER_NAME);
            }
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            filePath = Path.Combine(basePath, fileName);
            FileStream fileStream = new FileInfo(filePath).Create();
            return new StreamWriter(fileStream, System.Text.Encoding.GetEncoding("windows-1250"));
        }
        protected static StreamWriter OpenProtokolFile(string fileName)
        {
            string filePath = Path.GetFullPath(Path.Combine(TestFolder, fileName));

            string currPath = Path.GetFullPath(".");
            int nameCount = currPath.Split(Path.DirectorySeparatorChar).Length;

            while (!currPath.EndsWith(PARENT_PROTOKOL_FOLDER_NAME) && nameCount != 1)
            {
                currPath = Path.GetDirectoryName(currPath);
            }
            string basePath = Path.Combine(currPath, PROTOKOL_FOLDER_NAME);
            if (nameCount <= 1)
            {
                basePath = Path.Combine(Path.GetFullPath("."), PROTOKOL_FOLDER_NAME);
            }
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            filePath = Path.Combine(basePath, fileName);
            FileStream fileStream = new FileInfo(filePath).Open(FileMode.Append, FileAccess.Write);
            return new StreamWriter(fileStream, System.Text.Encoding.GetEncoding("windows-1250"));
        }
        protected static string[] ExportPropsStart()
        {
            string[] headerList = new string[]
            {
                "EmployeeNumb", // A
                "EmployeeName", // B
                "PeriodName",   // C
                "AgrWorkRatio", // D
                "AgrWorkMaxim", // E
                "AgrWorkTarif", // F
                "AgrWorkLimit", // G
                "AgrWorkHours", // H
                "AgrTaskRatio", // I
                "AgrTaskMaxim", // J
                "AgrTaskTarif", // K
                "AgrTaskLimit", // L
                "AgrTaskHours", // M
                "ClothesHours", // N
                "ClothesDaily", // O
                "MealConDaily", // P
                "HomeOffMonth", // Q
                "HomeOffTarif", // R
                "HomeOffHours", // S
                "MSalaryAward", // T
                "HsalaryAward", // U
                "FPremiumBase", // V 
                "FPremiumBoss", // W  
                "FPremiumPers", // X  
                "", // Y  
                "", // Z  
                "FullSheetHrs", // AA 
                "TimeSheetHrs", // AB  
                "HoliSheetHrs", // AC 
                "WorkSheetHrs", // AD
                "WorkSheetDay", // AE
                "OverSheetHrs", // AF
                "VacaRecomHrs", // AG
                "PaidRecomHrs", // AH
                "HoliRecomHrs", // AI
                "OverAllowHrs", // AJ
                "OverAllowRio", // AK
                "RestAllowHrs", // AL
                "RestAllowRio", // AM
                "WendAllowHrs", // AN
                "WendAllowRio", // AO
                "NighAllowHrs", // AP
                "NighAllowRio", // AQ
                "HoliAllowHrs", // AR
                "HoliAllowRio", // AS
                "QClothesBase", // AT
                "QHOfficeBase", // AU
                "QAgrWorkBase", // AV
                "QSumWorkHour", // AW
            };
            return headerList.ToArray();
        }
        protected static void ExportPropsXlsStart(StreamWriter protokol)
        {
            string[] headerList = ExportPropsStart();
            protokol.WriteLine(string.Join('\t', headerList));
        }
        protected static void ExportPropsCsvStart(StreamWriter protokol)
        {
            string[] headerList = ExportPropsStart();
            protokol.WriteLine(string.Join(';', headerList)+";");
        }
        protected static void ExportPropsEnd(StreamWriter protokol)
        {
        }
        protected void ServiceExamplesCreateImport(IEnumerable<OptimulaGenerator> tests, IPeriod testPeriod, Int32 testPeriodCode, Int32 prevPeriodCode)
        {
            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);

            try
            {
                testPeriod.Code.Should().Be(testPeriodCode);

                var prevPeriod = PrevYear(testPeriod);
                prevPeriod.Code.Should().Be(prevPeriodCode);

                var testLegalResult = _leg.GetBundle(testPeriod);
                testLegalResult.IsSuccess.Should().Be(true);

                var testRuleset = testLegalResult.Value;

                var prevLegalResult = _leg.GetBundle(prevPeriod);
                prevLegalResult.IsSuccess.Should().Be(true);

                var prevRuleset = prevLegalResult.Value;

                using (var testProtokol = CreateProtokolFile($"Optimula{TestCaseName}Import_{testPeriod.Year}.xls"))
                {
                    ExportPropsXlsStart(testProtokol);

                    foreach (var example in tests)
                    {
                        foreach (var impLine in example.BuildImportXlsString(testPeriod, testRuleset, prevRuleset))
                        {
                            testProtokol.WriteLine(impLine);
                        }
                    }
                    ExportPropsEnd(testProtokol);
                }
                using (var testProtokol = CreateProtokolFile($"Optimula{TestCaseName}Import_{testPeriod.Year}.csv"))
                {
                    ExportPropsCsvStart(testProtokol);

                    foreach (var example in tests)
                    {
                        foreach (var impLine in example.BuildImportCsvString(testPeriod, testRuleset, prevRuleset))
                        {
                            testProtokol.WriteLine(impLine);
                        }
                    }
                    ExportPropsEnd(testProtokol);
                }
            }
            catch (Xunit.Sdk.XunitException e)
            {
                throw e;
            }
        }
        protected abstract string GetExampleOptResultsLine(OptimulaGenerator example, IPeriod period, IEnumerable<Result<ITermResult, ITermResultError>> results);
        protected abstract void ShoulBeValidTestCase(OptimulaGenerator example, IEnumerable<Result<ITermResult, ITermResultError>> results);

        public static string DecFormatDouble(decimal decValue)
        {
            //string resultText = string.Format("{0:N2}", decValue);
            string resultText = decValue.ToString("0.00");
            return resultText;
        }
        protected decimal GetDecResultSelect<T>(IEnumerable<ResultMonad.Result<ITermResult, HraveMzdy.Procezor.Service.Errors.ITermResultError>> res, OptimulaArticleConst artCode, Func<T, decimal> selVal)
            where T : class, ITermResult
        {
            decimal resultInit = default;
            var result = res.Where((e) => (e.IsSuccess && e.Value.Article.Value == (Int32)artCode)).Select((x) => (x.Value)).ToList();
            var resultList = result.Select((c) => (c as T));
            var resultVals = resultList.Aggregate(resultInit, (agr, x) => (agr + selVal(x)));
            return resultVals;
        }

        protected void ServiceExampleTest(OptimulaGenerator example, IPeriod testPeriod, Int32 testPeriodCode, Int32 prevPeriodCode)
        {
            output.WriteLine($"Test: {example.Name}, Number: {example.Number}");
            try
            {
                testPeriod.Code.Should().Be(testPeriodCode);

                var prevPeriod = PrevYear(testPeriod);
                prevPeriod.Code.Should().Be(prevPeriodCode);

                var testLegalResult = _leg.GetBundle(testPeriod);
                testLegalResult.IsSuccess.Should().Be(true);

                var testRuleset = testLegalResult.Value;

                var prevLegalResult = _leg.GetBundle(prevPeriod);
                prevLegalResult.IsSuccess.Should().Be(true);

                var prevRuleset = prevLegalResult.Value;

                var targets = example.BuildSpecTargets(testPeriod, testRuleset, prevRuleset);
                foreach (var (target, index) in targets.Select((item, index) => (item, index)))
                {
                    var targetValue = target as OptimulaTermTarget;
                    var articleSymbol = target.ArticleDescr();
                    var conceptSymbol = target.ConceptDescr();
                    output.WriteLine("Index: {0}; ART: {1}; CON: {2}; con: {3}; pos: {4}; var: {5}; Target: {6}", index, articleSymbol, conceptSymbol, target.Contract.Value, target.Position.Value, target.Variant.Value, targetValue.TargetMessage());
                }

                var initService = _sut.InitWithPeriod(testPeriod);
                initService.Should().BeTrue();

                var restService = _sut.GetResults(testPeriod, testRuleset, targets);
                restService.Count().Should().NotBe(0);

                output.WriteLine($"Result Test: {example.Name}, Number: {example.Number}");

                foreach (var (result, index) in restService.Select((item, index) => (item, index)))
                {
                    if (result.IsSuccess)
                    {
                        var resultValue = result.Value as OptimulaTermResult;
                        var articleSymbol = resultValue.ArticleDescr();
                        var conceptSymbol = resultValue.ConceptDescr();
                        output.WriteLine("Index: {0}; ART: {1}; CON: {2}; Result: {3}", index, articleSymbol, conceptSymbol, resultValue.ResultMessage());
                    }
                    else if (result.IsFailure)
                    {
                        var errorValue = result.Error;
                        var articleSymbol = errorValue.ArticleDescr();
                        var conceptSymbol = errorValue.ConceptDescr();
                        output.WriteLine("Index: {0}; ART: {1}; CON: {2}; Error: {3}", index, articleSymbol, conceptSymbol, errorValue.Description());
                    }
                }
                ShoulBeValidTestCase(example, restService);
            }
            catch (Xunit.Sdk.XunitException e)
            {
                throw e;
            }
        }
        protected void ServiceTemplateExampleTest(OptimulaGenerator example, IPeriod testPeriod, Int32 testPeriodCode, Int32 prevPeriodCode)
        {
            if (example.Id == 1)
            {
                string[] strHeaderRadkaPRAC = new string[] {
                    "EmployeeNumb",
                    "EmployeeName",
                    "PeriodName",
                    "IMP-WorkSheetHrs",
                    "IMP-WorkSheetDay",
                    "IMP-WorkAbsenHrs",
                    "IMP-WorkAbsenDay",
                    "IMP-OverSheetHrs",
                    "RES-AgrWorkPaymt",
                    "RES-AgrWorkHours",
                    "RES-ClothesPaymt",
                    "RES-HomeOffPaymt",
                    "IMP-MSalaryAward",
                    "RES-MSalaryAward",
                    "IMP-HSalaryAward",
                    "RES-HSalaryAward",
                    "IMP-FPremiumBase",
                    "RES-FPremiumBase",
                    "IMP-FPremiumBoss",
                    "RES-FPremiumBoss",
                    "IMP-FPremiumPers",
                    "RES-FPremiumPers",
                    "IMP-QAverageBase",
                    "IMP-AverPremsPay",
                    "IMP-AverVacasPay",
                    "IMP-AverOversPay",
                };
                using (var testProtokol = CreateProtokolFile($"OPTIMIT_{TestCaseName.ToUpper()}_TEST_{testPeriod.Year}_{testPeriod.Code}.CSV"))
                {
                    testProtokol.WriteLine(string.Join(";", strHeaderRadkaPRAC) + ";");
                }
            }
            output.WriteLine($"Test: {example.Name}, Number: {example.Number}");

            try
            {
                testPeriod.Code.Should().Be(testPeriodCode);

                var prevPeriod = PrevYear(testPeriod);
                prevPeriod.Code.Should().Be(prevPeriodCode);

                var testLegalResult = _leg.GetBundle(testPeriod);
                testLegalResult.IsSuccess.Should().Be(true);

                var testRuleset = testLegalResult.Value;

                var prevLegalResult = _leg.GetBundle(prevPeriod);
                prevLegalResult.IsSuccess.Should().Be(true);

                var prevRuleset = prevLegalResult.Value;

                var targets = example.BuildSpecTargets(testPeriod, testRuleset, prevRuleset);

                foreach (var (target, index) in targets.Select((item, index) => (item, index)))
                {
                    var articleSymbol = target.ArticleDescr();
                    var conceptSymbol = target.ConceptDescr();
                    output.WriteLine("Index: {0}; ART: {1}; CON: {2}; con: {3}; pos: {4}; var: {5}", index, articleSymbol, conceptSymbol, target.Contract.Value, target.Position.Value, target.Variant.Value);
                }

                var initService = _sut.InitWithPeriod(testPeriod);
                initService.Should().BeTrue();

                var restService = _sut.GetResults(testPeriod, testRuleset, targets);
                restService.Count().Should().NotBe(0);

                using (var testProtokol = OpenProtokolFile($"OPTIMIT_{TestCaseName.ToUpper()}_TEST_{testPeriod.Year}_{testPeriod.Code}.CSV"))
                {
                    var testResultLine = GetExampleOptResultsLine(example, testPeriod, restService);
                    testProtokol.WriteLine(testResultLine);
                }

                output.WriteLine($"Result Test: {example.Name}, Number: {example.Number}");

                foreach (var (result, index) in restService.Select((item, index) => (item, index)))
                {
                    if (result.IsSuccess)
                    {
                        var resultValue = result.Value as OptimulaTermResult;
                        var articleSymbol = resultValue.ArticleDescr();
                        var conceptSymbol = resultValue.ConceptDescr();
                        output.WriteLine("Index: {0}; ART: {1}; CON: {2}; Result: {3}", index, articleSymbol, conceptSymbol, resultValue.ResultMessage());
                    }
                    else if (result.IsFailure)
                    {
                        var errorValue = result.Error;
                        var articleSymbol = errorValue.ArticleDescr();
                        var conceptSymbol = errorValue.ConceptDescr();
                        output.WriteLine("Index: {0}; ART: {1}; CON: {2}; Error: {3}", index, articleSymbol, conceptSymbol, errorValue.Description());
                    }
                }
                ShoulBeValidTestCase(example, restService);
            }
            catch (Xunit.Sdk.XunitException e)
            {
                throw e;
            }
        }
    }
}

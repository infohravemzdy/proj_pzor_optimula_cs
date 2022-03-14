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
    [Collection("Non-Parallel")]
    public class ServiceTestPuzzleExampleTemplate
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

        protected readonly IServiceProcezor _sut;
        protected readonly IServiceLegalios _leg;

        protected static readonly OptimulaGenerator[] _genTests = new OptimulaGenerator[] {
            OptimulaPuzzleGenerator.Spec(1, "Dohoda_DPP_DPC_ZERO", "101").WithFullSheetHrsVal(168*60).WithTimeSheetHrsVal(168*60).WithWorkSheetHrsVal(168*60).WithWorkSheetDayVal(21*100)
                .WithFPremiumBaseVal( 7798*100).WithFPremiumPersVal(0*100)
                .WithHomeOffMonthVal(3947*100).WithClothesDailyVal(66*100).WithMealConDailyVal(82*100+60)
                .WithAgrWorkLimitVal(  860*100).WithAgrHourLimitVal( 5*60).WithAgrWorkTarifVal(170*100),
            OptimulaPuzzleGenerator.Spec(2, "Dohoda_DPP_DPC_VALS", "102").WithFullSheetHrsVal(168*60).WithTimeSheetHrsVal(168*60).WithWorkSheetHrsVal( 80*60).WithWorkSheetDayVal(10*100)
                .WithFPremiumBaseVal(14350*100).WithFPremiumPersVal(0*100)
                .WithHomeOffMonthVal(   0*100).WithClothesDailyVal(50*100).WithMealConDailyVal(82*100+60)
                .WithAgrWorkLimitVal(10000*100).WithAgrHourLimitVal(21*60).WithAgrWorkTarifVal(380*100)
                .WithAgtWorkLimitVal( 3499*100).WithAgtHourLimitVal(20*60).WithAgtWorkTarifVal(170*100),
        };

        public static IEnumerable<object[]> GetGenTestDecData(IEnumerable<OptimulaGenerator> tests, IPeriod testPeriod, Int32 testPeriodCode, Int32 prevPeriodCode)
        {
            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);

            return tests.Select((tt) => (new object[] { tt }));
        }

        public ServiceTestPuzzleExampleTemplate(ITestOutputHelper output)
        {
            this.output = output;

            this._sut = new ServiceOptimulaPuzzle();
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
        protected static void ExportPropsXlsStart(StreamWriter protokol)
        {
            string[] headerList = new string[]
            {
                "EmployeeNumb", // A//   1  //Evideční číslo  	101
                "EmployeeName", // B//   2  //Jméno a příjmení 	Drahota Jakub
                "PeriodName",   // C//   3  //Mzdové období 	    202201
                "FPremiumBase", // D//   4  //Celková částka v čistém
                "FPremiumPers", // E//   5  //ODMĚNY
                "ClothesDaily", // F//   6  //Ošatné/den
                "HomeOffMonth", // G//   7  //Home office/měs.
                "MealConDaily", // H//   8  //Strav.paušál/den
                "AgrWorkLimit", // I//   9  //DPP/měs.-základní
                "AgrHourLimit", // J//  10  //DPP hodiny/měs.-základní
                "AgrWorkTarif", // K//  11  //Sazba DPP/hod
                "AgtWorkLimit", // L//  12  //DPČ/měs.-základní
                "AgtHourLimit", // M//  13  //DPČ hodiny/měs.-základní
                "AgtWorkTarif", // N//  14  //Sazba DPČ/hod
                "WorkSheetHrs", // O//  15  //Odpracované dny
                "WorkSheetDay", // P//  16  //Odpracované hodiny
                "TimeSheetHrs", // Q//  17  //Fond
            };                     
            protokol.WriteLine(string.Join('\t', headerList));
        }
        protected static void ExportPropsCsvStart(StreamWriter protokol)
        {
            string[] headerList = new string[]
            {
                "EmployeeNumb", // A//   1  //Evideční číslo  	101
                "EmployeeName", // B//   2  //Jméno a příjmení 	Drahota Jakub
                "PeriodName",   // C//   3  //Mzdové období 	    202201
                "FPremiumBase", // D//   4  //Celková částka v čistém
                "FPremiumPers", // E//   5  //ODMĚNY
                "ClothesDaily", // F//   6  //Ošatné/den
                "HomeOffMonth", // G//   7  //Home office/měs.
                "MealConDaily", // H//   8  //Strav.paušál/den
                "AgrWorkLimit", // I//   9  //DPP/měs.-základní
                "AgrHourLimit", // J//  10  //DPP hodiny/měs.-základní
                "AgrWorkTarif", // K//  11  //Sazba DPP/hod
                "AgtWorkLimit", // L//  12  //DPČ/měs.-základní
                "AgtHourLimit", // M//  13  //DPČ hodiny/měs.-základní
                "AgtWorkTarif", // N//  14  //Sazba DPČ/hod
                "WorkSheetHrs", // O//  15  //Odpracované dny
                "WorkSheetDay", // P//  16  //Odpracované hodiny
                "TimeSheetHrs", // Q//  17  //Fond
            };                     
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

                using (var testProtokol = CreateProtokolFile($"OptimulaImport_{testPeriod.Year}.xls"))
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
                using (var testProtokol = CreateProtokolFile($"OptimulaImport_{testPeriod.Year}.csv"))
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
        protected string GetExampleOptResultsLine(OptimulaGenerator example, IPeriod period, IEnumerable<ResultMonad.Result<ITermResult, HraveMzdy.Procezor.Service.Errors.ITermResultError>> results)
        {
            decimal IMP_WORKSHEETHRS = GetDecResultSelect<TimeactualWorkResult>(results,
                OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK, (x) => (x.WorkSheetHrsVal)); 
            decimal IMP_WORKSHEETDAY = GetDecResultSelect<TimeactualWorkResult>(results,
                OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK, (x) => (x.WorkSheetDayVal)); 
            decimal RES_AGRWORKPAYMT = GetDecResultSelect<AgrworkHoursResult>(results,
                OptimulaArticleConst.ARTICLE_AGRWORK_TARGETS, (x) => (x.ResultValue)); 
            decimal RES_AGRWORKHOURS = GetDecResultSelect<AgrworkHoursResult>(results,
                OptimulaArticleConst.ARTICLE_AGRWORK_TARGETS, (x) => (x.AgrResultsHours)); 
            decimal RES_AGRTASKPAYMT = GetDecResultSelect<AgrtaskHoursResult>(results,
                OptimulaArticleConst.ARTICLE_AGRTASK_TARGETS, (x) => (x.ResultValue)); 
            decimal RES_AGRTASKHOURS = GetDecResultSelect<AgrtaskHoursResult>(results,
                OptimulaArticleConst.ARTICLE_AGRTASK_TARGETS, (x) => (x.AgrResultsHours)); 
            decimal RES_CLOTDAYPAYMT = GetDecResultSelect<AllowceDailyResult>(results,
                OptimulaArticleConst.ARTICLE_ALLOWCE_CLOTDAY, (x) => (x.ResultValue)); 
            decimal RES_MEALDAYPAYMT = GetDecResultSelect<AlldownDailyResult>(results,
                OptimulaArticleConst.ARTICLE_ALLOWCE_MEALDAY, (x) => (x.ResultValue)); 
            decimal RES_HOMEOFFPAYMT = GetDecResultSelect<AllowceMfullResult>(results,
                OptimulaArticleConst.ARTICLE_ALLOWCE_HOFFICE, (x) => (x.ResultValue)); 
            decimal IMP_FPREMIUMBASE = GetDecResultSelect<OptimusNettoResult>(results,
                OptimulaArticleConst.ARTICLE_PREMIUM_TARGETS, (x) => (x.OptimusBasisVal)); 
            decimal RES_FPREMIUMBASE = GetDecResultSelect<ReducedNettoResult>(results,
                OptimulaArticleConst.ARTICLE_PREMIUM_RESULTS, (x) => (x.ResultValue)); 
            decimal RES_ALLOWCENETTO = GetDecResultSelect<SettlemAllnettResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_ALLNETT, (x) => (x.ResultValue)); 
            decimal RES_TARGETSNETTO = GetDecResultSelect<SettlemTarnettResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_TARNETT, (x) => (x.ResultValue)); 
            decimal RES_AGRWORKGROSS = GetDecResultSelect<SettlemAgrworkResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK, (x) => (x.ResultValue)); 
            decimal RES_AGRTASKGROSS = GetDecResultSelect<SettlemAgrtaskResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_AGRTASK, (x) => (x.ResultValue)); 
            decimal RES_SETTLEMNETTO = RES_ALLOWCENETTO + OperationsRound.RoundToInt(OperationsDec.Multiply(RES_AGRWORKGROSS + RES_AGRTASKGROSS, 0.85m));
            decimal RES_RESULTSNETTO = GetDecResultSelect<SettlemResnettResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_RESNETT, (x) => (x.ResultValue)); 

            string[] resultLine = new string[]
            {
                example.Number,             //"Evidenční číslo",
                example.Name,               //"Jméno zaměstnance",
                period.Code.ToString(),     //"Mzdové období",
                DecFormatDouble(IMP_WORKSHEETHRS), //"Odprac.hodiny",
                DecFormatDouble(IMP_WORKSHEETDAY), //"Odprac.dny",
                DecFormatDouble(RES_CLOTDAYPAYMT), //"Ošatné/měs.",
                DecFormatDouble(RES_HOMEOFFPAYMT), //"Home office/měs.",
                DecFormatDouble(RES_MEALDAYPAYMT), //"Strav. paušál/měs.",
                DecFormatDouble(RES_AGRWORKPAYMT), //"DPP/měs.",
                DecFormatDouble(RES_AGRWORKHOURS), //"DPP hodiny/měs.",
                DecFormatDouble(RES_AGRTASKPAYMT), //"DPČ/měs.",
                DecFormatDouble(RES_AGRTASKHOURS), //"DPČ hodiny/měs.",
                DecFormatDouble(RES_TARGETSNETTO), //"CELKEM ČISTÉHO K VYPLACENÍ",
                DecFormatDouble(RES_SETTLEMNETTO), //"ČISTÉHO K VÝPLATĚ CELKEM",
                DecFormatDouble(RES_RESULTSNETTO), //"ROZDÍL V ČISTÉM",
            };

            return string.Join(";", resultLine) + ";";
        }

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
            var resultValue = result.Select((c) => (c as T))
                .Aggregate(resultInit, (agr, x) => (agr + selVal(x)));
            return resultValue;
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
                    "Evidenční číslo",
                    "Jméno zaměstnance",
                    "Období",
                    "Odprac.hodiny",
                    "Odprac.dny",
                    "Ošatné/měs.",
                    "Home office/měs.",
                    "Strav. paušál/měs.",
                    "DPP/měs.",
                    "DPP hodiny/měs.",
                    "DPČ/měs.",
                    "DPČ hodiny/měs.",
                    "CELKEM ČISTÉHO K VYPLACENÍ",
                    "ČISTÉHO K VÝPLATĚ CELKEM",
                    "ROZDÍL V ČISTÉM",
                };
                using (var testProtokol = CreateProtokolFile($"OPTIMIT_PUZZLE_TEST_{testPeriod.Year}_{testPeriod.Code}.CSV"))
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

                using (var testProtokol = OpenProtokolFile($"OPTIMIT_PUZZLE_TEST_{testPeriod.Year}_{testPeriod.Code}.CSV"))
                {
                    var testResults = GetExampleOptResultsLine(example, testPeriod, restService);
                    testProtokol.WriteLine(testResults);
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
            }
            catch (Xunit.Sdk.XunitException e)
            {
                throw e;
            }
        }

        public OptimulaGenerator Example_1_Dohoda_DPP_DPC_ZERO()
        {
            return OptimulaPuzzleGenerator.Spec(1, "Dohoda_DPP_DPC_ZERO", "101")
                .WithFPremiumBaseVal(7798 * 100).WithFPremiumPersVal(0 * 100)
                .WithFullSheetHrsVal(168 * 60).WithTimeSheetHrsVal(168 * 60).WithWorkSheetHrsVal(168 * 60).WithWorkSheetDayVal(21 * 100)
                .WithHomeOffMonthVal(3947 * 100).WithClothesDailyVal(66 * 100).WithMealConDailyVal(82 * 100 + 60)
                .WithAgrWorkLimitVal(860 * 100).WithAgrHourLimitVal(5 * 60).WithAgrWorkTarifVal(170 * 100);
        }
        public OptimulaGenerator Example_2_Dohoda_DPP_DPC_VALS()
        {
            return OptimulaPuzzleGenerator.Spec(2, "Dohoda_DPP_DPC_VALS", "102")
                .WithFPremiumBaseVal(14350 * 100).WithFPremiumPersVal(0 * 100)
                .WithFullSheetHrsVal(168 * 60).WithTimeSheetHrsVal(168 * 60).WithWorkSheetHrsVal(80 * 60).WithWorkSheetDayVal(10 * 100)
                .WithHomeOffMonthVal(0 * 100).WithClothesDailyVal(50 * 100).WithMealConDailyVal(82 * 100 + 60)
                .WithAgrWorkLimitVal(10000 * 100).WithAgrHourLimitVal(21 * 60).WithAgrWorkTarifVal(380 * 100)
                .WithAgtWorkLimitVal(3499 * 100).WithAgtHourLimitVal(20 * 60).WithAgtWorkTarifVal(170 * 100);
        }
    }
}

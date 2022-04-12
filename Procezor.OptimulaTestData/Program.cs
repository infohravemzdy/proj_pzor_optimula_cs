using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Procezor.OptimulaTestData
{
    class TestSpec
    {
        public string TestCase { get; private set; }
        public string TestName { get; private set; }

        public decimal ParseTestCase(string val)
        {
            TestCase = val;
            return 0m;
        }
        public decimal ParseTestName(string val)
        {
            TestName = val;
            return 0m;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);

            const string TEST_FOLDER_NAME = "test_data";
            string testFolderNameFull = ExecutableTestFolder(TEST_FOLDER_NAME);
            Console.WriteLine(testFolderNameFull);

            const string TEST_TARGET_FILE_NAME = "TestPuzzle-ExportVstupy-202001.csv";
            string testTargetFileNameFull = Path.Combine(testFolderNameFull, TEST_TARGET_FILE_NAME);
            const string TEST_RESULT_FILE_NAME = "TestPuzzle-ExportVysledky-202001.csv";
            string testResultFileNameFull = Path.Combine(testFolderNameFull, TEST_RESULT_FILE_NAME);
            const string PROT_FILE_NAME = "TestPuzzle-CodeLines-202001.txt";
            string testProtNameFull = Path.Combine(testFolderNameFull, PROT_FILE_NAME);

            var testTargetList = new List<Tuple<string, string, string[]>>();
            var testResultList = new List<Tuple<string, string, string[]>>();

            using (var streamTargets = new FileStream(testTargetFileNameFull, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var testTargets = new StreamReader(streamTargets, Encoding.GetEncoding("windows-1250")))
                {
                    while (testTargets.EndOfStream == false)
                    {
                        string targetString = testTargets.ReadLine();
                        string[] targetDefValues = targetString.Split(';');

                        if (targetDefValues.Length == 0)
                        {
                            continue;
                        }
                        if (targetDefValues.Length > 0 && targetDefValues[0] == "EmployeeNumb")
                        {
                            continue;
                        }
                        TestSpec testSpec = new TestSpec();
                        Func<string, decimal>[] targetParser = new Func<string, decimal>[]
                        {
                            testSpec.ParseTestCase, //Evideční číslo  	101
                            testSpec.ParseTestName, //Jméno a příjmení 	Drahota Jakub
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                            ParseDecimal,
                        };
                        decimal[] targetDecValues = targetDefValues.Zip(targetParser).Select((x) => x.Second(x.First)).ToArray();
                        Func<decimal, string>[] targetGenerator = new Func<decimal, string>[]
                        {
                            CodeTargetStarts,          // A0 Evideční číslo  	101
                            WithNADecimalVal,          // B0 Jméno a příjmení 	Drahota Jakub
                            WithTargetTestsPeriodCode, // C0 Mzdové období 	    202201
                            WithTargetAgrWorkRatioVal, // D0 AgrWorkRatio	0,14
                            WithTargetAgrWorkMaximVal, // E0 AgrWorkLimit	0,00
                            WithTargetAgrWorkTarifVal, // F0 AgrWorkTarif	105,00
                            WithTargetAgrWorkLimitVal, // G0 AgrHourLimit	0,00
                            WithTargetAgrWorkHoursVal, // H0 
                            WithTargetAgrTaskRatioVal, // I0 
                            WithTargetAgrTaskMaximVal, // J0 
                            WithTargetAgrTaskTarifVal, // K0 
                            WithTargetAgrTaskLimitVal, // L0 
                            WithTargetAgrTaskHoursVal, // M0 
                            WithTargetClothesHoursVal, // N0 ClothesHours	11,17
                            WithTargetClothesDailyVal, // O0 ClothesDaily	106,00
                            WithTargetMealConDailyVal, // P0 
                            WithTargetHomeOffMonthVal, // Q0 
                            WithTargetHomeOffTarifVal, // R0 HomeOffTarif	0,00
                            WithTargetHomeOffHoursVal, // S0 HomeOffHours	0,00
                            WithTargetMSalaryAwardVal, // T0 MSalaryAward	8 000,00
                            WithTargetHSalaryAwardVal, // U0 HSalaryAward	0,00
                            WithTargetFPremiumBaseVal, // V0 FPremiumBase	0,00
                            WithTargetFPremiumBossVal, // W0 FPremiumBoss	0,00
                            WithTargetFPremiumPersVal, // X0 FPremiumPers	0,00
                            WithNADecimalVal,     // Y0  ----
                            WithNADecimalVal,     // Z0  ----
                            WithTargetFullSheetHrsVal, // AA FullSheetHrs	176,00
                            WithTargetTimeSheetHrsVal, // AB TimeSheetHrs	176,00
                            WithTargetHoliSheetHrsVal, // AC HoliSheetHrs	0,00
                            WithTargetWorkSheetHrsVal, // AD WorkSheetHrs	96,00
                            WithTargetWorkSheetDayVal, // AE WorkSheetDay	12,00
                            WithTargetOverSheetHrsVal, // AF OverSheetHrs	40,00
                            WithTargetVacaRecomHrsVal, // AG VacaRecomHrs	80,00
                            WithTargetPaidRecomHrsVal, // AH PaidRecomHrs	0,00
                            WithTargetHoliRecomHrsVal, // AI HoliRecomHrs	0,00
                            WithTargetOverAllowHrsVal, // AJ OverAllowHrs	40,00
                            WithTargetOverAllowRioVal, // AK OverAllowRio	0,25
                            WithTargetRestAllowHrsVal, // AL RestAllowHrs	0,00
                            WithTargetRestAllowRioVal, // AM RestAllowRio	0,00
                            WithTargetWendAllowHrsVal, // AN WendAllowHrs	0,00
                            WithTargetWendAllowRioVal, // AO WendAllowRio	0,00
                            WithTargetNighAllowHrsVal, // AP NighAllowHrs	18,25
                            WithTargetNighAllowRioVal, // AQ NighAllowRio	0,10
                            WithTargetHoliAllowHrsVal, // AR HoliAllowHrs	0,00
                            WithTargetHoliAllowRioVal, // AS HoliAllowRio	0,00
                            WithTargetQClothesBaseVal, // AT QClothesBase	3 506,00
                            WithTargetQHOfficeBaseVal, // AU QHOfficeBase	0,00
                            WithTargetQAgrWorkBaseVal, // AV QAgrWorkBase	8 852,00
                            WithTargetQSumWorkHourVal, // AW QSumWorkHour	912,08
                        };               
                        var targetCodeLines = targetDecValues.Zip(targetGenerator)
                            .Select((x) => x.Second(x.First))
                            .Where((x) => (string.IsNullOrEmpty(x) == false));

                        testTargetList.Add(new Tuple<string, string, string[]>(testSpec.TestCase, testSpec.TestName, 
                            targetCodeLines.Where((x) => (string.IsNullOrEmpty(x) == false)).ToArray()));
                    }
                }
            }

            using (var streamResults = new FileStream(testResultFileNameFull, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var testResults = new StreamReader(streamResults, Encoding.GetEncoding("windows-1250")))
                {
                    while (testResults.EndOfStream == false)
                    {
                        string resultString = testResults.ReadLine();
                        string[] resultDefValues = resultString.Split(';');

                        if (resultDefValues.Length == 0)
                        {
                            continue;
                        }
                        if (resultDefValues.Length > 0 && resultDefValues[0] == "EmployeeNumb")
                        {
                            continue;
                        }
                        TestSpec testSpec = new TestSpec();
                        Func<string, decimal>[] resultParser = new Func<string, decimal>[]
                        {
                        testSpec.ParseTestCase, //Evideční číslo  	101
                        testSpec.ParseTestName, //Jméno a příjmení 	Drahota Jakub
                        ParseDecimal,   //Mzdové období 	202201
                        ParseDecimal,   //IMP-WorkSheetHrs
                        ParseDecimal,   //IMP-WorkSheetDay
                        ParseDecimal,   //IMP-WotkAbsenHrs
                        ParseDecimal,   //IMP-WotkAbsenDay
                        ParseDecimal,   //IMP-OverSheetHrs
                        ParseDecimal,   //RES-AgrWorkPaymt
                        ParseDecimal,   //RES-AgrWorkHours
                        ParseDecimal,   //RES-AgtWorkPaymt
                        ParseDecimal,   //RES-AgtWorkHours
                        ParseDecimal,   //RES-ClothesPaymt
                        ParseDecimal,   //RES-MealConPaymt
                        ParseDecimal,   //RES-HomeOffPaymt
                        ParseDecimal,   //IMP-MSalaryAward
                        ParseDecimal,   //RES-MSalaryAward
                        ParseDecimal,   //IMP-HSalaryAward
                        ParseDecimal,   //RES-HSalaryAward
                        ParseDecimal,   //IMP-FPremiumBase
                        ParseDecimal,   //RES-FPremiumBase
                        ParseDecimal,   //IMP-FPremiumBoss
                        ParseDecimal,   //RES-FPremiumBoss
                        ParseDecimal,   //IMP-FPremiumPers
                        ParseDecimal,   //RES-FPremiumPers
                        ParseDecimal,   //IMP-QAverageBase
                        ParseDecimal,   //IMP-AverPremsPay
                        ParseDecimal,   //IMP-AverVacasPay
                        ParseDecimal,   //IMP-AverOversPay
                        ParseDecimal,   //RES-IncomesNetto
                        ParseDecimal,   //RES-PaymentNetto
                        ParseDecimal,   //RES-DiffValNetto
                        };
                        decimal[] resultDecValues = resultDefValues.Zip(resultParser).Select((x) => x.Second(x.First)).ToArray();

                        Func<decimal, string>[] resultGenerator = new Func<decimal, string>[]
                        {
                        CodeResultStarts,          //Evideční číslo  	101
                        WithNADecimalVal,          //Jméno a příjmení 	Drahota Jakub
                        WithResultTestsPeriodCode,   //Mzdové období 	    202201
                        WithResultImpWorkSheetHrs,   //IMP-WorkSheetHrs
                        WithResultImpWorkSheetDay,   //IMP-WorkSheetDay
                        WithResultImpWotkAbsenHrs,   //IMP-WotkAbsenHrs
                        WithResultImpWotkAbsenDay,   //IMP-WotkAbsenDay
                        WithResultImpOverSheetHrs,   //IMP-OverSheetHrs
                        WithResultResAgrWorkPaymt,   //RES-AgrWorkPaymt
                        WithResultResAgrWorkHours,   //RES-AgrWorkHours
                        WithResultResAgrTaskPaymt,   //RES-AgtWorkPaymt
                        WithResultResAgrTaskHours,   //RES-AgtWorkHours
                        WithResultResClothesPaymt,   //RES-ClothesPaymt
                        WithResultResMealConPaymt,   //RES-MealConPaymt
                        WithResultResHomeOffPaymt,   //RES-HomeOffPaymt
                        WithResultImpMSalaryAward,   //IMP-MSalaryAward
                        WithResultResMSalaryAward,   //RES-MSalaryAward
                        WithResultImpHSalaryAward,   //IMP-HSalaryAward
                        WithResultResHSalaryAward,   //RES-HSalaryAward
                        WithResultImpFPremiumBase,   //IMP-FPremiumBase
                        WithResultResFPremiumBase,   //RES-FPremiumBase
                        WithResultImpFPremiumBoss,   //IMP-FPremiumBoss
                        WithResultResFPremiumBoss,   //RES-FPremiumBoss
                        WithResultImpFPremiumPers,   //IMP-FPremiumPers
                        WithResultResFPremiumPers,   //RES-FPremiumPers
                        WithResultImpQAverageBase,   //IMP-QAverageBase
                        WithResultImpAverPremsPay,   //IMP-AverPremsPay
                        WithResultImpAverVacasPay,   //IMP-AverVacasPay
                        WithResultImpAverOversPay,   //IMP-AverOversPay
                        WithResultResIncomesNetto,   //RES-IncomesNetto
                        WithResultResPaymentNetto,   //RES-PaymentNetto
                        WithResultResDiffValNetto,   //RES-DiffValNetto
                        };
                        var resultCodeLines = resultDecValues.Zip(resultGenerator).Select((x) => x.Second(x.First)).ToArray();

                        testResultList.Add(new Tuple<string, string, string[]>(testSpec.TestCase, testSpec.TestName, 
                            resultCodeLines.Where((x) => (string.IsNullOrEmpty(x)==false)).ToArray()));
                    }
                }
            }
            using (var streamProtokol = new FileInfo(testProtNameFull).Create())
            {
                var testCaseList = testTargetList.Zip(testResultList).Select((x, idx) => 
                    (idx + 1, x.First.Item1, x.First.Item2, x.First.Item3, x.Second.Item3)).ToArray();

                using (var testProtokol = new StreamWriter(streamProtokol, System.Text.Encoding.GetEncoding("windows-1250")))
                {
                    foreach (var testCase in testCaseList)
                    {
                        testProtokol.WriteLine($"OptimulaPuzzleGenerator.Spec({testCase.Item1}, \"{testCase.Item3}\", \"{testCase.Item2}\")");

                        testProtokol.WriteLine(string.Join('\n', testCase.Item4));
                        testProtokol.WriteLine(string.Join('\n', testCase.Item5) + ",");
                    }
                }
            }
        }

        private static string CodeTargetStarts(decimal val)
        {
            return "// Begin Test's Targets";
        }
        private static string CodeResultStarts(decimal val)
        {
            return "// Begin Test's Results";
        }
        private static string WithNADecimalVal(decimal val)
        {
            return "";
        }
        private static string WithTargetTestsPeriodCode(decimal val) { return ""; }
        private static string WithResultTestsPeriodCode(decimal val) { return ""; }
        private static string WithResultImpWorkSheetHrs(decimal val) { return TestResultCode(".WithTestImpWorkSheetHrs", val); }
        private static string WithResultImpWorkSheetDay(decimal val) { return TestResultCode(".WithTestImpWorkSheetDay", val); }
        private static string WithResultImpWotkAbsenHrs(decimal val) { return TestResultCode(".WithTestImpWotkAbsenHrs", val); }
        private static string WithResultImpWotkAbsenDay(decimal val) { return TestResultCode(".WithTestImpWotkAbsenDay", val); }
        private static string WithResultImpOverSheetHrs(decimal val) { return TestResultCode(".WithTestImpOverSheetHrs", val); }
        private static string WithResultResAgrWorkPaymt(decimal val) { return TestResultCode(".WithTestResAgrWorkPaymt", val); }
        private static string WithResultResAgrWorkHours(decimal val) { return TestResultCode(".WithTestResAgrWorkHours", val); }
        private static string WithResultResAgrTaskPaymt(decimal val) { return TestResultCode(".WithTestResAgrTaskPaymt", val); }
        private static string WithResultResAgrTaskHours(decimal val) { return TestResultCode(".WithTestResAgrTaskHours", val); }
        private static string WithResultResClothesPaymt(decimal val) { return TestResultCode(".WithTestResClothesPaymt", val); }
        private static string WithResultResMealConPaymt(decimal val) { return TestResultCode(".WithTestResMealConPaymt", val); }
        private static string WithResultResHomeOffPaymt(decimal val) { return TestResultCode(".WithTestResHomeOffPaymt", val); }
        private static string WithResultImpMSalaryAward(decimal val) { return TestResultCode(".WithTestImpMSalaryAward", val); }
        private static string WithResultResMSalaryAward(decimal val) { return TestResultCode(".WithTestResMSalaryAward", val); }
        private static string WithResultImpHSalaryAward(decimal val) { return TestResultCode(".WithTestImpHSalaryAward", val); }
        private static string WithResultResHSalaryAward(decimal val) { return TestResultCode(".WithTestResHSalaryAward", val); }
        private static string WithResultImpFPremiumBase(decimal val) { return TestResultCode(".WithTestImpFPremiumBase", val); }
        private static string WithResultResFPremiumBase(decimal val) { return TestResultCode(".WithTestResFPremiumBase", val); }
        private static string WithResultImpFPremiumBoss(decimal val) { return TestResultCode(".WithTestImpFPremiumBoss", val); }
        private static string WithResultResFPremiumBoss(decimal val) { return TestResultCode(".WithTestResFPremiumBoss", val); }
        private static string WithResultImpFPremiumPers(decimal val) { return TestResultCode(".WithTestImpFPremiumPers", val); }
        private static string WithResultResFPremiumPers(decimal val) { return TestResultCode(".WithTestResFPremiumPers", val); }
        private static string WithResultImpQAverageBase(decimal val) { return TestResultCode(".WithTestImpQAverageBase", val); }
        private static string WithResultImpAverPremsPay(decimal val) { return TestResultCode(".WithTestImpAverPremsPay", val); }
        private static string WithResultImpAverVacasPay(decimal val) { return TestResultCode(".WithTestImpAverVacasPay", val); }
        private static string WithResultImpAverOversPay(decimal val) { return TestResultCode(".WithTestImpAverOversPay", val); }
        private static string WithResultResIncomesNetto(decimal val) { return TestResultCode(".WithTestResIncomesNetto", val, true); }
        private static string WithResultResPaymentNetto(decimal val) { return TestResultCode(".WithTestResPaymentNetto", val, true); }
        private static string WithResultResDiffValNetto(decimal val) { return TestResultCode(".WithTestResDiffValNetto", val, true); }

        private static string TestResultCode(string function, decimal val, bool always = false) { 
            if (always == false && val == 0)
            {
                return "";
            }
            var valCode = $"{val}".Replace(',', '.'); 
            return $"{function}({valCode}m)"; 
        }

        private static string WithTargetAgrWorkRatioVal(decimal val) { return TestTargetX100(".WithAgrWorkRatioVal", val); }//   5  AgrWorkRatio	0,14
        private static string WithTargetAgrWorkMaximVal(decimal val) { return TestTargetX100(".WithAgrWorkMaximVal", val); }//   5  AgrWorkMaxim	0,00
        private static string WithTargetAgrWorkTarifVal(decimal val) { return TestTargetX100(".WithAgrWorkTarifVal", val); }//   4  AgrWorkTarif	105,00
        private static string WithTargetAgrWorkLimitVal(decimal val) { return TestTargetX100(".WithAgrWorkLimitVal", val); }//   7  AgrWorkLimit	0,00
        private static string WithTargetAgrWorkHoursVal(decimal val) { return TestTargetX060(".WithAgrWorkHoursVal", val); }//   6  AgrWorkHours	0,00
        private static string WithTargetAgrTaskRatioVal(decimal val) { return TestTargetX100(".WithAgrTaskRatioVal", val); }//   5  AgrTaskRatio	0,14
        private static string WithTargetAgrTaskMaximVal(decimal val) { return TestTargetX100(".WithAgrTaskMaximVal", val); }//   5  AgrTaskMaxim	0,00
        private static string WithTargetAgrTaskTarifVal(decimal val) { return TestTargetX100(".WithAgrTaskTarifVal", val); }//   4  AgrTaskTarif	105,00
        private static string WithTargetAgrTaskLimitVal(decimal val) { return TestTargetX100(".WithAgrTaskLimitVal", val); }//   7  AgrTaskLimit	0,00
        private static string WithTargetAgrTaskHoursVal(decimal val) { return TestTargetX060(".WithAgrTaskHoursVal", val); }//   6  AgrTaskHours	0,00
        private static string WithTargetClothesHoursVal(decimal val) { return TestTargetX060(".WithClothesHoursVal", val); }//   8  ClothesHours	11,17
        private static string WithTargetClothesDailyVal(decimal val) { return TestTargetX100(".WithClothesDailyVal", val); }//   9  ClothesDaily	106,00
        private static string WithTargetMealConDailyVal(decimal val) { return TestTargetX100(".WithMealConDailyVal", val); }//   9  MealConDaily	80,60
        private static string WithTargetHomeOffMonthVal(decimal val) { return TestTargetX100(".WithHomeOffMonthVal", val); }//  10  HomeOffMonth	0,00
        private static string WithTargetHomeOffTarifVal(decimal val) { return TestTargetX100(".WithHomeOffTarifVal", val); }//  10  HomeOffTarif	0,00
        private static string WithTargetHomeOffHoursVal(decimal val) { return TestTargetX060(".WithHomeOffHoursVal", val); }//  11  HomeOffHours	0,00
        private static string WithTargetMSalaryAwardVal(decimal val) { return TestTargetX100(".WithMSalaryAwardVal", val); }//  12  MSalaryAward	8 000,00
        private static string WithTargetHSalaryAwardVal(decimal val) { return TestTargetX100(".WithHSalaryAwardVal", val); }//  13  HSalaryAward	0,00
        private static string WithTargetFPremiumBaseVal(decimal val) { return TestTargetX100(".WithFPremiumBaseVal", val); }//  14  FPremiumBase	0,00
        private static string WithTargetFPremiumBossVal(decimal val) { return TestTargetX100(".WithFPremiumBossVal", val); }//  15  FPremiumBoss	0,00
        private static string WithTargetFPremiumPersVal(decimal val) { return TestTargetX100(".WithFPremiumPersVal", val); }//  16  FPremiumPers	0,00
        private static string WithTargetFullSheetHrsVal(decimal val) { return TestTargetX060(".WithFullSheetHrsVal", val); }//  17  FullSheetHrs	176,00
        private static string WithTargetTimeSheetHrsVal(decimal val) { return TestTargetX060(".WithTimeSheetHrsVal", val); }//  18  TimeSheetHrs	176,00
        private static string WithTargetHoliSheetHrsVal(decimal val) { return TestTargetX060(".WithHoliSheetHrsVal", val); }//  19  HoliSheetHrs	0,00
        private static string WithTargetWorkSheetHrsVal(decimal val) { return TestTargetX060(".WithWorkSheetHrsVal", val); }//  20  WorkSheetHrs	96,00
        private static string WithTargetWorkSheetDayVal(decimal val) { return TestTargetX100(".WithWorkSheetDayVal", val); }//  21  WorkSheetDay	12,00
        private static string WithTargetOverSheetHrsVal(decimal val) { return TestTargetX060(".WithOverSheetHrsVal", val); }//  22  OverSheetHrs	40,00
        private static string WithTargetVacaRecomHrsVal(decimal val) { return TestTargetX060(".WithVacaRecomHrsVal", val); }//  23  VacaRecomHrs	80,00
        private static string WithTargetPaidRecomHrsVal(decimal val) { return TestTargetX060(".WithPaidRecomHrsVal", val); }//  24  PaidRecomHrs	0,00
        private static string WithTargetHoliRecomHrsVal(decimal val) { return TestTargetX060(".WithHoliRecomHrsVal", val); }//  25  HoliRecomHrs	0,00
        private static string WithTargetOverAllowHrsVal(decimal val) { return TestTargetX060(".WithOverAllowHrsVal", val); }//  27  OverAllowHrs	40,00
        private static string WithTargetOverAllowRioVal(decimal val) { return TestTargetX100(".WithOverAllowRioVal", val); }//  28  OverAllowRio	0,25
        private static string WithTargetRestAllowHrsVal(decimal val) { return TestTargetX060(".WithRestAllowHrsVal", val); }//  29  RestAllowHrs	0,00
        private static string WithTargetRestAllowRioVal(decimal val) { return TestTargetX100(".WithRestAllowRioVal", val); }//  30  RestAllowRio	0,00
        private static string WithTargetWendAllowHrsVal(decimal val) { return TestTargetX060(".WithWendAllowHrsVal", val); }//  31  WendAllowHrs	0,00
        private static string WithTargetWendAllowRioVal(decimal val) { return TestTargetX100(".WithWendAllowRioVal", val); }//  32  WendAllowRio	0,00
        private static string WithTargetNighAllowHrsVal(decimal val) { return TestTargetX060(".WithNighAllowHrsVal", val); }//  33  NighAllowHrs	18,25
        private static string WithTargetNighAllowRioVal(decimal val) { return TestTargetX100(".WithNighAllowRioVal", val); }//  34  NighAllowRio	0,10
        private static string WithTargetHoliAllowHrsVal(decimal val) { return TestTargetX060(".WithHoliAllowHrsVal", val); }//  35  HoliAllowHrs	0,00
        private static string WithTargetHoliAllowRioVal(decimal val) { return TestTargetX100(".WithHoliAllowRioVal", val); }//  36  HoliAllowRio	0,00
        private static string WithTargetQClothesBaseVal(decimal val) { return TestTargetX100(".WithQClothesBaseVal", val); }//  37  QClothesBase	3 506,00
        private static string WithTargetQHOfficeBaseVal(decimal val) { return TestTargetX100(".WithQHOfficeBaseVal", val); }//  38  QHOfficeBase	0,00
        private static string WithTargetQAgrWorkBaseVal(decimal val) { return TestTargetX100(".WithQAgrWorkBaseVal", val); }//  39  QAgrWorkBase	8 852,00
        private static string WithTargetQSumWorkHourVal(decimal val) { return TestTargetCode(".WithQSumWorkHourVal", val); }//  40  QSumWorkHour	912,08                                                                                                                                                  

        private static string TestTargetCode(string function, decimal val, bool always = false)
        {
            if (always == false && val == 0)
            {
                return "";
            }
            Int32 valParam = decimal.ToInt32(val);
            return $"{function}({valParam})";
        }

        private static string TestTargetX100(string function, decimal val, bool always = false)
        {
            if (always == false && val == 0)
            {
                return "";
            }
            Int32 valInt = decimal.ToInt32(val);
            Int32 val100 = decimal.ToInt32(val * 100);
            Int32 valDec = val100 - (valInt * 100);
            if (valDec != 0)
            {
                return $"{function}({valInt} * 100 + {valDec})";
            }
            return $"{function}({valInt} * 100)";
        }
        private static string TestTargetX060(string function, decimal val, bool always = false)
        {
            if (always == false && val == 0)
            {
                return "";
            }
            Int32 valInt = decimal.ToInt32(val);
            Int32 val060 = decimal.ToInt32(val * 60);
            Int32 valDec = val060 - (valInt * 60);
            if (valDec != 0)
            {
                return $"{function}({valInt} * 60 + {valDec})";
            }
            return $"{function}({valInt} * 60)";
        }

        private static decimal ParseDecimal(string valString)
        {
            if (valString.Trim().Equals(""))
            {
                return 0;
            }
            string numberToParse = valString.Replace('.', ',').TrimEnd('%').Replace("Kč", "").TrimEnd(' ');
            decimal numberValue = 0;
            try
            {
                numberValue = decimal.Parse(numberToParse);
            }
            catch (Exception e)
            {
            }
            return (numberValue);
        }

        private static string ExecutableTestFolder(string folderName)
        {
            const string PARENT_FOLDER_NAME = "Procezor.OptimulaTestData";

            string[] args = Environment.GetCommandLineArgs();

            string appExecutableFileNm = args[0];

            string currPath = Path.GetDirectoryName(appExecutableFileNm);
            if (string.IsNullOrEmpty(currPath))
            {
                return "";
            }
            int nameCount = currPath.Split(Path.DirectorySeparatorChar).Length;

            while (!currPath.EndsWith(PARENT_FOLDER_NAME) && nameCount != 1)
            {
                currPath = Path.GetDirectoryName(currPath);
            }
            string basePath = Path.Combine(currPath, folderName);
            if (nameCount <= 1)
            {
                basePath = Path.Combine(Path.GetFullPath("."), folderName);
            }
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            string normPath = Path.GetFullPath(basePath);

            return normPath;
        }
    }
}

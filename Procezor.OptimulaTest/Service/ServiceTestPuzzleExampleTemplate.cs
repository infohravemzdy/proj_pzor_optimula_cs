using System;
using System.Collections.Generic;
using HraveMzdy.Procezor.Optimula.Service;
using HraveMzdy.Procezor.Optimula.Registry.Constants;
using HraveMzdy.Procezor.Optimula.Registry.Providers;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Procezor.Generator;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service.Errors;
using HraveMzdy.Procezor.Service.Interfaces;
using Xunit.Abstractions;
using ResultMonad;
using FluentAssertions;

namespace Procezor.OptimulaTest.Service
{
    public class ServiceTestPuzzleExampleTemplate : ServiceTestExampleTemplate
    {
        protected static readonly OptimulaGenerator[] _genTests = new OptimulaGenerator[] {
            Example_1_Dohoda_DPP_DPC_ZERO(),
            Example_2_Dohoda_DPP_DPC_VALS(),
        };

        public ServiceTestPuzzleExampleTemplate(ITestOutputHelper output) : base(output, new ServiceOptimulaPuzzle(), "Puzzle")
        {
        }

        public static OptimulaGenerator Example_1_Dohoda_DPP_DPC_ZERO()
        {
            return OptimulaPuzzleGenerator.Spec(1, "Dohoda_DPP_DPC_ZERO", "101")
                // Begin Test's Targets
                .WithAgrWorkTarifVal(170 * 100)
                .WithAgrWorkLimitVal(860 * 100)
                .WithAgrWorkHoursVal(5 * 60)
                .WithClothesDailyVal(66 * 100)
                .WithMealConDailyVal(82 * 100 + 60)
                .WithHomeOffMonthVal(3947 * 100)
                .WithFPremiumBaseVal(7798 * 100)
                .WithFPremiumPersVal(0 * 100)
                .WithFullSheetHrsVal(168 * 60)
                .WithTimeSheetHrsVal(168 * 60)
                .WithWorkSheetHrsVal(168 * 60)
                .WithWorkSheetDayVal(21 * 100)
                // Begin Test's Results
                .WithTestImpWorkSheetHrs(168.00m)
                .WithTestImpWorkSheetDay(21.00m)
                .WithTestResAgrWorkPaymt(860.00m)
                .WithTestResAgrWorkHours(5.00m)
                .WithTestResClothesPaymt(1386.00m)
                .WithTestResMealConPaymt(1734.00m)
                .WithTestResHomeOffPaymt(3947.00m)
                .WithTestImpFPremiumBase(7798.00m)
                .WithTestResIncomesNetto(7798.00m)
                .WithTestResPaymentNetto(7798.00m)
                .WithTestResDiffValNetto(0.00m);

        }
        public static OptimulaGenerator Example_2_Dohoda_DPP_DPC_VALS()
        {
            return OptimulaPuzzleGenerator.Spec(2, "Dohoda_DPP_DPC_VALS", "102")
                // Begin Test's Targets
                .WithAgrWorkTarifVal(380 * 100)
                .WithAgrWorkLimitVal(10000 * 100)
                .WithAgrWorkHoursVal(21 * 60)
                .WithAgrTaskTarifVal(170 * 100)
                .WithAgrTaskLimitVal(3499 * 100)
                .WithAgrTaskHoursVal(20 * 60)
                .WithClothesDailyVal(50 * 100)
                .WithMealConDailyVal(82 * 100 + 60)
                .WithFPremiumBaseVal(14350 * 100)
                .WithTimeSheetHrsVal(168 * 60)
                .WithWorkSheetHrsVal(80 * 60)
                .WithWorkSheetDayVal(10 * 100)
                // Begin Test's Results
                .WithTestImpWorkSheetHrs(80.00m)
                .WithTestImpWorkSheetDay(10.00m)
                .WithTestResAgrWorkPaymt(10000.00m)
                .WithTestResAgrWorkHours(21.00m)
                .WithTestResAgrTaskPaymt(3499.00m)
                .WithTestResAgrTaskHours(20.00m)
                .WithTestResClothesPaymt(500.00m)
                .WithTestResMealConPaymt(826.00m)
                .WithTestImpFPremiumBase(14350.00m)
                .WithTestResFPremiumBase(1550.00m)
                .WithTestResIncomesNetto(14350.00m)
                .WithTestResPaymentNetto(12800.00m)
                .WithTestResDiffValNetto(1550.00m);
        }
        protected override string GetExampleOptResultsLine(OptimulaGenerator example, IPeriod period, IEnumerable<ResultMonad.Result<ITermResult, HraveMzdy.Procezor.Service.Errors.ITermResultError>> results)
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
        protected override void ShoulBeValidTestCase(OptimulaGenerator example, IEnumerable<Result<ITermResult, ITermResultError>> results)
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
            decimal IMP_FPREMEXTBASE = GetDecResultSelect<OptimusNettoResult>(results,
                OptimulaArticleConst.ARTICLE_PREMEXT_TARGETS, (x) => (x.OptimusBasisVal));
            decimal RES_FPREMEXTBASE = GetDecResultSelect<ReducedNettoResult>(results,
                OptimulaArticleConst.ARTICLE_PREMEXT_RESULTS, (x) => (x.ResultValue));
            decimal RES_ALLOWCENETTO = GetDecResultSelect<SettlemAllnettResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_ALLNETT, (x) => (x.ResultValue));
            decimal RES_TARGETSNETTO = GetDecResultSelect<SettlemTarnettResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_TARNETT, (x) => (x.ResultValue));
            decimal RES_AGRWORKGROSS = GetDecResultSelect<SettlemAgrworkResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK, (x) => (x.ResultValue));
            decimal RES_AGRTASKGROSS = GetDecResultSelect<SettlemAgrtaskResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_AGRTASK, (x) => (x.ResultValue));
            decimal RES_SETTLEMNETTO = RES_ALLOWCENETTO + OperationsRound.RoundToInt(
                OperationsDec.Multiply(RES_AGRWORKGROSS + RES_AGRTASKGROSS, 0.85m));
            decimal RES_RESULTSNETTO = GetDecResultSelect<SettlemResnettResult>(results,
                OptimulaArticleConst.ARTICLE_SETTLEM_RESNETT, (x) => (x.ResultValue));

            try
            {
                IMP_WORKSHEETHRS.Should().Be(example.TestImpWorkSheetHrs);
                IMP_WORKSHEETDAY.Should().Be(example.TestImpWorkSheetDay);
                RES_AGRWORKPAYMT.Should().Be(example.TestResAgrWorkPaymt);
                RES_AGRWORKHOURS.Should().Be(example.TestResAgrWorkHours);
                RES_AGRTASKPAYMT.Should().Be(example.TestResAgrTaskPaymt);
                RES_AGRTASKHOURS.Should().Be(example.TestResAgrTaskHours);
                RES_CLOTDAYPAYMT.Should().Be(example.TestResClothesPaymt);
                RES_MEALDAYPAYMT.Should().Be(example.TestResMealConPaymt);
                RES_HOMEOFFPAYMT.Should().Be(example.TestResHomeOffPaymt);
                IMP_FPREMIUMBASE.Should().Be(example.TestImpFPremiumBase);
                RES_FPREMIUMBASE.Should().Be(example.TestResFPremiumBase);
                IMP_FPREMEXTBASE.Should().Be(example.TestImpFPremiumPers);
                RES_FPREMEXTBASE.Should().Be(example.TestResFPremiumPers);
                RES_TARGETSNETTO.Should().Be(example.TestResIncomesNetto);
                RES_SETTLEMNETTO.Should().Be(example.TestResPaymentNetto);
                RES_RESULTSNETTO.Should().Be(example.TestResDiffValNetto);
            }
            catch (Xunit.Sdk.XunitException e)
            {
                throw e;
            }
        }
    }
}

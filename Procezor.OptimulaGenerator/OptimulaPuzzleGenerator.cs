using System;
using System.Collections.Generic;
using System.Linq;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Optimula.Registry.Constants;
using HraveMzdy.Procezor.Optimula.Registry.Providers;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Service.Types;

namespace HraveMzdy.Procezor.Generator
{
    public class OptimulaPuzzleGenerator : OptimulaGenerator
    {
        public OptimulaPuzzleGenerator(int id, string name, string number) : base(id, name, number)
        {
        }

        public static OptimulaPuzzleGenerator Spec(Int32 id, string name, string number)
        {
            return new OptimulaPuzzleGenerator(id, name, number);
        }
        public static OptimulaPuzzleGenerator ParseSpec(Int32 id, string specString)
        {
            string[] specDefValues = specString.Split(';');
            if (specDefValues.Length < 1)
            {
                return new OptimulaPuzzleGenerator(id, "Unknownn", "Unknownn");
            }
            if (specDefValues.Length < 2)
            {
                return new OptimulaPuzzleGenerator(id, "Unknownn", specDefValues[0]);
            }
            var gen = new OptimulaPuzzleGenerator(id, specDefValues[1], specDefValues[0]);

            Func<string, Int32>[] specParser = new Func<string, Int32>[]
            {
                ParseIntNumber,   //   1  //Evideční číslo  	101
                ParseNANothing,   //   2  //Jméno a příjmení 	Drahota Jakub
                ParseNANothing,   //   3  //Mzdové období 	    202201
                ParseDecNumber,   //   4  //Celková částka v čistém
                ParseDecNumber,   //   5  //ODMĚNY
                ParseDecNumber,   //   6  //Ošatné/den
                ParseDecNumber,   //   7  //Home office/měs.
                ParseDecNumber,   //   8  //Strav.paušál/den
                ParseDecNumber,   //   9  //DPP/měs.-základní
                ParseHrsNumber,   //  10  //DPP hodiny/měs.-základní
                ParseDecNumber,   //  11  //Sazba DPP/hod
                ParseDecNumber,   //  12  //DPČ/měs.-základní
                ParseHrsNumber,   //  13  //DPČ hodiny/měs.-základní
                ParseDecNumber,   //  14  //Sazba DPČ/hod
                ParseDecNumber,   //  15  //Odpracované dny
                ParseHrsNumber,   //  16  //Odpracované hodiny
                ParseHrsNumber,   //  17  //Fond
            };
            Int32[] specIntValues = specDefValues.Zip(specParser).Select((x) => x.Second(x.First)).ToArray();

            Func<Int32, OptimulaGenerator>[] specGenerator = new Func<Int32, OptimulaGenerator>[]
            {
                gen.WithNANothingVal,      //   1  //Evideční číslo  	101
                gen.WithNANothingVal,      //   2  //Jméno a příjmení 	Drahota Jakub
                gen.WithNANothingVal,      //   3  //Mzdové období 	    202201
                gen.WithFPremiumBaseVal,   //   4  //Celková částka v čistém
                gen.WithFPremiumPersVal,   //   5  //ODMĚNY
                gen.WithClothesDailyVal,   //   6  //Ošatné/den
                gen.WithHomeOffMonthVal,   //   7  //Home office/měs.
                gen.WithMealConDailyVal,   //   8  //Strav.paušál/den
                gen.WithAgrWorkLimitVal,   //   9  //DPP/měs.-základní
                gen.WithAgrWorkMaximVal,   //  10  //DPP hodiny/měs.-základní
                gen.WithAgrWorkTarifVal,   //  11  //Sazba DPP/hod
                gen.WithAgrTaskLimitVal,   //  12  //DPČ/měs.-základní
                gen.WithAgrTaskMaximVal,   //  13  //DPČ hodiny/měs.-základní
                gen.WithAgrTaskTarifVal,   //  14  //Sazba DPČ/hod
                gen.WithWorkSheetDayVal,   //  15  //Odpracované dny
                gen.WithWorkSheetHrsVal,   //  16  //Odpracované hodiny
                gen.WithTimeSheetHrsVal,   //  17  //Fond
            };
            specIntValues.Zip(specGenerator).Select((x) => x.Second(x.First)).ToArray();

            return gen;
        }
        public override IEnumerable<ITermTarget> BuildSpecTargets(IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            var montCode = MonthCode.Get(period.Code);

            Int16 CONTRACT_NULL = 0;
            Int16 POSITION_NULL = 0;

            var contractEmp = ContractCode.Get(CONTRACT_NULL);
            var positionEmp = PositionCode.Get(POSITION_NULL);
            var variant1Emp = VariantCode.Get(1);

            Int32 AgrWorkTarifVal = AgrWorkTarifFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(FPremiumBaseVal)}", // D//Celková částka v čistém 
            Int32 AgrWorkLimitVal = AgrWorkLimitFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(ClothesDailyVal)}", // F//Ošatné/den
            Int32 AgrWorkHoursVal = AgrWorkHoursFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(FPremiumPersVal)}", // E//ODMĚNY  
            Int32 AgrTaskTarifVal = AgrTaskTarifFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(HomeOffMonthVal)}", // G//Home office/měs.
            Int32 AgrTaskLimitVal = AgrTaskLimitFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(AgrWorkLimitVal)}", // I//DPP/měs.-základní 
            Int32 AgrTaskHoursVal = AgrTaskHoursFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(MealConDailyVal)}", // H//Strav.paušál/den
            Int32 ClothesDailyVal = ClothesDailyFunc(this, period, ruleset, prevset);//$"{HrsFormatIntX060(AgrHourLimitVal)}", // J//DPP hodiny/měs.-základní
            Int32 MealConDailyVal = MealConDailyFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(AgrWorkTarifVal)}", // K//Sazba DPP/hod
            Int32 HomeOffMonthVal = HomeOffMonthFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(AgtWorkLimitVal)}", // L//DPČ/měs.-základní
            Int32 FPremiumBaseVal = FPremiumBaseFunc(this, period, ruleset, prevset);//$"{HrsFormatIntX060(AgtHourLimitVal)}", // M  //DPČ hodiny/měs.-základní
            Int32 FPremiumPersVal = FPremiumPersFunc(this, period, ruleset, prevset);//$"{CcyFormatIntX100(AgtWorkTarifVal)}", // N  //Sazba DPČ/hod
            Int32 WorkSheetDayVal = WorkSheetDayFunc(this, period, ruleset, prevset);//$"{DayFormatIntX100(WorkSheetDayVal)}", // O  //Odpracované dny 
            Int32 WorkSheetHrsVal = WorkSheetHrsFunc(this, period, ruleset, prevset);//$"{HrsFormatIntX060(WorkSheetHrsVal)}", // P  //Odpracované hodiny  
            Int32 TimeSheetHrsVal = TimeSheetHrsFunc(this, period, ruleset, prevset);//$"{HrsFormatIntX060(TimeSheetHrsVal)}", // Q  //Fond

            // ContractTimePlan	CONTRACT_TIME_PLAN
            var contractTimePlan = new TimesheetsPlanTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_PLAN),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_TIMESHEETS_PLAN), TimeSheetHrsVal);
            // ContractTimeWork	CONTRACT_TIME_WORK
            var contractTimeWork = new TimesheetsWorkTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_TIMESHEETS_WORK), TimeSheetHrsVal, 0);
            // TimeactualWork	TIMEACTUAL_WORK
            var contractTimeActa = new TimeactualWorkTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_TIMEACTUAL_WORK), 
                WorkSheetHrsVal, WorkSheetDayVal, 0);
            // OptimusNetto		OPTIMUS_NETTO
            var optPremiumBase = new OptimusNettoTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMIUM_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_NETTO), 
                FPremiumBaseVal, FPremiumPersVal);
            // ReducedNetto		REDUCED_NETTO
            var redPremiumBase = new ReducedNettoTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMIUM_RESULTS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_REDUCED_NETTO), 
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMIUM_TARGETS));           
            // AgrworkHours		AGRWORK_HOURS
            var allowceAgrwork = new AgrworkHoursTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_AGRWORK_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_AGRWORK_HOURS), 
                AgrWorkTarifVal, 0, AgrWorkLimitVal, AgrWorkHoursVal);
            // AgtworkHours		AGRTASK_HOURS
            var allowceAgrtask = new AgrtaskHoursTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_AGRTASK_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_AGRTASK_HOURS), 
                AgrTaskTarifVal, 0, AgrTaskLimitVal, AgrTaskHoursVal);
            // AllowceHfull		ALLOWCE_HFULL
            var allowceHOffice = new AllowceMfullTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_ALLOWCE_HOFFICE),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_MFULL),
                HomeOffMonthVal);
            // AllowceDaily		ALLOWCE_DAILY
            var allowceClotDay = new AllowceDailyTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_ALLOWCE_CLOTDAY),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_DAILY),
                ClothesDailyVal);
            // AlldownDaily		ALLDOWN_DAILY
            var allowceMealDay = new AlldownDailyTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_ALLOWCE_MEALDAY),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_ALLDOWN_DAILY),
                MealConDailyVal);

            var targets = new ITermTarget[] {
                contractTimePlan,
                contractTimeWork,
                contractTimeActa,
            };

            if (FPremiumBaseVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { optPremiumBase, redPremiumBase }).ToArray();
            }
            if (AgrWorkTarifVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceAgrwork }).ToArray();
            }
            if (AgrTaskTarifVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceAgrtask }).ToArray();
            }
            if (HomeOffMonthVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceHOffice }).ToArray();
            }
            if (ClothesDailyVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceClotDay }).ToArray();
            }
            if (MealConDailyVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceMealDay }).ToArray();
            }

            return targets;
        }
    }
}

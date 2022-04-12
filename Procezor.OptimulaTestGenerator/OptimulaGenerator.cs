using System;
using System.Collections.Generic;
using System.Linq;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Procezor.Service.Interfaces;

namespace HraveMzdy.Procezor.Generator
{
    public abstract class OptimulaGenerator
    {
        public OptimulaGenerator(int id, string name, string number)
        {
            Id = id;
            Name = name;
            Number = number;
            ResultDescription = "";

            DefaultAgrWorkTarifValue = 0;
            DefaultAgrWorkRatioValue = 0;
            DefaultAgrWorkMaximValue = 0;
            DefaultAgrWorkLimitValue = 0;
            DefaultAgrTaskTarifValue = 0;
            DefaultAgrTaskRatioValue = 0;
            DefaultAgrTaskMaximValue = 0;
            DefaultAgrTaskLimitValue = 0;
            DefaultClothesHoursValue = 0;
            DefaultClothesDailyValue = 0;
            DefaultMealConDailyValue = 0;
            DefaultHomeOffMonthValue = 0;
            DefaultHomeOffTarifValue = 0;
            DefaultHomeOffHoursValue = 0;
            DefaultMSalaryAwardValue = 0;
            DefaultHSalaryAwardValue = 0;
            DefaultFPremiumBaseValue = 0;
            DefaultFPremiumBossValue = 0;
            DefaultFPremiumPersValue = 0;
            DefaultFullSheetHrsValue = 0;
            DefaultTimeSheetHrsValue = 0;
            DefaultHoliSheetHrsValue = 0;
            DefaultWorkSheetHrsValue = 0;
            DefaultWorkSheetDayValue = 0;
            DefaultOverSheetHrsValue = 0;
            DefaultVacaRecomHrsValue = 0;
            DefaultPaidRecomHrsValue = 0;
            DefaultHoliRecomHrsValue = 0;
            DefaultOverAllowHrsValue = 0;
            DefaultOverAllowRioValue = 0;
            DefaultRestAllowHrsValue = 0;
            DefaultRestAllowRioValue = 0;
            DefaultWendAllowHrsValue = 0;
            DefaultWendAllowRioValue = 0;
            DefaultNighAllowHrsValue = 0;
            DefaultNighAllowRioValue = 0;
            DefaultHoliAllowHrsValue = 0;
            DefaultHoliAllowRioValue = 0;
            DefaultQClothesBaseValue = 0;
            DefaultQHOfficeBaseValue = 0;
            DefaultQAgrWorkBaseValue = 0;
            DefaultQSumWorkHourValue = 0;

            AgrWorkRatioFunc = DefaultAgrWorkRatio;
            AgrWorkMaximFunc = DefaultAgrWorkMaxim;
            AgrWorkTarifFunc = DefaultAgrWorkTarif;
            AgrWorkLimitFunc = DefaultAgrWorkLimit;
            AgrWorkHoursFunc = DefaultAgrWorkHours;
            AgrTaskRatioFunc = DefaultAgrTaskRatio;
            AgrTaskMaximFunc = DefaultAgrTaskMaxim;
            AgrTaskTarifFunc = DefaultAgrTaskTarif;
            AgrTaskLimitFunc = DefaultAgrTaskLimit;
            AgrTaskHoursFunc = DefaultAgrTaskHours;
            ClothesHoursFunc = DefaultClothesHours;
            ClothesDailyFunc = DefaultClothesDaily;
            MealConDailyFunc = DefaultMealConDaily;
            HomeOffMonthFunc = DefaultHomeOffMonth;
            HomeOffTarifFunc = DefaultHomeOffTarif;
            HomeOffHoursFunc = DefaultHomeOffHours;
            MSalaryAwardFunc = DefaultMSalaryAward;
            HSalaryAwardFunc = DefaultHSalaryAward;
            FPremiumBaseFunc = DefaultFPremiumBase;
            FPremiumBossFunc = DefaultFPremiumBoss;
            FPremiumPersFunc = DefaultFPremiumPers;
            FullSheetHrsFunc = DefaultFullSheetHrs;
            TimeSheetHrsFunc = DefaultTimeSheetHrs;
            HoliSheetHrsFunc = DefaultHoliSheetHrs;
            WorkSheetHrsFunc = DefaultWorkSheetHrs;
            WorkSheetDayFunc = DefaultWorkSheetDay;
            OverSheetHrsFunc = DefaultOverSheetHrs;
            VacaRecomHrsFunc = DefaultVacaRecomHrs;
            PaidRecomHrsFunc = DefaultPaidRecomHrs;
            HoliRecomHrsFunc = DefaultHoliRecomHrs;
            OverAllowHrsFunc = DefaultOverAllowHrs;
            OverAllowRioFunc = DefaultOverAllowRio;
            RestAllowHrsFunc = DefaultRestAllowHrs;
            RestAllowRioFunc = DefaultRestAllowRio;
            WendAllowHrsFunc = DefaultWendAllowHrs;
            WendAllowRioFunc = DefaultWendAllowRio;
            NighAllowHrsFunc = DefaultNighAllowHrs;
            NighAllowRioFunc = DefaultNighAllowRio;
            HoliAllowHrsFunc = DefaultHoliAllowHrs;
            HoliAllowRioFunc = DefaultHoliAllowRio;
            QClothesBaseFunc = DefaultQClothesBase;
            QHOfficeBaseFunc = DefaultQHOfficeBase;
            QAgrWorkBaseFunc = DefaultQAgrWorkBase;
            QSumWorkHourFunc = DefaultQSumWorkHour;

            TestImpWorkSheetHrs = 0;
            TestImpWorkSheetDay = 0;
            TestImpWotkAbsenHrs = 0;
            TestImpWotkAbsenDay = 0;
            TestImpOverSheetHrs = 0;
            TestResAgrWorkPaymt = 0;
            TestResAgrWorkHours = 0;
            TestResAgrTaskPaymt = 0;
            TestResAgrTaskHours = 0;
            TestResClothesPaymt = 0;
            TestResMealConPaymt = 0;
            TestResHomeOffPaymt = 0;
            TestImpMSalaryAward = 0;
            TestResMSalaryAward = 0;
            TestImpHSalaryAward = 0;
            TestResHSalaryAward = 0;
            TestImpFPremiumBase = 0;
            TestResFPremiumBase = 0;
            TestImpFPremiumBoss = 0;
            TestResFPremiumBoss = 0;
            TestImpFPremiumPers = 0;
            TestResFPremiumPers = 0;
            TestImpQAverageBase = 0;
            TestImpAverPremsPay = 0;
            TestImpAverVacasPay = 0;
            TestImpAverOversPay = 0;
            TestResIncomesNetto = 0;
            TestResPaymentNetto = 0;
            TestResDiffValNetto = 0;
        }
        public OptimulaGenerator ParseResult(string resultString)
        {
            string[] resultDefValues = resultString.Split(';');

            Func<string, decimal>[] specParser = new Func<string, decimal>[]
            {
                ParseNADecimal, //Evideční číslo  	101
                ParseNADecimal, //Jméno a příjmení 	Drahota Jakub
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
            decimal[] resultIntValues = resultDefValues.Zip(specParser).Select((x) => x.Second(x.First)).ToArray();

            Func<decimal, OptimulaGenerator>[] resultGenerator = new Func<decimal, OptimulaGenerator>[]
            {
                WithNADecimalVal,          //Evideční číslo  	101
                WithNADecimalVal,          //Jméno a příjmení 	Drahota Jakub
                WithTestResultPeriodNum,   //Mzdové období 	    202201
                WithTestImpWorkSheetHrs,   //IMP-WorkSheetHrs
                WithTestImpWorkSheetDay,   //IMP-WorkSheetDay
                WithTestImpWotkAbsenHrs,   //IMP-WotkAbsenHrs
                WithTestImpWotkAbsenDay,   //IMP-WotkAbsenDay
                WithTestImpOverSheetHrs,   //IMP-OverSheetHrs
                WithTestResAgrWorkPaymt,   //RES-AgrWorkPaymt
                WithTestResAgrWorkHours,   //RES-AgrWorkHours
                WithTestResAgrTaskPaymt,   //RES-AgtWorkPaymt
                WithTestResAgrTaskHours,   //RES-AgtWorkHours
                WithTestResClothesPaymt,   //RES-ClothesPaymt
                WithTestResMealConPaymt,   //RES-MealConPaymt
                WithTestResHomeOffPaymt,   //RES-HomeOffPaymt
                WithTestImpMSalaryAward,   //IMP-MSalaryAward
                WithTestResMSalaryAward,   //RES-MSalaryAward
                WithTestImpHSalaryAward,   //IMP-HSalaryAward
                WithTestResHSalaryAward,   //RES-HSalaryAward
                WithTestImpFPremiumBase,   //IMP-FPremiumBase
                WithTestResFPremiumBase,   //RES-FPremiumBase
                WithTestImpFPremiumBoss,   //IMP-FPremiumBoss
                WithTestResFPremiumBoss,   //RES-FPremiumBoss
                WithTestImpFPremiumPers,   //IMP-FPremiumPers
                WithTestResFPremiumPers,   //RES-FPremiumPers
                WithTestImpQAverageBase,   //IMP-QAverageBase
                WithTestImpAverPremsPay,   //IMP-AverPremsPay
                WithTestImpAverVacasPay,   //IMP-AverVacasPay
                WithTestImpAverOversPay,   //IMP-AverOversPay
                WithTestResIncomesNetto,   //RES-IncomesNetto
                WithTestResPaymentNetto,   //RES-PaymentNetto
                WithTestResDiffValNetto,   //RES-DiffValNetto
            };
            resultIntValues.Zip(resultGenerator).Select((x) => x.Second(x.First)).ToArray();

            CreateResultDescription(TestResultPeriod);

            return this;
        }
        public OptimulaGenerator CreateResultDescription(IPeriod period)
        {
            string[] resultLine = new string[]
            {
                Number,
                Name,
                period.Code.ToString(),
                DecFormatDecimal(TestImpWorkSheetHrs), //IMP-WorkSheetHrs
                DecFormatDecimal(TestImpWorkSheetDay), //IMP-WorkSheetDay
                DecFormatDecimal(TestImpWotkAbsenHrs), //IMP-WotkAbsenHrs
                DecFormatDecimal(TestImpWotkAbsenDay), //IMP-WotkAbsenDay
                DecFormatDecimal(TestImpOverSheetHrs), //IMP-OverSheetHrs
                DecFormatDecimal(TestResAgrWorkPaymt), //RES-AgrWorkPaymt
                DecFormatDecimal(TestResAgrWorkHours), //RES-AgrWorkHours
                DecFormatDecimal(TestResAgrTaskPaymt), //RES-AgtWorkPaymt
                DecFormatDecimal(TestResAgrTaskHours), //RES-AgtWorkHours
                DecFormatDecimal(TestResClothesPaymt), //RES-ClothesPaymt
                DecFormatDecimal(TestResMealConPaymt), //RES-MealConPaymt
                DecFormatDecimal(TestResHomeOffPaymt), //RES-HomeOffPaymt
                DecFormatDecimal(TestImpMSalaryAward), //IMP-MSalaryAward
                DecFormatDecimal(TestResMSalaryAward), //RES-MSalaryAward
                DecFormatDecimal(TestImpHSalaryAward), //IMP-HSalaryAward
                DecFormatDecimal(TestResHSalaryAward), //RES-HSalaryAward
                DecFormatDecimal(TestImpFPremiumBase), //IMP-FPremiumBase
                DecFormatDecimal(TestResFPremiumBase), //RES-FPremiumBase
                DecFormatDecimal(TestImpFPremiumBoss), //IMP-FPremiumBoss
                DecFormatDecimal(TestResFPremiumBoss), //RES-FPremiumBoss
                DecFormatDecimal(TestImpFPremiumPers), //IMP-FPremiumPers
                DecFormatDecimal(TestResFPremiumPers), //RES-FPremiumPers
                DecFormatDecimal(TestImpQAverageBase), //IMP-QAverageBase
                DecFormatDecimal(TestImpAverPremsPay), //IMP-AverPremsPay
                DecFormatDecimal(TestImpAverVacasPay), //IMP-AverVacasPay
                DecFormatDecimal(TestImpAverOversPay), //IMP-AverOversPay
                DecFormatDecimal(TestResIncomesNetto), //RES-IncomesNetto
                DecFormatDecimal(TestResPaymentNetto), //RES-PaymentNetto
                DecFormatDecimal(TestResDiffValNetto), //RES-DiffValNetto
            };
            ResultDescription = string.Join(";", resultLine) + ";";

            return this;
        }
        public string[] BuildImportString(IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            Int32 AgrWorkRatioVal = AgrWorkRatioFunc(this, period, ruleset, prevset);
            Int32 AgrWorkMaximVal = AgrWorkMaximFunc(this, period, ruleset, prevset);
            Int32 AgrWorkTarifVal = AgrWorkTarifFunc(this, period, ruleset, prevset);
            Int32 AgrWorkLimitVal = AgrWorkLimitFunc(this, period, ruleset, prevset);
            Int32 AgrWorkHoursVal = AgrWorkHoursFunc(this, period, ruleset, prevset);
            Int32 AgrTaskRatioVal = AgrTaskRatioFunc(this, period, ruleset, prevset);
            Int32 AgrTaskMaximVal = AgrTaskMaximFunc(this, period, ruleset, prevset);
            Int32 AgrTaskTarifVal = AgrTaskTarifFunc(this, period, ruleset, prevset);
            Int32 AgrTaskLimitVal = AgrTaskLimitFunc(this, period, ruleset, prevset);
            Int32 AgrTaskHoursVal = AgrTaskHoursFunc(this, period, ruleset, prevset);
            Int32 ClothesHoursVal = ClothesHoursFunc(this, period, ruleset, prevset);
            Int32 ClothesDailyVal = ClothesDailyFunc(this, period, ruleset, prevset);
            Int32 MealConDailyVal = MealConDailyFunc(this, period, ruleset, prevset);
            Int32 HomeOffMonthVal = HomeOffMonthFunc(this, period, ruleset, prevset);
            Int32 HomeOffTarifVal = HomeOffTarifFunc(this, period, ruleset, prevset);
            Int32 HomeOffHoursVal = HomeOffHoursFunc(this, period, ruleset, prevset);

            Int32 MSalaryAwardVal = MSalaryAwardFunc(this, period, ruleset, prevset);
            Int32 HSalaryAwardVal = HSalaryAwardFunc(this, period, ruleset, prevset);
            Int32 FPremiumBaseVal = FPremiumBaseFunc(this, period, ruleset, prevset);
            Int32 FPremiumBossVal = FPremiumBossFunc(this, period, ruleset, prevset);
            Int32 FPremiumPersVal = FPremiumPersFunc(this, period, ruleset, prevset);
            Int32 FullSheetHrsVal = FullSheetHrsFunc(this, period, ruleset, prevset);
            Int32 TimeSheetHrsVal = TimeSheetHrsFunc(this, period, ruleset, prevset);
            Int32 HoliSheetHrsVal = HoliSheetHrsFunc(this, period, ruleset, prevset);
            Int32 WorkSheetHrsVal = WorkSheetHrsFunc(this, period, ruleset, prevset);
            Int32 WorkSheetDayVal = WorkSheetDayFunc(this, period, ruleset, prevset);
            Int32 OverSheetHrsVal = OverSheetHrsFunc(this, period, ruleset, prevset);
            Int32 VacaRecomHrsVal = VacaRecomHrsFunc(this, period, ruleset, prevset);
            Int32 PaidRecomHrsVal = PaidRecomHrsFunc(this, period, ruleset, prevset);
            Int32 HoliRecomHrsVal = HoliRecomHrsFunc(this, period, ruleset, prevset);
            Int32 OverAllowHrsVal = OverAllowHrsFunc(this, period, ruleset, prevset);
            Int32 OverAllowRioVal = OverAllowRioFunc(this, period, ruleset, prevset);
            Int32 RestAllowHrsVal = RestAllowHrsFunc(this, period, ruleset, prevset);
            Int32 RestAllowRioVal = RestAllowRioFunc(this, period, ruleset, prevset);
            Int32 WendAllowHrsVal = WendAllowHrsFunc(this, period, ruleset, prevset);
            Int32 WendAllowRioVal = WendAllowRioFunc(this, period, ruleset, prevset);
            Int32 NighAllowHrsVal = NighAllowHrsFunc(this, period, ruleset, prevset);
            Int32 NighAllowRioVal = NighAllowRioFunc(this, period, ruleset, prevset);
            Int32 HoliAllowHrsVal = HoliAllowHrsFunc(this, period, ruleset, prevset);
            Int32 HoliAllowRioVal = HoliAllowRioFunc(this, period, ruleset, prevset);
            Int32 QClothesBaseVal = QClothesBaseFunc(this, period, ruleset, prevset);
            Int32 QHOfficeBaseVal = QHOfficeBaseFunc(this, period, ruleset, prevset);
            Int32 QAgrWorkBaseVal = QAgrWorkBaseFunc(this, period, ruleset, prevset);
            Int32 QSumWorkHourVal = QSumWorkHourFunc(this, period, ruleset, prevset);

            string[] valuesList = new string[]
            {
                Number, // A
                Name,   // B
                period.Code.ToString(), // C
                $"{NumFormatIntX100(AgrWorkRatioVal)}", // D
                $"{HrsFormatIntX060(AgrWorkMaximVal)}", // E
                $"{CcyFormatIntX100(AgrWorkTarifVal)}", // F //Sazba DPP/hod
                $"{CcyFormatIntX100(AgrWorkLimitVal)}", // G //DPP/měs.-základní 
                $"{HrsFormatIntX060(AgrWorkHoursVal)}", // H //DPP hodiny/měs.-základní
                $"{NumFormatIntX100(AgrTaskRatioVal)}", // I
                $"{HrsFormatIntX060(AgrTaskMaximVal)}", // J
                $"{CcyFormatIntX100(AgrTaskTarifVal)}", // K //Sazba DPČ/hod
                $"{CcyFormatIntX100(AgrTaskLimitVal)}", // L //DPČ/měs.-základní
                $"{HrsFormatIntX060(AgrTaskHoursVal)}", // M //DPČ hodiny/měs.-základní
                $"{CcyFormatIntX100(ClothesHoursVal)}", // N "CompClothHsTariff",   OPTIONAL, 12), // Sazba ošatné                              
                $"{CcyFormatIntX100(ClothesDailyVal)}", // O //Ošatné/den
                $"{CcyFormatIntX100(MealConDailyVal)}", // P //Strav.paušál/den
                $"{CcyFormatIntX100(HomeOffMonthVal)}", // Q //Home office/měs.
                $"{CcyFormatIntX100(HomeOffTarifVal)}", // R //Home office/měs.
                $"{HrsFormatIntX060(HomeOffHoursVal)}", // S "CompHOfficeHours",    OPTIONAL, 16), // Počet hodin HO                        
                $"{CcyFormatIntX100(MSalaryAwardVal)}", // T "BonusSalaryAmount",   OPTIONAL, 17), // Osobní ohodnocení                     
                $"{CcyFormatIntX100(HSalaryAwardVal)}", // U "BonusHourlyAmount",   OPTIONAL, 18), // Osobní ohodnocení                     
                $"{CcyFormatIntX100(FPremiumBaseVal)}", // V //Celková částka v čistém 
                $"{CcyFormatIntX100(FPremiumBossVal)}", // W "PremiumBossAmount",   OPTIONAL, 20), // Prémie                                
                $"{CcyFormatIntX100(FPremiumPersVal)}", // X //ODMĚNY  
                "", // ---------------
                "", // ---------------
                $"{HrsFormatIntX060(FullSheetHrsVal)}", // "$AA", "FullsheetHours",      OPTIONAL, 22), // Zákonný úvazek                        
                $"{HrsFormatIntX060(TimeSheetHrsVal)}", // "$AB", //Fond
                $"{HrsFormatIntX060(HoliSheetHrsVal)}", // "$AC", "HolisheetHours",      OPTIONAL, 24), // Hodiny svátků v ES                    
                $"{HrsFormatIntX060(WorkSheetHrsVal)}", // "$AD", //Odpracované hodiny  
                $"{DayFormatIntX100(WorkSheetDayVal)}", // "$AE", //Odpracované dny 
                $"{HrsFormatIntX060(OverSheetHrsVal)}", // "$AF", "OversheetHours",      OPTIONAL, 27), // Přesčas  (hod)                        
                $"{HrsFormatIntX060(VacaRecomHrsVal)}", // "$AG", "RecomVacaHours",      OPTIONAL, 28), // Dovolená (hod)                        
                $"{HrsFormatIntX060(PaidRecomHrsVal)}", // "$AH", "RecomPaidHours",      OPTIONAL, 29), // Dovolená (hod)                        
                $"{HrsFormatIntX060(HoliRecomHrsVal)}", // "$AI", "RecomHoliHours",      OPTIONAL, 30), // Svátky (hod)                          
                $"{HrsFormatIntX060(OverAllowHrsVal)}", // "$AJ", "AllowOverHours",      OPTIONAL, 31), // Příplatky (hod, proc)                 
                $"{CcyFormatIntX100(OverAllowRioVal)}", // "$AK", "AllowOverRatio",      OPTIONAL, 32), // Příplatky (hod, proc)                 
                $"{HrsFormatIntX060(RestAllowHrsVal)}", // "$AL", "AllowRestHours",      OPTIONAL, 33), // Příplatky (hod, proc)                 
                $"{CcyFormatIntX100(RestAllowRioVal)}", // "$AM", "AllowRestRatio",      OPTIONAL, 34), // Příplatky (hod, proc)                 
                $"{HrsFormatIntX060(WendAllowHrsVal)}", // "$AN", "AllowWendHours",      OPTIONAL, 35), // Příplatky (hod, proc)                 
                $"{CcyFormatIntX100(WendAllowRioVal)}", // "$AO", "AllowWendRatio",      OPTIONAL, 36), // Příplatky (hod, proc)                 
                $"{HrsFormatIntX060(NighAllowHrsVal)}", // "$AP", "AllowNighHours",      OPTIONAL, 37), // Příplatky (hod, proc)                 
                $"{CcyFormatIntX100(NighAllowRioVal)}", // "$AQ", "AllowNighRatio",      OPTIONAL, 38), // Příplatky (hod, proc)                 
                $"{HrsFormatIntX060(HoliAllowHrsVal)}", // "$AR", "AllowHoliHours",      OPTIONAL, 39), // Příplatky (hod, proc)                 
                $"{CcyFormatIntX100(HoliAllowRioVal)}", // "$AS", "AllowHoliRatio",      OPTIONAL, 40), // Příplatky (hod, proc)                 
                $"{CcyFormatIntX100(QClothesBaseVal)}", // "$AT", "CompClothesBasis",    OPTIONAL, 41), // Průměr z min.Q                        
                $"{CcyFormatIntX100(QHOfficeBaseVal)}", // "$AU", "CompHOfficeBasis",    OPTIONAL, 42), // Průměr z min.Q                        
                $"{CcyFormatIntX100(QAgrWorkBaseVal)}", // "$AV", "CompAgrWorkBasis",    OPTIONAL, 43), // Průměr z min.Q                        
                $"{CcyFormatIntX100(QSumWorkHourVal)}", // "$AW", "CompAverageHours",    OPTIONAL, 44), // Průměr z min.Q                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
            };

            if (WorkSheetHrsVal != 0)
            {
                return valuesList.ToArray();
            }
            return Array.Empty<string>();
        }

        public string[] BuildImportXlsString(IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            string[] valuesList = BuildImportString(period, ruleset, prevset);
            if (valuesList.Length != 0)
            {
                string[] importResult = new string[] { string.Join('\t', valuesList) + ";" };
                return importResult;
            }

            return Array.Empty<string>();
        }
        public string[] BuildImportCsvString(IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            string[] valuesList = BuildImportString(period, ruleset, prevset);
            if (valuesList.Length != 0)
            {
                string[] importResult = new string[] { string.Join(';', valuesList) + ";" };
                return importResult;
            }

            return Array.Empty<string>();
        }
        public abstract IEnumerable<ITermTarget> BuildSpecTargets(IPeriod period, IBundleProps ruleset, IBundleProps prevset);
        private Int32 DefaultAgrWorkTarif(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrWorkTarifValue;
        }
        private Int32 DefaultAgrWorkRatio(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrWorkRatioValue;
        }
        private Int32 DefaultAgrWorkMaxim(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrWorkMaximValue;
        }
        private Int32 DefaultAgrWorkLimit(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrWorkLimitValue;
        }
        private Int32 DefaultAgrWorkHours(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrWorkHoursValue;
        }
        private Int32 DefaultAgrTaskRatio(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrTaskRatioValue;
        }
        private Int32 DefaultAgrTaskMaxim(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrTaskMaximValue;
        }
        private Int32 DefaultAgrTaskTarif(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrTaskTarifValue;
        }
        private Int32 DefaultAgrTaskLimit(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrTaskLimitValue;
        }
        private Int32 DefaultAgrTaskHours(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultAgrTaskHoursValue;
        }
        private Int32 DefaultClothesHours(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultClothesHoursValue;
        }
        private Int32 DefaultClothesDaily(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultClothesDailyValue;
        }
        private Int32 DefaultMealConDaily(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultMealConDailyValue;
        }
        private Int32 DefaultHomeOffMonth(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultHomeOffMonthValue;
        }
        private Int32 DefaultHomeOffTarif(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultHomeOffTarifValue;
        }
        private Int32 DefaultHomeOffHours(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultHomeOffHoursValue;
        }
        private Int32 DefaultMSalaryAward(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultMSalaryAwardValue;
        }
        private Int32 DefaultHSalaryAward(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultHSalaryAwardValue;
        }
        private Int32 DefaultFPremiumBase(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultFPremiumBaseValue;
        }
        private Int32 DefaultFPremiumBoss(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultFPremiumBossValue;
        }
        private Int32 DefaultFPremiumPers(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultFPremiumPersValue;
        }
        private Int32 DefaultFullSheetHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultFullSheetHrsValue;
        }
        private Int32 DefaultTimeSheetHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultTimeSheetHrsValue;
        }
        private Int32 DefaultHoliSheetHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultHoliSheetHrsValue;
        }
        private Int32 DefaultWorkSheetHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultWorkSheetHrsValue;
        }
        private Int32 DefaultWorkSheetDay(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultWorkSheetDayValue;
        }
        private Int32 DefaultOverSheetHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultOverSheetHrsValue;
        }
        private Int32 DefaultVacaRecomHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultVacaRecomHrsValue;
        }
        private Int32 DefaultPaidRecomHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultPaidRecomHrsValue;
        }
        private Int32 DefaultHoliRecomHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultHoliRecomHrsValue;
        }
        private Int32 DefaultOverAllowHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultOverAllowHrsValue;
        }
        private Int32 DefaultOverAllowRio(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultOverAllowRioValue;
        }
        private Int32 DefaultRestAllowHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultRestAllowHrsValue;
        }
        private Int32 DefaultRestAllowRio(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultRestAllowRioValue;
        }
        private Int32 DefaultWendAllowHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultWendAllowHrsValue;
        }
        private Int32 DefaultWendAllowRio(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultWendAllowRioValue;
        }
        private Int32 DefaultNighAllowHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultNighAllowHrsValue;
        }
        private Int32 DefaultNighAllowRio(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultNighAllowRioValue;
        }
        private Int32 DefaultHoliAllowHrs(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultHoliAllowHrsValue;
        }
        private Int32 DefaultHoliAllowRio(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultHoliAllowRioValue;
        }
        private Int32 DefaultQClothesBase(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultQClothesBaseValue;
        }
        private Int32 DefaultQHOfficeBase(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultQHOfficeBaseValue;
        }
        private Int32 DefaultQAgrWorkBase(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultQAgrWorkBaseValue;
        }
        private Int32 DefaultQSumWorkHour(OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            return DefaultQSumWorkHourValue;
        }

        public int Id { get; }
        public string Name { get; }
        public string Number { get; }
        public string ResultDescription { get; protected set; }

        public Int32 DefaultAgrWorkRatioValue { get; private set; }
        public Int32 DefaultAgrWorkMaximValue { get; private set; }
        public Int32 DefaultAgrWorkTarifValue { get; private set; }
        public Int32 DefaultAgrWorkLimitValue { get; private set; }
        public Int32 DefaultAgrWorkHoursValue { get; private set; }
        public Int32 DefaultAgrTaskRatioValue { get; private set; }
        public Int32 DefaultAgrTaskMaximValue { get; private set; }
        public Int32 DefaultAgrTaskTarifValue { get; private set; }
        public Int32 DefaultAgrTaskLimitValue { get; private set; }
        public Int32 DefaultAgrTaskHoursValue { get; private set; }
        public Int32 DefaultClothesHoursValue { get; private set; }
        public Int32 DefaultClothesDailyValue { get; private set; }
        public Int32 DefaultMealConDailyValue { get; private set; }
        public Int32 DefaultHomeOffMonthValue { get; private set; }
        public Int32 DefaultHomeOffTarifValue { get; private set; }
        public Int32 DefaultHomeOffHoursValue { get; private set; }
        public Int32 DefaultMSalaryAwardValue { get; private set; }
        public Int32 DefaultHSalaryAwardValue { get; private set; }
        public Int32 DefaultFPremiumBaseValue { get; private set; }
        public Int32 DefaultFPremiumBossValue { get; private set; }
        public Int32 DefaultFPremiumPersValue { get; private set; }
        public Int32 DefaultFullSheetHrsValue { get; private set; }
        public Int32 DefaultTimeSheetHrsValue { get; private set; }
        public Int32 DefaultHoliSheetHrsValue { get; private set; }
        public Int32 DefaultWorkSheetHrsValue { get; private set; }
        public Int32 DefaultWorkSheetDayValue { get; private set; }
        public Int32 DefaultOverSheetHrsValue { get; private set; }
        public Int32 DefaultVacaRecomHrsValue { get; private set; }
        public Int32 DefaultPaidRecomHrsValue { get; private set; }
        public Int32 DefaultHoliRecomHrsValue { get; private set; }
        public Int32 DefaultOverAllowHrsValue { get; private set; }
        public Int32 DefaultOverAllowRioValue { get; private set; }
        public Int32 DefaultRestAllowHrsValue { get; private set; }
        public Int32 DefaultRestAllowRioValue { get; private set; }
        public Int32 DefaultWendAllowHrsValue { get; private set; }
        public Int32 DefaultWendAllowRioValue { get; private set; }
        public Int32 DefaultNighAllowHrsValue { get; private set; }
        public Int32 DefaultNighAllowRioValue { get; private set; }
        public Int32 DefaultHoliAllowHrsValue { get; private set; }
        public Int32 DefaultHoliAllowRioValue { get; private set; }
        public Int32 DefaultQClothesBaseValue { get; private set; }
        public Int32 DefaultQHOfficeBaseValue { get; private set; }
        public Int32 DefaultQAgrWorkBaseValue { get; private set; }
        public Int32 DefaultQSumWorkHourValue { get; private set; }

        public IPeriod TestResultPeriod { get; private set; }
        public decimal TestImpWorkSheetHrs { get; private set; }
        public decimal TestImpWorkSheetDay { get; private set; }      
        public decimal TestImpWotkAbsenHrs { get; private set; }       
        public decimal TestImpWotkAbsenDay { get; private set; }   
        public decimal TestImpOverSheetHrs { get; private set; }   
        public decimal TestResAgrWorkPaymt { get; private set; }
        public decimal TestResAgrWorkHours { get; private set; }
        public decimal TestResAgrTaskPaymt { get; private set; }
        public decimal TestResAgrTaskHours { get; private set; }
        public decimal TestResClothesPaymt { get; private set; }
        public decimal TestResMealConPaymt { get; private set; }
        public decimal TestResHomeOffPaymt { get; private set; }
        public decimal TestImpMSalaryAward { get; private set; }
        public decimal TestResMSalaryAward { get; private set; }
        public decimal TestImpHSalaryAward { get; private set; }
        public decimal TestResHSalaryAward { get; private set; }
        public decimal TestImpFPremiumBase { get; private set; }
        public decimal TestResFPremiumBase { get; private set; }
        public decimal TestImpFPremiumBoss { get; private set; }
        public decimal TestResFPremiumBoss { get; private set; }
        public decimal TestImpFPremiumPers { get; private set; }
        public decimal TestResFPremiumPers { get; private set; }
        public decimal TestImpQAverageBase { get; private set; }
        public decimal TestImpAverPremsPay { get; private set; }
        public decimal TestImpAverVacasPay { get; private set; }
        public decimal TestImpAverOversPay { get; private set; }
        public decimal TestResIncomesNetto { get; private set; }
        public decimal TestResPaymentNetto { get; private set; }
        public decimal TestResDiffValNetto { get; private set; }

        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrWorkTarifFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrWorkRatioFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrWorkMaximFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrWorkLimitFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrWorkHoursFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrTaskTarifFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrTaskRatioFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrTaskMaximFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrTaskLimitFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> AgrTaskHoursFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> ClothesHoursFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> ClothesDailyFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> MealConDailyFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> HomeOffMonthFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> HomeOffTarifFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> HomeOffHoursFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> MSalaryAwardFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> HSalaryAwardFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> FPremiumBaseFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> FPremiumBossFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> FPremiumPersFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> FullSheetHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> TimeSheetHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> HoliSheetHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> WorkSheetHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> WorkSheetDayFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> OverSheetHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> VacaRecomHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> PaidRecomHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> HoliRecomHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> OverAllowHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> OverAllowRioFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> RestAllowHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> RestAllowRioFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> WendAllowHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> WendAllowRioFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> NighAllowHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> NighAllowRioFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> HoliAllowHrsFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> HoliAllowRioFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> QClothesBaseFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> QHOfficeBaseFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> QAgrWorkBaseFunc { get; private set; }
        public Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> QSumWorkHourFunc { get; private set; }
        public static Int32 ParseNANothing(string valString)
        {
            return 0;
        }
        public static Int32 ParseIntNumber(string valString)
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
            return decimal.ToInt32(numberValue);
        }
        public static Int32 ParseDecNumber(string valString)
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
            return decimal.ToInt32(numberValue * 100);
        }
        public static Int32 ParseHrsNumber(string valString)
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
            return decimal.ToInt32(numberValue * 60);
        }
        public static decimal ParseNADecimal(string valString)
        {
            return 0;
        }
        public static decimal ParseDecimal(string valString)
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
        protected static Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> WithValue(Int32 val)
        {
            return (OptimulaGenerator gen, IPeriod period, IBundleProps ruleset, IBundleProps prevset) => (val);
        }
        public OptimulaGenerator WithResultDescription(string val)
        {
            this.ResultDescription = val;
            return this;
        }
        public OptimulaGenerator WithNANothingVal(Int32 val)
        {
            return this;
        }
        public OptimulaGenerator WithAgrWorkRatioVal(Int32 val)
        {
            DefaultAgrWorkRatioValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrWorkMaximVal(Int32 val)
        {
            DefaultAgrWorkMaximValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrWorkTarifVal(Int32 val)
        {
            DefaultAgrWorkTarifValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrWorkLimitVal(Int32 val)
        {
            DefaultAgrWorkLimitValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrWorkHoursVal(Int32 val)
        {
            DefaultAgrWorkHoursValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrTaskRatioVal(Int32 val)
        {
            DefaultAgrTaskRatioValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrTaskMaximVal(Int32 val)
        {
            DefaultAgrTaskMaximValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrTaskTarifVal(Int32 val)
        {
            DefaultAgrTaskTarifValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrTaskLimitVal(Int32 val)
        {
            DefaultAgrTaskLimitValue = val;
            return this;
        }
        public OptimulaGenerator WithAgrTaskHoursVal(Int32 val)
        {
            DefaultAgrTaskHoursValue = val;
            return this;
        }
        public OptimulaGenerator WithClothesHoursVal(Int32 val)
        {
            DefaultClothesHoursValue = val;
            return this;
        }
        public OptimulaGenerator WithClothesDailyVal(Int32 val)
        {
            DefaultClothesDailyValue = val;
            return this;
        }
        public OptimulaGenerator WithMealConDailyVal(Int32 val)
        {
            DefaultMealConDailyValue = val;
            return this;
        }
        public OptimulaGenerator WithHomeOffMonthVal(Int32 val)
        {
            DefaultHomeOffMonthValue = val;
            return this;
        }
        public OptimulaGenerator WithHomeOffTarifVal(Int32 val)
        {
            DefaultHomeOffTarifValue = val;
            return this;
        }
        public OptimulaGenerator WithHomeOffHoursVal(Int32 val)
        {
            DefaultHomeOffHoursValue = val;
            return this;
        }
        public OptimulaGenerator WithMSalaryAwardVal(Int32 val)
        {
            DefaultMSalaryAwardValue = val;
            return this;
        }
        public OptimulaGenerator WithHSalaryAwardVal(Int32 val)
        {
            DefaultHSalaryAwardValue = val;
            return this;
        }
        public OptimulaGenerator WithFPremiumBaseVal(Int32 val)
        {
            DefaultFPremiumBaseValue = val;
            return this;
        }
        public OptimulaGenerator WithFPremiumBossVal(Int32 val)
        {
            DefaultFPremiumBossValue = val;
            return this;
        }
        public OptimulaGenerator WithFPremiumPersVal(Int32 val)
        {
            DefaultFPremiumPersValue = val;
            return this;
        }
        public OptimulaGenerator WithFullSheetHrsVal(Int32 val)
        {
            DefaultFullSheetHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithTimeSheetHrsVal(Int32 val)
        {
            DefaultTimeSheetHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithHoliSheetHrsVal(Int32 val)
        {
            DefaultHoliSheetHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithWorkSheetHrsVal(Int32 val)
        {
            DefaultWorkSheetHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithWorkSheetDayVal(Int32 val)
        {
            DefaultWorkSheetDayValue = val;
            return this;
        }
        public OptimulaGenerator WithOverSheetHrsVal(Int32 val)
        {
            DefaultOverSheetHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithVacaRecomHrsVal(Int32 val)
        {
            DefaultVacaRecomHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithPaidRecomHrsVal(Int32 val)
        {
            DefaultPaidRecomHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithHoliRecomHrsVal(Int32 val)
        {
            DefaultHoliRecomHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithOverAllowHrsVal(Int32 val)
        {
            DefaultOverAllowHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithOverAllowRioVal(Int32 val)
        {
            DefaultOverAllowRioValue = val;
            return this;
        }
        public OptimulaGenerator WithRestAllowHrsVal(Int32 val)
        {
            DefaultRestAllowHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithRestAllowRioVal(Int32 val)
        {
            DefaultRestAllowRioValue = val;
            return this;
        }
        public OptimulaGenerator WithWendAllowHrsVal(Int32 val)
        {
            DefaultWendAllowHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithWendAllowRioVal(Int32 val)
        {
            DefaultWendAllowRioValue = val;
            return this;
        }
        public OptimulaGenerator WithNighAllowHrsVal(Int32 val)
        {
            DefaultNighAllowHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithNighAllowRioVal(Int32 val)
        {
            DefaultNighAllowRioValue = val;
            return this;
        }
        public OptimulaGenerator WithHoliAllowHrsVal(Int32 val)
        {
            DefaultHoliAllowHrsValue = val;
            return this;
        }
        public OptimulaGenerator WithHoliAllowRioVal(Int32 val)
        {
            DefaultHoliAllowRioValue = val;
            return this;
        }
        public OptimulaGenerator WithQClothesBaseVal(Int32 val)
        {
            DefaultQClothesBaseValue = val;
            return this;
        }
        public OptimulaGenerator WithQHOfficeBaseVal(Int32 val)
        {
            DefaultQHOfficeBaseValue = val;
            return this;
        }
        public OptimulaGenerator WithQAgrWorkBaseVal(Int32 val)
        {
            DefaultQAgrWorkBaseValue = val;
            return this;
        }
        public OptimulaGenerator WithQSumWorkHourVal(Int32 val)
        {
            DefaultQSumWorkHourValue = val;
            return this;
        }

        public OptimulaGenerator WithNANothing(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            return this;
        }
        public OptimulaGenerator WithAgrWorkTarif(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            AgrWorkTarifFunc = func;
            return this;
        }
        public OptimulaGenerator WithAgrWorkRatio(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            AgrWorkRatioFunc = func;
            return this;
        }
        public OptimulaGenerator WithAgrWorkMaxim(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            AgrWorkMaximFunc = func;
            return this;
        }
        public OptimulaGenerator WithAgrWorkLimit(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            AgrWorkLimitFunc = func;
            return this;
        }
        public OptimulaGenerator WithAgtWorkTarif(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            AgrTaskTarifFunc = func;
            return this;
        }
        public OptimulaGenerator WithAgtWorkRatio(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            AgrTaskRatioFunc = func;
            return this;
        }
        public OptimulaGenerator WithAgrTaskMaxim(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            AgrTaskMaximFunc = func;
            return this;
        }
        public OptimulaGenerator WithAgrTaskLimit(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            AgrTaskLimitFunc = func;
            return this;
        }
        public OptimulaGenerator WithClothesHours(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            ClothesHoursFunc = func;
            return this;
        }
        public OptimulaGenerator WithClothesDaily(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            ClothesDailyFunc = func;
            return this;
        }
        public OptimulaGenerator WithHomeOffMonth(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            HomeOffMonthFunc = func;
            return this;
        }
        public OptimulaGenerator WithHomeOffTarif(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            HomeOffTarifFunc = func;
            return this;
        }
        public OptimulaGenerator WithHomeOffHours(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            HomeOffHoursFunc = func;
            return this;
        }
        public OptimulaGenerator WithMSalaryAward(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            MSalaryAwardFunc = func;
            return this;
        }
        public OptimulaGenerator WithHSalaryAward(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            HSalaryAwardFunc = func;
            return this;
        }
        public OptimulaGenerator WithFPremiumBase(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            FPremiumBaseFunc = func;
            return this;
        }
        public OptimulaGenerator WithFPremiumBoss(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            FPremiumBossFunc = func;
            return this;
        }
        public OptimulaGenerator WithFPremiumPers(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            FPremiumPersFunc = func;
            return this;
        }
        public OptimulaGenerator WithFullSheetHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            FullSheetHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithTimeSheetHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            TimeSheetHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithHoliSheetHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            HoliSheetHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithWorkSheetHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            WorkSheetHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithWorkSheetDay(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            WorkSheetDayFunc = func;
            return this;
        }
        public OptimulaGenerator WithOverSheetHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            OverSheetHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithVacaRecomHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            VacaRecomHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithPaidRecomHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            PaidRecomHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithHoliRecomHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            HoliRecomHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithOverAllowHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            OverAllowHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithOverAllowRio(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            OverAllowRioFunc = func;
            return this;
        }
        public OptimulaGenerator WithRestAllowHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            RestAllowHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithRestAllowRio(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            RestAllowRioFunc = func;
            return this;
        }
        public OptimulaGenerator WithWendAllowHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            WendAllowHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithWendAllowRio(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            WendAllowRioFunc = func;
            return this;
        }
        public OptimulaGenerator WithNighAllowHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            NighAllowHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithNighAllowRio(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            NighAllowRioFunc = func;
            return this;
        }
        public OptimulaGenerator WithHoliAllowHrs(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            HoliAllowHrsFunc = func;
            return this;
        }
        public OptimulaGenerator WithHoliAllowRio(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            HoliAllowRioFunc = func;
            return this;
        }
        public OptimulaGenerator WithQClothesBase(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            QClothesBaseFunc = func;
            return this;
        }
        public OptimulaGenerator WithQHOfficeBase(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            QHOfficeBaseFunc = func;
            return this;
        }
        public OptimulaGenerator WithQAgrWorkBase(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            QAgrWorkBaseFunc = func;
            return this;
        }
        public OptimulaGenerator WithQSumWorkHour(Func<OptimulaGenerator, IPeriod, IBundleProps, IBundleProps, Int32> func)
        {
            QSumWorkHourFunc = func;
            return this;
        }

        public OptimulaGenerator WithNADecimalVal(decimal val)
        {
            return this;
        }
        public OptimulaGenerator WithTestResultPeriodNum(decimal val)
        {
            TestResultPeriod = new Period(decimal.ToInt32(val));
            return this;
        }
        public OptimulaGenerator WithTestImpWorkSheetHrs(decimal val)
        {
            TestImpWorkSheetHrs = val;
            return this;
        }
        public OptimulaGenerator WithTestImpWorkSheetDay(decimal val)
        {
            TestImpWorkSheetDay = val;
            return this;
        }
        public OptimulaGenerator WithTestImpWotkAbsenHrs(decimal val)
        {
            TestImpWotkAbsenHrs = val;
            return this;
        }
        public OptimulaGenerator WithTestImpWotkAbsenDay(decimal val)
        {
            TestImpWotkAbsenDay = val;
            return this;
        }
        public OptimulaGenerator WithTestImpOverSheetHrs(decimal val)
        {
            TestImpOverSheetHrs = val;
            return this;
        }
        public OptimulaGenerator WithTestResAgrWorkPaymt(decimal val)
        {
            TestResAgrWorkPaymt = val;
            return this;
        }
        public OptimulaGenerator WithTestResAgrWorkHours(decimal val)
        {
            TestResAgrWorkHours = val;
            return this;
        }
        public OptimulaGenerator WithTestResAgrTaskPaymt(decimal val)
        {
            TestResAgrTaskPaymt = val;
            return this;
        }
        public OptimulaGenerator WithTestResAgrTaskHours(decimal val)
        {
            TestResAgrTaskHours = val;
            return this;
        }
        public OptimulaGenerator WithTestResClothesPaymt(decimal val)
        {
            TestResClothesPaymt = val;
            return this;
        }
        public OptimulaGenerator WithTestResMealConPaymt(decimal val)
        {
            TestResMealConPaymt = val;
            return this;
        }
        public OptimulaGenerator WithTestResHomeOffPaymt(decimal val)
        {
            TestResHomeOffPaymt = val;
            return this;
        }
        public OptimulaGenerator WithTestImpMSalaryAward(decimal val)
        {
            TestImpMSalaryAward = val;
            return this;
        }
        public OptimulaGenerator WithTestResMSalaryAward(decimal val)
        {
            TestResMSalaryAward = val;
            return this;
        }
        public OptimulaGenerator WithTestImpHSalaryAward(decimal val)
        {
            TestImpHSalaryAward = val;
            return this;
        }
        public OptimulaGenerator WithTestResHSalaryAward(decimal val)
        {
            TestResHSalaryAward = val;
            return this;
        }
        public OptimulaGenerator WithTestImpFPremiumBase(decimal val)
        {
            TestImpFPremiumBase = val;
            return this;
        }
        public OptimulaGenerator WithTestResFPremiumBase(decimal val)
        {
            TestResFPremiumBase = val;
            return this;
        }
        public OptimulaGenerator WithTestImpFPremiumBoss(decimal val)
        {
            TestImpFPremiumBoss = val;
            return this;
        }
        public OptimulaGenerator WithTestResFPremiumBoss(decimal val)
        {
            TestResFPremiumBoss = val;
            return this;
        }
        public OptimulaGenerator WithTestImpFPremiumPers(decimal val)
        {
            TestImpFPremiumPers = val;
            return this;
        }
        public OptimulaGenerator WithTestResFPremiumPers(decimal val)
        {
            TestResFPremiumPers = val;
            return this;
        }
        public OptimulaGenerator WithTestImpQAverageBase(decimal val)
        {
            TestImpQAverageBase = val;
            return this;
        }
        public OptimulaGenerator WithTestImpAverPremsPay(decimal val)
        {
            TestImpAverPremsPay = val;
            return this;
        }
        public OptimulaGenerator WithTestImpAverVacasPay(decimal val)
        {
            TestImpAverVacasPay = val;
            return this;
        }
        public OptimulaGenerator WithTestImpAverOversPay(decimal val)
        {
            TestImpAverOversPay = val;
            return this;
        }
        public OptimulaGenerator WithTestResIncomesNetto(decimal val)
        {
            TestResIncomesNetto = val;
            return this;
        }
        public OptimulaGenerator WithTestResPaymentNetto(decimal val)
        {
            TestResPaymentNetto = val;
            return this;
        }
        public OptimulaGenerator WithTestResDiffValNetto(decimal val)
        {
            TestResDiffValNetto = val;
            return this;
        }
                                                                                                                        
        public static double ResultDivDouble(double dblUpper, double dblDown, double multiplex = 1.0)
        {
            if (dblDown == 0.0)
            {
                return 0;
            }

            double dblReturn = ((dblUpper / dblDown) * multiplex);

            return (dblReturn);
        }
        public static Int32 RoundDoubleToInt(double dblValue)
        {
            const double NEZADANO_N0DOUBLE = 0.0;
            double dblAdjusted5 = ((dblValue >= NEZADANO_N0DOUBLE) ? dblValue + 0.5 : dblValue - 0.5);
            double dblRoundRown = Math.Truncate(dblAdjusted5);
            return Convert.ToInt32(dblRoundRown);
        }
        public static string CcyFormatDouble(double dblValue)
        {
            string resultText = string.Format("{0:N2}", dblValue);
            // No fear of rounding and takes the default number format
            // decimal decValue = dblValue;
            // decValue.ToString("0.00");
            // dblValue.ToString("F");
            // String.Format("{0:0.00}", dblValue);
            return resultText;
        }
        public static string NumFormatDouble(double dblValue)
        {
            string resultText = string.Format("{0:0.00}", dblValue);
            // No fear of rounding and takes the default number format
            // decimal decValue = dblValue;
            // decValue.ToString("0.00");
            // dblValue.ToString("F");
            return resultText;
        }
        public static string NumFormatInteger(Int32 nValue)
        {
            string resultText = string.Format("{0:0}", nValue);
            // No fear of rounding and takes the default number format
            // decimal decValue = dblValue;
            // decValue.ToString("0.00");
            // dblValue.ToString("F");
            return resultText;
        }
        public static string HrsFormatHHMM(double dblValue)
        {
            int nIntSumMinut = RoundDoubleToInt(dblValue * 60);
            int nIntHours = nIntSumMinut / 60;
            int nIntMinut = nIntSumMinut % 60;

            return string.Format("{0}:{1:00}", nIntHours, nIntMinut);
        }
        public static string HrsFormatDouble(double dblValue)
        {
            return string.Format("{0:N2}", dblValue);
        }
        public static string HrdFormatDouble(double dblValue)
        {
            return string.Format("{0:0.00}", dblValue);
        }
        public static string DayFormatDouble(double dblValue)
        {
            return string.Format("{0:N2}", dblValue);
        }
        public static string DecFormatDouble(double dblValue)
        {
            return string.Format("{0:N4}", dblValue);
        }
        public static string CcyFormatIntX100(Int32 value)
        {
            double dblValue = ResultDivDouble(value, 100);
            return CcyFormatDouble(dblValue);
        }
        public static string NumFormatIntX(Int32 value, bool bIntNumbers)
        {
            if (bIntNumbers)
            {
                return NumFormatInteger(value / 100);
            }
            else
            {
                double dblValue = ResultDivDouble(value, 100);
                return NumFormatDouble(dblValue);
            }
        }
        public static string RatFormatIntX100(Int32 value)
        {
            double dblValue = ResultDivDouble(value, 10000);
            return NumFormatDouble(dblValue);
        }
        public static string NumFormatIntX100(Int32 value)
        {
            double dblValue = ResultDivDouble(value, 100);
            return NumFormatDouble(dblValue);
        }
        public static string HrsFormatIntX060(Int32 value)
        {
            double dblValue = ResultDivDouble(value, 60);
            return HrsFormatDouble(dblValue);
        }
        public static string HrdFormatIntX060(Int32 value)
        {
            double dblValue = ResultDivDouble(value, 60);
            return HrdFormatDouble(dblValue);
        }
        public static string DayFormatIntX100(Int32 value)
        {
            double dblValue = ResultDivDouble(value, 100);
            return DayFormatDouble(dblValue);
        }
        public static string DecFormatDecimal(decimal decValue)
        {
            //string resultText = string.Format("{0:N2}", decValue);
            string resultText = decValue.ToString("0.00");
            return resultText;
        }
    }
}

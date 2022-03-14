using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Procezor.Optimula.Registry.Constants;
using HraveMzdy.Procezor.Optimula.Registry.Providers;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Service.Types;

namespace Procezor.OptimulaTest.Examples
{
    public class OptimulaPamicaGenerator : OptimulaGenerator
    {
        public OptimulaPamicaGenerator(int id, string name, string number) : base(id, name, number)
        {
        }

        public static OptimulaPamicaGenerator Spec(Int32 id, string name, string number)
        {
            return new OptimulaPamicaGenerator(id, name, number);
        }
        public static OptimulaPamicaGenerator ParseSpec(Int32 id, string specString)
        {
            string[] specDefValues = specString.Split(';');
            if (specDefValues.Length < 1)
            {
                return new OptimulaPamicaGenerator(id, "Unknownn", "Unknownn");
            }
            if (specDefValues.Length < 2)
            {
                return new OptimulaPamicaGenerator(id, "Unknownn", specDefValues[0]);
            }
            var gen = new OptimulaPamicaGenerator(id, specDefValues[1], specDefValues[0]);

            Func<string, Int32>[] specParser = new Func<string, Int32>[]
            {
                ParseIntNumber,   //   1  EmployeeNumb	101
                ParseNANothing,   //   2  EmployeeName	Drahota Jakub
                ParseNANothing,   //   3  PeriodName  	202201
                ParseDecNumber,   //   3  AgrWorkTarif	105,00
                ParseDecNumber,   //   4  AgrWorkRatio	0,14
                ParseHrsNumber,   //   5  AgrHourLimit	0,00
                ParseDecNumber,   //   6  AgrWorkLimit	0,00
                ParseDecNumber,   //   7  ClothesHours	11,17
                ParseDecNumber,   //   8  ClothesDaily	57,00
                ParseDecNumber,   //   9  MealConDaily	57,00
                ParseDecNumber,   //  10  HomeOffTarif	0,00
                ParseHrsNumber,   //  11  HomeOffHours	0,00
                ParseDecNumber,   //  12  MSalaryAward	8 000,00
                ParseDecNumber,   //  13  HSalaryAward	0,00
                ParseDecNumber,   //  14  FPremiumBase	0,00
                ParseDecNumber,   //  15  FPremiumBoss	0,00
                ParseDecNumber,   //  16  FPremiumPers	0,00
                ParseHrsNumber,   //  17  FullSheetHrs	176,00
                ParseHrsNumber,   //  18  TimeSheetHrs	176,00
                ParseHrsNumber,   //  19  HoliSheetHrs	0,00
                ParseHrsNumber,   //  20  WorkSheetHrs	96,00
                ParseDecNumber,   //  21  WorkSheetDay	12,00
                ParseHrsNumber,   //  22  OverSheetHrs	40,00
                ParseHrsNumber,   //  23  VacaRecomHrs	80,00
                ParseHrsNumber,   //  24  PaidRecomHrs	0,00
                ParseHrsNumber,   //  25  HoliRecomHrs	0,00
                ParseNANothing,   //26  -----------
                ParseHrsNumber,   //   27  OverAllowHrs	40,00
                ParseDecNumber,   //   28  OverAllowRio	0,25
                ParseHrsNumber,   //   29  RestAllowHrs	0,00
                ParseDecNumber,   //   30  RestAllowRio	0,00
                ParseHrsNumber,   //   31  WendAllowHrs	0,00
                ParseDecNumber,   //   32  WendAllowRio	0,00
                ParseHrsNumber,   //   33  NighAllowHrs	18,25
                ParseDecNumber,   //   34  NighAllowRio	0,10
                ParseHrsNumber,   //   35  HoliAllowHrs	0,00
                ParseDecNumber,   //   36  HoliAllowRio	0,00
                ParseDecNumber,   //   37  QClothesBase	3 506,00
                ParseDecNumber,   //   38  QHOfficeBase	0,00
                ParseDecNumber,   //   39  QAgrWorkBase	8 852,00
                ParseDecNumber,   //   40  QSumWorkHour	912,08
            };
            Int32[] specIntValues = specDefValues.Zip(specParser).Select((x) => x.Second(x.First)).ToArray();


#if __ALL_VALUES__
            Func<Int32, OptimulaGenerator>[] specGenerator = new Func<Int32, OptimulaGenerator>[]
            {
                gen.WithNANothingVal,      //   1  EmployeeNumb	101
                gen.WithNANothingVal,      //   2  EmployeeName	Drahota Jakub
                gen.WithNANothingVal,      //   3  PeriodName  	202201
                gen.WithAgrWorkTarifVal,   //   4  AgrWorkTarif	105,00
                gen.WithAgrWorkRatioVal,   //   5  AgrWorkRatio	0,14
                gen.WithAgrHourLimitVal,   //   6  AgrHourLimit	0,00
                gen.WithAgrWorkLimitVal,   //   7  AgrWorkLimit	0,00
                gen.WithClothesHoursVal,   //   8  ClothesHours	11,17
                gen.WithClothesDailyVal,   //   9  ClothesDaily	106,00
                gen.WithMealConDailyVal,   //  10  MealConDaily	106,00
                gen.WithHomeOffTarifVal,   //  11  HomeOffTarif	0,00
                gen.WithHomeOffHoursVal,   //  12  HomeOffHours	0,00
                gen.WithMSalaryAwardVal,   //  13  MSalaryAward	8 000,00
                gen.WithHSalaryAwardVal,   //  14  HSalaryAward	0,00
                gen.WithFPremiumBaseVal,   //  15  FPremiumBase	0,00
                gen.WithFPremiumBossVal,   //  16  FPremiumBoss	0,00
                gen.WithFPremiumPersVal,   //  17  FPremiumPers	0,00
                gen.WithFullSheetHrsVal,   //  18  FullSheetHrs	176,00
                gen.WithTimeSheetHrsVal,   //  19  TimeSheetHrs	176,00
                gen.WithHoliSheetHrsVal,   //  20  HoliSheetHrs	0,00
                gen.WithWorkSheetHrsVal,   //  21  WorkSheetHrs	96,00
                gen.WithWorkSheetDayVal,   //  22  WorkSheetDay	12,00
                gen.WithOverSheetHrsVal,   //  23  OverSheetHrs	40,00
                gen.WithVacaRecomHrsVal,   //  24  VacaRecomHrs	80,00
                gen.WithPaidRecomHrsVal,   //  25  PaidRecomHrs	0,00
                gen.WithHoliRecomHrsVal,   //  26  HoliRecomHrs	0,00
                gen.WithOverAllowHrsVal,   //  27  OverAllowHrs	40,00
                gen.WithOverAllowRioVal,   //  28  OverAllowRio	0,25
                gen.WithRestAllowHrsVal,   //  29  RestAllowHrs	0,00
                gen.WithRestAllowRioVal,   //  30  RestAllowRio	0,00
                gen.WithWendAllowHrsVal,   //  31  WendAllowHrs	0,00
                gen.WithWendAllowRioVal,   //  32  WendAllowRio	0,00
                gen.WithNighAllowHrsVal,   //  33  NighAllowHrs	18,25
                gen.WithNighAllowRioVal,   //  34  NighAllowRio	0,10
                gen.WithHoliAllowHrsVal,   //  35  HoliAllowHrs	0,00
                gen.WithHoliAllowRioVal,   //  36  HoliAllowRio	0,00
                gen.WithQClothesBaseVal,   //  37  QClothesBase	3 506,00
                gen.WithQHOfficeBaseVal,   //  38  QHOfficeBase	0,00
                gen.WithQAgrWorkBaseVal,   //  39  QAgrWorkBase	8 852,00
                gen.WithQSumWorkHourVal,   //  40  QSumWorkHour	912,08
            };
#else
            Func<Int32, OptimulaGenerator>[] specGenerator = new Func<Int32, OptimulaGenerator>[]
            {
                gen.WithNANothingVal,      //   1  EmployeeNumb	101
                gen.WithNANothingVal,      //   2  EmployeeName	Drahota Jakub
                gen.WithNANothingVal,      //   3  PeriodName  	202201
                gen.WithAgrWorkTarifVal,   //   4  AgrWorkTarif	105,00
                gen.WithAgrWorkRatioVal,   //   5  AgrWorkRatio	0,14
                gen.WithAgrHourLimitVal,   //   6  AgrHourLimit	0,00
                gen.WithAgrWorkLimitVal,   //   7  AgrWorkLimit	0,00
                gen.WithClothesHoursVal,   //   8  ClothesHours	11,17
                gen.WithClothesDailyVal,   //   9  ClothesDaily	106,00
                gen.WithMealConDailyVal,   //  10  MealConDaily	106,00
                gen.WithHomeOffTarifVal,   //  11  HomeOffTarif	0,00
                gen.WithHomeOffHoursVal,   //  12  HomeOffHours	0,00
                gen.WithMSalaryAwardVal,   //  13  MSalaryAward	8 000,00
                gen.WithHSalaryAwardVal,   //  14  HSalaryAward	0,00
                gen.WithFPremiumBaseVal,   //  15  FPremiumBase	0,00
                gen.WithFPremiumBossVal,   //  16  FPremiumBoss	0,00
                gen.WithFPremiumPersVal,   //  17  FPremiumPers	0,00
                gen.WithFullSheetHrsVal,   //  18  FullSheetHrs	176,00
                gen.WithTimeSheetHrsVal,   //  19  TimeSheetHrs	176,00
                gen.WithHoliSheetHrsVal,   //  20  HoliSheetHrs	0,00
                gen.WithWorkSheetHrsVal,   //  21  WorkSheetHrs	96,00
                gen.WithWorkSheetDayVal,   //  22  WorkSheetDay	12,00
                gen.WithOverSheetHrsVal,   //  23  OverSheetHrs	40,00
                gen.WithVacaRecomHrsVal,   //  24  VacaRecomHrs	80,00
                gen.WithPaidRecomHrsVal,   //  25  PaidRecomHrs	0,00
                gen.WithHoliRecomHrsVal,   //  26  HoliRecomHrs	0,00
                gen.WithNANothingVal,      //  27  OverAllowHrs	40,00
                gen.WithNANothingVal,      //  28  OverAllowRio	0,25
                gen.WithNANothingVal,      //  29  RestAllowHrs	0,00
                gen.WithNANothingVal,      //  30  RestAllowRio	0,00
                gen.WithNANothingVal,      //  31  WendAllowHrs	0,00
                gen.WithNANothingVal,      //  32  WendAllowRio	0,00
                gen.WithNANothingVal,      //  33  NighAllowHrs	18,25
                gen.WithNANothingVal,      //  34  NighAllowRio	0,10
                gen.WithNANothingVal,      //  35  HoliAllowHrs	0,00
                gen.WithNANothingVal,      //  36  HoliAllowRio	0,00
                gen.WithNANothingVal,      //  37  QClothesBase	3 506,00
                gen.WithNANothingVal,      //  38  QHOfficeBase	0,00
                gen.WithNANothingVal,      //  39  QAgrWorkBase	8 852,00
                gen.WithNANothingVal,      //  40  QSumWorkHour	912,08
            };
#endif
            specIntValues.Zip(specGenerator).Select((x) => x.Second(x.First)).ToArray();

            return gen;
        }
        public override string[] BuildImportXlsString(IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            Int32 AgrWorkTarifVal = AgrWorkTarifFunc(this, period, ruleset, prevset);
            Int32 AgrWorkRatioVal = AgrWorkRatioFunc(this, period, ruleset, prevset);
            Int32 AgrHourLimitVal = AgrHourLimitFunc(this, period, ruleset, prevset);
            Int32 AgrWorkLimitVal = AgrWorkLimitFunc(this, period, ruleset, prevset);
            Int32 AgtWorkTarifVal = AgtWorkTarifFunc(this, period, ruleset, prevset);
            Int32 AgtWorkRatioVal = AgtWorkRatioFunc(this, period, ruleset, prevset);
            Int32 AgtHourLimitVal = AgtHourLimitFunc(this, period, ruleset, prevset);
            Int32 AgtWorkLimitVal = AgtWorkLimitFunc(this, period, ruleset, prevset);
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
                period.Code.ToString(),                 // C
                $"{CcyFormatIntX100(AgrWorkTarifVal)}", // D
                $"{NumFormatIntX100(AgrWorkRatioVal)}", // E
                $"{HrsFormatIntX060(AgrHourLimitVal)}", // F
                $"{NumFormatIntX100(AgrWorkLimitVal)}", // G
                $"{CcyFormatIntX100(ClothesHoursVal)}", // H
                $"{CcyFormatIntX100(ClothesDailyVal)}", // I
                $"{CcyFormatIntX100(MealConDailyVal)}", // J
                $"{CcyFormatIntX100(HomeOffTarifVal)}", // K
                $"{HrsFormatIntX060(HomeOffHoursVal)}", // L
                $"{CcyFormatIntX100(MSalaryAwardVal)}", // M
                $"{CcyFormatIntX100(HSalaryAwardVal)}", // N
                $"{CcyFormatIntX100(FPremiumBaseVal)}", // O  
                $"{CcyFormatIntX100(FPremiumBossVal)}", // P  
                $"{CcyFormatIntX100(FPremiumPersVal)}", // Q  
                $"{HrsFormatIntX060(FullSheetHrsVal)}", // R  
                $"{HrsFormatIntX060(TimeSheetHrsVal)}", // S  
                $"{HrsFormatIntX060(HoliSheetHrsVal)}", // T 
                $"{HrsFormatIntX060(WorkSheetHrsVal)}", // U
                $"{DayFormatIntX100(WorkSheetDayVal)}", // V
                $"{HrsFormatIntX060(OverSheetHrsVal)}", // W  
                $"{HrsFormatIntX060(VacaRecomHrsVal)}", // X  
                $"{HrsFormatIntX060(PaidRecomHrsVal)}", // Y   
                $"{HrsFormatIntX060(HoliRecomHrsVal)}", // Z     
                $"{HrsFormatIntX060(OverAllowHrsVal)}", // AA
                $"{CcyFormatIntX100(OverAllowRioVal)}", // AB
                $"{HrsFormatIntX060(RestAllowHrsVal)}", // AC
                $"{CcyFormatIntX100(RestAllowRioVal)}", // AD
                $"{HrsFormatIntX060(WendAllowHrsVal)}", // AE
                $"{CcyFormatIntX100(WendAllowRioVal)}", // AF
                $"{HrsFormatIntX060(NighAllowHrsVal)}", // AG
                $"{CcyFormatIntX100(NighAllowRioVal)}", // AH
                $"{HrsFormatIntX060(HoliAllowHrsVal)}", // AI
                $"{CcyFormatIntX100(HoliAllowRioVal)}", // AJ
                $"{CcyFormatIntX100(QClothesBaseVal)}", // AK
                $"{CcyFormatIntX100(QHOfficeBaseVal)}", // AL
                $"{CcyFormatIntX100(QAgrWorkBaseVal)}", // AM
                $"{CcyFormatIntX100(QSumWorkHourVal)}", // AN
            };
            if (WorkSheetHrsVal != 0)
            {
                string[] importResult = new string[] { string.Join('\t', valuesList) };

                return importResult;
            }
            return Array.Empty<string>();
        }
        public override string[] BuildImportCsvString(IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            Int32 AgrWorkTarifVal = AgrWorkTarifFunc(this, period, ruleset, prevset);
            Int32 AgrWorkRatioVal = AgrWorkRatioFunc(this, period, ruleset, prevset);
            Int32 AgrHourLimitVal = AgrHourLimitFunc(this, period, ruleset, prevset);
            Int32 AgrWorkLimitVal = AgrWorkLimitFunc(this, period, ruleset, prevset);
            Int32 AgtWorkTarifVal = AgtWorkTarifFunc(this, period, ruleset, prevset);
            Int32 AgtWorkRatioVal = AgtWorkRatioFunc(this, period, ruleset, prevset);
            Int32 AgtHourLimitVal = AgtHourLimitFunc(this, period, ruleset, prevset);
            Int32 AgtWorkLimitVal = AgtWorkLimitFunc(this, period, ruleset, prevset);
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
                period.Code.ToString(),                 // C
                $"{CcyFormatIntX100(AgrWorkTarifVal)}", // D
                $"{NumFormatIntX100(AgrWorkRatioVal)}", // E
                $"{HrsFormatIntX060(AgrHourLimitVal)}", // F
                $"{NumFormatIntX100(AgrWorkLimitVal)}", // G
                $"{CcyFormatIntX100(ClothesHoursVal)}", // H
                $"{CcyFormatIntX100(ClothesDailyVal)}", // I
                $"{CcyFormatIntX100(MealConDailyVal)}", // J
                $"{CcyFormatIntX100(HomeOffTarifVal)}", // K
                $"{HrsFormatIntX060(HomeOffHoursVal)}", // L
                $"{CcyFormatIntX100(MSalaryAwardVal)}", // M
                $"{CcyFormatIntX100(HSalaryAwardVal)}", // N
                $"{CcyFormatIntX100(FPremiumBaseVal)}", // O 
                $"{CcyFormatIntX100(FPremiumBossVal)}", // P 
                $"{CcyFormatIntX100(FPremiumPersVal)}", // Q 
                $"{HrsFormatIntX060(FullSheetHrsVal)}", // R 
                $"{HrsFormatIntX060(TimeSheetHrsVal)}", // S 
                $"{HrsFormatIntX060(HoliSheetHrsVal)}", // T 
                $"{HrsFormatIntX060(WorkSheetHrsVal)}", // U
                $"{DayFormatIntX100(WorkSheetDayVal)}", // V
                $"{HrsFormatIntX060(OverSheetHrsVal)}", // W 
                $"{HrsFormatIntX060(VacaRecomHrsVal)}", // X    
                $"{HrsFormatIntX060(PaidRecomHrsVal)}", // Y     
                $"{HrsFormatIntX060(HoliRecomHrsVal)}", // Z     
                $"{HrsFormatIntX060(OverAllowHrsVal)}", // AA
                $"{CcyFormatIntX100(OverAllowRioVal)}", // AB
                $"{HrsFormatIntX060(RestAllowHrsVal)}", // AC
                $"{CcyFormatIntX100(RestAllowRioVal)}", // AD
                $"{HrsFormatIntX060(WendAllowHrsVal)}", // AE
                $"{CcyFormatIntX100(WendAllowRioVal)}", // AF
                $"{HrsFormatIntX060(NighAllowHrsVal)}", // AG
                $"{CcyFormatIntX100(NighAllowRioVal)}", // AH
                $"{HrsFormatIntX060(HoliAllowHrsVal)}", // AI
                $"{CcyFormatIntX100(HoliAllowRioVal)}", // AJ
                $"{CcyFormatIntX100(QClothesBaseVal)}", // AK
                $"{CcyFormatIntX100(QHOfficeBaseVal)}", // AL
                $"{CcyFormatIntX100(QAgrWorkBaseVal)}", // AM
                $"{CcyFormatIntX100(QSumWorkHourVal)}", // AN
            };         
            
            if (WorkSheetHrsVal != 0)
            {
                string[] importResult = new string[] { string.Join(';', valuesList) + ";" };

                return importResult;
            }
            return Array.Empty<string>();
        }
        public override IEnumerable<ITermTarget> BuildSpecTargets(IPeriod period, IBundleProps ruleset, IBundleProps prevset)
        {
            var montCode = MonthCode.Get(period.Code);

            Int16 CONTRACT_NULL = 0;
            Int16 POSITION_NULL = 0;

            var contractEmp = ContractCode.Get(CONTRACT_NULL);
            var positionEmp = PositionCode.Get(POSITION_NULL);
            var variant1Emp = VariantCode.Get(1);

            Int32 AgrWorkTarifVal = AgrWorkTarifFunc(this, period, ruleset, prevset);
            Int32 AgrWorkRatioVal = AgrWorkRatioFunc(this, period, ruleset, prevset);
            Int32 AgrHourLimitVal = AgrHourLimitFunc(this, period, ruleset, prevset);
            Int32 AgrWorkLimitVal = AgrWorkLimitFunc(this, period, ruleset, prevset);
            Int32 AgtWorkTarifVal = AgtWorkTarifFunc(this, period, ruleset, prevset);
            Int32 AgtWorkRatioVal = AgtWorkRatioFunc(this, period, ruleset, prevset);
            Int32 AgtHourLimitVal = AgtHourLimitFunc(this, period, ruleset, prevset);
            Int32 AgtWorkLimitVal = AgtWorkLimitFunc(this, period, ruleset, prevset);
            Int32 ClothesHoursVal = ClothesHoursFunc(this, period, ruleset, prevset);
            Int32 ClothesDailyVal = ClothesDailyFunc(this, period, ruleset, prevset);
            Int32 MealConDailyVal = MealConDailyFunc(this, period, ruleset, prevset);
            Int32 HomeOffMonthVal = HomeOffMonthFunc(this, period, ruleset, prevset);
            Int32 HomeOffTarifVal = HomeOffTarifFunc(this, period, ruleset, prevset);
            Int32 HomeOffHoursVal = HomeOffHoursFunc(this, period, ruleset, prevset);

            Int32 MSalaryBasisVal = 0;
            Int32 MawardsBasisVal = MSalaryAwardFunc(this, period, ruleset, prevset);
            Int32 HawardsBasisVal = HSalaryAwardFunc(this, period, ruleset, prevset);
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

            // ContractTimePlan	CONTRACT_TIME_PLAN
            var contractTimePlan = new TimesheetsPlanTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_PLAN),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_TIMESHEETS_PLAN), FullSheetHrsVal);
            // ContractTimeWork	CONTRACT_TIME_WORK
            var contractTimeWork = new TimesheetsWorkTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_TIMESHEETS_WORK), TimeSheetHrsVal, HoliSheetHrsVal);
            // TimeactualWork	TIMEACTUAL_WORK
            var contractTimeActa = new TimeactualWorkTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_TIMEACTUAL_WORK), 
                WorkSheetHrsVal, WorkSheetDayVal, OverSheetHrsVal);
            // PaymentBasis		PAYMENT_BASIS
            var paymentMSalary = new PaymentBasisTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_MSALARY_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_PAYMENT_BASIS), 
                MSalaryBasisVal);
            // OptimusBasis		OPTIMUS_BASIS
            var optimusMawards = new OptimusBasisTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_MAWARDS_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_BASIS), 
                MawardsBasisVal);
            // ReducedBasis		REDUCED_BASIS
            var reducedMawards = new ReducedBasisTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_MAWARDS_RESULTS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_REDUCED_BASIS),
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_MAWARDS_TARGETS));
            // OptimusHours		OPTIMUS_HOURS
            var optimusHawards = new OptimusHoursTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_HAWARDS_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_HOURS),
                HawardsBasisVal, WorkSheetHrsVal);
            // ReducedHours		REDUCED_HOURS
            var reducedHawards = new ReducedHoursTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_HAWARDS_RESULTS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_REDUCED_HOURS),
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_HAWARDS_TARGETS));
            // OptimusFixed		OPTIMUS_FIXED
            var optPremiumBase = new OptimusFixedTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMIUM_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_FIXED), 
                FPremiumBaseVal);
            var optPremiumBoss = new OptimusFixedTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMADV_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_FIXED), 
                FPremiumBossVal);
            var optPremiumPers = new OptimusFixedTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMEXT_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_FIXED), 
                FPremiumPersVal);
            // ReducedFixed		REDUCED_FIXED
            var redPremiumBase = new ReducedFixedTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMIUM_RESULTS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_REDUCED_FIXED), 
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMIUM_TARGETS));           
            var redPremiumBoss = new ReducedFixedTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMADV_RESULTS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_REDUCED_FIXED), 
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMADV_TARGETS));   
            var redPremiumPers = new ReducedFixedTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMEXT_RESULTS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_REDUCED_FIXED), 
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_PREMEXT_TARGETS));          
            // AgrworkHours		AGRWORK_HOURS
            var allowceAgrwork = new AgrworkHoursTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_AGRWORK_TARGETS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_AGRWORK_HOURS), 
                AgrWorkTarifVal, AgrWorkRatioVal, AgrWorkLimitVal, AgrHourLimitVal);
            // AllowceHfull		ALLOWCE_HFULL
            var allowceHOffice = new AllowceHfullTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_ALLOWCE_HOFFICE),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_HFULL), 
                HomeOffTarifVal, HomeOffHoursVal);
            // AllowceDaily		ALLOWCE_DAILY
            var allowceClotDay = new AllowceDailyTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_ALLOWCE_CLOTDAY),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_DAILY),
                ClothesDailyVal);
            // AllowceHours		ALLOWCE_HOURS
            var allowceClotHrs = new AllowceHoursTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_ALLOWCE_CLOTHRS),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_HOURS),
                ClothesHoursVal);
            // AllowceDaily		ALLOWCE_DAILY
            var allowceMealDay = new AlldownDailyTarget(montCode, contractEmp, positionEmp, variant1Emp,
                ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_ALLOWCE_MEALDAY),
                ConceptCode.Get((Int32)OptimulaConceptConst.CONCEPT_ALLDOWN_DAILY),
                MealConDailyVal);

            var targets = new ITermTarget[] {
                contractTimePlan,
                contractTimeWork,
                contractTimeActa,
            };

            if (MSalaryBasisVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { paymentMSalary }).ToArray();
            }
            if (MawardsBasisVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { optimusMawards, reducedMawards }).ToArray();
            }
            if (HawardsBasisVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { optimusHawards, reducedHawards }).ToArray();
            }
            if (FPremiumBaseVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { optPremiumBase, redPremiumBase }).ToArray();
            }
            if (FPremiumBossVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { optPremiumBoss, redPremiumBoss }).ToArray();
            }
            if (FPremiumPersVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { optPremiumPers, redPremiumPers }).ToArray();
            }
            if (AgrWorkTarifVal != 0 && AgrWorkRatioVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceAgrwork }).ToArray();
            }
            if (HomeOffTarifVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceHOffice }).ToArray();
            }
            if (ClothesDailyVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceClotDay }).ToArray();
            }
            if (ClothesHoursVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceClotHrs }).ToArray();
            }
            if (MealConDailyVal != 0)
            {
                targets = targets.Concat(new ITermTarget[] { allowceMealDay }).ToArray();
            }

            return targets;
        }
    }
}

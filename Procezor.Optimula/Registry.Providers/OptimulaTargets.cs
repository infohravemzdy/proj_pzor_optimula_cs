using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HraveMzdy.Procezor.Service.Types;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Procezor.Optimula.Registry.Constants;

namespace HraveMzdy.Procezor.Optimula.Registry.Providers
{
    // TimesheetsPlan	TIMESHEETS_PLAN
    public class TimesheetsPlanTarget : OptimulaTermTarget
    {
        public Int32 FullSheetHrsVal { get; private set; }

        public TimesheetsPlanTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 fullSheetHrsVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            FullSheetHrsVal = fullSheetHrsVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Full Hours: {OperationsDec.Divide(this.FullSheetHrsVal, 60)}";
        }
    }

    // TimesheetsWork	TIMESHEETS_WORK
    public class TimesheetsWorkTarget : OptimulaTermTarget
    {
        public Int32 TimeSheetHrsVal { get; private set; }
        public Int32 HoliSheetHrsVal { get; private set; }

        public TimesheetsWorkTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 timeSheetHrsVal, Int32 holiSheetHrsVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            TimeSheetHrsVal = timeSheetHrsVal;
            HoliSheetHrsVal = holiSheetHrsVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Time Hours: {OperationsDec.Divide(this.TimeSheetHrsVal, 60)}, Holi Hours: {OperationsDec.Divide(this.HoliSheetHrsVal, 60)}";
        }
    }

    // TimeactualWork	TIMEACTUAL_WORK
    public class TimeactualWorkTarget : OptimulaTermTarget
    {
        public Int32 WorkSheetHrsVal { get; private set; }
        public Int32 WorkSheetDayVal { get; private set; }
        public Int32 OverSheetHrsVal { get; private set; }

        public TimeactualWorkTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 workSheetHrsVal, Int32 workSheetDayVal, Int32 overSheetHrsVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            WorkSheetHrsVal = workSheetHrsVal;
            WorkSheetDayVal = workSheetDayVal;
            OverSheetHrsVal = overSheetHrsVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Work Hours: {OperationsDec.Divide(this.WorkSheetHrsVal, 60)}, Work Days: {OperationsDec.Divide(this.WorkSheetDayVal, 100)}, Over Hours: {OperationsDec.Divide(this.OverSheetHrsVal, 100)}";
        }
    }

    // PaymentBasis		PAYMENT_BASIS
    public class PaymentBasisTarget : OptimulaTermTarget
    {
        public Int32 PaymentBasisVal { get; private set; }

        public PaymentBasisTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 paymentBasisVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            PaymentBasisVal = paymentBasisVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Basis: {OperationsDec.Divide(this.PaymentBasisVal, 100)}";
        }
    }

    // PaymentHours		PAYMENT_HOURS
    public class PaymentHoursTarget : OptimulaTermTarget
    {
        public Int32 PaymentBasisVal { get; private set; }
        public Int32 PaymentHoursVal { get; private set; }

        public PaymentHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 paymentBasisVal, Int32 paymentHoursVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            PaymentBasisVal = paymentBasisVal;
            PaymentHoursVal = paymentHoursVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Basis: {OperationsDec.Divide(this.PaymentBasisVal, 100)}, Target Hours: {OperationsDec.Divide(this.PaymentHoursVal, 60)}";
        }
    }

    // PaymentFixed		PAYMENT_FIXED
    public class PaymentFixedTarget : OptimulaTermTarget
    {
        public Int32 PaymentBasisVal { get; private set; }

        public PaymentFixedTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 paymentBasisVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            PaymentBasisVal = paymentBasisVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Basis: {OperationsDec.Divide(this.PaymentBasisVal, 100)}";
        }
    }

    // OptimusBasis		OPTIMUS_BASIS
    public class OptimusBasisTarget : OptimulaTermTarget
    {
        public Int32 OptimusBasisVal { get; private set; }

        public OptimusBasisTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 optimusBasisVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            OptimusBasisVal = optimusBasisVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Basis: {OperationsDec.Divide(this.OptimusBasisVal, 100)}";
        }
    }

    // OptimusHours		OPTIMUS_HOURS
    public class OptimusHoursTarget : OptimulaTermTarget
    {
        public Int32 OptimusBasisVal { get; private set; }
        public Int32 OptimusHoursVal { get; private set; }

        public OptimusHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 optimusBasisVal, Int32 optimusHoursVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            OptimusBasisVal = optimusBasisVal;
            OptimusHoursVal = optimusHoursVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Basis: {OperationsDec.Divide(this.OptimusBasisVal, 100)}, Target Hours: {OperationsDec.Divide(this.OptimusHoursVal, 60)}";
        }
    }

    // OptimusFixed		OPTIMUS_FIXED
    public class OptimusFixedTarget : OptimulaTermTarget
    {
        public Int32 OptimusBasisVal { get; private set; }

        public OptimusFixedTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 optimusBasisVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            OptimusBasisVal = optimusBasisVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Basis: {OperationsDec.Divide(this.OptimusBasisVal, 100)}";
        }
    }

    // OptimusNetto		OPTIMUS_NETTO
    public class OptimusNettoTarget : OptimulaTermTarget 
    {
        public Int32 OptimusBasisVal { get; private set; }
        public Int32 OptimusExtraVal { get; private set; }
    
        public OptimusNettoTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 optimusBasisVal, Int32 optimusExtraVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            OptimusBasisVal = optimusBasisVal;
            OptimusExtraVal = optimusExtraVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Basis: {OperationsDec.Divide(this.OptimusBasisVal, 100)}, Target Extra: {OperationsDec.Divide(this.OptimusExtraVal, 100)}";
        }
    }

    // ReducedBasis		REDUCED_BASIS
    public class ReducedBasisTarget : OptimulaTermTarget
    {
        public ArticleCode ArticleTarget { get; private set; }

        public ReducedBasisTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            ArticleCode articleTarget) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            ArticleTarget = articleTarget;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Article: {ServiceArticleEnumUtils.GetSymbol(this.ArticleTarget.Value)}";
        }
    }

    // ReducedHours		REDUCED_HOURS
    public class ReducedHoursTarget : OptimulaTermTarget
    {
        public ArticleCode ArticleTarget { get; private set; }

        public ReducedHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            ArticleCode articleTarget) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            ArticleTarget = articleTarget;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Article: {ServiceArticleEnumUtils.GetSymbol(this.ArticleTarget.Value)}";
        }
    }

    // ReducedFixed		REDUCED_FIXED
    public class ReducedFixedTarget : OptimulaTermTarget
    {
        public ArticleCode ArticleTarget { get; private set; }

        public ReducedFixedTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            ArticleCode articleTarget) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            ArticleTarget = articleTarget;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Article: {ServiceArticleEnumUtils.GetSymbol(this.ArticleTarget.Value)}";
        }
    }

    // ReducedNetto		REDUCED_NETTO
    public class ReducedNettoTarget : OptimulaTermTarget
    {
        public ArticleCode ArticleTarget { get; private set; }

        public ReducedNettoTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            ArticleCode articleTarget) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            ArticleTarget = articleTarget;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Article: {ServiceArticleEnumUtils.GetSymbol(this.ArticleTarget.Value)}";
        }
    }

    // AgrworkHours		AGRWORK_HOURS
    public class AgrworkHoursTarget : OptimulaTermTarget
    {
        public Int32 AgrWorkTarifVal { get; private set; }
        public Int32 AgrWorkRatioVal { get; private set; }
        public Int32 AgrWorkLimitVal { get; private set; }
        public Int32 AgrHourLimitVal { get; private set; }

        public AgrworkHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 agrWorkTarifVal, Int32 agrWorkRatioVal, Int32 agrWorkLimitVal, Int32 agrHourLimitVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AgrWorkTarifVal = agrWorkTarifVal;
            AgrWorkRatioVal = agrWorkRatioVal;
            AgrWorkLimitVal = agrWorkLimitVal;
            AgrHourLimitVal = agrHourLimitVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Tariff: {OperationsDec.Divide(this.AgrWorkTarifVal, 100)}, Target Ratio: {OperationsDec.Divide(this.AgrWorkRatioVal, 100)}, Limit Value: {OperationsDec.Divide(this.AgrWorkLimitVal, 100)}, Limit Hours: {OperationsDec.Divide(this.AgrHourLimitVal, 60)}";
        }
    }

    // AgrtaskHours		AGRTASK_HOURS
    public class AgrtaskHoursTarget : OptimulaTermTarget {
        public Int32 AgrWorkTarifVal { get; private set; }
        public Int32 AgrWorkRatioVal { get; private set; }
        public Int32 AgrWorkLimitVal { get; private set; }
        public Int32 AgrHourLimitVal { get; private set; }

        public AgrtaskHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 agrWorkTarifVal, Int32 agrWorkRatioVal, Int32 agrWorkLimitVal, Int32 agrHourLimitVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AgrWorkTarifVal = agrWorkTarifVal;
            AgrWorkRatioVal = agrWorkRatioVal;
            AgrWorkLimitVal = agrWorkLimitVal;
            AgrHourLimitVal = agrHourLimitVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Tariff: {OperationsDec.Divide(this.AgrWorkTarifVal, 100)}, Target Ratio: {OperationsDec.Divide(this.AgrWorkRatioVal, 100)}, Limit Value: {OperationsDec.Divide(this.AgrWorkLimitVal, 100)}, Limit Hours: {OperationsDec.Divide(this.AgrHourLimitVal, 60)}";
        }
    }

    // AllowceMfull		ALLOWCE_Mfull
    public class AllowceMfullTarget : OptimulaTermTarget
    {
        public Int32 AllowceBasisVal { get; private set; }

        public AllowceMfullTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceBasisVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceBasisVal = allowceBasisVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Basis: {OperationsDec.Divide(this.AllowceBasisVal, 100)}";
        }
    }

    // AllowceHfull		ALLOWCE_HFULL
    public class AllowceHfullTarget : OptimulaTermTarget
    {
        public Int32 AllowceTarifVal { get; private set; }
        public Int32 AllowceHfullVal { get; private set; }

        public AllowceHfullTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceTarifVal, Int32 allowceHfullVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceTarifVal = allowceTarifVal;
            AllowceHfullVal = allowceHfullVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Tariff: {OperationsDec.Divide(this.AllowceTarifVal, 100)}, Target Hours: {OperationsDec.Divide(this.AllowceHfullVal, 60)}";
        }
    }

    // AllowceHours		ALLOWCE_HOURS
    public class AllowceHoursTarget : OptimulaTermTarget
    {
        public Int32 AllowceTarifVal { get; private set; }

        public AllowceHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceTarifVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceTarifVal = allowceTarifVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Tariff: {OperationsDec.Divide(this.AllowceTarifVal, 100)}";
        }
    }

    // AllowceDaily		ALLOWCE_DAILY
    public class AllowceDailyTarget : OptimulaTermTarget
    {
        public Int32 AllowceDailyVal { get; private set; }

        public AllowceDailyTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceDailyVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceDailyVal = allowceDailyVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Daily: {OperationsDec.Divide(this.AllowceDailyVal, 100)}";
        }
    }

    // AlldownDaily		ALLDOWN_DAILY
    public class AlldownDailyTarget : OptimulaTermTarget {
        public Int32 AllowceDailyVal { get; private set; }

        public AlldownDailyTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceDailyVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceDailyVal = allowceDailyVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Daily: {OperationsDec.Divide(this.AllowceDailyVal, 100)}";
        }
    }

    // OffworkHours		OFFWORK_HOURS
    public class OffworkHoursTarget : OptimulaTermTarget
    {
        public Int32 AgrWorkTarifVal { get; private set; }
        public Int32 AgrWorkRatioVal { get; private set; }
        public Int32 AgrWorkLimitVal { get; private set; }
        public Int32 AgrHourLimitVal { get; private set; }

        public OffworkHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 agrWorkTarifVal, Int32 agrWorkRatioVal, Int32 agrWorkLimitVal, Int32 agrHourLimitVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AgrWorkTarifVal = agrWorkTarifVal;
            AgrWorkRatioVal = agrWorkRatioVal;
            AgrWorkLimitVal = agrWorkLimitVal;
            AgrHourLimitVal = agrHourLimitVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Tariff: {OperationsDec.Divide(this.AgrWorkTarifVal, 100)}, Target Ratio: {OperationsDec.Divide(this.AgrWorkRatioVal, 100)}, Limit Value: {OperationsDec.Divide(this.AgrWorkLimitVal, 100)}, Limit Hours: {OperationsDec.Divide(this.AgrHourLimitVal, 60)}";
        }
    }

    // OfftaskHours		OFFTASK_HOURS
    public class OfftaskHoursTarget : OptimulaTermTarget {
        public Int32 AgrWorkTarifVal { get; private set; }
        public Int32 AgrWorkRatioVal { get; private set; }
        public Int32 AgrWorkLimitVal { get; private set; }
        public Int32 AgrHourLimitVal { get; private set; }

        public OfftaskHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 agrWorkTarifVal, Int32 agrWorkRatioVal, Int32 agrWorkLimitVal, Int32 agrHourLimitVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AgrWorkTarifVal = agrWorkTarifVal;
            AgrWorkRatioVal = agrWorkRatioVal;
            AgrWorkLimitVal = agrWorkLimitVal;
            AgrHourLimitVal = agrHourLimitVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Tariff: {OperationsDec.Divide(this.AgrWorkTarifVal, 100)}, Target Ratio: {OperationsDec.Divide(this.AgrWorkRatioVal, 100)}, Limit Value: {OperationsDec.Divide(this.AgrWorkLimitVal, 100)}, Limit Hours: {OperationsDec.Divide(this.AgrHourLimitVal, 60)}";
        }
    }

    // OffsetsHfull		OFFSETS_HFULL
    public class OffsetsHfullTarget : OptimulaTermTarget
    {
        public Int32 AllowceTarifVal { get; private set; }
        public Int32 AllowceHfullVal { get; private set; }

        public OffsetsHfullTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceTarifVal, Int32 allowceHfullVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceTarifVal = allowceTarifVal;
            AllowceHfullVal = allowceHfullVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Tariff: {OperationsDec.Divide(this.AllowceTarifVal, 100)}, Target Hours: {OperationsDec.Divide(this.AllowceHfullVal, 60)}";
        }
    }

    // OffsetsHours		OFFSETS_HOURS
    public class OffsetsHoursTarget : OptimulaTermTarget
    {
        public Int32 AllowceTarifVal { get; private set; }

        public OffsetsHoursTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceTarifVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceTarifVal = allowceTarifVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Tariff: {OperationsDec.Divide(this.AllowceTarifVal, 100)}";
        }
    }

    // OffsetsDaily		OFFSETS_DAILY
    public class OffsetsDailyTarget : OptimulaTermTarget
    {
        public Int32 AllowceDailyVal { get; private set; }

        public OffsetsDailyTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceDailyVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceDailyVal = allowceDailyVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Daily: {OperationsDec.Divide(this.AllowceDailyVal, 100)}";
        }
    }

    // OffdownDaily		OFFDOWN_DAILY
    public class OffdownDailyTarget : OptimulaTermTarget {
        public Int32 AllowceDailyVal { get; private set; }

        public OffdownDailyTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept,
            Int32 allowceDailyVal) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
            AllowceDailyVal = allowceDailyVal;
        }
        public override string TargetMessage()
        {
            return $"Basis: {this.TargetBasis}, Target Daily: {OperationsDec.Divide(this.AllowceDailyVal, 100)}";
        }
    }

    // SettlemTargets   SETTLEM_TARGETS
    public class SettlemTargetsTarget : OptimulaTermTarget
    {
        public SettlemTargetsTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemTarnett	SETTLEM_TARNETT
    public class SettlemTarnettTarget : OptimulaTermTarget
    {
        public SettlemTarnettTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemAgrwork   SETTLEM_AGRWORK
    public class SettlemAgrworkTarget : OptimulaTermTarget
    {
        public SettlemAgrworkTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemAgrtask		SETTLEM_AGRTASK
    public class SettlemAgrtaskTarget : OptimulaTermTarget {
        public SettlemAgrtaskTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) : 
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemAllowce   SETTLEM_ALLOWCE
    public class SettlemAllowceTarget : OptimulaTermTarget
    {
        public SettlemAllowceTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemAllnett		SETTLEM_ALLNETT
    public class SettlemAllnettTarget : OptimulaTermTarget
    {
        public SettlemAllnettTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemOffwork		SETTLEM_OFFWORK
    public class SettlemOffworkTarget : OptimulaTermTarget
    {
        public SettlemOffworkTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemOfftask		SETTLEM_OFFTASK
    public class SettlemOfftaskTarget : OptimulaTermTarget {
        public SettlemOfftaskTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemOffsets		SETTLEM_OFFSETS
    public class SettlemOffsetsTarget : OptimulaTermTarget
    {
        public SettlemOffsetsTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemResults   SETTLEM_RESULTS
    public class SettlemResultsTarget : OptimulaTermTarget
    {
        public SettlemResultsTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // SettlemResnett	SETTLEM_RESNETT
    public class SettlemResnettTarget : OptimulaTermTarget 
    {
        public SettlemResnettTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) : 
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // IncomesTaxfree   INCOMES_TAXFREE
    public class IncomesTaxfreeTarget : OptimulaTermTarget
    {
        public IncomesTaxfreeTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // IncomesTaxbase   INCOMES_TAXBASE
    public class IncomesTaxbaseTarget : OptimulaTermTarget
    {
        public IncomesTaxbaseTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // IncomesTaxwins   INCOMES_TAXWINS
    public class IncomesTaxwinsTarget : OptimulaTermTarget
    {
        public IncomesTaxwinsTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

    // IncomesSummary   INCOMES_SUMMARY
    public class IncomesSummaryTarget : OptimulaTermTarget
    {
        public IncomesSummaryTarget(MonthCode monthCode, ContractCode contract, PositionCode position, VariantCode variant,
            ArticleCode article, ConceptCode concept) :
            base(monthCode, contract, position, variant, article, concept, BASIS_ZERO)
        {
        }
    }

}

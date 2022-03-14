using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Optimula.Registry.Constants;

namespace HraveMzdy.Procezor.Optimula.Registry.Providers
{
    // TimesheetsPlan	TIMESHEETS_PLAN
    public class TimesheetsPlanResult : OptimulaTermResult
    {
        public TimesheetsPlanResult(ITermTarget target, IArticleSpec spec,
            decimal fullSheetHrsVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            FullSheetHrsVal = fullSheetHrsVal;
        }

        public decimal FullSheetHrsVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Full Time: {this.FullSheetHrsVal}";
        }
    }

    // TimesheetsWork	TIMESHEETS_WORK
    public class TimesheetsWorkResult : OptimulaTermResult
    {
        public TimesheetsWorkResult(ITermTarget target, IArticleSpec spec,
            decimal timeSheetHrsVal, decimal holiSheetHrsVal, decimal workLoadHrsCoef,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            TimeSheetHrsVal = timeSheetHrsVal;
            HoliSheetHrsVal = holiSheetHrsVal;
            WorkLoadHrsCoef = workLoadHrsCoef;
        }
        public decimal TimeSheetHrsVal { get; private set; }
        public decimal HoliSheetHrsVal { get; private set; }
        public decimal WorkLoadHrsCoef { get; private set; }

        public override string ResultMessage()
        {
            return $"Work Time: {this.TimeSheetHrsVal}; Holi Time: {this.HoliSheetHrsVal}; Work Coeff: {this.WorkLoadHrsCoef}";
        }
    }

    // TimeactualWork	TIMEACTUAL_WORK
    public class TimeactualWorkResult : OptimulaTermResult
    {
        public TimeactualWorkResult(ITermTarget target, IArticleSpec spec,
            decimal workSheetHrsVal, decimal workSheetDayVal, decimal overSheetHrsVal, decimal workTimeHrsCoef, 
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            WorkSheetHrsVal = workSheetHrsVal;
            WorkSheetDayVal = workSheetDayVal;
            OverSheetHrsVal = overSheetHrsVal;
            WorkTimeHrsCoef = workTimeHrsCoef;
        }

        public decimal WorkSheetHrsVal { get; private set; }
        public decimal WorkSheetDayVal { get; private set; }
        public decimal OverSheetHrsVal { get; private set; }
        public decimal WorkTimeHrsCoef { get; private set; }

        public override string ResultMessage()
        {
            return $"Work Hours: {this.WorkSheetHrsVal}; Work Days: {this.WorkSheetDayVal}; Over Hours: {this.OverSheetHrsVal}; Work Coeff: {this.WorkTimeHrsCoef}";
        }
    }

    // PaymentBasis	PAYMENT_BASIS
    public class PaymentBasisResult : OptimulaTermResult
    {
        public PaymentBasisResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            PaymentBasisVal = paymentBasisVal;
        }
        public decimal PaymentBasisVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Payment Basis: {this.PaymentBasisVal}";
        }
    }

    // PaymentHours	PAYMENT_HOURS
    public class PaymentHoursResult : OptimulaTermResult
    {
        public PaymentHoursResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal, decimal paymentHoursVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            PaymentBasisVal = paymentBasisVal;
            PaymentHoursVal = paymentHoursVal;
        }
        public decimal PaymentBasisVal { get; private set; }
        public decimal PaymentHoursVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Payment Basis: {this.PaymentBasisVal}; Payment Hours: {this.PaymentHoursVal}";
        }
    }

    // PaymentFixed	PAYMENT_FIXED
    public class PaymentFixedResult : OptimulaTermResult
    {
        public PaymentFixedResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            PaymentBasisVal = paymentBasisVal;
        }
        public decimal PaymentBasisVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Payment Basis: {this.PaymentBasisVal}";
        }
    }

    // OptimusBasis	OPTIMUS_BASIS
    public class OptimusBasisResult : OptimulaTermResult
    {
        public OptimusBasisResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            OptimusBasisVal = paymentBasisVal;
        }
        public decimal OptimusBasisVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Optimus Basis: {this.OptimusBasisVal}";
        }
    }

    // OptimusHours	OPTIMUS_HOURS
    public class OptimusHoursResult : OptimulaTermResult
    {
        public OptimusHoursResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal, decimal paymentHoursVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            OptimusBasisVal = paymentBasisVal;
            OptimusHoursVal = paymentHoursVal;
        }
        public decimal OptimusBasisVal { get; private set; }
        public decimal OptimusHoursVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Optimus Basis: {this.OptimusBasisVal}; Optimus Hours: {this.OptimusHoursVal}";
        }
    }

    // OptimusFixed	OPTIMUS_FIXED
    public class OptimusFixedResult : OptimulaTermResult
    {
        public OptimusFixedResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            OptimusBasisVal = paymentBasisVal;
        }
        public decimal OptimusBasisVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Optimus Basis: {this.OptimusBasisVal}";
        }
    }

    // OptimusNetto	OPTIMUS_NETTO
    public class OptimusNettoResult : OptimulaTermResult 
    {
        public OptimusNettoResult(ITermTarget target, IArticleSpec spec, 
            decimal paymentBasisVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            OptimusBasisVal = paymentBasisVal;
        }
        public decimal OptimusBasisVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Optimus Basis: {this.OptimusBasisVal}";
        }
    }

    // ReducedBasis	REDUCED_BASIS
    public class ReducedBasisResult : OptimulaTermResult
    {
        public ReducedBasisResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            ReducedBasisVal = paymentBasisVal;
        }
        public decimal ReducedBasisVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Reduced Basis: {this.ReducedBasisVal}";
        }
    }

    // ReducedHours	REDUCED_HOURS
    public class ReducedHoursResult : OptimulaTermResult
    {
        public ReducedHoursResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal, decimal paymentHoursVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            ReducedBasisVal = paymentBasisVal;
            ReducedHoursVal = paymentHoursVal;
        }
        public decimal ReducedBasisVal { get; private set; }
        public decimal ReducedHoursVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Reduced Basis: {this.ReducedBasisVal}; Reduced Hours: {this.ReducedHoursVal}";
        }
    }

    // ReducedFixed	REDUCED_FIXED
    public class ReducedFixedResult : OptimulaTermResult
    {
        public ReducedFixedResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            ReducedBasisVal = paymentBasisVal;
        }
        public decimal ReducedBasisVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Reduced Basis: {this.ReducedBasisVal}";
        }
    }

    // ReducedNetto	REDUCED_NETTO
    public class ReducedNettoResult : OptimulaTermResult
    {
        public ReducedNettoResult(ITermTarget target, IArticleSpec spec,
            decimal paymentBasisVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            ReducedBasisVal = paymentBasisVal;
        }
        public decimal ReducedBasisVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Reduced Basis: {this.ReducedBasisVal}";
        }
    }

    // AgrworkHours	AGRWORK_HOURS
    public class AgrworkHoursResult : OptimulaTermResult
    {
        public AgrworkHoursResult(ITermTarget target, IArticleSpec spec,
            decimal agrWorkTarifVal, decimal agrWorkRatioVal, decimal agrWorkLimitVal, decimal agrHourLimitVal,
            decimal agrResultsHours, decimal agrResultsBasis,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AgrWorkTarifVal = agrWorkTarifVal;
            AgrWorkRatioVal = agrWorkRatioVal;
            AgrWorkLimitVal = agrWorkLimitVal;
            AgrHourLimitVal = agrHourLimitVal;
            AgrResultsHours = agrResultsHours;
            AgrResultsBasis = agrResultsBasis;
        }

        public decimal AgrWorkTarifVal { get; private set; }
        public decimal AgrWorkRatioVal { get; private set; }
        public decimal AgrWorkLimitVal { get; private set; }
        public decimal AgrHourLimitVal { get; private set; }
        public decimal AgrResultsHours { get; private set; }
        public decimal AgrResultsBasis { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Tariff: {this.AgrWorkTarifVal}; Ratio: {this.AgrWorkRatioVal}; Tariff Limit: {this.AgrWorkLimitVal}; Hours Limit: {this.AgrHourLimitVal}; Hours Result: {this.AgrResultsHours}; Tariff Result: {this.AgrResultsBasis}";
        }
    }

    // AgrtaskHours	AGRTASK_HOURS
    public class AgrtaskHoursResult : OptimulaTermResult {
        public AgrtaskHoursResult(ITermTarget target, IArticleSpec spec,
            decimal agrWorkTarifVal, decimal agrWorkRatioVal, decimal agrWorkLimitVal, decimal agrHourLimitVal,
            decimal agrResultsHours, decimal agrResultsBasis,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AgrWorkTarifVal = agrWorkTarifVal;
            AgrWorkRatioVal = agrWorkRatioVal;
            AgrWorkLimitVal = agrWorkLimitVal;
            AgrHourLimitVal = agrHourLimitVal;
            AgrResultsHours = agrResultsHours;
            AgrResultsBasis = agrResultsBasis;
        }

        public decimal AgrWorkTarifVal { get; private set; }
        public decimal AgrWorkRatioVal { get; private set; }
        public decimal AgrWorkLimitVal { get; private set; }
        public decimal AgrHourLimitVal { get; private set; }
        public decimal AgrResultsHours { get; private set; }
        public decimal AgrResultsBasis { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Tariff: {this.AgrWorkTarifVal}; Ratio: {this.AgrWorkRatioVal}; Tariff Limit: {this.AgrWorkLimitVal}; Hours Limit: {this.AgrHourLimitVal}; Hours Result: {this.AgrResultsHours}; Tariff Result: {this.AgrResultsBasis}";
        }
    }

    // AllowceMfull	ALLOWCE_MFULL
    public class AllowceMfullResult : OptimulaTermResult
    {
        public AllowceMfullResult(ITermTarget target, IArticleSpec spec,
            decimal allowceBasisVal, 
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceBasisVal = allowceBasisVal;
        }

        public decimal AllowceBasisVal { get; private set; }
      
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Target Basis: {this.AllowceBasisVal}";
        }
    }

    // AllowceHfull	ALLOWCE_HFULL
    public class AllowceHfullResult : OptimulaTermResult
    {
        public AllowceHfullResult(ITermTarget target, IArticleSpec spec,
            decimal allowceTarifVal, decimal allowceHfullVal, 
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceTarifVal = allowceTarifVal;
            AllowceHfullVal = allowceHfullVal;
        }

        public decimal AllowceTarifVal { get; private set; }
        public decimal AllowceHfullVal { get; private set; }
       
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Tariff: {this.AllowceTarifVal}; Full Hours: {this.AllowceHfullVal}";
        }
    }

    // AllowceHours	ALLOWCE_HOURS
    public class AllowceHoursResult : OptimulaTermResult
    {
        public AllowceHoursResult(ITermTarget target, IArticleSpec spec,
            decimal allowceTarifVal, 
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceTarifVal = allowceTarifVal;
        }
        public decimal AllowceTarifVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // AllowceDaily	ALLOWCE_DAILY
    public class AllowceDailyResult : OptimulaTermResult
    {
        public AllowceDailyResult(ITermTarget target, IArticleSpec spec,
            decimal allowceDailyVal, 
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceDailyVal = allowceDailyVal;
        }
        public decimal AllowceDailyVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // AlldownDaily	ALLDOWN_DAILY
    public class AlldownDailyResult : OptimulaTermResult {
        public AlldownDailyResult(ITermTarget target, IArticleSpec spec,
            decimal allowceDailyVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceDailyVal = allowceDailyVal;
        }
        public decimal AllowceDailyVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // OffworkHours	OFFWORK_HOURS
    public class OffworkHoursResult : OptimulaTermResult
    {
        public OffworkHoursResult(ITermTarget target, IArticleSpec spec,
            decimal agrWorkTarifVal, decimal agrWorkRatioVal, decimal agrWorkLimitVal, decimal agrHourLimitVal,
            decimal agrResultsHours, decimal agrResultsBasis,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AgrWorkTarifVal = agrWorkTarifVal;
            AgrWorkRatioVal = agrWorkRatioVal;
            AgrWorkLimitVal = agrWorkLimitVal;
            AgrHourLimitVal = agrHourLimitVal;
            AgrResultsHours = agrResultsHours;
            AgrResultsBasis = agrResultsBasis;
        }

        public decimal AgrWorkTarifVal { get; private set; }
        public decimal AgrWorkRatioVal { get; private set; }
        public decimal AgrWorkLimitVal { get; private set; }
        public decimal AgrHourLimitVal { get; private set; }
        public decimal AgrResultsHours { get; private set; }
        public decimal AgrResultsBasis { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Tariff: {this.AgrWorkTarifVal}; Ratio: {this.AgrWorkRatioVal}; Tariff Limit: {this.AgrWorkLimitVal}; Hours Limit: {this.AgrHourLimitVal}; Hours Result: {this.AgrResultsHours}; Tariff Result: {this.AgrResultsBasis}";
        }
    }

    // OfftaskHours	OFFTASK_HOURS
    public class OfftaskHoursResult : OptimulaTermResult {
        public OfftaskHoursResult(ITermTarget target, IArticleSpec spec,
            decimal agrWorkTarifVal, decimal agrWorkRatioVal, decimal agrWorkLimitVal, decimal agrHourLimitVal,
            decimal agrResultsHours, decimal agrResultsBasis,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AgrWorkTarifVal = agrWorkTarifVal;
            AgrWorkRatioVal = agrWorkRatioVal;
            AgrWorkLimitVal = agrWorkLimitVal;
            AgrHourLimitVal = agrHourLimitVal;
            AgrResultsHours = agrResultsHours;
            AgrResultsBasis = agrResultsBasis;
        }

        public decimal AgrWorkTarifVal { get; private set; }
        public decimal AgrWorkRatioVal { get; private set; }
        public decimal AgrWorkLimitVal { get; private set; }
        public decimal AgrHourLimitVal { get; private set; }
        public decimal AgrResultsHours { get; private set; }
        public decimal AgrResultsBasis { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Tariff: {this.AgrWorkTarifVal}; Ratio: {this.AgrWorkRatioVal}; Tariff Limit: {this.AgrWorkLimitVal}; Hours Limit: {this.AgrHourLimitVal}; Hours Result: {this.AgrResultsHours}; Tariff Result: {this.AgrResultsBasis}";
        }
    }

    // OffsetsHfull	OFFSETS_HFULL
    public class OffsetsHfullResult : OptimulaTermResult
    {
        public OffsetsHfullResult(ITermTarget target, IArticleSpec spec,
            decimal allowceTarifVal, decimal allowceHfullVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceTarifVal = allowceTarifVal;
            AllowceHfullVal = allowceHfullVal;
        }

        public decimal AllowceTarifVal { get; private set; }
        public decimal AllowceHfullVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}; Tariff: {this.AllowceTarifVal}; Full Hours: {this.AllowceHfullVal}";
        }
    }

    // OffsetsHours	OFFSETS_HOURS
    public class OffsetsHoursResult : OptimulaTermResult
    {
        public OffsetsHoursResult(ITermTarget target, IArticleSpec spec,
            decimal allowceTarifVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceTarifVal = allowceTarifVal;
        }
        public decimal AllowceTarifVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // OffsetsDaily	OFFSETS_DAILY
    public class OffsetsDailyResult : OptimulaTermResult
    {
        public OffsetsDailyResult(ITermTarget target, IArticleSpec spec,
            decimal allowceDailyVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceDailyVal = allowceDailyVal;
        }
        public decimal AllowceDailyVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // OffdownDaily	OFFDOWN_DAILY
    public class OffdownDailyResult : OptimulaTermResult {
        public OffdownDailyResult(ITermTarget target, IArticleSpec spec,
            decimal allowceDailyVal,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
            AllowceDailyVal = allowceDailyVal;
        }
        public decimal AllowceDailyVal { get; private set; }

        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemTargets	SETTLEM_TARGETS
    public class SettlemTargetsResult : OptimulaTermResult
    {
        public SettlemTargetsResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemTarnett	SETTLEM_TARNETT
    public class SettlemTarnettResult : OptimulaTermResult {
        public SettlemTarnettResult(ITermTarget target, IArticleSpec spec, 
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemAgrwork	SETTLEM_AGRWORK
    public class SettlemAgrworkResult : OptimulaTermResult
    {
        public SettlemAgrworkResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemAgrtask	SETTLEM_AGRTASK
    public class SettlemAgrtaskResult : OptimulaTermResult {
        public SettlemAgrtaskResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemAllowce	SETTLEM_ALLOWCE
    public class SettlemAllowceResult : OptimulaTermResult
    {
        public SettlemAllowceResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemAllnett	SETTLEM_ALLNETT
    public class SettlemAllnettResult : OptimulaTermResult 
    {
        public SettlemAllnettResult(ITermTarget target, IArticleSpec spec, 
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemOffwork	SETTLEM_OFFWORK
    public class SettlemOffworkResult : OptimulaTermResult
    {
        public SettlemOffworkResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemOfftask	SETTLEM_OFFTASK
    public class SettlemOfftaskResult : OptimulaTermResult {
        public SettlemOfftaskResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemOffsets	SETTLEM_OFFSETS
    public class SettlemOffsetsResult : OptimulaTermResult
    {
        public SettlemOffsetsResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemResults	SETTLEM_RESULTS
    public class SettlemResultsResult : OptimulaTermResult
    {
        public SettlemResultsResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // SettlemResnett	SETTLEM_RESNETT
    public class SettlemResnettResult : OptimulaTermResult {
        public SettlemResnettResult(ITermTarget target, IArticleSpec spec, 
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // IncomesTaxfree	INCOMES_TAXFREE
    public class IncomesTaxfreeResult : OptimulaTermResult
    {
        public IncomesTaxfreeResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // IncomesTaxbase	INCOMES_TAXBASE
    public class IncomesTaxbaseResult : OptimulaTermResult
    {
        public IncomesTaxbaseResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // IncomesTaxwins	INCOMES_TAXWINS
    public class IncomesTaxwinsResult : OptimulaTermResult
    {
        public IncomesTaxwinsResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

    // IncomesSummary	INCOMES_SUMMARY
    public class IncomesSummaryResult : OptimulaTermResult
    {
        public IncomesSummaryResult(ITermTarget target, IArticleSpec spec,
            Int32 value, Int32 basis) : base(target, spec, value, basis)
        {
        }
        public override string ResultMessage()
        {
            return $"Value: {this.ResultValue}; Basis: {this.ResultBasis}";
        }
    }

}

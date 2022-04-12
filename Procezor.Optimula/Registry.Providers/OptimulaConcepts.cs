using System;
using System.Collections.Generic;
using System.Linq;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service.Errors;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Service.Providers;
using HraveMzdy.Procezor.Service.Types;
using HraveMzdy.Procezor.Optimula.Registry.Constants;
using MaybeMonad;
using ResultMonad;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Procezor.Optimula.Service;

namespace HraveMzdy.Procezor.Optimula.Registry.Providers
{
    // TimesheetsPlan   TIMESHEETS_PLAN
    class TimesheetsPlanConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_TIMESHEETS_PLAN;
        public TimesheetsPlanConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new TimesheetsPlanConSpec(this.Code.Value);
        }
    }

    class TimesheetsPlanConSpec : OptimulaConceptSpec
    {
        public TimesheetsPlanConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new TimesheetsPlanTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<TimesheetsPlanTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            TimesheetsPlanTarget evalTarget = resTarget.Value;

            decimal fullSheetHrsVal = OperationsDec.Divide(evalTarget.FullSheetHrsVal, 60);

            ITermResult resultsValues = new TimesheetsPlanResult(target, spec, 
                fullSheetHrsVal, 
                0, 0);

            return BuildOkResults(resultsValues);
        }
    }

    // TimesheetsWork   TIMESHEETS_WORK
    class TimesheetsWorkConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_TIMESHEETS_WORK;
        public TimesheetsWorkConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new TimesheetsWorkConSpec(this.Code.Value);
        }
    }

    class TimesheetsWorkConSpec : OptimulaConceptSpec
    {
        public TimesheetsWorkConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_PLAN,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new TimesheetsWorkTarget(month, con, pos, var, article, this.Code, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<TimesheetsWorkTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            TimesheetsWorkTarget evalTarget = resTarget.Value;

            decimal timeSheetHrsVal = OperationsDec.Divide(evalTarget.TimeSheetHrsVal, 60);
            decimal holiSheetHrsVal = OperationsDec.Divide(evalTarget.HoliSheetHrsVal, 60);

            var resTimePlan = GetContractResult<TimesheetsPlanResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_PLAN));

            if (resTimePlan.IsFailure)
            {
                return BuildFailResults(resTimePlan.Error);
            }

            var evalTimePlan = resTimePlan.Value;

            decimal hoursWorkPlan = evalTimePlan.FullSheetHrsVal;

            decimal hoursWorkCoef = salaryRules.CoeffWithPartAndFullHours(timeSheetHrsVal, hoursWorkPlan);

            ITermResult resultsValues = new TimesheetsWorkResult(target, spec,
                timeSheetHrsVal, holiSheetHrsVal, hoursWorkCoef,
                0, 0);

            return BuildOkResults(resultsValues);
        }
    }

    // TimeactualWork	TIMEACTUAL_WORK
    class TimeactualWorkConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_TIMEACTUAL_WORK;
        public TimeactualWorkConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new TimeactualWorkConSpec(this.Code.Value);
        }
    }

    class TimeactualWorkConSpec : OptimulaConceptSpec
    {
        public TimeactualWorkConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new TimeactualWorkTarget(month, con, pos, var, article, this.Code, 0, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<TimeactualWorkTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            TimeactualWorkTarget evalTarget = resTarget.Value;

            decimal workSheetHrsVal = OperationsDec.Divide(evalTarget.WorkSheetHrsVal, 60);
            decimal workSheetDayVal = OperationsDec.Divide(evalTarget.WorkSheetDayVal, 100); 
            decimal overSheetHrsVal = OperationsDec.Divide(evalTarget.OverSheetHrsVal, 60);

            var resTimeWork = GetContractResult<TimesheetsWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK));

            if (resTimeWork.IsFailure)
            {
                return BuildFailResults(resTimeWork.Error);
            }

            var evalTimeWork = resTimeWork.Value;

            decimal hoursTimeWork = evalTimeWork.TimeSheetHrsVal;

            decimal hoursWorkCoef = salaryRules.CoeffWithPartAndFullHours(hoursTimeWork, workSheetHrsVal);

            ITermResult resultsValues = new TimeactualWorkResult(target, spec,
                workSheetHrsVal, workSheetDayVal, overSheetHrsVal, hoursWorkCoef,
                0, 0);

            return BuildOkResults(resultsValues);
        }
    }

    // PaymentBasis		PAYMENT_BASIS
    class PaymentBasisConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_PAYMENT_BASIS;
        public PaymentBasisConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new PaymentBasisConSpec(this.Code.Value);
        }
    }

    class PaymentBasisConSpec : OptimulaConceptSpec
    {
        public PaymentBasisConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new PaymentBasisTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<PaymentBasisTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            PaymentBasisTarget evalTarget = resTarget.Value;

            decimal paymentBasisVal = OperationsDec.Divide(evalTarget.PaymentBasisVal, 100);

            var resTimeWork = GetContractResult<TimesheetsWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK));

            if (resTimeWork.IsFailure)
            {
                return BuildFailResults(resTimeWork.Error);
            }

            var evalTimeWork = resTimeWork.Value;

            decimal hoursTimeCoef = evalTimeWork.WorkLoadHrsCoef;

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal hoursWorkCoef = evalTimeActa.WorkTimeHrsCoef;

            decimal paymentValueRes = salaryRules.RelativePaymentWithMonthlyAndCoeffAndWorkCoeff(
                paymentBasisVal, hoursTimeCoef, hoursWorkCoef);

            ITermResult resultsValues = new PaymentBasisResult(target, spec,
                paymentBasisVal, 
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // PaymentHours		PAYMENT_HOURS
    class PaymentHoursConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_PAYMENT_HOURS;
        public PaymentHoursConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new PaymentHoursConSpec(this.Code.Value);
        }
    }

    class PaymentHoursConSpec : OptimulaConceptSpec
    {
        public PaymentHoursConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new PaymentHoursTarget(month, con, pos, var, article, this.Code, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<PaymentHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            PaymentHoursTarget evalTarget = resTarget.Value;

            decimal paymentBasisVal = OperationsDec.Divide(evalTarget.PaymentBasisVal, 100);
            decimal paymentHoursVal = OperationsDec.Divide(evalTarget.PaymentHoursVal, 60);

            decimal paymentValueRes = salaryRules.PaymentWithTariffAndUnits(paymentBasisVal, paymentHoursVal);

            ITermResult resultsValues = new PaymentHoursResult(target, spec,
                paymentBasisVal, paymentHoursVal, 
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // PaymentFixed		PAYMENT_FIXED
    class PaymentFixedConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_PAYMENT_FIXED;
        public PaymentFixedConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new PaymentFixedConSpec(this.Code.Value);
        }
    }

    class PaymentFixedConSpec : OptimulaConceptSpec
    {
        public PaymentFixedConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new PaymentFixedTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<PaymentFixedTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            PaymentFixedTarget evalTarget = resTarget.Value;

            decimal paymentBasisVal = OperationsDec.Divide(evalTarget.PaymentBasisVal, 100);

            decimal paymentValueRes = salaryRules.PaymentWithAmountFixed(paymentBasisVal);

            ITermResult resultsValues = new PaymentFixedResult(target, spec,
                paymentBasisVal,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OptimusBasis		OPTIMUS_BASIS
    class OptimusBasisConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_BASIS;
        public OptimusBasisConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new OptimusBasisConSpec(this.Code.Value);
        }
    }

    class OptimusBasisConSpec : OptimulaConceptSpec
    {
        public OptimusBasisConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OptimusBasisTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OptimusBasisTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OptimusBasisTarget evalTarget = resTarget.Value;

            decimal paymentBasisVal = OperationsDec.Divide(evalTarget.OptimusBasisVal, 100);

            var resTimeWork = GetContractResult<TimesheetsWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK));

            if (resTimeWork.IsFailure)
            {
                return BuildFailResults(resTimeWork.Error);
            }

            var evalTimeWork = resTimeWork.Value;

            decimal hoursTimeCoef = evalTimeWork.WorkLoadHrsCoef;

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal hoursWorkCoef = evalTimeActa.WorkTimeHrsCoef;

            decimal paymentValueRes = salaryRules.RelativePaymentWithMonthlyAndCoeffAndWorkCoeff(
                paymentBasisVal, hoursTimeCoef, hoursWorkCoef);

            ITermResult resultsValues = new OptimusBasisResult(target, spec,
                paymentBasisVal,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OptimusHours		OPTIMUS_HOURS
    class OptimusHoursConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_HOURS;
        public OptimusHoursConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new OptimusHoursConSpec(this.Code.Value);
        }
    }

    class OptimusHoursConSpec : OptimulaConceptSpec
    {
        public OptimusHoursConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OptimusHoursTarget(month, con, pos, var, article, this.Code, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OptimusHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OptimusHoursTarget evalTarget = resTarget.Value;

            decimal paymentBasisVal = OperationsDec.Divide(evalTarget.OptimusBasisVal, 100);
            decimal paymentHoursVal = OperationsDec.Divide(evalTarget.OptimusHoursVal, 60);

            decimal paymentValueRes = salaryRules.PaymentWithTariffAndUnits(paymentBasisVal, paymentHoursVal);

            ITermResult resultsValues = new OptimusHoursResult(target, spec,
                paymentBasisVal, paymentHoursVal,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OptimusFixed		OPTIMUS_FIXED
    class OptimusFixedConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_FIXED;
        public OptimusFixedConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new OptimusFixedConSpec(this.Code.Value);
        }
    }

    class OptimusFixedConSpec : OptimulaConceptSpec
    {
        public OptimusFixedConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OptimusFixedTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OptimusFixedTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OptimusFixedTarget evalTarget = resTarget.Value;

            decimal paymentBasisVal = OperationsDec.Divide(evalTarget.OptimusBasisVal, 100);

            decimal paymentValueRes = salaryRules.PaymentWithAmountFixed(paymentBasisVal);

            ITermResult resultsValues = new OptimusFixedResult(target, spec,
                paymentBasisVal,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OptimusNetto			OPTIMUS_NETTO
    class OptimusNettoConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OPTIMUS_NETTO;
        public OptimusNettoConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new OptimusNettoConSpec(this.Code.Value);
        }
    }

    class OptimusNettoConSpec : OptimulaConceptSpec {
        public OptimusNettoConSpec(Int32 code) : base(code) {
            Path = new List<ArticleCode>();
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OptimusNettoTarget(month, con, pos, var, article, this.Code, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OptimusNettoTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OptimusNettoTarget evalTarget = resTarget.Value;
    
            decimal paymentBasisVal = OperationsDec.Divide(evalTarget.OptimusBasisVal, 100);
            decimal paymentExtraVal = OperationsDec.Divide(evalTarget.OptimusExtraVal, 100);

            decimal paymentValueRes = salaryRules.PaymentWithAmountFixed(paymentBasisVal+paymentExtraVal);

            ITermResult resultsValues = new OptimusNettoResult(target, spec,
                paymentBasisVal,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // ReducedBasis		REDUCED_BASIS
    class ReducedBasisConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_REDUCED_BASIS;
        public ReducedBasisConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new ReducedBasisConSpec(this.Code.Value);
        }
    }

    class ReducedBasisConSpec : OptimulaConceptSpec
    {
        public ReducedBasisConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new ReducedBasisTarget(month, con, pos, var, article, this.Code, ArticleCode.Zero),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<ReducedBasisTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            ReducedBasisTarget evalTarget = resTarget.Value;

            var resTimeWork = GetContractResult<TimesheetsWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK));

            if (resTimeWork.IsFailure)
            {
                return BuildFailResults(resTimeWork.Error);
            }

            var evalTimeWork = resTimeWork.Value;

            decimal hoursTimeCoef = evalTimeWork.WorkLoadHrsCoef;

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal hoursWorkCoef = evalTimeActa.WorkTimeHrsCoef;

            var resOptimusTargets = GetContractResult<OptimusBasisResult>(target, period, results,
               target.Contract, evalTarget.ArticleTarget);

            if (resOptimusTargets.IsFailure)
            {
                return BuildFailResults(resOptimusTargets.Error);
            }

            var evalOptimusTargets = resOptimusTargets.Value;

            var resSettlemAllowce = GetContractResult<SettlemAllowceResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE));

            if (resSettlemAllowce.IsFailure)
            {
                return BuildFailResults(resSettlemAllowce.Error);
            }

            var evalSettlemAllowce = resSettlemAllowce.Value;

            decimal settlemAllowceVal = evalSettlemAllowce.ResultValue;
            decimal optimusTargetsVal = evalOptimusTargets.ResultValue;

            var resSettlemAgrwork = GetContractResult<SettlemAgrworkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK));

            if (resSettlemAgrwork.IsFailure)
            {
                return BuildFailResults(resSettlemAgrwork.Error);
            }

            var evalSettlemAgrwork = resSettlemAgrwork.Value;

            decimal settlemAgrWorkVal = evalSettlemAgrwork.ResultValue;

            var settlemResultsList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_RESULTS))))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultBasis)).ToArray();

            decimal settlemResultsBasis = settlemResultsList.Aggregate(decimal.Zero,
                (agr, basis) => decimal.Add(agr, basis));

            decimal settlemDiffsVal = (optimusTargetsVal + settlemResultsBasis - settlemAllowceVal - settlemAgrWorkVal);

            decimal reducedBasisVal = salaryRules.ReverzedPaymentWithMonthlyAndCoeffAndWorkCoeff(Math.Max(0, settlemDiffsVal), 
                hoursTimeCoef, hoursWorkCoef);
            decimal reducedResValue = Math.Max(0, settlemDiffsVal);
            decimal reducedResBasis = Math.Max(0, optimusTargetsVal - reducedBasisVal);

            ITermResult resultsValues = new ReducedBasisResult(target, spec,
                reducedBasisVal,
                RoundToInt(reducedResValue), RoundToInt(reducedResBasis));

            return BuildOkResults(resultsValues);
        }
    }

    // ReducedHours		REDUCED_HOURS
    class ReducedHoursConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_REDUCED_HOURS;
        public ReducedHoursConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new ReducedHoursConSpec(this.Code.Value);
        }
    }

    class ReducedHoursConSpec : OptimulaConceptSpec
    {
        public ReducedHoursConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new ReducedHoursTarget(month, con, pos, var, article, this.Code, ArticleCode.Zero),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<ReducedHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            ReducedHoursTarget evalTarget = resTarget.Value;

            var resOptimusTargets = GetContractResult<OptimusHoursResult>(target, period, results,
               target.Contract, evalTarget.ArticleTarget);

            if (resOptimusTargets.IsFailure)
            {
                return BuildFailResults(resOptimusTargets.Error);
            }

            var evalOptimusTargets = resOptimusTargets.Value;

            var resSettlemAllowce = GetContractResult<SettlemAllowceResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE));

            if (resSettlemAllowce.IsFailure)
            {
                return BuildFailResults(resSettlemAllowce.Error);
            }

            var evalSettlemAllowce = resSettlemAllowce.Value;

            decimal settlemAllowceVal = evalSettlemAllowce.ResultValue;
            decimal optimusTargetsVal = evalOptimusTargets.ResultValue;

            var resSettlemAgrwork = GetContractResult<SettlemAgrworkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK));

            if (resSettlemAgrwork.IsFailure)
            {
                return BuildFailResults(resSettlemAgrwork.Error);
            }

            var evalSettlemAgrwork = resSettlemAgrwork.Value;

            decimal settlemAgrWorkVal = evalSettlemAgrwork.ResultValue;

            var settlemResultsList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_RESULTS))))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultBasis)).ToArray();

            decimal settlemResultsBasis = settlemResultsList.Aggregate(decimal.Zero,
                (agr, basis) => decimal.Add(agr, basis));

            decimal settlemDiffsVal = (optimusTargetsVal + settlemResultsBasis - settlemAllowceVal - settlemAgrWorkVal);

            decimal reducedHoursVal = evalOptimusTargets.OptimusHoursVal;
            decimal reducedTarifVal = salaryRules.TariffWithPaymentAndUnits(Math.Max(0, settlemDiffsVal), reducedHoursVal);
            decimal reducedResValue = Math.Max(0, settlemDiffsVal);
            decimal reducedResBasis = Math.Max(0, optimusTargetsVal - reducedResValue);

            ITermResult resultsValues = new ReducedHoursResult(target, spec,
                reducedTarifVal, reducedHoursVal,
                RoundToInt(reducedResValue), RoundToInt(reducedResBasis));

            return BuildOkResults(resultsValues);
        }
    }

    // ReducedFixed		REDUCED_FIXED
    class ReducedFixedConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_REDUCED_FIXED;
        public ReducedFixedConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new ReducedFixedConSpec(this.Code.Value);
        }
    }

    class ReducedFixedConSpec : OptimulaConceptSpec
    {
        public ReducedFixedConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new ReducedFixedTarget(month, con, pos, var, article, this.Code, ArticleCode.Zero),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<ReducedFixedTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            ReducedFixedTarget evalTarget = resTarget.Value;

            var resOptimusTargets = GetContractResult<OptimusFixedResult>(target, period, results,
               target.Contract, evalTarget.ArticleTarget);

            if (resOptimusTargets.IsFailure)
            {
                return BuildFailResults(resOptimusTargets.Error);
            }

            var evalOptimusTargets = resOptimusTargets.Value;

            var resSettlemAllowce = GetContractResult<SettlemAllowceResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE));

            if (resSettlemAllowce.IsFailure)
            {
                return BuildFailResults(resSettlemAllowce.Error);
            }

            var evalSettlemAllowce = resSettlemAllowce.Value;

            decimal settlemAllowceVal = evalSettlemAllowce.ResultValue;
            decimal optimusTargetsVal = evalOptimusTargets.ResultValue;

            var resSettlemAgrwork = GetContractResult<SettlemAgrworkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK));

            if (resSettlemAgrwork.IsFailure)
            {
                return BuildFailResults(resSettlemAgrwork.Error);
            }

            var evalSettlemAgrwork = resSettlemAgrwork.Value;

            decimal settlemAgrWorkVal = evalSettlemAgrwork.ResultValue;

            var settlemResultsList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_RESULTS))))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultBasis)).ToArray();

            decimal settlemResultsBasis = settlemResultsList.Aggregate(decimal.Zero,
                (agr, basis) => decimal.Add(agr, basis));

            decimal settlemDiffsVal = (optimusTargetsVal + settlemResultsBasis - settlemAllowceVal - settlemAgrWorkVal);

            decimal reducedBasisVal = Math.Max(0, settlemDiffsVal);
            decimal reducedResValue = Math.Max(0, settlemDiffsVal);
            decimal reducedResBasis = Math.Max(0, optimusTargetsVal - reducedBasisVal);

            ITermResult resultsValues = new ReducedFixedResult(target, spec,
                reducedBasisVal,
                RoundToInt(reducedResValue), RoundToInt(reducedResBasis));

            return BuildOkResults(resultsValues);
        }
    }

    // ReducedNetto		REDUCED_NETTO
    class ReducedNettoConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_REDUCED_NETTO;
        public ReducedNettoConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new ReducedNettoConSpec(this.Code.Value);
        }
    }

    class ReducedNettoConSpec : OptimulaConceptSpec
    {
        public ReducedNettoConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARNETT,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRTASK,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new ReducedNettoTarget(month, con, pos, var, article, this.Code, ArticleCode.Zero),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<ReducedNettoTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            ReducedNettoTarget evalTarget = resTarget.Value;

            var resOptimusTargets = GetContractResult<OptimusNettoResult>(target, period, results,
               target.Contract, evalTarget.ArticleTarget);

            if (resOptimusTargets.IsFailure)
            {
                return BuildFailResults(resOptimusTargets.Error);
            }

            var evalOptimusTargets = resOptimusTargets.Value;

            var resSettlemAllowce = GetContractResult<SettlemAllnettResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLNETT));

            if (resSettlemAllowce.IsFailure)
            {
                return BuildFailResults(resSettlemAllowce.Error);
            }

            var evalSettlemAllowce = resSettlemAllowce.Value;

            decimal settlemAllowceVal = evalSettlemAllowce.ResultValue;
            decimal optimusTargetsVal = evalOptimusTargets.ResultValue;

            var resSettlemAgrwork = GetContractResult<SettlemAgrworkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK));

            if (resSettlemAgrwork.IsFailure)
            {
                return BuildFailResults(resSettlemAgrwork.Error);
            }

            var evalSettlemAgrwork = resSettlemAgrwork.Value;

            decimal settlemAgrWorkVal = evalSettlemAgrwork.ResultValue;

            var resSettlemAgrTask = GetContractResult<SettlemAgrtaskResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRTASK));

            if (resSettlemAgrTask.IsFailure)
            {
                return BuildFailResults(resSettlemAgrTask.Error);
            }

            var evalSettlemAgrTask = resSettlemAgrTask.Value;

            decimal settlemAgrTaskVal = evalSettlemAgrTask.ResultValue;

            var settlemResultsList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_RESNETT))))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultBasis)).ToArray();

            decimal settlemResultsBasis = settlemResultsList.Aggregate(decimal.Zero,
                (agr, basis) => decimal.Add(agr, basis));

            decimal settlemDiffsVal = (optimusTargetsVal + settlemResultsBasis - settlemAllowceVal - 
                RoundToInt(OperationsDec.Multiply(settlemAgrWorkVal + settlemAgrTaskVal, 0.85m)));

            decimal reducedBasisVal = Math.Max(0, settlemDiffsVal);
            decimal reducedResValue = Math.Max(0, settlemDiffsVal);
            decimal reducedResBasis = Math.Max(0, optimusTargetsVal - reducedBasisVal);

            ITermResult resultsValues = new ReducedNettoResult(target, spec,
                optimusTargetsVal,
                reducedBasisVal,
                RoundToInt(reducedResValue), RoundToInt(reducedResBasis));

            return BuildOkResults(resultsValues);
        }
    }

    // AgrworkHours		AGRWORK_HOURS
    class AgrworkHoursConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_AGRWORK_HOURS;
        public AgrworkHoursConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            if (version.Value == ServiceOptimula.TEST_VERSION_SCM)
            {
                return new AgrworkHoursConScmSpec(this.Code.Value);
            }
            else if (version.Value == ServiceOptimula.TEST_VERSION_EPS)
            {
                return new AgrworkHoursConEpsSpec(this.Code.Value);
            }
            else if (version.Value == ServiceOptimula.TEST_VERSION_PUZZLE)
            {
                return new AgrworkHoursConPzzSpec(this.Code.Value);
            }
            return new AgrworkHoursConEpsSpec(this.Code.Value);
        }
    }

    class AgrworkHoursConScmSpec : OptimulaConceptSpec
    {
        public AgrworkHoursConScmSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AgrworkHoursTarget(month, con, pos, var, article, this.Code, 0, 0, 0, 0),
            };
        }
        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<AgrworkHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AgrworkHoursTarget evalTarget = resTarget.Value;

            var resSettlemTargets = GetContractResult<SettlemTargetsResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS));

            var resSettlemAllowce = GetContractResult<SettlemAllowceResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE));

            var resCompound = GetFailedOrOk(resSettlemTargets.ErrOrOk(), resSettlemAllowce.ErrOrOk());
            if (resCompound.IsFailure)
            {
                return BuildFailResults(resCompound.Error);
            }

            var evalSettlemTargets = resSettlemTargets.Value;
            var evalSettlemAllowce = resSettlemAllowce.Value;

            decimal redWorkTarifVal = OperationsDec.Divide(evalTarget.AgrWorkTarifVal, 100);
            decimal redWorkRatioVal = OperationsDec.Divide(evalTarget.AgrWorkRatioVal, 100);
            decimal redWorkLimitVal = OperationsDec.Divide(evalTarget.AgrWorkLimitVal, 100);
            decimal redHourLimitVal = OperationsDec.Divide(evalTarget.AgrHourLimitVal, 60);

            decimal paymentHoursRes = 0.0m;
            decimal paymentBasisRes = 0.0m;

            decimal paymentValueRes = 0.0m;

            Int32 settlemResultsDif = (evalSettlemTargets.ResultValue - evalSettlemAllowce.ResultValue);
            if (settlemResultsDif > 0)
            {
                paymentValueRes = settlemResultsDif;

                decimal agrCandidsHours = salaryRules.HoursToHalfHoursDown(OperationsDec.Divide(paymentValueRes, redWorkTarifVal));

                paymentHoursRes = Math.Max(0, agrCandidsHours);

                if (paymentHoursRes != 0.0m)
                {
                    decimal redCandidsValue = Math.Min(10000.0m, paymentValueRes);

                    decimal redCandidsHours = salaryRules.HoursToHalfHoursDown(OperationsDec.Divide(redCandidsValue, redWorkTarifVal));
                    decimal redCandidsBasis = salaryRules.MoneyToRoundDown(OperationsDec.Divide(redCandidsValue, redCandidsHours));

                    if (redCandidsHours > 25.0m)
                    {
                        redCandidsHours = 25.0m;
                        redCandidsBasis = salaryRules.MoneyToRoundDown(OperationsDec.Divide(redCandidsValue, redCandidsHours));
                    }
                    decimal minCandidsBasis = salaryRules.PaymentWithAmountFixed(OperationsDec.Divide(salaryRules.MinHourlyWage + 200, 100m));

                    if (redCandidsBasis < minCandidsBasis)
                    {
                        redCandidsBasis = minCandidsBasis;
                        redCandidsHours = salaryRules.HoursToHalfHoursDown(OperationsDec.Divide(redCandidsValue, redCandidsBasis));
                    }
                    paymentHoursRes = Math.Max(0, redCandidsHours);
                    paymentBasisRes = Math.Max(0, Math.Max(paymentBasisRes, redCandidsBasis));

                    paymentValueRes = redCandidsValue;
                }
            }

            ITermResult resultsValues = new AgrworkHoursResult(target, spec,
                redWorkTarifVal, redWorkRatioVal, redWorkLimitVal, redHourLimitVal,
                paymentHoursRes, paymentBasisRes,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    class AgrworkHoursConEpsSpec : OptimulaConceptSpec
    {
        public AgrworkHoursConEpsSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AgrworkHoursTarget(month, con, pos, var, article, this.Code, 0, 0, 0, 0),
            };
        }
        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<AgrworkHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AgrworkHoursTarget evalTarget = resTarget.Value;

            decimal redWorkTarifVal = OperationsDec.Divide(evalTarget.AgrWorkTarifVal, 100);
            decimal redWorkRatioVal = OperationsDec.Divide(evalTarget.AgrWorkRatioVal, 100);
            decimal redWorkLimitVal = OperationsDec.Divide(evalTarget.AgrWorkLimitVal, 100);
            decimal redHourLimitVal = OperationsDec.Divide(evalTarget.AgrHourLimitVal, 60);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal hoursWorkActa = evalTimeActa.WorkSheetHrsVal;

            decimal paymentBasisRes = redWorkTarifVal;
            decimal paymentHoursRes = 0m;

            if (redHourLimitVal == 0m)
            {
                paymentHoursRes = salaryRules.HoursToHalfHoursNorm(OperationsDec.Multiply(hoursWorkActa, redWorkRatioVal));
            }
            else
            {
                decimal agrCandidsHours = 0m;
                agrCandidsHours = salaryRules.HoursToHalfHoursNorm(OperationsDec.Multiply(hoursWorkActa, redWorkRatioVal));
                if (redWorkRatioVal == 0m)
                {
                    paymentHoursRes = Math.Min(agrCandidsHours, salaryRules.HoursToHalfHoursNorm(redHourLimitVal));
                }
                else
                {
                    paymentHoursRes = salaryRules.HoursToHalfHoursNorm(redHourLimitVal);
                }
            }
            decimal paymentValueRes = salaryRules.PaymentWithTariffAndUnits(paymentBasisRes, paymentHoursRes);

            var resSettlemTargets = GetContractResult<SettlemTargetsResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS));

            var resSettlemAllowce = GetContractResult<SettlemAllowceResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE));

            var resCompound = GetFailedOrOk(resSettlemTargets.ErrOrOk(), resSettlemAllowce.ErrOrOk());
            if (resCompound.IsFailure)
            {
                return BuildFailResults(resCompound.Error);
            }

            var evalSettlemTargets = resSettlemTargets.Value;
            var evalSettlemAllowce = resSettlemAllowce.Value;

            Int32 settlemResultsDif = (evalSettlemTargets.ResultValue - evalSettlemAllowce.ResultValue);
            if (settlemResultsDif < paymentValueRes)
            {
                decimal overcapValueRes = (paymentValueRes - settlemResultsDif);

                decimal agrCandidsHours = salaryRules.HoursToHalfHoursUp(OperationsDec.Divide(overcapValueRes, paymentBasisRes));

                paymentHoursRes = Math.Max(0, (paymentHoursRes - agrCandidsHours));

                paymentValueRes = salaryRules.PaymentWithTariffAndUnits(paymentBasisRes, paymentHoursRes);
            }

            ITermResult resultsValues = new AgrworkHoursResult(target, spec,
                redWorkTarifVal, redWorkRatioVal, redWorkLimitVal, redHourLimitVal,
                paymentHoursRes, paymentBasisRes,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    class AgrworkHoursConPzzSpec : OptimulaConceptSpec
    {
        public AgrworkHoursConPzzSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AgrworkHoursTarget(month, con, pos, var, article, this.Code, 0, 0, 0, 0),
            };
        }
        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resPrTaxing = GetTaxingPropsResult(ruleset, target, period);
            if (resPrTaxing.IsFailure)
            {
                return BuildFailResults(resPrTaxing.Error);
            }
            IPropsTaxing taxingRules = resPrTaxing.Value;

            var resTarget = GetTypedTarget<AgrworkHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AgrworkHoursTarget evalTarget = resTarget.Value;

            decimal redWorkTarifVal = OperationsDec.Divide(evalTarget.AgrWorkTarifVal, 100);
            decimal redWorkRatioVal = OperationsDec.Divide(evalTarget.AgrWorkRatioVal, 100);
            decimal redWorkLimitVal = OperationsDec.Divide(evalTarget.AgrWorkLimitVal, 100);
            decimal redHourLimitVal = OperationsDec.Divide(evalTarget.AgrHourLimitVal, 60);

            var resSettlemTarnett = GetContractResult<SettlemTarnettResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARNETT));

            var resSettlemAllowce = GetContractResult<SettlemAllnettResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLNETT));

            var resCompound = GetFailedOrOk(resSettlemTarnett.ErrOrOk(), resSettlemAllowce.ErrOrOk());
            if (resCompound.IsFailure)
            {
                return BuildFailResults(resCompound.Error);
            }

            var evalSettlemTarnett = resSettlemTarnett.Value;
            var evalSettlemAllowce = resSettlemAllowce.Value;

            decimal agrWorkLimitVal = taxingRules.MarginIncomeOfWthAgr;
            decimal agrHourLimitVal = 25m;

            decimal paymentBasisRes = 0m;
            decimal paymentHoursRes = 0m;

            Int32 settlemResultsDif = (evalSettlemTarnett.ResultValue - evalSettlemAllowce.ResultValue);
            if (settlemResultsDif > 0)
            {
                decimal redNettLimitVal = settlemResultsDif;
                decimal redGrosLimitVal = OperationsDec.Divide(redNettLimitVal, 0.85m);

                var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
                   target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

                if (resTimeActa.IsFailure)
                {
                    return BuildFailResults(resTimeActa.Error);
                }

                var evalTimeActa = resTimeActa.Value;

                decimal hoursWorkActa = evalTimeActa.WorkSheetHrsVal;


                if (hoursWorkActa > 0m && redWorkLimitVal > 0m)
                {
                    paymentBasisRes = Math.Min(redGrosLimitVal, agrWorkLimitVal);

                    paymentHoursRes = redHourLimitVal;
                    if (paymentBasisRes != redWorkLimitVal)
                    {
                        paymentHoursRes = Math.Min(
                            salaryRules.HoursToHalfHoursNorm(OperationsDec.Divide(paymentBasisRes, redWorkTarifVal)), agrHourLimitVal);
                    }
                }
            }

            decimal paymentValueRes = salaryRules.PaymentWithAmountFixed(paymentBasisRes);

            ITermResult resultsValues = new AgrworkHoursResult(target, spec,
                redWorkTarifVal, redWorkRatioVal, redWorkLimitVal, redHourLimitVal,
                paymentHoursRes, paymentBasisRes,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // AgrtaskHours		AGRTASK_HOURS
    class AgrtaskHoursConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_AGRTASK_HOURS;
        public AgrtaskHoursConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new AgrtaskHoursConPzzSpec(this.Code.Value);
        }
    }

    class AgrtaskHoursConPzzSpec : OptimulaConceptSpec
    {
        public AgrtaskHoursConPzzSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARNETT,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK,
                (Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLNETT,
                (Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK,
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AgrworkHoursTarget(month, con, pos, var, article, this.Code, 0, 0, 0, 0),
            };
        }
        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resPrTaxing = GetTaxingPropsResult(ruleset, target, period);
            if (resPrTaxing.IsFailure)
            {
                return BuildFailResults(resPrTaxing.Error);
            }
            IPropsTaxing taxingRules = resPrTaxing.Value;

            var resTarget = GetTypedTarget<AgrtaskHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AgrtaskHoursTarget evalTarget = resTarget.Value;

            decimal redWorkTarifVal = OperationsDec.Divide(evalTarget.AgrWorkTarifVal, 100);
            decimal redWorkRatioVal = OperationsDec.Divide(evalTarget.AgrWorkRatioVal, 100);
            decimal redWorkLimitVal = OperationsDec.Divide(evalTarget.AgrWorkLimitVal, 100);
            decimal redHourLimitVal = OperationsDec.Divide(evalTarget.AgrHourLimitVal, 60);

            var resTimesheetsWork = GetContractResult<TimesheetsWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMESHEETS_WORK));

            var resSettlemTargets = GetContractResult<SettlemTarnettResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARNETT));

            var resSettlemAllowce = GetContractResult<SettlemAllnettResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLNETT));

            var resSettlemAgrWork = GetContractResult<SettlemAgrworkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK));

            var resCompound = GetFailedOrOk(resTimesheetsWork.ErrOrOk(), resSettlemTargets.ErrOrOk(), resSettlemAllowce.ErrOrOk(), resSettlemAgrWork.ErrOrOk());
            if (resCompound.IsFailure)
            {
                return BuildFailResults(resCompound.Error);
            }

            var evalTimesheetsWork = resTimesheetsWork.Value;
            var evalSettlemTargets = resSettlemTargets.Value;
            var evalSettlemAllowce = resSettlemAllowce.Value;
            var evalSettlemAgrWork = resSettlemAgrWork.Value;

            decimal agrWorkLimitVal = (taxingRules.MarginIncomeOfWthEmp - 1);
            decimal agrHourLimitVal = salaryRules.HoursToHalfHoursNorm(
                OperationsDec.Divide(evalTimesheetsWork.TimeSheetHrsVal, 2));

            decimal paymentBasisRes = 0m;
            decimal paymentHoursRes = 0m;

            Int32 settlemResultsDif = (evalSettlemTargets.ResultValue - evalSettlemAllowce.ResultValue - RoundToInt(OperationsDec.Multiply(evalSettlemAgrWork.ResultValue, 0.85m)));
            if (settlemResultsDif > 0)
            {
                decimal redNettLimitVal = settlemResultsDif;
                decimal redGrosLimitVal = OperationsDec.Divide(redNettLimitVal, 0.85m);

                var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
                   target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

                if (resTimeActa.IsFailure)
                {
                    return BuildFailResults(resTimeActa.Error);
                }

                var evalTimeActa = resTimeActa.Value;

                decimal hoursWorkActa = evalTimeActa.WorkSheetHrsVal;


                if (hoursWorkActa > 0m && redWorkLimitVal > 0m)
                {
                    paymentBasisRes = Math.Min(redGrosLimitVal, agrWorkLimitVal);

                    paymentHoursRes = redHourLimitVal;
                    if (paymentBasisRes != redWorkLimitVal)
                    {
                        paymentHoursRes = Math.Min(
                            salaryRules.HoursToHalfHoursNorm(OperationsDec.Divide(paymentBasisRes, redWorkTarifVal)), agrHourLimitVal);
                    }
                }
            }

            decimal paymentValueRes = salaryRules.PaymentWithAmountFixed(paymentBasisRes);

            ITermResult resultsValues = new AgrtaskHoursResult(target, spec,
                redWorkTarifVal, redWorkRatioVal, redWorkLimitVal, redHourLimitVal,
                paymentHoursRes, paymentBasisRes,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // AllowceMfull		ALLOWCE_MFULL
    class AllowceMfullConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_MFULL;
        public AllowceMfullConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new AllowceMfullConSpec(this.Code.Value);
        }
    }

    class AllowceMfullConSpec : OptimulaConceptSpec
    {
        public AllowceMfullConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AllowceMfullTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<AllowceMfullTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AllowceMfullTarget evalTarget = resTarget.Value;

            decimal allowceBasisVal = OperationsDec.Divide(evalTarget.AllowceBasisVal, 100);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal workHoursActa = evalTimeActa.WorkSheetHrsVal;

            decimal monthlyCoeffs = 1.0m;

            decimal workingCoeffs = evalTimeActa.WorkTimeHrsCoef;

            decimal allowceValueRes = salaryRules.PaymentWithMonthlyAndCoeffAndWorkCoeff(allowceBasisVal, monthlyCoeffs, workingCoeffs);

            ITermResult resultsValues = new AllowceMfullResult(target, spec,
                allowceBasisVal, 
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // AllowceHfull		ALLOWCE_HFULL
    class AllowceHfullConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_HFULL;
        public AllowceHfullConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new AllowceHfullConSpec(this.Code.Value);
        }
    }

    class AllowceHfullConSpec : OptimulaConceptSpec
    {
        public AllowceHfullConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AllowceHfullTarget(month, con, pos, var, article, this.Code, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<AllowceHfullTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AllowceHfullTarget evalTarget = resTarget.Value;

            decimal allowceTarifVal = OperationsDec.Divide(evalTarget.AllowceTarifVal, 100);
            decimal allowceHfullVal = OperationsDec.Divide(evalTarget.AllowceHfullVal, 60);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal workHoursActa = evalTimeActa.WorkSheetHrsVal;

            decimal monthlyCoeffs = 1.0m;

            decimal workingCoeffs = evalTimeActa.WorkTimeHrsCoef;

            decimal hoursAllowceRes = salaryRules.RelativeAmountWithMonthlyAndCoeffAndWorkCoeff(allowceHfullVal, monthlyCoeffs, workingCoeffs);

            decimal allowceValueRes = salaryRules.PaymentWithTariffAndUnits(allowceTarifVal, hoursAllowceRes);

            ITermResult resultsValues = new AllowceHfullResult(target, spec, 
                allowceTarifVal, allowceHfullVal, 
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // AllowceHours		ALLOWCE_HOURS
    class AllowceHoursConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_HOURS;
        public AllowceHoursConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new AllowceHoursConSpec(this.Code.Value);
        }
    }

    class AllowceHoursConSpec : OptimulaConceptSpec
    {
        public AllowceHoursConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AllowceHoursTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<AllowceHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AllowceHoursTarget evalTarget = resTarget.Value;

            decimal allowceTarifVal = OperationsDec.Divide(evalTarget.AllowceTarifVal, 100);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal hoursWorked = evalTimeActa.WorkSheetHrsVal;

            decimal allowceValueRes = salaryRules.PaymentWithTariffAndUnits(allowceTarifVal, hoursWorked);

            ITermResult resultsValues = new AllowceHoursResult(target, spec, 
                allowceTarifVal, 
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // AllowceDaily		ALLOWCE_DAILY
    class AllowceDailyConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_ALLOWCE_DAILY;
        public AllowceDailyConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new AllowceDailyConSpec(this.Code.Value);
        }
    }

    class AllowceDailyConSpec : OptimulaConceptSpec
    {
        public AllowceDailyConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AllowceDailyTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<AllowceDailyTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AllowceDailyTarget evalTarget = resTarget.Value;

            decimal allowceDailyVal = OperationsDec.Divide(evalTarget.AllowceDailyVal, 100);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal dailyWorked = evalTimeActa.WorkSheetDayVal;

            decimal allowceValueRes = salaryRules.PaymentWithTariffAndUnits(allowceDailyVal, dailyWorked);

            ITermResult resultsValues = new AllowceDailyResult(target, spec, 
                allowceDailyVal, 
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // AlldownDaily			ALLDOWN_DAILY
    class AlldownDailyConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_ALLDOWN_DAILY;
        public AlldownDailyConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new AlldownDailyConSpec(this.Code.Value);
        }
    }

    class AlldownDailyConSpec : OptimulaConceptSpec {
        public AlldownDailyConSpec(Int32 code) : base(code) {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new AlldownDailyTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<AlldownDailyTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            AlldownDailyTarget evalTarget = resTarget.Value;

            decimal allowceDailyVal = OperationsDec.Divide(evalTarget.AllowceDailyVal, 100);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal dailyWorked = evalTimeActa.WorkSheetDayVal;

            decimal allowceValueRes = salaryRules.PaymentRoundDownWithTariffAndUnits(allowceDailyVal, dailyWorked);

            ITermResult resultsValues = new AlldownDailyResult(target, spec,
                allowceDailyVal,
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OffworkHours			OFFWORK_HOURS
    class OffworkHoursConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OFFWORK_HOURS;
        public OffworkHoursConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new OffworkHoursConSpec(this.Code.Value);
        }
    }

    class OffworkHoursConSpec : OptimulaConceptSpec
    {
        public OffworkHoursConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OffworkHoursTarget(month, con, pos, var, article, this.Code, 0, 0, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OffworkHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OffworkHoursTarget evalTarget = resTarget.Value;

            decimal redWorkTarifVal = OperationsDec.Divide(evalTarget.AgrWorkTarifVal, 100);
            decimal redWorkRatioVal = OperationsDec.Divide(evalTarget.AgrWorkRatioVal, 100);
            decimal redWorkLimitVal = OperationsDec.Divide(evalTarget.AgrWorkLimitVal, 100);
            decimal redHourLimitVal = OperationsDec.Divide(evalTarget.AgrHourLimitVal, 60);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal hoursWorkActa = evalTimeActa.WorkSheetHrsVal;

            decimal paymentBasisRes = redWorkTarifVal;
            decimal paymentHoursRes = 0m;

            if (redHourLimitVal == 0m)
            {
                paymentHoursRes = salaryRules.HoursToHalfHoursNorm(OperationsDec.Multiply(hoursWorkActa, redWorkRatioVal));
            }
            else
            {
                decimal agrCandidsHours = 0m;
                agrCandidsHours = salaryRules.HoursToHalfHoursNorm(OperationsDec.Multiply(hoursWorkActa, redWorkRatioVal));
                if (redWorkRatioVal == 0m)
                {
                    paymentHoursRes = Math.Min(agrCandidsHours, salaryRules.HoursToHalfHoursNorm(redHourLimitVal));
                }
                else
                {
                    paymentHoursRes = salaryRules.HoursToHalfHoursNorm(redHourLimitVal);
                }
            }
            decimal paymentValueRes = salaryRules.PaymentWithTariffAndUnits(paymentBasisRes, paymentHoursRes);

            ITermResult resultsValues = new OffworkHoursResult(target, spec,
                redWorkTarifVal, redWorkRatioVal, redWorkLimitVal, redHourLimitVal,
                paymentHoursRes, paymentBasisRes,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OfftaskHours			OFFTASK_HOURS
    class OfftaskHoursConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OFFTASK_HOURS;
        public OfftaskHoursConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new OfftaskHoursConSpec(this.Code.Value);
        }
    }

    class OfftaskHoursConSpec : OptimulaConceptSpec {
        public OfftaskHoursConSpec(Int32 code) : base(code) {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OfftaskHoursTarget(month, con, pos, var, article, this.Code, 0, 0, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OfftaskHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OfftaskHoursTarget evalTarget = resTarget.Value;

            decimal redWorkTarifVal = OperationsDec.Divide(evalTarget.AgrWorkTarifVal, 100);
            decimal redWorkRatioVal = OperationsDec.Divide(evalTarget.AgrWorkRatioVal, 100);
            decimal redWorkLimitVal = OperationsDec.Divide(evalTarget.AgrWorkLimitVal, 100);
            decimal redHourLimitVal = OperationsDec.Divide(evalTarget.AgrHourLimitVal, 60);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal hoursWorkActa = evalTimeActa.WorkSheetHrsVal;

            decimal paymentBasisRes = redWorkTarifVal;
            decimal paymentHoursRes = 0m;

            if (redHourLimitVal == 0m)
            {
                paymentHoursRes = salaryRules.HoursToHalfHoursNorm(OperationsDec.Multiply(hoursWorkActa, redWorkRatioVal));
            }
            else
            {
                decimal agrCandidsHours = 0m;
                agrCandidsHours = salaryRules.HoursToHalfHoursNorm(OperationsDec.Multiply(hoursWorkActa, redWorkRatioVal));
                if (redWorkRatioVal == 0m)
                {
                    paymentHoursRes = Math.Min(agrCandidsHours, salaryRules.HoursToHalfHoursNorm(redHourLimitVal));
                }
                else
                {
                    paymentHoursRes = salaryRules.HoursToHalfHoursNorm(redHourLimitVal);
                }
            }
            decimal paymentValueRes = salaryRules.PaymentWithTariffAndUnits(paymentBasisRes, paymentHoursRes);

            ITermResult resultsValues = new OfftaskHoursResult(target, spec,
                redWorkTarifVal, redWorkRatioVal, redWorkLimitVal, redHourLimitVal,
                paymentHoursRes, paymentBasisRes,
                RoundToInt(paymentValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OffsetsHfull			OFFSETS_HFULL
    class OffsetsHfullConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OFFSETS_HFULL;
        public OffsetsHfullConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new OffsetsHfullConSpec(this.Code.Value);
        }
    }

    class OffsetsHfullConSpec : OptimulaConceptSpec
    {
        public OffsetsHfullConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OffsetsHfullTarget(month, con, pos, var, article, this.Code, 0, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OffsetsHfullTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OffsetsHfullTarget evalTarget = resTarget.Value;

            decimal allowceTarifVal = OperationsDec.Divide(evalTarget.AllowceTarifVal, 100);
            decimal allowceHfullVal = OperationsDec.Divide(evalTarget.AllowceHfullVal, 60);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal workHoursActa = evalTimeActa.WorkSheetHrsVal;

            decimal monthlyCoeffs = 1.0m;

            decimal workingCoeffs = evalTimeActa.WorkTimeHrsCoef;

            decimal hoursAllowceRes = salaryRules.RelativeAmountWithMonthlyAndCoeffAndWorkCoeff(allowceHfullVal, monthlyCoeffs, workingCoeffs);

            decimal allowceValueRes = salaryRules.PaymentWithTariffAndUnits(allowceTarifVal, hoursAllowceRes);

            ITermResult resultsValues = new OffsetsHfullResult(target, spec,
                allowceTarifVal, allowceHfullVal,
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OffsetsHours			OFFSETS_HOURS
    class OffsetsHoursConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OFFSETS_HOURS;
        public OffsetsHoursConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new OffsetsHoursConSpec(this.Code.Value);
        }
    }

    class OffsetsHoursConSpec : OptimulaConceptSpec
    {
        public OffsetsHoursConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OffsetsHoursTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OffsetsHoursTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OffsetsHoursTarget evalTarget = resTarget.Value;

            decimal allowceTarifVal = OperationsDec.Divide(evalTarget.AllowceTarifVal, 100);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal hoursWorked = evalTimeActa.WorkSheetHrsVal;

            decimal allowceValueRes = salaryRules.PaymentWithTariffAndUnits(allowceTarifVal, hoursWorked);

            ITermResult resultsValues = new OffsetsHoursResult(target, spec,
                allowceTarifVal,
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OffsetsDaily			OFFSETS_DAILY
    class OffsetsDailyConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OFFSETS_DAILY;
        public OffsetsDailyConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new OffsetsDailyConSpec(this.Code.Value);
        }
    }

    class OffsetsDailyConSpec : OptimulaConceptSpec
    {
        public OffsetsDailyConSpec(Int32 code) : base(code)
        {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OffsetsDailyTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OffsetsDailyTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OffsetsDailyTarget evalTarget = resTarget.Value;

            decimal allowceDailyVal = OperationsDec.Divide(evalTarget.AllowceDailyVal, 100);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal dailyWorked = evalTimeActa.WorkSheetDayVal;

            decimal allowceValueRes = salaryRules.PaymentWithTariffAndUnits(allowceDailyVal, dailyWorked);

            ITermResult resultsValues = new OffsetsDailyResult(target, spec,
                allowceDailyVal,
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // OffdownDaily			OFFDOWN_DAILY
    class OffdownDailyConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_OFFDOWN_DAILY;
        public OffdownDailyConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new OffdownDailyConSpec(this.Code.Value);
        }
    }

    class OffdownDailyConSpec : OptimulaConceptSpec {
        public OffdownDailyConSpec(Int32 code) : base(code) {
            Path = ConceptSpec.ConstToPathArray(new List<Int32>() {
                (Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK,
            });
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new OffdownDailyTarget(month, con, pos, var, article, this.Code, 0),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resPrSalary = GetSalaryPropsResult(ruleset, target, period);
            if (resPrSalary.IsFailure)
            {
                return BuildFailResults(resPrSalary.Error);
            }
            IPropsSalary salaryRules = resPrSalary.Value;

            var resTarget = GetTypedTarget<OffdownDailyTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            OffdownDailyTarget evalTarget = resTarget.Value;

            decimal allowceDailyVal = OperationsDec.Divide(evalTarget.AllowceDailyVal, 100);

            var resTimeActa = GetContractResult<TimeactualWorkResult>(target, period, results,
               target.Contract, ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_TIMEACTUAL_WORK));

            if (resTimeActa.IsFailure)
            {
                return BuildFailResults(resTimeActa.Error);
            }

            var evalTimeActa = resTimeActa.Value;

            decimal dailyWorked = evalTimeActa.WorkSheetDayVal;

            decimal allowceValueRes = salaryRules.PaymentRoundDownWithTariffAndUnits(allowceDailyVal, dailyWorked);

            ITermResult resultsValues = new OffdownDailyResult(target, spec,
                allowceDailyVal,
                RoundToInt(allowceValueRes), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemTargets	SETTLEM_TARGETS
    class SettlemTargetsConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_TARGETS;
        public SettlemTargetsConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new SettlemTargetsConSpec(this.Code.Value);
        }
    }

    class SettlemTargetsConSpec : OptimulaConceptSpec
    {
        public SettlemTargetsConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemTargetsTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<SettlemTargetsTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemTargetsTarget evalTarget = resTarget.Value;


            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemTargetsResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemTarnett			SETTLEM_TARNETT
    class SettlemTarnettConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_TARNETT;
        public SettlemTarnettConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new SettlemTarnettConSpec(this.Code.Value);
        }
    }

    class SettlemTarnettConSpec : OptimulaConceptSpec {
        public SettlemTarnettConSpec(Int32 code) : base(code) {
            Path = new List<ArticleCode>();
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemTarnettTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resTarget = GetTypedTarget<SettlemTarnettTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemTarnettTarget evalTarget = resTarget.Value;
    
            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemTarnettResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemAgrwork	SETTLEM_AGRWORK
    class SettlemAgrworkConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_AGRWORK;
        public SettlemAgrworkConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new SettlemAgrworkConSpec(this.Code.Value);
        }
    }

    class SettlemAgrworkConSpec : OptimulaConceptSpec
    {
        public SettlemAgrworkConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemAgrworkTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<SettlemAgrworkTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemAgrworkTarget evalTarget = resTarget.Value;


            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemAgrworkResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemAgrtask			SETTLEM_AGRTASK
    class SettlemAgrtaskConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_AGRTASK;
        public SettlemAgrtaskConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new SettlemAgrtaskConSpec(this.Code.Value);
        }
    }

    class SettlemAgrtaskConSpec : OptimulaConceptSpec {
        public SettlemAgrtaskConSpec(Int32 code) : base(code) {
            Path = new List<ArticleCode>();
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemAgrtaskTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resTarget = GetTypedTarget<SettlemAgrtaskTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemAgrtaskTarget evalTarget = resTarget.Value;
    
            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemAgrtaskResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemAllowce	SETTLEM_ALLOWCE
    class SettlemAllowceConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_ALLOWCE;
        public SettlemAllowceConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new SettlemAllowceConSpec(this.Code.Value);
        }
    }

    class SettlemAllowceConSpec : OptimulaConceptSpec
    {
        public SettlemAllowceConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemAllowceTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<SettlemAllowceTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemAllowceTarget evalTarget = resTarget.Value;


            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemAllowceResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemAllnett			SETTLEM_ALLNETT
    class SettlemAllnettConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_ALLNETT;
        public SettlemAllnettConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new SettlemAllnettConSpec(this.Code.Value);
        }
    }

    class SettlemAllnettConSpec : OptimulaConceptSpec {
        public SettlemAllnettConSpec(Int32 code) : base(code) {
            Path = new List<ArticleCode>();
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemAllnettTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resTarget = GetTypedTarget<SettlemAllnettTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemAllnettTarget evalTarget = resTarget.Value;
    
            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemAllnettResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemOffwork			SETTLEM_OFFWORK
    class SettlemOffworkConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_OFFWORK;
        public SettlemOffworkConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new SettlemOffworkConSpec(this.Code.Value);
        }
    }

    class SettlemOffworkConSpec : OptimulaConceptSpec
    {
        public SettlemOffworkConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemOffworkTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<SettlemOffworkTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemOffworkTarget evalTarget = resTarget.Value;

            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemOffworkResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemOfftask			SETTLEM_OFFTASK
    class SettlemOfftaskConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_OFFTASK;
        public SettlemOfftaskConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new SettlemOfftaskConSpec(this.Code.Value);
        }
    }

    class SettlemOfftaskConSpec : OptimulaConceptSpec {
        public SettlemOfftaskConSpec(Int32 code) : base(code) {
            Path = new List<ArticleCode>();
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemOfftaskTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resTarget = GetTypedTarget<SettlemOfftaskTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemOfftaskTarget evalTarget = resTarget.Value;
    
            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemOfftaskResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemOffsets			SETTLEM_OFFSETS
    class SettlemOffsetsConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_OFFSETS;
        public SettlemOffsetsConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new SettlemOffsetsConSpec(this.Code.Value);
        }
    }

    class SettlemOffsetsConSpec : OptimulaConceptSpec
    {
        public SettlemOffsetsConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemOffsetsTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<SettlemOffsetsTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemOffsetsTarget evalTarget = resTarget.Value;

            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemOffsetsResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemResults	SETTLEM_RESULTS
    class SettlemResultsConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_RESULTS;
        public SettlemResultsConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new SettlemResultsConSpec(this.Code.Value);
        }
    }

    class SettlemResultsConSpec : OptimulaConceptSpec
    {
        public SettlemResultsConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemResultsTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<SettlemResultsTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemResultsTarget evalTarget = resTarget.Value;


            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemResultsResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // SettlemResnett	SETTLEM_RESNETT
    class SettlemResnettConProv : ConceptSpecProvider {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_SETTLEM_RESNETT;
        public SettlemResnettConProv() : base(CONCEPT_CODE) {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version) {
            return new SettlemResnettConSpec(this.Code.Value);
        }
    }

    class SettlemResnettConSpec : OptimulaConceptSpec {
        public SettlemResnettConSpec(Int32 code) : base(code) {
            Path = new List<ArticleCode>();
    
            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var) {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new SettlemResnettTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results) {
            var resTarget = GetTypedTarget<SettlemResnettTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            SettlemResnettTarget evalTarget = resTarget.Value;
    
            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new SettlemResnettResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // IncomesTaxfree	INCOMES_TAXFREE
    class IncomesTaxfreeConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_INCOMES_TAXFREE;
        public IncomesTaxfreeConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new IncomesTaxfreeConSpec(this.Code.Value);
        }
    }

    class IncomesTaxfreeConSpec : OptimulaConceptSpec
    {
        public IncomesTaxfreeConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new IncomesTaxfreeTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<IncomesTaxfreeTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            IncomesTaxfreeTarget evalTarget = resTarget.Value;


            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new IncomesTaxfreeResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // IncomesTaxbase	INCOMES_TAXBASE
    class IncomesTaxbaseConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_INCOMES_TAXBASE;
        public IncomesTaxbaseConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new IncomesTaxbaseConSpec(this.Code.Value);
        }
    }

    class IncomesTaxbaseConSpec : OptimulaConceptSpec
    {
        public IncomesTaxbaseConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new IncomesTaxbaseTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<IncomesTaxbaseTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            IncomesTaxbaseTarget evalTarget = resTarget.Value;

            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new IncomesTaxbaseResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // IncomesTaxwins	INCOMES_TAXWINS
    class IncomesTaxwinsConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_INCOMES_TAXWINS;
        public IncomesTaxwinsConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new IncomesTaxwinsConSpec(this.Code.Value);
        }
    }

    class IncomesTaxwinsConSpec : OptimulaConceptSpec
    {
        public IncomesTaxwinsConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new IncomesTaxwinsTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<IncomesTaxwinsTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            IncomesTaxwinsTarget evalTarget = resTarget.Value;


            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new IncomesTaxwinsResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

    // IncomesSummary	INCOMES_SUMMARY
    class IncomesSummaryConProv : ConceptSpecProvider
    {
        const Int32 CONCEPT_CODE = (Int32)OptimulaConceptConst.CONCEPT_INCOMES_SUMMARY;
        public IncomesSummaryConProv() : base(CONCEPT_CODE)
        {
        }

        public override IConceptSpec GetSpec(IPeriod period, VersionCode version)
        {
            return new IncomesSummaryConSpec(this.Code.Value);
        }
    }

    class IncomesSummaryConSpec : OptimulaConceptSpec
    {
        public IncomesSummaryConSpec(Int32 code) : base(code)
        {
            Path = new List<ArticleCode>();

            ResultDelegate = ConceptEval;
        }

        public override IEnumerable<ITermTarget> DefaultTargetList(ArticleCode article, IPeriod period, IBundleProps ruleset, MonthCode month, IEnumerable<IContractTerm> conTerms, IEnumerable<IPositionTerm> posTerms, IEnumerable<ITermTarget> targets, VariantCode var)
        {
            var con = ContractCode.Zero;
            var pos = PositionCode.Zero;
            if (targets.Count() != 0)
            {
                return Array.Empty<ITermTarget>();
            }
            return new ITermTarget[] {
                new IncomesSummaryTarget(month, con, pos, var, article, this.Code),
            };
        }

        private IList<Result<ITermResult, ITermResultError>> ConceptEval(ITermTarget target, IArticleSpec spec, IPeriod period, IBundleProps ruleset, IList<Result<ITermResult, ITermResultError>> results)
        {
            var resTarget = GetTypedTarget<IncomesSummaryTarget>(target, period);
            if (resTarget.IsFailure)
            {
                return BuildFailResults(resTarget.Error);
            }
            IncomesSummaryTarget evalTarget = resTarget.Value;


            var incomeList = results
                .Where((x) => (x.IsSuccess)).Select((r) => (r.Value))
                .Where((v) => (v.Spec.Sums.Contains(evalTarget.Article)))
                .Select((v) => (v as OptimulaTermResult))
                .Select((tr) => (tr.ResultValue)).ToArray();

            decimal resValue = incomeList.Aggregate(decimal.Zero,
                (agr, item) => decimal.Add(agr, item));

            ITermResult resultsValues = new IncomesSummaryResult(target, spec,
                RoundToInt(resValue), 0);

            return BuildOkResults(resultsValues);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HraveMzdy.Procezor.Registry.Constants;

namespace HraveMzdy.Procezor.Optimula.Registry.Constants
{
    public enum OptimulaConceptConst : Int32
    {
        CONCEPT_TIMESHEETS_PLAN,
        CONCEPT_TIMESHEETS_WORK,
        CONCEPT_TIMEACTUAL_WORK, 
        CONCEPT_PAYMENT_BASIS,
        CONCEPT_PAYMENT_HOURS,
        CONCEPT_PAYMENT_FIXED,
        CONCEPT_OPTIMUS_BASIS,
        CONCEPT_OPTIMUS_HOURS,
        CONCEPT_OPTIMUS_FIXED,
        CONCEPT_OPTIMUS_NETTO,
        CONCEPT_REDUCED_BASIS,
        CONCEPT_REDUCED_HOURS,
        CONCEPT_REDUCED_FIXED,
        CONCEPT_REDUCED_NETTO,
        CONCEPT_AGRWORK_HOURS,
        CONCEPT_AGRTASK_HOURS,
        CONCEPT_ALLOWCE_MFULL,
        CONCEPT_ALLOWCE_HFULL,
        CONCEPT_ALLOWCE_HOURS,
        CONCEPT_ALLOWCE_DAILY,
        CONCEPT_ALLDOWN_DAILY,
        CONCEPT_OFFWORK_HOURS,
        CONCEPT_OFFTASK_HOURS,
        CONCEPT_OFFSETS_HFULL,
        CONCEPT_OFFSETS_HOURS,
        CONCEPT_OFFSETS_DAILY,
        CONCEPT_OFFDOWN_DAILY,
        CONCEPT_SETTLEM_TARGETS,
        CONCEPT_SETTLEM_TARNETT,
        CONCEPT_SETTLEM_AGRWORK,
        CONCEPT_SETTLEM_AGRTASK,
        CONCEPT_SETTLEM_ALLOWCE,
        CONCEPT_SETTLEM_ALLNETT,
        CONCEPT_SETTLEM_OFFWORK,
        CONCEPT_SETTLEM_OFFTASK,
        CONCEPT_SETTLEM_OFFSETS,
        CONCEPT_SETTLEM_RESULTS,
        CONCEPT_SETTLEM_RESNETT,
        CONCEPT_INCOMES_TAXFREE,
        CONCEPT_INCOMES_TAXBASE,
        CONCEPT_INCOMES_TAXWINS,
        CONCEPT_INCOMES_SUMMARY,
    }
    public static class ServiceConceptExtensions
    {
        public static string GetSymbol(this OptimulaConceptConst article)
        {
            return article.ToString();
        }
    }
    class ServiceConceptEnumUtils : EnumConstUtils<OptimulaConceptConst>
    {
    }
}

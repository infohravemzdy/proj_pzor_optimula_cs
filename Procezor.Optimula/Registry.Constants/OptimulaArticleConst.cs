using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HraveMzdy.Procezor.Registry.Constants;

namespace HraveMzdy.Procezor.Optimula.Registry.Constants
{
    public enum OptimulaArticleConst : Int32
    {
        ARTICLE_TIMESHEETS_PLAN,     // Full Timesheets Hours
        ARTICLE_TIMESHEETS_WORK,     // Work Timesheets Hours
        ARTICLE_TIMEACTUAL_WORK,     // Work Timeactual Hours
        ARTICLE_MSALARY_TARGETS,     // Base Salary
        ARTICLE_HSALARY_TARGETS,     // Base Salary
        ARTICLE_MAWARDS_TARGETS,     // Personal  Salary - Targets
        ARTICLE_HAWARDS_TARGETS,     // Personal  Salary - Targets
        ARTICLE_PREMIUM_TARGETS,     // Premium Bonus    - Targets
        ARTICLE_PREMADV_TARGETS,     // Premium Boss     - Targets
        ARTICLE_PREMEXT_TARGETS,     // Premium Personal - Targets
        ARTICLE_AGRWORK_TARGETS,     // Agreement Work Tariff - Targets
        ARTICLE_AGRTASK_TARGETS,     // Agreement Task Tariff - Targets
        ARTICLE_OFFWORK_TARGETS,     // Agreement Work Tariff - Targets Plus
        ARTICLE_OFFTASK_TARGETS,     // Agreement Task Tariff - Targets Plus
        ARTICLE_SETTLEM_TARGETS,     // Setlement - Targets
        ARTICLE_SETTLEM_TARNETT,     // Setlement - Targets
        ARTICLE_SETTLEM_AGRWORK,     // Setlement - Agreement Work
        ARTICLE_SETTLEM_AGRTASK,     // Setlement - Agreement Task
        ARTICLE_SETTLEM_ALLOWCE,     // Setlement - Allowance
        ARTICLE_SETTLEM_ALLNETT,     // Setlement - Allowance Netto
        ARTICLE_SETTLEM_OFFWORK,     // Setlement - Agreement Work Plus
        ARTICLE_SETTLEM_OFFTASK,     // Setlement - Agreement Task Plus
        ARTICLE_SETTLEM_OFFSETS,     // Setlement - Allowance Plus
        ARTICLE_PREMEXT_RESULTS,     // Premium Personal - Results
        ARTICLE_PREMADV_RESULTS,     // Premium Boss     - Results
        ARTICLE_PREMIUM_RESULTS,     // Premium Bonus    - Results
        ARTICLE_MAWARDS_RESULTS,     // Personal Award   - Results
        ARTICLE_HAWARDS_RESULTS,     // Personal Award   - Results
        ARTICLE_SETTLEM_RESULTS,     // Setlement - Results
        ARTICLE_SETTLEM_RESNETT,     // Setlement - Results
        ARTICLE_ALLOWCE_HOFFICE,     // HomeOffice Tariff
        ARTICLE_ALLOWCE_CLOTDAY,     // Clothing Daily Tarrif
        ARTICLE_ALLOWCE_CLOTHRS,     // Clothing Hours Tarrif
        ARTICLE_ALLOWCE_MEALDAY,     // Meal Contribution Tariff
        ARTICLE_OFFSETS_HOFFICE,     // HomeOffice Tariff
        ARTICLE_OFFSETS_CLOTDAY,     // Clothing Daily Tarrif
        ARTICLE_OFFSETS_CLOTHRS,     // Clothing Hours Tarrif
        ARTICLE_OFFSETS_MEALDAY,     // Meal Contribution Tariff
        ARTICLE_INCOMES_TAXFREE,     // Incomes Tax Free
        ARTICLE_INCOMES_TAXBASE,     // Incomes Tax
        ARTICLE_INCOMES_TAXWINS,     // Incomes Tax and Insurance
        ARTICLE_INCOMES_SUMMARY,     // Incomes Summary
        //ARTICLE_OVER_TIME,              // OverTimesheet Hours
        //ARTICLE_ABSENCE_TIME,           // Absence Timesheet Hours
        //ARTICLE_AVERAGE_PAY,            // Average Pay
        //ARTICLE_OVER_WORKTIME,          // Overtime hours
        //ARTICLE_WEEKEND_WORKTIME,       // Weekend hours
        //ARTICLE_NIGHT_WORKTIME,         // Night hours
        //ARTICLE_FEAST_WORKTIME,         // Feast hours
        //ARTICLE_HOLIDAY_ABSENCE,        // Holiday absence hours
        //ARTICLE_BANKDAY_ABSENCE,        // Bank holiday absence hours
        //ARTICLE_EEOBSTRUCT_ABSENCE,     // Employee obstruction absence hours
        //ARTICLE_EROBSTRUCT_ABSENCE,     // Employer obstruction absence hours
        //ARTICLE_OVER_ALLOWANCE,         // Overtime allowance
        //ARTICLE_WEEKEND_ALLOWANCE,      // Weekend allowance
        //ARTICLE_NIGHT_ALLOWANCE,        // Night allowance
        //ARTICLE_FEAST_ALLOWANCE,        // Feast allowance
        //ARTICLE_HOLIDAY_COMPENS,        // Holiday absence compensation
        //ARTICLE_BANKDAY_COMPENS,        // Bank holiday absence compensation
        //ARTICLE_EEOBSTRUCT_COMPENS,     // Employee obstruction absence compensation
        //ARTICLE_EROBSTRUCT_COMPENS,     // Employer obstruction absence compensation
        //ARTICLE_SETTLEMENT_COMPENS,     // Settlement from compensation
    }
    public static class ServiceArticleExtensions
    {
        public static string GetSymbol(this OptimulaArticleConst article)
        {
            return article.ToString();
        }
    }
    class ServiceArticleEnumUtils : EnumConstUtils<OptimulaArticleConst>
    {
    }
}

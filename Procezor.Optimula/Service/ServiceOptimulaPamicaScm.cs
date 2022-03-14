using System;
using System.Collections.Generic;
using System.Linq;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Service.Types;
using HraveMzdy.Procezor.Optimula.Registry.Constants;
using HraveMzdy.Procezor.Optimula.Registry.Factories;
using HraveMzdy.Procezor.Optimula.Registry.Providers;
using HraveMzdy.Legalios.Service.Types;

namespace HraveMzdy.Procezor.Optimula.Service
{
    public class ServiceOptimulaPamicaScm : ServiceOptimula
    {
        private static readonly IList<ArticleCode> TEST_FINAL_DEFS = new List<ArticleCode>() {
            ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_TARGETS),
            ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_RESULTS),
            ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_ALLOWCE),
            ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_SETTLEM_AGRWORK),
            ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_INCOMES_TAXFREE),
            ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_INCOMES_TAXBASE),
            ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_INCOMES_TAXWINS),
            ArticleCode.Get((Int32)OptimulaArticleConst.ARTICLE_INCOMES_SUMMARY),
        };

        public ServiceOptimulaPamicaScm() : base(TEST_VERSION_SCM, TEST_FINAL_DEFS)
        {
        }

        public override IEnumerable<IContractTerm> GetContractTerms(IPeriod period, IEnumerable<ITermTarget> targets)
        {
            return new List<IContractTerm>();
        }

        public override IEnumerable<IPositionTerm> GetPositionTerms(IPeriod period, IEnumerable<IContractTerm> contracts, IEnumerable<ITermTarget> targets)
        {
            return new List<IPositionTerm>();
        }

        protected override bool BuildArticleFactory()
        {
            ArticleFactory = new ServiceScmArticleFactory();

            return true;
        }

        protected override bool BuildConceptFactory()
        {
            ConceptFactory = new ServiceConceptFactory();

            return true;
        }
    }
}

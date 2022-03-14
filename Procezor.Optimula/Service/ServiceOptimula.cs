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
    public abstract class ServiceOptimula : ServiceProcezor
    {
        public const Int32 TEST_VERSION_SCM = 100;
        public const Int32 TEST_VERSION_EPS = 200;
        public const Int32 TEST_VERSION_PUZZLE = 300;

        public ServiceOptimula(Int32 version, IList<ArticleCode> calcArticles) : base(version, calcArticles)
        {
        }
    }
}

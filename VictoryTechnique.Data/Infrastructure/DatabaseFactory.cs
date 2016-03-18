using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictoryTechnique.Core.Infrastructure;

namespace VictoryTechnique.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly VictoryTechniqueDataContext _dataContext;

        public VictoryTechniqueDataContext GetDataContext()
        {
            return _dataContext ?? new VictoryTechniqueDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new VictoryTechniqueDataContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null) _dataContext.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictoryTechnique.Core.Domain;
using VictoryTechnique.Core.Repository;
using VictoryTechnique.Data.Infrastructure;

namespace VictoryTechnique.Data.Repository
{
    public class AreaOfStudyRepository : Repository<AreaOfStudy>, IAreaOfStudyRepository
    {
        public AreaOfStudyRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}

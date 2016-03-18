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
    public class VidSubmissionTagRepository : Repository<VidSubmissionTag>, IVidSubmissionTagRepository
    {
        public VidSubmissionTagRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}

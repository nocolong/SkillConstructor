using System.Collections.Generic;
using VictoryTechnique.Core.Models;

namespace VictoryTechnique.Core.Domain
{
    public class AreaOfStudy
    {
        public int AreaOfStudyId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<VidSubmission> VidSubmissions { get; set; }

        // empty constructor so that Entity Framework can instantiate this object when it fetches from the database
        public AreaOfStudy()
        {

        }

        public AreaOfStudy(AreaOfStudyModel model)
        {
            this.Update(model);
        }

        public void Update(AreaOfStudyModel model)
        {
            AreaOfStudyId = model.AreaOfStudyId;
            Description = model.Description;
        }
    }
}

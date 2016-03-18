using System.Collections.Generic;
using VictoryTechnique.Core.Models;

namespace VictoryTechnique.Core.Domain
{
    public class VidCritique
    {
        public int VidCritiqueId { get; set; }
        public int UserId { get; set; }
        public int VidSubmissionId { get; set; }
        public string VidCritiqueUrl { get; set; }
        public string VidCritiqueText { get; set; }

        public virtual User User { get; set; }
        public virtual VidSubmission VidSubmission { get; set; }

        public virtual ICollection<VidCritiqueTag> Tags { get; set; }

        public VidCritique()
        {

        }

        public VidCritique(VidCritiqueModel model)
        {
            this.Update(model);
        }

        public void Update(VidCritiqueModel model)
        {
            VidCritiqueId = model.VidCritiqueId;
            VidSubmissionId = model.VidSubmissionId;
            UserId = model.UserId;
            VidCritiqueUrl = model.VidCritiqueUrl;
            VidCritiqueText = model.VidCritiqueText;

        }
    }
}

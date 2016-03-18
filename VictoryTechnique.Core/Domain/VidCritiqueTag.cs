using VictoryTechnique.Core.Models;

namespace VictoryTechnique.Core.Domain
{
    public class VidCritiqueTag
    {
        public int UserId { get; set; }
        public int VidCritiqueId { get; set; }

        public virtual User User { get; set; }
        public virtual VidCritique VidCritique { get; set; }

        public VidCritiqueTag()
        {

        }

        public VidCritiqueTag(VidCritiqueTagModel model)
        {
            this.Update(model);
        }

        public void Update(VidCritiqueTagModel model)
        {
            VidCritiqueId = model.VidCritiqueId;
            UserId = model.UserId;
        }
    }
}

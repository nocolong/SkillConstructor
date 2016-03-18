using VictoryTechnique.Core.Models;

namespace VictoryTechnique.Core.Domain
{
    public class VidSubmissionTag
    {
        public int UserId { get; set; }
        public int VidSubmissionId { get; set; }

        public virtual User User { get; set; }
        public virtual VidSubmission VidSubmission { get; set; }

        public VidSubmissionTag()
        {

        }

        public VidSubmissionTag(VidSubmissionTagModel model)
        {
            this.Update(model);
        }

        public void Update(VidSubmissionTagModel model)
        {
            VidSubmissionId = model.VidSubmissionId;
            UserId = model.UserId;
        }
    }
}

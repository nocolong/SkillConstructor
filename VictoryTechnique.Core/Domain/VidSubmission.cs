using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VictoryTechnique.Core.Models;

namespace VictoryTechnique.Core.Domain
{
    public class VidSubmission
    {
        public int VidSubmissionId { get; set; }
        public int UserId { get; set; }
        public int AreaOfStudyId { get; set; }
        public string VidSubmissionUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }

        public virtual User User { get; set; }
        public virtual AreaOfStudy AreaOfStudy { get; set; }

        public virtual ICollection<VidCritique> VidCritiques { get; set; }
        public virtual ICollection<VidSubmissionTag> Tags { get; set; }

        public VidSubmission()
        {
            VidCritiques = new Collection<VidCritique>();
        }


        public VidSubmission(VidSubmissionModel model)
        {
            this.Update(model);
            this.DateOpened = DateTime.Now;

        }

        public void Update(VidSubmissionModel model)
        {
            VidSubmissionId = model.VidSubmissionId;
            AreaOfStudyId = model.AreaOfStudyId;
            UserId = model.UserId;
            VidSubmissionUrl = model.VidSubmissionUrl;
            Description = model.Description;
            DateOpened = model.DateOpened;
            DateClosed = model.DateClosed;
        }
    }
}

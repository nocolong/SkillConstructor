using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictoryTechnique.Core.Models
{
    public class VidSubmissionModel
    {
        public int VidSubmissionId { get; set; }
        public int UserId { get; set; }
        public int AreaOfStudyId { get; set; }
        public string VidSubmissionUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }

        public UserModel User { get; set; }
        public IEnumerable<VidSubmissionTagModel> Tags { get; set; }
    }
}

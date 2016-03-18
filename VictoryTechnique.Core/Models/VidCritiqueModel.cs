using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictoryTechnique.Core.Models
{
    public class VidCritiqueModel
    {
        public int VidCritiqueId { get; set; }
        public int UserId { get; set; }
        public int VidSubmissionId { get; set; }
        public string VidCritiqueUrl { get; set; }
        public string VidCritiqueText { get; set; }

        public IEnumerable<VidCritiqueTagModel> Tags { get; set; }
    }
}

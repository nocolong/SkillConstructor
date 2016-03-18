using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace VictoryTechnique.Core.Domain
{
    public enum Belts
    {
        White = 1,
        Blue = 2,
        Purple = 3,
        Brown = 4,
        Black = 5
    }
    public class User  : IUser<int> 
    {
        public int Id { get; set; } //string?
        public string UserName { get; set; }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public Belts? Belt { get; set; }

        public virtual ICollection<VidSubmission> VidSubmissions { get; set; }
        public virtual ICollection<VidCritique> VidCritiques { get; set; }
        public virtual ICollection<VidSubmissionTag> SubmissionTags { get; set; }
        public virtual ICollection<VidCritiqueTag> CritiqueTags { get; set; }

    }
}

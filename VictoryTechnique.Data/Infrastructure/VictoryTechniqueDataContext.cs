using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictoryTechnique.Core;
using VictoryTechnique.Core.Domain;

namespace VictoryTechnique.Data
{
    public class VictoryTechniqueDataContext : DbContext
    {
        public VictoryTechniqueDataContext() : base("VictoryTechnique")
        {

        }

        public IDbSet<AreaOfStudy> AreasOfStudy { get; set; }
        public IDbSet<VidSubmissionTag> SubmissionTags { get; set; }
        public IDbSet<VidCritiqueTag> CritiqueTags { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<VidCritique> VidCritiques { get; set; }
        public IDbSet<VidSubmission> VidSubmissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaOfStudy>()
                        .HasMany(aos => aos.VidSubmissions)
                        .WithRequired(vs => vs.AreaOfStudy)
                        .HasForeignKey(vs => vs.AreaOfStudyId);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.VidSubmissions)
                        .WithRequired(vs => vs.User)
                        .HasForeignKey(vs => vs.UserId);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.VidCritiques)
                        .WithRequired(vc => vc.User)
                        .HasForeignKey(vc => vc.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.SubmissionTags)
                        .WithRequired(t => t.User)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.CritiqueTags)
                        .WithRequired(ct => ct.User)
                        .HasForeignKey(ct => ct.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<VidCritique>()
                        .HasMany(vc => vc.Tags)
                        .WithRequired(t => t.VidCritique)
                        .HasForeignKey(t => t.VidCritiqueId);

            modelBuilder.Entity<VidSubmission>()
                        .HasMany(vs => vs.VidCritiques)
                        .WithRequired(vc => vc.VidSubmission)
                        .HasForeignKey(vc => vc.VidSubmissionId);

            modelBuilder.Entity<VidSubmission>()
                        .HasMany(vs => vs.Tags)
                        .WithRequired(t => t.VidSubmission)
                        .HasForeignKey(t => t.VidSubmissionId);

            modelBuilder.Entity<VidSubmissionTag>()
                        .HasKey(st => new { st.VidSubmissionId, st.UserId });

            modelBuilder.Entity<VidCritiqueTag>()
                        .HasKey(ct => new { ct.VidCritiqueId, ct.UserId });


        }
    }
}

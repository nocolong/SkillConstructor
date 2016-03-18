using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using VictoryTechnique.Core.Domain;
using VictoryTechnique.Core.Infrastructure;
using VictoryTechnique.Core.Models;
using VictoryTechnique.Core.Repository;
using VictoryTechnique.Infrastructure;

namespace VictoryTechnique.Controllers
{
    [Authorize]
    public class VidSubmissionTagsController : BaseApiController
    {
        private readonly IVidSubmissionTagRepository _vidSubmissionTagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VidSubmissionTagsController(IVidSubmissionTagRepository vidSubmissionTagRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository)
        {
            _vidSubmissionTagRepository = vidSubmissionTagRepository;
            _unitOfWork = unitOfWork;
        }

        // POST: api/VidSubmissionTags
        [ResponseType(typeof(VidSubmissionTag))]
        public IHttpActionResult PostVidCritiqueTag(VidSubmissionTagModel vidSubmissionTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure you have selected the correct user for this tag");
            }

            var dbVidSubmissionTag = new VidSubmissionTag(vidSubmissionTag);

            dbVidSubmissionTag.UserId = CurrentUser.Id;
            _vidSubmissionTagRepository.Add(dbVidSubmissionTag);
            _unitOfWork.Commit();

            //vidSubmission.DateSubmitted = dbVidSubmission.DateSubmitted;
            return Ok();
        }

        // DELETE: api/VidSubmissionTags/5
        [ResponseType(typeof(VidSubmissionTag))]
        public IHttpActionResult DeleteVidSubmissionTag(int id)
        {
            VidSubmissionTag vidSubmissionTag = _vidSubmissionTagRepository.GetById(id);
            if (vidSubmissionTag == null)
            {
                return NotFound();
            }

            _vidSubmissionTagRepository.Delete(vidSubmissionTag);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<VidSubmissionTagModel>(vidSubmissionTag));
        }

        private bool VidSubmissionTagExists(int vidSubmissionId, int userId)
        {
            return _vidSubmissionTagRepository.Any(vst => vst.VidSubmissionId == vidSubmissionId && userId == vst.UserId);
        }

    }
}
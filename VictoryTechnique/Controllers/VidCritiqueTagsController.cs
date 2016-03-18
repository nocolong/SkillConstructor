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
    public class VidCritiqueTagsController : BaseApiController
    {
        private readonly IVidCritiqueTagRepository _vidCritiqueTagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VidCritiqueTagsController(IVidCritiqueTagRepository vidCritiqueTagRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository)
        {
            _vidCritiqueTagRepository = vidCritiqueTagRepository;
            _unitOfWork = unitOfWork;
        }

        // POST: api/VidCritiqueTags
        [ResponseType(typeof(VidCritiqueTag))]
        public IHttpActionResult PostVidCritiqueTag(VidCritiqueTagModel vidCritiqueTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure you have selected the correct user for this tag");
            }

            var dbVidCritiqueTag = new VidCritiqueTag(vidCritiqueTag);

            dbVidCritiqueTag.UserId = CurrentUser.Id;
            _vidCritiqueTagRepository.Add(dbVidCritiqueTag);
            _unitOfWork.Commit();

            //vidCritique.DateSubmitted = dbVidCritique.DateSubmitted;
            return Ok();
        }

        // DELETE: api/VidCritiqueTags/5
        [ResponseType(typeof(VidCritiqueTag))]
        public IHttpActionResult DeleteVidCritiqueTag(int id)
        {
            VidCritiqueTag vidCritiqueTag = _vidCritiqueTagRepository.GetById(id);
            if (vidCritiqueTag == null)
            {
                return NotFound();
            }

            _vidCritiqueTagRepository.Delete(vidCritiqueTag);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<VidCritiqueTagModel>(vidCritiqueTag));
        }

    }
}
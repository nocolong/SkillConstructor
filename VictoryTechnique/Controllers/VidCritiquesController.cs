using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class VidCritiquesController : BaseApiController
    {
        private readonly IVidCritiqueRepository _vidCritiqueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VidCritiquesController(IVidCritiqueRepository vidCritiqueRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository)
        {
            _vidCritiqueRepository = vidCritiqueRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/VidCritiques
        public IEnumerable<VidCritiqueModel> GetVidCritiques()
        {
            return Mapper.Map<IEnumerable<VidCritiqueModel>>(_vidCritiqueRepository.GetAll());
        }

        // GET: api/VidCritiques/5
        [ResponseType(typeof(VidCritique))]
        public IHttpActionResult GetVidCritique(int id)
        {
            VidCritique vidCritique = _vidCritiqueRepository.GetById(id);

            if (vidCritique == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<VidCritiqueModel>(vidCritique));
        }

        // GET: api/VidCritiques/User
        [Route("api/vidCritique/user")]
        public IEnumerable<VidCritiqueModel> GetUserVidCritiques()
        {
            return Mapper.Map<IEnumerable<VidCritiqueModel>>(_vidCritiqueRepository.GetWhere(vc => vc.UserId == CurrentUser.Id));
        }

        // PUT: api/VidCritiques/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVidCritique(int id, VidCritiqueModel vidCritique)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vidCritique.VidCritiqueId)
            {
                return BadRequest();
            }

            var dbVidCritique = _vidCritiqueRepository.GetById(id);

            dbVidCritique.Update(vidCritique);

            _vidCritiqueRepository.Update(dbVidCritique);


            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!VidCritiqueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VidCritiques
        [ResponseType(typeof(VidCritique))]
        public IHttpActionResult PostVidCritique(VidCritiqueModel vidCritique)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbVidCritique = new VidCritique(vidCritique);

            dbVidCritique.UserId = CurrentUser.Id;
            _vidCritiqueRepository.Add(dbVidCritique);
            _unitOfWork.Commit();

            vidCritique.VidCritiqueId = dbVidCritique.VidCritiqueId;
            //vidCritique.DateSubmitted = dbVidCritique.DateSubmitted;

            return CreatedAtRoute("DefaultApi", new { id = vidCritique.VidCritiqueId }, vidCritique);
        }

        // DELETE: api/VidCritiques/5
        [ResponseType(typeof(VidCritique))]
        public IHttpActionResult DeleteVidCritique(int id)
        {
            VidCritique vidCritique = _vidCritiqueRepository.GetById(id);
            if (vidCritique == null)
            {
                return NotFound();
            }

            _vidCritiqueRepository.Delete(vidCritique);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<VidCritiqueModel>(vidCritique));
        }

        private bool VidCritiqueExists(int id)
        {
            return _vidCritiqueRepository.Any(e => e.VidCritiqueId == id);
        }
    }
}
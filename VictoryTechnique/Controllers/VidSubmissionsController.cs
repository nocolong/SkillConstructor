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
    public class VidSubmissionsController : BaseApiController
    {
        //private VictoryTechniqueDataContext db = new VictoryTechniqueDataContext();

        private readonly IVidSubmissionRepository _vidSubmissionRepository;
        private readonly IVidCritiqueRepository _vidCritiqueRepository;
        private readonly IUnitOfWork _unitOfWork;

        //Constructor based dependency injection
        public VidSubmissionsController(IVidSubmissionRepository vidSubmissionRepository, IUserRepository userRepository, IVidCritiqueRepository vidCritiqueRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _vidSubmissionRepository = vidSubmissionRepository;
            _unitOfWork = unitOfWork;
            _vidCritiqueRepository = vidCritiqueRepository;
        }

        // GET: api/VidSubmissions
        public IEnumerable<VidSubmissionModel> GetVidSubmissions()
        {
            //return Mapper.Map<IEnumerable<SubmissionModel>>(db.Submissions);
            return Mapper.Map<IEnumerable<VidSubmissionModel>>(_vidSubmissionRepository.GetAll());
        }

        //GET: api/VidSubmission/5/VidCritiques
        [Route("api/vidSubmissions/{VidSubmissionId}/vidCritiques")]
        public IEnumerable<VidCritiqueModel> GetVidCritiqueForVidSubmission(int VidSubmissionId)
        {
            return Mapper.Map<IEnumerable<VidCritiqueModel>>(
                _vidCritiqueRepository.GetWhere(vc => vc.VidSubmissionId == VidSubmissionId)
            );
        }

        // GET: api/VidSubmissions/User
        [Route("api/vidSubmissions/user")]
        public IEnumerable<VidSubmissionModel> GetUserVidSubmissions()
        {
            return Mapper.Map<IEnumerable<VidSubmissionModel>>(_vidSubmissionRepository.GetWhere(vs => vs.UserId == CurrentUser.Id));
        }

        // GET: api/VidSubmissions/Open
        [Route("api/vidSubmissions/open")]
        public IEnumerable<VidSubmissionModel> GetOpenVidSubmissions()
        {
            return Mapper.Map<IEnumerable<VidSubmissionModel>>(_vidSubmissionRepository.GetWhere(vs => vs.DateClosed == null));
        }

        // GET: api/Submissions/5/Responses/Paid
        //[Route("api/vidSubmissions/{VidSubmissionId}/vidCritiques/paid")]
        //public IEnumerable<ResponseModel> GetPaidResponses(ResponseModel response)
        //{
        //    return Mapper.Map<IEnumerable<ResponseModel>>(_vidCritiqueRepository.GetWhere(r => r.Purchased == true));
        //}

        // GET: api/Submissions/5/Responses/Unpaid
        //[Route("api/submissions/{SubmissionId}/responses/unpaid")]
        //public IEnumerable<ResponseModel> GetUnpaidResponses(ResponseModel response)
        //{
        //    return Mapper.Map<IEnumerable<ResponseModel>>(_vidCritiqueRepository.GetWhere(r => r.Purchased == false));
        //}

        // GET: api/VidSubmissions/5
        [ResponseType(typeof(Core.Domain.VidSubmission))]
        public IHttpActionResult GetVidSubmission(int id)
        {
            //VidSubmission vidSubmission = db.VidSubmissions.Find(id);
            VidSubmission vidSubmission = _vidSubmissionRepository.GetById(id);

            if (vidSubmission == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<VidSubmissionModel>(vidSubmission));
        }

        // PUT: api/VidSubmissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVidSubmission(int id, VidSubmissionModel vidSubmission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vidSubmission.VidSubmissionId)
            {
                return BadRequest();
            }

            var dbVidSubmission = _vidSubmissionRepository.GetById(id);

            dbVidSubmission.Update(vidSubmission);

            //db.Entry(dbVidSubmission).State = EntityState.Modified;
            _vidSubmissionRepository.Update(dbVidSubmission);

            try
            {
                //db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!VidSubmissionExists(id))
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

        // POST: api/VidSubmissions
        [ResponseType(typeof(VidSubmission))]
        public IHttpActionResult PostVidSubmission(VidSubmissionModel vidSubmission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbVidSubmission = new VidSubmission(vidSubmission);

            //db.VidSubmissions.Add(dbVidSubmission);
            //db.SaveChanges();
            dbVidSubmission.UserId = CurrentUser.Id;
            _vidSubmissionRepository.Add(dbVidSubmission);
            _unitOfWork.Commit();

            vidSubmission.VidSubmissionId = dbVidSubmission.VidSubmissionId;
            vidSubmission.DateOpened = dbVidSubmission.DateOpened;

            return CreatedAtRoute("DefaultApi", new { id = vidSubmission.VidSubmissionId }, vidSubmission);
        }

        // DELETE: api/VidSubmissions/5
        [ResponseType(typeof(VidSubmission))]
        public IHttpActionResult DeleteVidSubmission(int id)
        {
            //VidSubmission vidSubmission = db.VidSubmissions.Find(id);
            VidSubmission vidSubmission = _vidSubmissionRepository.GetById(id);

            if (vidSubmission == null)
            {
                return NotFound();
            }

            //db.VidSubmissions.Remove(vidSubmission);
            //db.SaveChanges();
            _vidSubmissionRepository.Delete(vidSubmission);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<VidSubmissionModel>(vidSubmission));
        }

        // POST: api/Submissions/5
        //[HttpPost]
        //[Route("api/vidSubmissions/close")]
        //public IHttpActionResult PickAnswer(ResponseModel response)
        //{
        //    var dbResponse = _vidCritiqueRepository.GetById(response.ResponseId);

        //    if (dbResponse.Submission.DateClosed == null)
        //    {
        //        dbResponse.Picked = true;
        //        dbResponse.Submission.DateClosed = DateTime.Now;

        //        _vidCritiqueRepository.Update(dbResponse);
        //        _unitOfWork.Commit();
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        private bool VidSubmissionExists(int id)
        {
            //return db.VidSubmissions.Count(e => e.VidSubmissionId == id) > 0;
            return _vidSubmissionRepository.Any(e => e.VidSubmissionId == id);
        }
    }
}
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
    public class AreasOfStudyController : ApiController
    {
        //private VictoryTechniqueDataContext db = new VictoryTechniqueDataContext();
        private readonly IAreaOfStudyRepository _areaOfStudyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AreasOfStudyController(IAreaOfStudyRepository areaOfStudyRepository, IUnitOfWork unitOfWork)
        {
            _areaOfStudyRepository = areaOfStudyRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/AreasOfStudy
        public IEnumerable<AreaOfStudyModel> GetAreaOfStudy()
        {
            //return Mapper.Map<IEnumerable<AreaOfStudyModel>>(db.AreasOfStudy);
            return Mapper.Map<IEnumerable<AreaOfStudyModel>>(_areaOfStudyRepository.GetAll());
        }

        // GET: api/AreasOfStudy/5
        [ResponseType(typeof(AreaOfStudy))]
        public IHttpActionResult GetAreaOfStudy(int id)
        {
            //AreaOfStudy areaOfStudy = db.AreasOfStudy.Find(id);
            AreaOfStudy areaOfStudy = _areaOfStudyRepository.GetById(id);
            if (areaOfStudy == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AreaOfStudyModel>(areaOfStudy));
        }

        // PUT: api/AreasOfStudy/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAreaOfStudy(int id, AreaOfStudyModel areaOfStudy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != areaOfStudy.AreaOfStudyId)
            {
                return BadRequest();
            }

            //var dbAreaOfStudy = db.AreasOfStudy.Find(id);
            var dbAreaOfStudy = _areaOfStudyRepository.GetById(id);

            dbAreaOfStudy.Update(areaOfStudy);

            // db.Entry(areaOfStudy).State = EntityState.Modified;
            _areaOfStudyRepository.Update(dbAreaOfStudy);

            try
            {
                //  db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!AreaOfStudyExists(id))
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

        // POST: api/AreasOfStudy
        [ResponseType(typeof(AreaOfStudy))]
        public IHttpActionResult PostAreaOfStudy(AreaOfStudyModel areaOfStudy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbAreaOfStudy = new AreaOfStudy(areaOfStudy);

            // db.AreasOfStudy.Add(dbAreaOfStudy);
            // db.SaveChanges();

            _areaOfStudyRepository.Add(dbAreaOfStudy);
            _unitOfWork.Commit();

            areaOfStudy.AreaOfStudyId = dbAreaOfStudy.AreaOfStudyId;

            return CreatedAtRoute("DefaultApi", new { id = areaOfStudy.AreaOfStudyId }, areaOfStudy);
        }

        // DELETE: api/AreasOfStudy/5
        [ResponseType(typeof(AreaOfStudy))]
        public IHttpActionResult DeleteAreaOfStudy(int id)
        {
            // AreaOfStudy areaOfStudy = db.AreasOfStudy.Find(id);
            AreaOfStudy areaOfStudy = _areaOfStudyRepository.GetById(id);

            if (areaOfStudy == null)
            {
                return NotFound();
            }

            //db.AreasOfStudy.Remove(areaOfStudy);
            //db.SaveChanges();

            _areaOfStudyRepository.Delete(areaOfStudy);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<AreaOfStudyModel>(areaOfStudy));
        }

        //SimpleInjector manages the lifetime for us, cleaning lady!
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool AreaOfStudyExists(int id)
        {
            //return db.AreasOfStudy.Count(e => e.AreaOfStudyId == id) > 0;
            return _areaOfStudyRepository.Any(e => e.AreaOfStudyId == id);
        }
    }
}
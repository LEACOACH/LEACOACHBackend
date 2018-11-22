using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEACOACHAAPIREST.Models;
using LEACOACHAAPIREST.dto;
using AutoMapper;

namespace LEACOACHAAPIREST.Controllers.leacoachApi
{
    public class TutorController : ApiController
    {
        private leacoachEntities2 context;
        private string EntityName = "TUTOR";
        public TutorController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/tutors")]
        public IHttpActionResult getTutors() {

            dynamic response = new ExpandoObject();

            try
            {
                
                response.Status = ConstantValues.ResponseStatus.OK;
                var tutors = context.Tutors.ToList();
                response.Code = HttpStatusCode.OK;
                response.Tutors = tutors.Select(Mapper.Map<Tutors, Tutorsdto>);
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Status = ConstantValues.ResponseStatus.ERROR;
                response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, response);
            }
    }
      

        [HttpGet]
        [Route("leacoach/v1/tutors/{id:int}")]
        public IHttpActionResult getTutor(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var tutor = context.Tutors.FirstOrDefault(c => c.id == id);
                if (tutor == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Tutor = Mapper.Map<Models.Tutors, dto.Tutorsdto>(tutor);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        [HttpGet]
        [Route("leacoach/v1/tutors/{tutorid:int}/publications")]
        public IHttpActionResult getPublicationsByTutors(int tutorid)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                var publications = context.Publications.Where(m => m.Tutor_courses.id_tutor == tutorid).ToList();
                Response.Publications = publications.Select(Mapper.Map<Publications, dto.PublicationResponsedto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }

        }

        

        [HttpPost]
        [Route("leacoach/v1/tutors")]
        public IHttpActionResult PostTutor( [FromBody] Tutorsdto tutorDto)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var tutor = Mapper.Map<Tutorsdto, Tutors>(tutorDto);
                context.Tutors.Add(tutor);
                context.SaveChanges();

                tutorDto.id = tutor.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Tutor = tutorDto;

                return Created(new Uri(Request.RequestUri + "/" + tutorDto.id), Response);

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                Response.tutor = tutorDto;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }
    }
}

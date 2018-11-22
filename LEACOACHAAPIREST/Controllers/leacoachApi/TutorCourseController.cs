using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Dynamic;
using LEACOACHAAPIREST.Models;
using LEACOACHAAPIREST.dto;
using AutoMapper;

namespace LEACOACHAAPIREST.Controllers.leacoachApi
{
    public class TutorCourseController : ApiController
    {

        private leacoachEntities2 context;
        private string EntityName = "TutorCourse";
        public TutorCourseController()
        {
            context = new leacoachEntities2();
        }
            
        [HttpGet]
        [Route("leacoach/v1/TutorCourses/{id:int}")]
        public IHttpActionResult getFavoritebyid(int id)//idTutor
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var tutor_courses = context.Tutor_courses.Where(c => c.id_tutor == id).ToList();
                var courses = tutor_courses.Select(c => c.Courses);
                
                if (courses == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Courses = courses.Select(Mapper.Map<Courses, Coursedto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }
               
    }
}

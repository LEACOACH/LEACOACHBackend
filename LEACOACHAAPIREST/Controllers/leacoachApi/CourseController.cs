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
    public class CourseController : ApiController
    {

        private leacoachEntities2 context;
        private string EntityName = "Courses";
        public CourseController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/Courses")]
        public IHttpActionResult getCourses()
        {

            dynamic response = new ExpandoObject();

            try
            {
                response.Status = ConstantValues.ResponseStatus.OK;
                var publicaciones = context.Courses.ToList();
                response.Code = HttpStatusCode.OK;
                response.CoursesList = publicaciones.Select(Mapper.Map<Courses, Coursedto>);
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
        [Route("leacoach/v1/Courses/{id:int}")]
        public IHttpActionResult getCoursebyid(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var course = context.Courses.FirstOrDefault(c => c.id == id);
                if (course == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Course = Mapper.Map<Models.Courses, dto.Coursedto>(course);
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
        [Route("leacoach/v1/Courses")]
        public IHttpActionResult PostCourses([FromBody] Coursedto CourseDto)
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

                var Course = Mapper.Map<Coursedto, Courses>(CourseDto);

                context.Courses.Add(Course);
                context.SaveChanges();

                CourseDto.id = Course.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Course = CourseDto;

                return Created(new Uri(Request.RequestUri + "/" + CourseDto.id), Response);

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

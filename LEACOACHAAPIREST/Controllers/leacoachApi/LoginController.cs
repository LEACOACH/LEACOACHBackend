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
    public class LoginController : ApiController
    {
        private leacoachEntities2 context;
        private string EntityName = "Login";
        public LoginController()
        {
            context = new leacoachEntities2();
        }

        [HttpPost]
        [Route("leacoach/v1/login/tutor")]
        public IHttpActionResult LoginTutor( [FromBody] Tutorsdto tutorDto)
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
                var login = context.Tutors.FirstOrDefault(c => c.email == tutor.email && c.password == tutor.password);
                var tut = Mapper.Map<Tutors, Tutorsdto>(login);
                if (login != null)
                {
                    Response.Status = ConstantValues.ResponseStatus.OK;                    
                    Response.Tutor = tut;
                    return Ok(Response);
                }
                else {
                    Response.Message = "Usuario o password incorrecto";
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    return Content(HttpStatusCode.InternalServerError, Response);
                }               

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                Response.tutor = tutorDto;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        [HttpPost]
        [Route("leacoach/v1/login/user")]
        public IHttpActionResult LoginUser([FromBody] Usersdto userDto)
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

                var user = Mapper.Map<dto.Usersdto, Users>(userDto);
                var login = context.Users.FirstOrDefault(c => c.email == user.email && c.password == user.password);
                var use = Mapper.Map<Users, dto.Usersdto>(login);

                if (login != null)
                {
                    Response.Status = ConstantValues.ResponseStatus.OK;
                    Response.User = use;
                    return Ok(Response);
                }
                else
                {
                    Response.Message = "Usuario o password incorrecto";
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    return Content(HttpStatusCode.InternalServerError, Response);
                }

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                Response.User = userDto;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }
    }
}

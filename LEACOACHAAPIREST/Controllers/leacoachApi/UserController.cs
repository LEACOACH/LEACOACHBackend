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
    public class UserController : ApiController
    {

        private leacoachEntities2 context;
        private string EntityName = "users";
        public UserController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/users")]
        public IHttpActionResult getusers()
        {

            dynamic response = new ExpandoObject();

            try
            {
                response.Status = ConstantValues.ResponseStatus.OK;
                var users = context.Users.ToList();
                response.Code = HttpStatusCode.OK;
                response.Users = users.Select(Mapper.Map<Users, Usersdto>);
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
        [Route("leacoach/v1/users/{id:int}")]
        public IHttpActionResult getuserbyid(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var users = context.Users.FirstOrDefault(c => c.id == id);
                if (users == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.user = Mapper.Map<Models.Users, dto.Usersdto>(users);
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
        [Route("leacoach/v1/users")]
        public IHttpActionResult PostUsers([FromBody] Usersdto usersdto)
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

                var users = Mapper.Map<Usersdto, Users>(usersdto);

                context.Users.Add(users);
                context.SaveChanges();

                usersdto.id = users.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.user = usersdto;

                return Created(new Uri(Request.RequestUri + "/" + usersdto.id), Response);

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

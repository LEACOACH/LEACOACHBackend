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
    public class TypesController : ApiController
    {
        private leacoachEntities2 context;
        private string EntityName = "Typess";
        public TypesController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/Types")]
        public IHttpActionResult getTypes()
        {

            dynamic response = new ExpandoObject();

            try
            {
                response.Status = ConstantValues.ResponseStatus.OK;
                var publicaciones = context.Types.ToList();
                response.Code = HttpStatusCode.OK;
                response.Types = publicaciones.Select(Mapper.Map<Types, Typesdto>);
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
        [Route("leacoach/v1/Types/{id:int}")]
        public IHttpActionResult getTypesbyid(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var types = context.Types.FirstOrDefault(c => c.id == id);
                if (types == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Types = Mapper.Map<Models.Types, dto.Typesdto>(types);
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
        [Route("leacoach/v1/Types")]
        public IHttpActionResult PostTypess([FromBody] Typesdto TypesDto)
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

                var Types = Mapper.Map<Typesdto, Types>(TypesDto);

                context.Types.Add(Types);
                context.SaveChanges();

                TypesDto.id = Types.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Types = TypesDto;

                return Created(new Uri(Request.RequestUri + "/" + TypesDto.id), Response);

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

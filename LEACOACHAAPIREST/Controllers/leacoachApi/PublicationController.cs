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
    public class PublicationController : ApiController
    {

        private leacoachEntities2 context;
        private string EntityName = "publications";
        public PublicationController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/publications")]
        public IHttpActionResult getPublications()
        {
            dynamic response = new ExpandoObject();

            try
            {
                response.Status = ConstantValues.ResponseStatus.OK;
                var publicaciones = context.Publications.ToList();

                response.Code = HttpStatusCode.OK;
                response.Publications = publicaciones.Select(Mapper.Map<Publications, PublicationResponsedto>);
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
        [Route("leacoach/v1/publications/{id:int}")]
        public IHttpActionResult getpublicationbyid(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var publicacion = context.Publications.FirstOrDefault(c => c.id == id);
                if (publicacion == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Publication = Mapper.Map<Models.Publications, dto.Publicationsdto>(publicacion);
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
        [Route("leacoach/v1/publications")]
        public IHttpActionResult PostPublications([FromBody] Publicationsdto publicationDto)
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

                var publication = Mapper.Map<Publicationsdto, Publications>(publicationDto);

                context.Publications.Add(publication);
                context.SaveChanges();

                publicationDto.id = publication.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.publication = publicationDto;

                return Created(new Uri(Request.RequestUri + "/" + publicationDto.id), Response);

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

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
    public class ImagesController : ApiController
    {
        private leacoachEntities2 context;
        private string EntityName = "Images";
        public ImagesController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/Images")]
        public IHttpActionResult getTypes()
        {

            dynamic response = new ExpandoObject();

            try
            {
                response.Status = ConstantValues.ResponseStatus.OK;
                var images = context.Images.ToList();
                response.Code = HttpStatusCode.OK;
                response.Images = images.Select(Mapper.Map<Images, Imagedto>);
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
        [Route("leacoach/v1/Images/{id:int}")]
        public IHttpActionResult getTypesbyid(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var images = context.Images.Where(c => c.id_publication == id).ToList();
                if (images == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Images = images.Select(Mapper.Map<Images, Imagedto>);
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
        [Route("leacoach/v1/Images")]
        public IHttpActionResult PostTypess([FromBody] Imagedto ImageDto)
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

                var Images = Mapper.Map<Imagedto, Images>(ImageDto);

                context.Images.Add(Images);
                context.SaveChanges();

                ImageDto.id = Images.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Images = ImageDto;

                return Created(new Uri(Request.RequestUri + "/" + ImageDto.id), Response);

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

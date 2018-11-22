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
    public class CareerController : ApiController
    {

        private leacoachEntities2 context;
        private string EntityName = "careers";
        public CareerController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/careers")]
        public IHttpActionResult getCareers()
        {

            dynamic response = new ExpandoObject();

            try
            {
                response.Status = ConstantValues.ResponseStatus.OK;
                var careers = context.Careers.ToList();
                response.Code = HttpStatusCode.OK;
                response.careers = careers.Select(Mapper.Map<Careers, Careerdto>);
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
        [Route("leacoach/v1/careers/{id:int}")]
        public IHttpActionResult getCareerbyid(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var careers = context.Careers.FirstOrDefault(c => c.id == id);
                if (careers == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.careers = Mapper.Map<Models.Careers, dto.Careerdto>(careers);
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
        [Route("leacoach/v1/careers")]
        public IHttpActionResult PostCareers([FromBody] Careerdto CareerDto)
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

                var Career = Mapper.Map<Careerdto, Careers>(CareerDto);

                context.Careers.Add(Career);
                context.SaveChanges();

                CareerDto.id = Career.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Career = CareerDto;

                return Created(new Uri(Request.RequestUri + "/" + CareerDto.id), Response);

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

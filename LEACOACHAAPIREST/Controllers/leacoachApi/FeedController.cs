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
    public class FeedController : ApiController
    {

        private leacoachEntities2 context;
        private string EntityName = "Feed";
        public FeedController()
        {
            context = new leacoachEntities2();
        }
            
        [HttpGet]
        [Route("leacoach/v1/Feed/TrendingTutors")]
        public IHttpActionResult getTrendingTutors()//idTutor
        {

            dynamic response = new ExpandoObject();                        
            try
            {

                var trendingTutors = context.Tutors.OrderByDescending(c => c.likes).Take(10);
                response.Status = ConstantValues.ResponseStatus.OK;
                response.Code = HttpStatusCode.OK;
                response.Tutors = trendingTutors.Select(Mapper.Map<Tutors, dto.Tutorsdto>);
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
        [Route("leacoach/v1/Feed/TrendingPublications")]
        public IHttpActionResult getTrendingPublications()//idTutor
        {

            dynamic response = new ExpandoObject();
            try
            {

                var trendingPublications = context.Publications.OrderByDescending(c => c.likes).Take(10);
                response.Status = ConstantValues.ResponseStatus.OK;
                response.Code = HttpStatusCode.OK;
                response.Publications = trendingPublications.Select(Mapper.Map<Publications, dto.PublicationResponsedto>);
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Status = ConstantValues.ResponseStatus.ERROR;
                response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}

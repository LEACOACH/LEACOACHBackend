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
    public class NotificationController : ApiController
    {
        private leacoachEntities2 context;
        private string EntityName = "Notification";
        public NotificationController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/notifications")]
        public IHttpActionResult getTutors() {

            dynamic response = new ExpandoObject();

            try
            {
                
                response.Status = ConstantValues.ResponseStatus.OK;
                var notifications = context.Notifications.ToList();
                response.Code = HttpStatusCode.OK;
                response.Notifications = notifications.Select(Mapper.Map<Notifications, dto.Notificationdto>);
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
        [Route("leacoach/v1/notifications/{id:int}")]
        public IHttpActionResult getTutor(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var notifications = context.Notifications.Where(c=>c.tutor_id==id);
                if (notifications == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Notifications = notifications.Select(Mapper.Map<Notifications, dto.Notificationdto>);
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
        [Route("leacoach/v1/notifications")]
        public IHttpActionResult PostTutor( [FromBody] dto.Notificationdto notificationdto)
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

                var noti = Mapper.Map<dto.Notificationdto, Notifications>(notificationdto);
                context.Notifications.Add(noti);
                context.SaveChanges();

                notificationdto.id = noti.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Notification = notificationdto;

                return Created(new Uri(Request.RequestUri + "/" + notificationdto.id), Response);

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                Response.Notification = notificationdto;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }
    }
}

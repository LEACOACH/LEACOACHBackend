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
    public class CommentController : ApiController
    {

        private leacoachEntities2 context;
        private string EntityName = "Comments";
        public CommentController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/Comments")]
        public IHttpActionResult getComments()
        {

            dynamic response = new ExpandoObject();

            try
            {
                response.Status = ConstantValues.ResponseStatus.OK;
                var comments = context.Comments.ToList();
                response.Code = HttpStatusCode.OK;
                response.CommentsList = comments.Select(Mapper.Map<Comments, Commentsdto>);
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
        [Route("leacoach/v1/Comments/{id:int}")]
        public IHttpActionResult getCommentbyid(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var comments = context.Comments.FirstOrDefault(c => c.id == id);
                if (comments == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Comment = Mapper.Map<Models.Comments, dto.Commentsdto>(comments);
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
        [Route("leacoach/v1/Comments")]
        public IHttpActionResult PostComments([FromBody] Commentsdto CommentDto)
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

                var Comment = Mapper.Map<Commentsdto, Comments>(CommentDto);

                context.Comments.Add(Comment);
                context.SaveChanges();

                CommentDto.id = Comment.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Comment = CommentDto;

                return Created(new Uri(Request.RequestUri + "/" + CommentDto.id), Response);

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

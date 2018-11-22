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
    public class FavoriteController : ApiController
    {

        private leacoachEntities2 context;
        private string EntityName = "Favorites";
        public FavoriteController()
        {
            context = new leacoachEntities2();
        }

        [HttpGet]
        [Route("leacoach/v1/Favorites")]
        public IHttpActionResult getFavorites()
        {

            dynamic response = new ExpandoObject();

            try
            {
                response.Status = ConstantValues.ResponseStatus.OK;
                var publicaciones = context.Favorites.ToList();
                response.Code = HttpStatusCode.OK;
                response.FavoritesList = publicaciones.Select(Mapper.Map<Favorites, Favoritesdto>);
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
        [Route("leacoach/v1/Favorites/{id:int}")]
        public IHttpActionResult getFavoritebyid(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var favorites = context.Favorites.FirstOrDefault(c => c.id == id);
                if (favorites == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Favorite = Mapper.Map<Models.Favorites, dto.Favoritesdto>(favorites);
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
        [Route("leacoach/v1/Favorites")]
        public IHttpActionResult PostFavorites([FromBody] Favoritesdto FavoriteDto)
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

                var Favorite = Mapper.Map<Favoritesdto, Favorites>(FavoriteDto);

                context.Favorites.Add(Favorite);
                context.SaveChanges();

                FavoriteDto.id = Favorite.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Favorite = FavoriteDto;

                return Created(new Uri(Request.RequestUri + "/" + FavoriteDto.id), Response);

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

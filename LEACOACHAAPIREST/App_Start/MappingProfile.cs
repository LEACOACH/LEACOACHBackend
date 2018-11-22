using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LEACOACHAAPIREST.Models;
using LEACOACHAAPIREST.dto;

namespace LEACOACHAAPIREST.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Models.Tutors, dto.Tutorsdto>();
            CreateMap<dto.Tutorsdto, Models.Tutors>();

            CreateMap<Models.Publications, dto.Publicationsdto>();
            CreateMap<dto.Publicationsdto, Models.Publications>();

            CreateMap<Models.Publications, dto.PublicationResponsedto>();
            CreateMap<dto.PublicationResponsedto, Models.Publications>();

            CreateMap<Models.Careers, dto.Careerdto>();
            CreateMap<dto.Careerdto, Models.Careers>();

            CreateMap<Models.Comments, dto.Commentsdto>();
            CreateMap<dto.Commentsdto, Models.Comments>();

            CreateMap<Models.Courses, dto.Coursedto>();
            CreateMap<dto.Coursedto, Models.Courses>();


            CreateMap<Models.Favorites, dto.Favoritesdto>();
            CreateMap<dto.Favoritesdto, Models.Favorites>();

            CreateMap<Models.Types, dto.Typesdto>();
            CreateMap<dto.Typesdto, Models.Types>();

            CreateMap<Models.Users, dto.Usersdto>();
            CreateMap<dto.Usersdto, Models.Users>();

            CreateMap<Models.Images, dto.Imagedto>();
            CreateMap<dto.Imagedto, Models.Images>();

            CreateMap<Models.Tutor_courses, dto.TutorCoursedto>();
            CreateMap<dto.TutorCoursedto, Models.Tutor_courses>();


            CreateMap<Models.Tutor_courses, dto.TutorCourseResponsedto>();
            CreateMap<dto.TutorCourseResponsedto, Models.Tutor_courses>();

            CreateMap<Models.Notifications, dto.Notificationdto>();
            CreateMap<dto.Notificationdto, Models.Notifications>();
        }

    }
}
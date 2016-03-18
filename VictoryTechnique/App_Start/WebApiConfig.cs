using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VictoryTechnique.Core.Domain;
using VictoryTechnique.Core.Models;

namespace VictoryTechnique
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            CreateMaps();

        }
        public static void CreateMaps()
        {
            Mapper.CreateMap<VidCritique, VidCritiqueModel>();
            Mapper.CreateMap<VidSubmission, VidSubmissionModel>();
            Mapper.CreateMap<VidCritiqueTag, VidCritiqueTagModel>();
            Mapper.CreateMap<VidSubmissionTag, VidSubmissionTagModel>();
            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<AreaOfStudy, AreaOfStudyModel>();
        }
    
    }
}

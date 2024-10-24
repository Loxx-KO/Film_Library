using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ClientServices.Providers;
using API.Models.InputModels.Categories;
using API.Models.InputModels.Directors;
using API.Models.InputModels.Movies;
using API.Models.InputModels.Orders;
using API.Models.OutputModels.Categories;
using API.Models.OutputModels.Directors;
using API.Models.OutputModels.Movies;
using API.Models.OutputModels.Orders;
using AutoMapper;
using Entities.Models;

namespace API.App_Start
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            // Category Map
            CreateMap<CategoryInputModel, Category>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Movies, d => d.Ignore());
            CreateMap<Category, CategoryOutputModel>()
                .ForMember(x => x.MoviesNames, d => d.MapFrom(l => l.Movies.Select(p => p.Title).ToList()));

            // Director Map
            CreateMap<DirectorInputModel, Director>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Movies, d => d.Ignore());
            CreateMap<Director, DirectorOutputModel>()
                .ForMember(x => x.MoviesNames, d => d.MapFrom(m => m.Movies.Select(p => p.Title).ToList()));

            // Movie Map
            CreateMap<MovieInputModel, Movie>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Director, d => d.Ignore())
                .ForMember(x => x.Orders, d => d.Ignore())
                .ForMember(x => x.Categories, d => d.Ignore());
            CreateMap<Movie, MovieOutputModel>()
                .ForMember(x => x.CategoriesNames, d => d.MapFrom(c => c.Categories.Select(p => p.Name).ToList()))
                .ForMember(x => x.DirectorName, d => d.MapFrom(dir => $"{dir.Director.Name} {dir.Director.Surname}"));

            // Order Map
            CreateMap<OrderInputModel, Order>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Movies, d => d.Ignore())
                .ForMember(x => x.TotalPrice, d => d.Ignore());
            CreateMap<Order, OrderOutputModel>()
                .ForMember(x => x.MoviesNames, d => d.MapFrom(o => o.Movies.Select(m => m.Title)));
        }
    }
}
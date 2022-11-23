using Movie_API.Interfaces;
using Movie_Core.Dtos;
using Movie_Core.Interfaces;
using Movie_Core.Models;
using Movie_Domain.Services;

namespace Movie_API.ModulesEndpoint
{
    public class Endpoints : IModule
    {
        public IEndpointRouteBuilder MapEndpoints (IEndpointRouteBuilder endpoints)
        {

            #region User Authentication
            endpoints
                .MapPost
                (
                    "api/v1/userAuthentication/register",
                    async (IUserAuthenticationService _userAuth, RegisterDtoModel model) =>
                    {
                        var result = _userAuth.RegisterAsync(model);
                        if (result.Result.StatusCode == 1)
                        {
                            return Results.Ok(result.Result);
                        }
                        return Results.BadRequest(result.Result);
                    }
                    );

            endpoints
                .MapPost
                (
                    "api/v1/userAuthentication/login",
                    async (IUserAuthenticationService _userAuth,
                    LoginDtoModel model) =>
                    {
                        var result = _userAuth.LoginAsync(model);
                        if (result.Result.StatusCode == 1)
                        {
                            return  Results.Ok(result.Result);
                        }
                        return Results.BadRequest(result.Result);
                    }

                );

            #endregion

            #region Genre Endpoint
            endpoints
               .MapPost
               (
                   "api/v1/genre/add",
                   async (IGenreService _genre,
                   GenreDtoModel model) =>
                   {
                       var result = _genre.Add(model);

                       return Results.Ok(result.Result);
                   }

               );

            endpoints
             .MapPost
             (
                 "api/v1/genre/update",
                 async (IGenreService _genre,
                 GenreDtoModel model) =>
                 {
                     var result = _genre.Update(model);

                     return Results.Ok(result.Result);
                 }

             );

            endpoints
             .MapPost
             (
                 "api/v1/genre/delete",
                 async (IGenreService _genre,
                 int id) =>
                 {
                     var result = _genre.Delete(id);

                     return Results.Ok(result);
                 }

             );

            endpoints
             .MapGet
             (
                 "api/v1/genre/getById",
                 async (IGenreService _genre,
                 int id) =>
                 {
                     var result = _genre.GetById(id);

                     return Results.Ok(result.Result);
                 }

             );

            endpoints
             .MapGet
             (
                 "api/v1/genre/getAll",
                 async (IGenreService _genre) =>
                 {
                     var result = _genre.GetAll();

                     return Results.Ok(result.Result);
                 }

             );

            #endregion

            // #region Movie Enpoint

            endpoints
           .MapPost
           (
               "api/v1/movie/add",
               async (IMovieService _movie,
               MovieDtoModel model) =>
               {
                   var result = _movie.Add(model);

                   return Results.Ok(result.Result);
               }

            );

            endpoints
             .MapPost
             (
                 "api/v1/movie/update",
                 async (IMovieService _movie,
                 MovieDtoModel model) =>
                 {
                     var result = _movie.Update(model);

                     return Results.Ok(result.Result);
                 }

             );

            endpoints
             .MapPost
             (
                 "api/v1/movie/delete",
                 async (IMovieService _movie,
                 int id) =>
                 {
                     var result = _movie.Delete(id);

                     return Results.Ok(result);
                 }

             );

            endpoints
             .MapGet
             (
                 "api/v1/movie/getById",
                 async (IGenreService _movie,
                 int id) =>
                 {
                     var result = _movie.GetById(id);

                     return Results.Ok(result.Result);
                 }

             );

            endpoints
             .MapGet
             (
                 "api/v1/movie/getAll",
                 async (IMovieService _movie) =>
                 {
                     var result = _movie.GetAll();

                     return Results.Ok(result.Result);
                 }

             );




            // #endregion

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMovieService, MovieService>();
            return services;
        }
    }
}

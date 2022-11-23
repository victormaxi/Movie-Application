using Movie_Core.Dtos;
using Movie_Core.Interfaces;
using Movie_Core.Models;
using Movie_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Domain.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext context;

        public MovieService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Add(MovieDtoModel model)
        {
            try
            {
                var result = new Movie()
                {
                    ReleaseYear = model.ReleaseYear,
                    Cast = model.Cast,
                    Director = model.Director,
                    MovieImage = model.MovieImage,
                    Title = model.Title,
                };
                 context.Movies.Add(result);
                context.SaveChanges();
                return true;
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = context.Movies.Find(id);
                if (result != null)
                {
                    context.Movies.Remove(result);
                    context.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IQueryable<Movie>> GetAll()
        {
            try
            {
                var data = context.Movies.AsQueryable();
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Movie> GetById(int id)
        {
            var result = await context.Movies.FindAsync(id);
           
                return result;
         
        }

        public async Task<bool> Update(MovieDtoModel model)
        {
            try
            {
                var result = new Movie()
                {
                    ReleaseYear = model.ReleaseYear,
                    Cast = model.Cast,
                    Director = model.Director,
                    MovieImage = model.MovieImage,
                    Title = model.Title,
                };
                 context.Movies.Update(result);
                context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

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
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext context;
        public GenreService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Add(GenreDtoModel model)
        {
            try
            {
                var result = new Genre()
                {
                    GenreName = model.GenreName,
                };
                context.Genres.Add(result);
               await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = context.Genres.Find(id);

                if (data != null)
                {
                    context.Genres.Remove(data);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IQueryable<Genre>> GetAll()
        {
            try
            {
                var data = context.Genres.AsQueryable();
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Genre> GetById(int id)
        {
            try
            {
                var result = await context.Genres.FindAsync(id);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(GenreDtoModel model)
        {
            try
            {
                var result = new Genre()
                {
                    GenreName = model.GenreName,
                };
                context.Genres.Update(result);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

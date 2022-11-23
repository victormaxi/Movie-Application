using Movie_Core.Dtos;
using Movie_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Core.Interfaces
{
    public interface IMovieService
    {
        Task<bool> Add(MovieDtoModel model);
        Task<bool> Update(MovieDtoModel model);
        bool Delete(int id);
        Task<Movie> GetById(int id);
        Task<IQueryable<Movie>> GetAll();
    }
}

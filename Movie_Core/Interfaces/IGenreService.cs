using Movie_Core.Dtos;
using Movie_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Core.Interfaces
{
    public interface IGenreService
    {
        Task<bool> Add(GenreDtoModel model);
        Task<bool> Update(GenreDtoModel model);
        bool Delete(int id);
        Task<Genre> GetById(int id);
        Task<IQueryable<Genre>> GetAll();
    }
}

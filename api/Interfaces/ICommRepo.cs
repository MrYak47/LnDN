using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICommRepo
    {
        Task<List<Comment>> GetAllAysnc();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> CreateAsync(Comment ComMod);
        Task<Comment?> DeleteAsync(int id);
    
    }
}
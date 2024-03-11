using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.Repository
{
    public class CommRepo: ICommRepo
    {
        private readonly AppDBContext _context;
        public CommRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAysnc()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);

        }

        // public Task<Comment?> GetByIdAsync(int id)
        // {
        //     throw new NotImplementedException();
        // }

       
    
    }
}
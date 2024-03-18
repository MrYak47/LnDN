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

        public async Task<Comment?> CreateAsync(Comment ComMod)
        {
            await _context.Comments.AddAsync(ComMod);
            await _context.SaveChangesAsync();
            return ComMod;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comMod = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(comMod == null)
            {
                return null;

            }

            _context.Comments.Remove(comMod);
            await _context.SaveChangesAsync();
            return comMod;

        }

        public async Task<List<Comment>> GetAllAysnc()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);

        }

        public async Task<Comment?> UpdateAsync(int id, Comment ComMod)
        {
            var exCom = await _context.Comments.FindAsync(id);

            if (exCom == null)
            {
                return null;
            }

            exCom.Title = ComMod.Title;
            exCom.Content =ComMod.Content;
            
            await _context.SaveChangesAsync();

            return ComMod;

        }

        // public Task<Comment?> GetByIdAsync(int id)
        // {
        //     throw new NotImplementedException();
        // }



    }
}
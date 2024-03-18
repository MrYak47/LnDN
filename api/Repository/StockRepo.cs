using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Repository
{
    public class StockRepo : IStockRepo
    {
        
        private readonly AppDBContext _context;

        public StockRepo(AppDBContext context)
        {
            _context = context;
        }


        public async Task<Stock?> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }


        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;

            }

            _context.Stock.Remove(stockModel);

            await _context.SaveChangesAsync();

            return stockModel;        
        }


        public async Task<List<Stock>> GetAllAsync(QueryObj query)
        {
            var stock =  _context.Stock.Include(c => c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = query.IsDecsending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
            }

            var skipNum = (query.PageNum - 1) * query.PageSize;

            return await stock.Skip(skipNum).Take(query.PageSize).ToListAsync();

        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
           return await _context.Stock.Include(c=> c.Comments).FirstOrDefaultAsync(i=> i.Id == id);
        }


        public Task<bool> StockExists(int id)
        {
            return _context.Stock.AnyAsync(s => s.Id == id );
        }


        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x  => x.Id == id);


            if (stockModel == null)
            {
                return null;

            }

            stockModel.Symbol = stockDto.Symbol;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.MarketCap = stockDto.MarketCap;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;

           await _context.SaveChangesAsync();

            return stockModel;
        }
    }

}
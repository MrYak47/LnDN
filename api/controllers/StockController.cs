using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.DTO.Stock;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Interfaces;
using System.Net.Sockets;
namespace api.controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IStockRepo _iStockrepo;
        public StockController(AppDBContext context, IStockRepo iStockrepo)
        {
            _iStockrepo = iStockrepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _iStockrepo.GetAllAsync();
            var stckDto = stocks.Select(s => s.ToStockDto());
            
            return Ok(stocks);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _iStockrepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            } else {
                return Ok(stock.ToStockDto());

            }
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _iStockrepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id= stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            
            var stockModel = await _iStockrepo.UpdateAsync(id, updateDto);
            
            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {  
            var stockModel = await _iStockrepo.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
            
        }




    }
}
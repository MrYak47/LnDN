using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using api.DTO;
using api.DTO.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommController: ControllerBase
    {
        private readonly ICommRepo _commRepo;
        private readonly IStockRepo _istockRepo;

        public CommController(ICommRepo commRepo, IStockRepo istockRepo)
        {
            _commRepo = commRepo;
            _istockRepo = istockRepo;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var comments = await _commRepo.GetAllAysnc();
            var commDto = comments.Select(s => s.ToCommDto());
            
            return Ok(commDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommDto());
        }
        
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId , CreateCommDto createCom)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _istockRepo.StockExists(stockId))
            {
                return BadRequest("Stock doesnt exsit");
            }

            var comMod = createCom.ToCommFromCreate(stockId);
            await _commRepo.CreateAsync(comMod);

            return CreatedAtAction(nameof(GetByID), new {id= comMod.Id}, comMod.ToCommDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comMod = await _commRepo.DeleteAsync(id);

            if(comMod == null)
            {
                return NotFound("Comment doest not exist");
            }

            return Ok(comMod);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommReqDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commRepo.UpdateAsync(id, updateDto.ToCommFromUpdate());

            if(comment == null)
            {
                return NotFound("Comment not Found");
            }

            return Ok(comment.ToCommDto());

        }



    }
}
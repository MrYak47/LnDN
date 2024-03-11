using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using api.DTO;
using api.DTO.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommController: ControllerBase
    {
        private readonly ICommRepo _commRepo;

        public CommController(ICommRepo commRepo)
        {
            _commRepo = commRepo;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var comments = await _commRepo.GetAllAysnc();
            var commDto = comments.Select(s => s.ToCommDto());
            
            return Ok(commDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var comment = await _commRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommDto());
        }
        
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId , CreateCommDto createCom)
        {
            
        }




    }
}
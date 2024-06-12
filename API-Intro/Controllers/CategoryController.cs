using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Intro.Data;
using API_Intro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Intro.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var data = await _context.Categories.FindAsync(id);
            if (data is null) return NotFound();
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int ? id)
        {
            if (id is null) return BadRequest();

            var data = await _context.Categories.FindAsync(id);

            if (data is null) NotFound();

            _context.Remove(data);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return CreatedAtAction("Create", category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var data = await _context.Categories.FindAsync(id);

            if (data is null) return NotFound();

            data.Name = category.Name;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string ? keyWord)
        {
            return Ok(keyWord==null ? await _context.Categories.ToListAsync(): await _context.Categories.Where(m => m.Name.Contains(keyWord)).ToListAsync());
        }
    }
}


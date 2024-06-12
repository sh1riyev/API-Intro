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
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Book", book);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _context.Books.FindAsync(id);
            if (data is null) return NotFound();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest();
            var data = await _context.Books.FindAsync(id);
            if (data is null) return NotFound();
            _context.Books.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest();
            var data = await _context.Books.FindAsync(id);
            if (data is null) return NotFound();
            data.Name = book.Name;
            data.PageCount = book.PageCount;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string? keyWord)
        {
            return Ok(keyWord == null ? await _context.Books.ToListAsync() : await _context.Books.Where(m => m.Name.Contains(keyWord)).ToListAsync());
        }
    }
}


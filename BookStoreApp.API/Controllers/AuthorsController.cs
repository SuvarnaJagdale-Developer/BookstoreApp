using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Authors;
using AutoMapper;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorsController(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadonlyDto>>> GetAuthors()
        {
            var authors = _mapper.Map<IEnumerable<AuthorReadonlyDto>>(await _context.Authors.ToListAsync());
            //if (_context.Authors == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Authors.ToListAsync();
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadonlyDto>> GetAuthor(int id)
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            var author = await _context.Authors.FindAsync(id);
            var authorDetails=_mapper.Map<AuthorReadonlyDto>(author);

            if (author == null)
            {
                return NotFound();
            }

            return authorDetails;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorupdate)
        {
            if (id != authorupdate.Id)
            {
                return BadRequest();
            }
            var getAuthor=_context.Authors.Find(id);
            if(getAuthor == null)
            {
                return NotFound();
            }    
             _mapper.Map(authorupdate,getAuthor);
            _context.Entry(getAuthor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorsCreateDto>> PostAuthor(AuthorsCreateDto authorDTO)
        {
            var author=_mapper.Map<Author>(authorDTO);
            //var author = new Author
            //{
            //    Bio=authorDTO.Bio,
            //    FirstName=authorDTO.FirstName,
            //    LastName=authorDTO.LastName,

            //};
                
          if (_context.Authors == null)
          {
              return Problem("Entity set 'BookStoreDbContext.Authors'  is null.");
          }
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Books;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper.Configuration;


namespace BookStoreApp.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext option,IMapper mapper)
        {
            _dbContext = option;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>>GetAllBook()
        {
            //await _dbContext.Get
            var books=await _dbContext.Books
                            .Include(q=>q.Author)
                            .ProjectTo<BookReadOnlyDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
            //var bookDto=_mapper.Map<IEnumerable<BookReadOnlyDto>>(books);
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailDto>> GetBook(int id)
        {
            var book=await _dbContext.Books
                .Include(q=>q.Author)
                .ProjectTo<BookDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q=>q.Id==id);

            //if(_dbContext==null)
            //{
            //    return NotFound();
            //}
            //var book=await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return BadRequest();
            }
            return book;

        }

        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateBook(int id, BookUpdateDTO bookdto)
        {
            if(_dbContext==null)
            {
                return NotFound();
            }
            if(id !=bookdto.Id)
            {
                return BadRequest();
            }
            var books = await _dbContext.Books.FindAsync(id);
            if(books == null)
            {
                return NotFound();
            }
            _mapper.Map(bookdto, books); 
            _dbContext.Entry(books).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;

            }
           
            return NoContent();

        }


        [HttpPost]
        public async Task<ActionResult<BooksCreateData>>AddBook(BooksCreateData bookData)
        {
            var book = _mapper.Map<Book>(bookData);
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            //var book = new Book
            //{
            //    Title = bookData.Title,
            //    Year = bookData.Year,
            //    Isbn = bookData.Isbn,
            //    Summary = bookData.Summary,
            //    Image = bookData.Image,
            //    Price = bookData.Price,
            //    AuthorId = bookData.AuthorId,
            //   // Author = bookData.Author,
            //};
            
            //if(_dbContext==null)
            //{
            //    return BadRequest();
            //}
            //_dbContext.Books.Add(book);

            //await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id },book);

        }

    }
}

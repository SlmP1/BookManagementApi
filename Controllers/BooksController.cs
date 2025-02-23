using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookManagementApi.Models.DTO;
using BookManagementApi.Repositories;
using BookManagementApi.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagementApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateAsync([FromBody] AddBookRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _bookRepository.ExistsByTitleAsync(request.Title))
            {
                return BadRequest("A book with this title already exists.");
            }

            var createdBook = await _bookRepository.CreateAsync(request);

            var bookDto = _mapper.Map<BookDto>(createdBook);  // Convert domain model to DTO
            return Created($"/api/Books/{bookDto.Title}", bookDto);
        }

        
        [HttpPost("bulk")]
        public async Task<ActionResult<IEnumerable<BookDto>>> CreateBulkAsync([FromBody] List<AddBookRequestDto> requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var titles = requests.Select(r => r.Title).ToList();
            if (titles.Count != titles.Distinct().Count())
            {
                return BadRequest("Duplicate titles found in the request.");
            }

            var existingTitles = new List<string>();
            foreach (var title in titles)
            {
                if (await _bookRepository.ExistsByTitleAsync(title))
                {
                    existingTitles.Add(title);
                }
            }

            if (existingTitles.Any())
            {
                return BadRequest($"Books with the following titles already exist: {string.Join(", ", existingTitles)}");
            }

           
            var createdBooks = await _bookRepository.CreateBulkAsync(requests);
            var createdBooksDto = _mapper.Map<List<BookDto>>(createdBooks);

            return Created("/api/Books/bulk", createdBooksDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAllAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be positive numbers.");
            }

            var books = await _bookRepository.GetAllAsync(pageNumber, pageSize);
           
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateAsync(int id, [FromBody] UpdateBookRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _bookRepository.ExistsByTitleAsync(request.Title))
            {
                return BadRequest("A book with this title already exists.");
            }

            
            var updatedBook = await _bookRepository.UpdateAsync(id, request);

            if (updatedBook == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            var updatedBookDto = _mapper.Map<BookDto>(updatedBook);
            return Ok(updatedBookDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _bookRepository.DeleteAsync(id);

            if (!result)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("bulk")]
        public async Task<ActionResult> DeleteBulkAsync([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("No IDs provided for deletion.");
            }

            var result = await _bookRepository.DeleteBulkAsync(ids);

            if (!result.Any())
            {
                return Ok("All specified books were already deleted or not found.");
            }

            return Ok($"Successfully deleted {result.Count()} books.");
        }
    }
}

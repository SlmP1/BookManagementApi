using Microsoft.EntityFrameworkCore;
using BookManagementApi.Data;
using BookManagementApi.Models;
using BookManagementApi.Models.DTO;
using BookManagementApi.Models.Domain;

namespace BookManagementApi.Repositories
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly BookManagementDbContext _context;

        public SQLBookRepository(BookManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(AddBookRequestDto request)
        {
            var book = new Book
            {
                Title = request.Title,
                AuthorName = request.AuthorName,
                PublicationYear = request.PublicationYear,
                ViewsCount = 0,
                IsDeleted = false
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<string>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Books
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.ViewsCount) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => b.Title) 
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (book != null)
            {
                book.ViewsCount++;
                await _context.SaveChangesAsync();
            }

            return book;
        }

        public async Task<Book?> UpdateAsync(int id, UpdateBookRequestDto request)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (book == null) return null;

            book.Title = request.Title;
            book.AuthorName = request.AuthorName;
            book.PublicationYear = request.PublicationYear;
            book.ViewsCount = 0;
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (book == null) return false;

            book.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByTitleAsync(string title)
        {
            return await _context.Books
                .AnyAsync(b => b.Title.ToLower() == title.ToLower() && !b.IsDeleted);
        }

        public async Task<IEnumerable<Book>> CreateBulkAsync(IEnumerable<AddBookRequestDto> requests)
        {
            // Create a list to store the new book entities
            var books = new List<Book>();

            foreach (var request in requests)
            {
                var book = new Book
                {
                    Title = request.Title,
                    AuthorName = request.AuthorName,
                    PublicationYear = request.PublicationYear,
                    ViewsCount = 0,
                    IsDeleted = false
                };
                books.Add(book);
            }

            // Add the range of books to the context
            await _context.Books.AddRangeAsync(books);

            // Save all changes in a single transaction
            await _context.SaveChangesAsync();

            return books;
        }
        public async Task<IEnumerable<int>> DeleteBulkAsync(IEnumerable<int> ids)
        {
            var booksToDelete = await _context.Books
                .Where(b => ids.Contains(b.Id) && !b.IsDeleted)
                .ToListAsync();

            if (!booksToDelete.Any())
            {
                return Enumerable.Empty<int>();
            }

            // Soft delete - update IsDeleted flag
            foreach (var book in booksToDelete)
            {
                book.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            return booksToDelete.Select(b => b.Id);
        }
        
    }
}
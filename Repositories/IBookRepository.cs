using BookManagementApi.Models.Domain;
using BookManagementApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementApi.Repositories
{
    public interface IBookRepository
    {
        Task<Book> CreateAsync(AddBookRequestDto request);
        Task<IEnumerable<string>> GetAllAsync(int pageNumber, int pageSize);
        Task<Book> GetByIdAsync(int id);
        Task<Book?> UpdateAsync(int id, UpdateBookRequestDto request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByTitleAsync(string title);
        Task<IEnumerable<Book>> CreateBulkAsync(IEnumerable<AddBookRequestDto> requests);

        Task<IEnumerable<int>> DeleteBulkAsync(IEnumerable<int> ids);
    }
}


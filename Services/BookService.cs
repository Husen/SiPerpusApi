using Microsoft.EntityFrameworkCore;
using SiPerpusApi.Dto.RackDto;
using SiPerpusApi.Dto.ViewModel;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Helpers;
using SiPerpusApi.Models;
using SiPerpusApi.Repositories;

namespace SiPerpusApi.Services;

public class BookService : IBookService
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Publisher> _publisherRepository;
    private readonly IRepository<Rack> _rackRepository;
    private readonly IPersistence _persistence;
    private readonly AppDbContext _dbContext;

    public BookService(IRepository<Book> bookRepository, IRepository<Category> categoryRepository,
        IRepository<Publisher> publisherRepository, IRepository<Rack> rackRepository, IPersistence persistence,
        AppDbContext dbContext)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
        _publisherRepository = publisherRepository;
        _rackRepository = rackRepository;
        _persistence = persistence;
        _dbContext = dbContext;
    }

    public BookResponse CreateBook(BookRequest bookRequest)
    {
        // category get and validate :
        var category = _categoryRepository.FindById(bookRequest.CategoryId);
        if (category is null) throw new NotFoundException("category id not found");

        // publisher get and validate :
        var publisher = _publisherRepository.FindById(bookRequest.PublisherId);
        if (publisher is null) throw new NotFoundException("publisher id not found");

        // rack get and validate :
        var rack = _rackRepository.FindById(bookRequest.RackId);
        if (rack is null) throw new NotFoundException("rack id not found");

        try
        {
            var book = new Book()
            {
                CodeBook = bookRequest.CodeBook,
                NameBook = bookRequest.NameBook,
                CategoryId = bookRequest.CategoryId,
                PublisherId = bookRequest.PublisherId,
                RackId = bookRequest.RackId,
                Pengarang = bookRequest.Pengarang,
                ISBN = bookRequest.ISBN,
                PageBook = bookRequest.PageBook,
                YearBook = bookRequest.YearBook,
                Stock = bookRequest.Stock
            };
            var newBook = _bookRepository.Save(book);
            _persistence.SaveChanges();

            var responseCategory = new BookResponse()
            {
                Id = newBook.Id,
                CodeBook = bookRequest.CodeBook,
                NameBook = bookRequest.NameBook,
                Category = category,
                Publisher = publisher,
                Rack = rack,
                Pengarang = bookRequest.Pengarang,
                ISBN = bookRequest.ISBN,
                PageBook = bookRequest.PageBook,
                YearBook = bookRequest.YearBook,
                Stock = bookRequest.Stock,
                CreatedAt = newBook.CreatedAt,
                UpdatedAt = newBook.UpdatedAt
            };
            return responseCategory;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed to create book");
        }
    }

    public BookResponse GetById(int id)
    {
        var book = _bookRepository.FindById(id);
        if (book is null) throw new NotFoundException("id not found");

        // -- include relation data
        _dbContext.Entry(book)
            .Reference(b => b.Category)
            .Load();

        _dbContext.Entry(book)
            .Reference(b => b.Publisher)
            .Load();

        _dbContext.Entry(book)
            .Reference(b => b.Rack)
            .Load();
        // --

        var bookResponse = new BookResponse()
        {
            Id = book.Id,
            CodeBook = book.CodeBook,
            NameBook = book.NameBook,
            Category = book.Category,
            Publisher = book.Publisher,
            Rack = book.Rack,
            Pengarang = book.Pengarang,
            ISBN = book.ISBN,
            PageBook = book.PageBook,
            YearBook = book.YearBook,
            Stock = book.Stock,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
        return bookResponse;
    }

    public ApiPaginationResponse<List<BookResponse>> GetAll(RequestPagination requestPagination)
    {
        try
        {
            IQueryable<Book> query = _bookRepository.FindAll();

            if (!string.IsNullOrWhiteSpace(requestPagination.SearchQuery))
            {
                var searchQueryLower = requestPagination.SearchQuery.ToLower();
                query = query.Where(b =>
                    b.NameBook.ToLower().Contains(searchQueryLower) ||
                    b.CodeBook.ToLower().Contains(searchQueryLower) ||
                    b.Pengarang.ToLower().Contains(searchQueryLower) ||
                    b.ISBN.Contains(searchQueryLower) ||
                    b.Category.NameCategory.ToLower().Contains(searchQueryLower) ||
                    b.Rack.CodeRack.ToLower().Contains(searchQueryLower) ||
                    b.Publisher.NamePublisher.ToLower().Contains(searchQueryLower)
                );
            }

            if (!string.IsNullOrWhiteSpace(requestPagination.SortBy))
            {
                query = PaginationHelper.ApplySorting(query, requestPagination.SortBy, requestPagination.SortDirection);
            }

            // Include Category, Publisher, dan Rack
            query = query
                .Include(book => book.Category)
                .Include(book => book.Publisher)
                .Include(book => book.Rack);

            var totalRecords = query.Count();

            var skip = (requestPagination.Page - 1) * requestPagination.Limit;
            query = query.Skip((int)skip).Take(requestPagination.Limit.Value);

            var result = query.Select(book => new BookResponse()
            {
                Id = book.Id,
                CodeBook = book.CodeBook,
                NameBook = book.NameBook,
                Category = book.Category,
                Publisher = book.Publisher,
                Rack = book.Rack,
                Pengarang = book.Pengarang,
                ISBN = book.ISBN,
                PageBook = book.PageBook,
                YearBook = book.YearBook,
                Stock = book.Stock,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            }).ToList();

            var totalPages = (int)Math.Ceiling((double)totalRecords / requestPagination.Limit.Value);

            var apiPaginationResponse = new ApiPaginationResponse<List<BookResponse>>
            {
                Page = requestPagination.Page.Value,
                Limit = requestPagination.Limit.Value,
                TotalRows = totalRecords,
                TotalPage = totalPages,
                Data = result
            };

            return apiPaginationResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed to get all book");
        }
    }

    public BookResponse UpdateBook(int id, BookRequest bookRequest)
    {
        var book = _bookRepository.FindById(id);
        if (book is null) throw new NotFoundException("id not found");
        book.CodeBook = bookRequest.CodeBook;
        book.NameBook = bookRequest.NameBook;
        book.CategoryId = bookRequest.CategoryId;
        book.PublisherId = bookRequest.PublisherId;
        book.RackId = bookRequest.RackId;
        book.Pengarang = bookRequest.Pengarang;
        book.ISBN = bookRequest.ISBN;
        book.PageBook = bookRequest.PageBook;
        book.YearBook = bookRequest.YearBook;
        book.Stock = bookRequest.Stock;
        book.UpdatedAt = DateTime.UtcNow;
        
        _bookRepository.Update(book);
        _persistence.SaveChanges();
        var newBook = _bookRepository.FindById(id);
        
        // -- include relation data
        _dbContext.Entry(newBook)
            .Reference(b => b.Category)
            .Load();

        _dbContext.Entry(newBook)
            .Reference(b => b.Publisher)
            .Load();

        _dbContext.Entry(newBook)
            .Reference(b => b.Rack)
            .Load();
        // --

        var bookResponse = new BookResponse()
        {
            Id = newBook.Id,
            CodeBook = newBook.CodeBook,
            NameBook = newBook.NameBook,
            Category = newBook.Category,
            Publisher = newBook.Publisher,
            Rack = newBook.Rack,
            Pengarang = newBook.Pengarang,
            ISBN = newBook.ISBN,
            PageBook = newBook.PageBook,
            YearBook = newBook.YearBook,
            Stock = newBook.Stock,
            CreatedAt = newBook.CreatedAt,
            UpdatedAt = newBook.UpdatedAt
        };
        return bookResponse;
    }

    public void DeleteBook(int id)
    {
        var book = _bookRepository.FindById(id);
        if (book is null) throw new NotFoundException("id not found");
        _bookRepository.Delete(book);
        _persistence.SaveChanges();
    }
}
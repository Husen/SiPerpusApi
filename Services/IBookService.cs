using SiPerpusApi.Dto.RackDto;
using SiPerpusApi.Dto.ViewModel;

namespace SiPerpusApi.Services;

public interface IBookService
{
    BookResponse CreateBook(BookRequest bookRequest);
    BookResponse GetById(int id);
    ApiPaginationResponse<List<BookResponse>> GetAll(RequestPagination requestPagination);
    BookResponse UpdateBook(int id, BookRequest bookRequest);
    void DeleteBook(int id);
}
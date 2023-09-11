using SiPerpusApi.Dto;

namespace SiPerpusApi.Services;

public interface ILoanService
{
    LoanResponse CreateLoan(CreateLoanRequest createLoanRequest);
    LoanResponse GetById(int id);
    ApiPaginationResponse<List<LoanResponse>> GetAll(RequestPagination requestPagination);
    LoanResponse ReturnedLoan(int id, ReturnedLoanRequest returnedLoanRequest);
}
using SiPerpusApi.Dto;
using SiPerpusApi.Models;
using SiPerpusApi.Repositories;

namespace SiPerpusApi.Services;

public class LoanService : ILoanService
{
    private readonly IRepository<Loan> _loanRepository;
    private readonly IRepository<LoanDetail> _loanDetailRepository;
    private readonly IPersistence _persistence;
    private readonly AppDbContext _dbContext;

    public LoanService(IRepository<Loan> loanRepository, IRepository<LoanDetail> loanDetailRepository, IPersistence persistence, AppDbContext dbContext)
    {
        _loanRepository = loanRepository;
        _loanDetailRepository = loanDetailRepository;
        _persistence = persistence;
        _dbContext = dbContext;
    }

    public LoanResponse CreateLoan(CreateLoanRequest createLoanRequest)
    {
        throw new NotImplementedException();
    }

    public LoanResponse GetById(int id)
    {
        throw new NotImplementedException();
    }

    public ApiPaginationResponse<List<LoanResponse>> GetAll(RequestPagination requestPagination)
    {
        throw new NotImplementedException();
    }

    public LoanResponse ReturnedLoan(int id, ReturnedLoanRequest returnedLoanRequest)
    {
        throw new NotImplementedException();
    }
}
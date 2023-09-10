using SiPerpusApi.Dto.RackDto;
using SiPerpusApi.Dto.ViewModel;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Helpers;
using SiPerpusApi.Models;
using SiPerpusApi.Repositories;

namespace SiPerpusApi.Services;

public class RackService : IRackService
{
    private readonly IRepository<Rack> _repository;
    private readonly IPersistence _persistence;

    public RackService(IRepository<Rack> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public RackResponse CreateRack(RackRequest rackRequest)
    {
        try
        {
            var rack = new Rack()
            {
                CodeRack = rackRequest.CodeRack
            };
            var newRack = _repository.Save(rack);
            _persistence.SaveChanges();

            var responseRack = new RackResponse()
            {
                Id = newRack.Id,
                CodeRack = newRack.CodeRack,
                CreatedAt = newRack.CreatedAt,
                UpdatedAt = newRack.UpdatedAt
            };
            return responseRack;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed to create rack");
        }
    }

    public RackResponse GetById(int id)
    {
        var rack = _repository.FindById(id);
        if (rack is null) throw new NotFoundException("id not found");
        var responseRack = new RackResponse()
        {
            Id = rack.Id,
            CodeRack = rack.CodeRack,
            CreatedAt = rack.CreatedAt,
            UpdatedAt = rack.UpdatedAt
        };
        return responseRack;
    }

    public ApiPaginationResponse<List<RackResponse>> GetAll(RequestPagination requestPagination)
    {
        try
        {
            IQueryable<Rack> query = _repository.FindAll();

            if (!string.IsNullOrWhiteSpace(requestPagination.SearchQuery))
            {
                var searchQueryLower = requestPagination.SearchQuery.ToLower();
                query = query.Where(p => p.CodeRack.ToLower().Contains(searchQueryLower));
            }

            if (!string.IsNullOrWhiteSpace(requestPagination.SortBy))
            {
                query = PaginationHelper.ApplySorting(query, requestPagination.SortBy, requestPagination.SortDirection);
            }

            var totalRecords = query.Count();

            var skip = (requestPagination.Page - 1) * requestPagination.Limit;
            query = query.Skip((int)skip).Take(requestPagination.Limit.Value);

            var result = query.Select(c => new RackResponse()
            {
                Id = c.Id,
                CodeRack = c.CodeRack,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            var totalPages = (int)Math.Ceiling((double)totalRecords / requestPagination.Limit.Value);

            var apiPaginationResponse = new ApiPaginationResponse<List<RackResponse>>
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
            throw new Exception("Failed to get all rack");
        }
    }

    public RackResponse UpdateRack(int id, RackRequest rackRequest)
    {
        var rack = _repository.FindById(id);
        if (rack is null) throw new NotFoundException("id not found");
        rack.CodeRack = rackRequest.CodeRack;
        rack.UpdatedAt = DateTime.UtcNow;
            
        _repository.Update(rack);
        _persistence.SaveChanges();
        var newRack = _repository.FindById(id);

        var responseRack = new RackResponse()
        {
            Id = newRack.Id,
            CodeRack = newRack.CodeRack,
            CreatedAt = newRack.CreatedAt,
            UpdatedAt = newRack.UpdatedAt
        };
        return responseRack;
    }

    public void DeleteRack(int id)
    {
        var rack = _repository.FindById(id);
        if (rack is null) throw new NotFoundException("id not found");
        _repository.Delete(rack);
        _persistence.SaveChanges();
    }
}
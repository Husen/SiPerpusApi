using SiPerpusApi.Dto.PublisherDto;
using SiPerpusApi.Dto.ViewModel;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Helpers;
using SiPerpusApi.Models;
using SiPerpusApi.Repositories;

namespace SiPerpusApi.Services;

public class PublisherService : IPublisherService
{
    private readonly IRepository<Publisher> _repository;
    private readonly IPersistence _persistence;

    public PublisherService(IRepository<Publisher> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public PublisherResponse CreatePublisher(PublisherRequest publisherRequest)
    {
        try
        {
            var publisher = new Publisher()
            {
                NamePublisher = publisherRequest.NamePublisher
            };
            var newPublisher = _repository.Save(publisher);
            _persistence.SaveChanges();

            var responsePublisher = new PublisherResponse()
            {
                Id = newPublisher.Id,
                NamePublisher = newPublisher.NamePublisher,
                CreatedAt = newPublisher.CreatedAt,
                UpdatedAt = newPublisher.UpdatedAt
            };
            return responsePublisher;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed to create publisher");
        }
    }

    public PublisherResponse GetById(int id)
    {
        var publisher = _repository.FindById(id);
        if (publisher is null) throw new NotFoundException("id not found");
        var responsePublisher = new PublisherResponse()
        {
            Id = publisher.Id,
            NamePublisher = publisher.NamePublisher,
            CreatedAt = publisher.CreatedAt,
            UpdatedAt = publisher.UpdatedAt
        };
        return responsePublisher;
    }

    public ApiPaginationResponse<List<PublisherResponse>> GetAll(RequestPagination requestPagination)
    {
        try
        {
            IQueryable<Publisher> query = _repository.FindAll();

            if (!string.IsNullOrWhiteSpace(requestPagination.SearchQuery))
            {
                var searchQueryLower = requestPagination.SearchQuery.ToLower();
                query = query.Where(p => p.NamePublisher.ToLower().Contains(searchQueryLower));
            }

            if (!string.IsNullOrWhiteSpace(requestPagination.SortBy))
            {
                query = PaginationHelper.ApplySorting(query, requestPagination.SortBy, requestPagination.SortDirection);
            }

            var totalRecords = query.Count();

            var skip = (requestPagination.Page - 1) * requestPagination.Limit;
            query = query.Skip((int)skip).Take(requestPagination.Limit.Value);

            var result = query.Select(c => new PublisherResponse()
            {
                Id = c.Id,
                NamePublisher = c.NamePublisher,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            var totalPages = (int)Math.Ceiling((double)totalRecords / requestPagination.Limit.Value);

            var apiPaginationResponse = new ApiPaginationResponse<List<PublisherResponse>>
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
            throw new Exception("Failed to get all publisher");
        }
    }

    public PublisherResponse UpdatePublisher(int id, PublisherRequest publisherRequest)
    {
        var publisher = _repository.FindById(id);
        if (publisher is null) throw new NotFoundException("id not found");
        publisher.NamePublisher = publisherRequest.NamePublisher;
        publisher.UpdatedAt = DateTime.UtcNow;
            
        _repository.Update(publisher);
        _persistence.SaveChanges();
        var newPublisher = _repository.FindById(id);

        var responsePublisher = new PublisherResponse()
        {
            Id = newPublisher.Id,
            NamePublisher = newPublisher.NamePublisher,
            CreatedAt = newPublisher.CreatedAt,
            UpdatedAt = newPublisher.UpdatedAt
        };
        return responsePublisher;
    }

    public void DeletePublisher(int id)
    {
        var publisher = _repository.FindById(id);
        if (publisher is null) throw new NotFoundException("id not found");
        _repository.Delete(publisher);
        _persistence.SaveChanges();
    }
}
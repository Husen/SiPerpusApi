using SiPerpusApi.Dto;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Helpers;
using SiPerpusApi.Models;
using SiPerpusApi.Repositories;

namespace SiPerpusApi.Services;

public class MemberService : IMemberService
{
    private readonly IRepository<Member> _repository;
    private readonly IPersistence _persistence;

    public MemberService(IRepository<Member> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public MemberResponse CreateMember(MemberRequest memberRequest)
    {
        try
        {
            var member = new Member()
            {
                FullName = memberRequest.FullName,
                Email = memberRequest.Email,
                PhoneNumber = memberRequest.PhoneNumber
            };
            var newMember = _repository.Save(member);
            _persistence.SaveChanges();

            var responseMember = new MemberResponse()
            {
                Id = newMember.Id,
                FullName = newMember.FullName,
                Email = newMember.Email,
                PhoneNumber = newMember.PhoneNumber,
                CreatedAt = newMember.CreatedAt,
                UpdatedAt = newMember.UpdatedAt
            };
            return responseMember;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed to create member");
        }
    }

    public MemberResponse GetById(int id)
    {
        var member = _repository.FindById(id);
        if (member is null) throw new NotFoundException("id not found");
        var responseMember = new MemberResponse()
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber,
            CreatedAt = member.CreatedAt,
            UpdatedAt = member.UpdatedAt
        };
        return responseMember;
    }

    public ApiPaginationResponse<List<MemberResponse>> GetAll(RequestPagination requestPagination)
    {
        try
        {
            IQueryable<Member> query = _repository.FindAll();

            if (!string.IsNullOrWhiteSpace(requestPagination.SearchQuery))
            {
                var searchQueryLower = requestPagination.SearchQuery.ToLower();
                query = query.Where(m => 
                    m.FullName.ToLower().Contains(searchQueryLower) ||
                    m.Email.ToLower().Contains(searchQueryLower) ||
                    m.PhoneNumber.Contains(searchQueryLower)
                );
            }

            if (!string.IsNullOrWhiteSpace(requestPagination.SortBy))
            {
                query = PaginationHelper.ApplySorting(query, requestPagination.SortBy, requestPagination.SortDirection);
            }

            var totalRecords = query.Count();

            var skip = (requestPagination.Page - 1) * requestPagination.Limit.Value;
            query = query.Skip((int)skip).Take(requestPagination.Limit.Value);

            var result = query.Select(m => new MemberResponse()
            {
                Id = m.Id,
                FullName = m.FullName,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            }).ToList();

            var totalPages = (int)Math.Ceiling((double)totalRecords / requestPagination.Limit.Value);

            var apiPaginationResponse = new ApiPaginationResponse<List<MemberResponse>>
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
            throw new Exception("Failed to get all categories");
        }
    }

    public MemberResponse UpdateMember(int id, MemberRequest memberRequest)
    {
        var member = _repository.FindById(id);
        if (member is null) throw new NotFoundException("id not found");
        member.FullName = memberRequest.FullName;
        member.Email = memberRequest.Email;
        member.PhoneNumber = memberRequest.PhoneNumber;
        member.UpdatedAt = DateTime.UtcNow;
            
        _repository.Update(member);
        _persistence.SaveChanges();
        var newCategory = _repository.FindById(id);

        var responseMember = new MemberResponse()
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber,
            CreatedAt = member.CreatedAt,
            UpdatedAt = member.UpdatedAt
        };
        return responseMember;
    }

    public void DeleteMember(int id)
    {
        var member = _repository.FindById(id);
        if (member is null) throw new NotFoundException("id not found");
        _repository.Delete(member);
        _persistence.SaveChanges();
    }
}
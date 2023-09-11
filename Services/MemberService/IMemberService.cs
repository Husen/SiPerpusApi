using SiPerpusApi.Dto;

namespace SiPerpusApi.Services;

public interface IMemberService
{
    MemberResponse CreateMember(MemberRequest memberRequest);
    MemberResponse GetById(int id);
    ApiPaginationResponse<List<MemberResponse>> GetAll(RequestPagination requestPagination);
    MemberResponse UpdateMember(int id, MemberRequest memberRequest);
    void DeleteMember(int id);
}
using SiPerpusApi.Dto;

namespace SiPerpusApi.Services;

public interface IRackService
{
    RackResponse CreateRack(RackRequest rackRequest);
    RackResponse GetById(int id);
    ApiPaginationResponse<List<RackResponse>> GetAll(RequestPagination requestPagination);
    RackResponse UpdateRack(int id, RackRequest rackRequest);
    void DeleteRack(int id);
}
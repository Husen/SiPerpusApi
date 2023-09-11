using SiPerpusApi.Dto;

namespace SiPerpusApi.Services;

public interface IPublisherService
{
    PublisherResponse CreatePublisher(PublisherRequest publisherRequest);
    PublisherResponse GetById(int id);
    ApiPaginationResponse<List<PublisherResponse>> GetAll(RequestPagination requestPagination);
    PublisherResponse UpdatePublisher(int id, PublisherRequest publisherRequest);
    void DeletePublisher(int id);
}
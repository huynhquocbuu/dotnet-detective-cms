using Cms.Application.Public.Models;

namespace Cms.Application.Public.Interfaces;

public interface IHomeUseCase
{
    HomeInfoDto GetAboutUsInfo();
    List<HomeInfoDto> GetServicesInfo();
    List<HomeInfoDto> GetPostsInfo();
    
}
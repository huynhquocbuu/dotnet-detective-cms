using Cms.Application.Admin.Models.Post;

namespace Cms.Application.Public.Interfaces;

public interface IPostPublicUseCase
{
    PostDetailDto GetPostDetail(string slug);
}
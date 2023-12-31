using Cms.Application.Admin.Models.Post;

namespace Cms.Application.Admin.Interfaces;

public interface IPostUseCase
{
    Task<List<PostDto>> GetAllAsync();
    Task<PostDto> GetNewPostDto();
    Task<PostDto> GetEditPostDto(long id);
    Task<int> DoEdit(PostDto model);
    Task AddPost(PostDto dto, string authorId);
}
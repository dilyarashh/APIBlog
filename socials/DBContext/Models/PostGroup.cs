using socials.DBContext.DTO.Post;

namespace socials.DBContext.Models;

public class PostGroup
{
    public List<PostDTO> Posts { get; set; }
    public PageInfoModel Pagination { get; set; }
}
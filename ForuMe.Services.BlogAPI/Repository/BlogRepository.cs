using AutoMapper;
using ForuMe.Services.BlogAPI.DbContexts;
using ForuMe.Services.BlogAPI.Models;
using ForuMe.Services.BlogAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ForuMe.Services.BlogAPI.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public BlogRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<BlogDto> CreateUpdateBlog(BlogDto blogDto)
        {
            var blog = _mapper.Map<Blog>(blogDto);
            if (blog.Id > 0)
            {
                _db.Blogs.Update(blog);
            }
            else
            {
                _db.Blogs.Add(blog);
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<BlogDto>(blog);
        }

        public async Task<bool> DeleteBlog(int blogId)
        {
            try
            {
                var blog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == blogId);
                if (blog == null)
                {
                    return false;
                }
                _db.Blogs.Remove(blog);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public async Task<BlogDto> GetBlogById(int blogId)
        {
            var blog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == blogId);
            return _mapper.Map<BlogDto>(blog);
        }

        public async Task<IEnumerable<BlogDto>> GetBlogs()
        {
            var blogs = await _db.Blogs.ToListAsync();
            return _mapper.Map<List<BlogDto>>(blogs);
        }
    }
}

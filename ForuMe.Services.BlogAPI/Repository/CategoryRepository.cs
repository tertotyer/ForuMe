using AutoMapper;
using ForuMe.Services.BlogAPI.DbContexts;
using ForuMe.Services.BlogAPI.Models;
using ForuMe.Services.BlogAPI.Models.Dtos;
using ForuMe.Services.BlogAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ForuMe.Services.BlogAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateUpdateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            if (category.Id > 0)
            {
                _db.Categories.Update(category);
            }
            else
            {
                _db.Categories.Add(category);
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
                if (category == null)
                {
                    return false;
                }
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _db.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}

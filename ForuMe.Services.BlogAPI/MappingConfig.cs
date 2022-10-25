using AutoMapper;
using ForuMe.Services.BlogAPI.Models;
using ForuMe.Services.BlogAPI.Models.Dtos;

namespace ForuMe.Services.BlogAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<BlogDto, Blog>();
                config.CreateMap<Blog, BlogDto>();
            });

            return mappingConfig;
        }
    }
}

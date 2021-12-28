using AutoMapper;

namespace Infrastructure.AutoMapperProfile
{
    public class AutoMapperProfile
    {
        private static MapperConfiguration _configuration;

        public static MapperConfiguration InitializeAutoMapper()
        {
            if (_configuration is null)
            {
                _configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<CadastroProfile>();
                   
                });
            }
            return _configuration;
        }
    }
}

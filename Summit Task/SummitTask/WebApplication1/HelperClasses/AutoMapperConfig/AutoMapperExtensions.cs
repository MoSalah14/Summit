using AutoMapper;

namespace Summit_Task.HelperClasses.AutoMapperConfig
{
    public static class AutoMapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            return mapper.Map<TDestination>(source);
        }

    }
}

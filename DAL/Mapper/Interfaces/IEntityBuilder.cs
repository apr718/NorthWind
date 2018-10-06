namespace DAL.Mapper.Interfaces
{
    public interface IEntityBuilder
    {
        TDestination Map<TSource, TDestination>(TSource source);

        TDestination Map<TSource, TDestination>(TSource source, System.Action<TSource, TDestination> afterOptionsMap);
    }
}

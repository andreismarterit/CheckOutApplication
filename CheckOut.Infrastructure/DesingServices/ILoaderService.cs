namespace CheckOut.Infrastructure.DesignServices
{
    public interface ILoaderService<TReturnType, TLoadBy>
    {
        Task<TReturnType> LoadAsync(TLoadBy loadBy);
    }

}

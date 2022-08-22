namespace CheckOut.Infrastructure.DesignServices
{
    public interface IBuilderService<TOut, TIn>
    {
        Task<TOut> BuildAsync(TIn data);
    }
}

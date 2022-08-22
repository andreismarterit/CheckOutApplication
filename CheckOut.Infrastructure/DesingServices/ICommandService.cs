namespace CheckOut.Infrastructure.DesignServices
{
    public interface ICommandService<TOut, TIn>
    {
        Task<TOut> ExecuteAsync(TIn input);
    }
}

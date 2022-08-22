using CheckOut.Infrastructure.Executors.WebApi.Models;

namespace CheckOut.Infrastructure.Executors.WebApi.Command
{
    public interface IWebApiCommandService<TOutput>
    {
        Task<WebApiCommandResponse<TOutput>> ExecuteAsync();
    }

    public interface IWebApiCommandService<TOutput, TInput>
    {
        Task<WebApiCommandResponse<TOutput>> ExecuteAsync(TInput input);
    }

    public interface IWebApiCommandService<TOutput, TInput1, TInput2>
    {
        Task<WebApiCommandResponse<TOutput>> ExecuteAsync(TInput1 input1, TInput2 input2);
    }
}

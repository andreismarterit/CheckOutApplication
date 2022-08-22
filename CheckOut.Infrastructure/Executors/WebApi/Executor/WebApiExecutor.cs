using CheckOut.Infrastructure.Executors.WebApi.Command;
using CheckOut.Infrastructure.Executors.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace CheckOut.Infrastructure.Executors.WebApi.Executor
{
    public class WebApiExecutor
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public WebApiExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetService(typeof(ILogger<WebApiExecutor>)) as ILogger;
        }

        public async Task<ActionResult<TOutput>> ExecuteAsync<TCommand, TOutput>() where TCommand : IWebApiCommandService<TOutput>
        {
            return await ExecuteImplementation(async () =>
            {
                IWebApiCommandService<TOutput> webApiCommand = _serviceProvider.GetService(typeof(TCommand)) as IWebApiCommandService<TOutput>;
                return await webApiCommand.ExecuteAsync();
            },
            typeof(TCommand));
        }

        public async Task<ActionResult<TOutput>> ExecuteAsync<TCommand, TOutput, TInput>(TInput input) where TCommand : IWebApiCommandService<TOutput, TInput>
        {
            return await ExecuteImplementation(async () =>
            {
                IWebApiCommandService<TOutput, TInput> webApiCommand = _serviceProvider.GetService(typeof(TCommand)) as IWebApiCommandService<TOutput, TInput>;
                return await webApiCommand.ExecuteAsync(input);
            },
            typeof(TCommand), input);
        }

        public async Task<ActionResult<TOutput>> ExecuteAsync<TCommand, TOutput, TInput1, TInput2>(TInput1 input1, TInput2 input2) where TCommand : IWebApiCommandService<TOutput, TInput1, TInput2>
        {
            return await ExecuteImplementation(async () =>
            {
                IWebApiCommandService<TOutput, TInput1, TInput2> webApiCommand = _serviceProvider.GetService(typeof(TCommand)) as IWebApiCommandService<TOutput, TInput1, TInput2>;
                return await webApiCommand.ExecuteAsync(input1, input2);
            },
            typeof(TCommand), input1, input2);
        }

        private async Task<ActionResult<TOutput>> ExecuteImplementation<TOutput>(Func<Task<WebApiCommandResponse<TOutput>>> comandFunction, params object[] argsToLog)
        {
            try
            {
                WebApiCommandResponse<TOutput> commandResponse = await comandFunction();

                switch (commandResponse.HttpStatusCode)
                {
                    case HttpStatusCode.Created:
                        return new CreatedResult(string.Empty, commandResponse.ResponseObject);

                    case HttpStatusCode.OK:
                        return new OkObjectResult(commandResponse.ResponseObject);

                    case HttpStatusCode.NotFound:
                        return new NotFoundObjectResult(commandResponse.Errors);

                    case HttpStatusCode.NoContent:
                        return new NoContentResult();

                    case HttpStatusCode.BadRequest:
                        return new BadRequestObjectResult(commandResponse.Errors);

                    default:
                        return new StatusCodeResult((int)commandResponse.HttpStatusCode);
                }
            }
            catch (Exception ex)
            {
                string argsToLogAsJson = ConvertToJson(argsToLog);
                _logger.LogError(ex, "An exception has occured.\nArguments:\n{Arguments}", argsToLogAsJson);

                return await Task.FromResult(new StatusCodeResult((int)HttpStatusCode.InternalServerError));
            }
        }

        private string ConvertToJson(params object[] argsToLog)
        {
            string result = null;
            List<object> list = new List<object>();

            if (argsToLog != null)
            {
                foreach (var item in argsToLog)
                {
                    if (item != null)
                    {
                        list.Add(new { item.GetType().Name, Value = item });
                    }
                }
            }

            if (list.Any())
            {
                result = JsonConvert.SerializeObject(list, new JsonSerializerSettings()
                {
                    Error = (ser, err) => err.ErrorContext.Handled = true,
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });
            }

            return result;
        }
    }
}

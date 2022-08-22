using CheckOut.BusinsessLogic.WebApiCommands.Baskets.CheckOut.Dtos;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.Create;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.Create.Dtos;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.GetById;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.GetById.Dtos;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.AddArticle;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.AddArticle.Dtos;
using CheckOut.Infrastructure.Executors.WebApi.Executor;
using Microsoft.AspNetCore.Mvc;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.CheckOut;

namespace CheckOut.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly WebApiExecutor _webApiExecutor;

        public BasketsController(WebApiExecutor webApiExecutor)
        {
            _webApiExecutor = webApiExecutor;
        }

        [HttpPost()]
        public async Task<ActionResult<Guid>> Post([FromBody] BasketCreateFromBodyRequestDto basketCreateFromBodyRequestDto)
        {
            return await _webApiExecutor.ExecuteAsync<IBasketCreateWebApiCommand, Guid, BasketCreateFromBodyRequestDto>(basketCreateFromBodyRequestDto);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] BasketAddArticleFromBodyRequestDto basketAddArticleFromBodyRequestDto)
        {
            return await _webApiExecutor.ExecuteAsync<IBasketAddArticleWebApiCommand, bool, Guid, BasketAddArticleFromBodyRequestDto>(id, basketAddArticleFromBodyRequestDto);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<ActionResult<bool>> Patch(Guid id, [FromBody] BasketCheckOutFromBodyRequest batchCheckOutFromBodyRequest)
        {
            return await _webApiExecutor.ExecuteAsync<IBasketCheckOutWebApiCommand, bool, Guid, BasketCheckOutFromBodyRequest>(id, batchCheckOutFromBodyRequest);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<BasketGetByIdResponseDto>> GetById(Guid id)
        {
            return await _webApiExecutor.ExecuteAsync<IBasketGetByIdWebApiCommand, BasketGetByIdResponseDto, Guid>(id);
        }
    }
}
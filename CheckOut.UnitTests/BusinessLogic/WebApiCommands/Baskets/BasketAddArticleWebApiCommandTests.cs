using CheckOut.BusinsessLogic.DesingServices.Loaders;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.AddArticle;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.AddArticle.Dtos;
using CheckOut.DataAccess;
using CheckOut.DataAccess.Entities.BasketItems;
using CheckOut.DataAccess.Entities.Baskets;
using Moq;
using System.Net;

namespace CheckOut.UnitTests.BusinessLogic.WebApiCommands.Baskets
{
    public class BasketAddArticleWebApiCommandTests
    {
        private Mock<CheckOutDbContext> _checkOutDbContextMock;
        private Mock<IBasketByIdLoader> _basketByIdLoaderMock;
        private BasketAddArticleWebApiCommand _basketAddArticleWebApiCommand;

        private Guid _id;
        private BasketAddArticleFromBodyRequestDto _basketAddArticleFromBodyRequestDto;

        [SetUp]
        public void Setup()
        {
            _checkOutDbContextMock = new Mock<CheckOutDbContext>();
            _basketByIdLoaderMock = new Mock<IBasketByIdLoader>();

            _basketAddArticleWebApiCommand = new BasketAddArticleWebApiCommand(_checkOutDbContextMock.Object, _basketByIdLoaderMock.Object);

            _id = Guid.NewGuid();
            _basketAddArticleFromBodyRequestDto = new BasketAddArticleFromBodyRequestDto();

            _basketByIdLoaderMock.Setup(x => x.LoadAsync(_id)).Returns(Task.FromResult(new Basket { ID = _id, Items = new List<BasketItem>() }));
        }

        [Test]
        public async Task ExecuteAsync_AddsArticleToBasket()
        {
            var result = await _basketAddArticleWebApiCommand.ExecuteAsync(_id, _basketAddArticleFromBodyRequestDto);

            Assert.AreEqual(HttpStatusCode.OK, result.HttpStatusCode);
            Assert.IsTrue(result.ResponseObject);
            Assert.IsNull(result.Errors);
        }

        [Test]
        public async Task ExecuteAsync_ReturnsNotFound_WhenIDNotFound()
        {
            _basketByIdLoaderMock.Setup(x => x.LoadAsync(_id)).Returns(Task.FromResult((Basket)null));

            var result = await _basketAddArticleWebApiCommand.ExecuteAsync(_id, _basketAddArticleFromBodyRequestDto);

            Assert.AreEqual(HttpStatusCode.NotFound, result.HttpStatusCode);
            Assert.IsFalse(result.ResponseObject);
            Assert.IsNotNull(result.Errors);
        }

        [Test]
        public async Task ExecuteAsync_ReturnsNotFound_WhenBasketIsClosed()
        {
            _basketByIdLoaderMock.Setup(x => x.LoadAsync(_id)).Returns(Task.FromResult(new Basket { ID = _id, Close = true }));

            var result = await _basketAddArticleWebApiCommand.ExecuteAsync(_id, _basketAddArticleFromBodyRequestDto);

            Assert.AreEqual(HttpStatusCode.NotFound, result.HttpStatusCode);
            Assert.IsFalse(result.ResponseObject);
            Assert.IsNotNull(result.Errors);
        }
    }
}

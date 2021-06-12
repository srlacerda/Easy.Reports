using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Xunit;

namespace Easy.Reports.Application.Tests.UseCases.ConsolidatedReport
{
    [Collection(nameof(GetHandlerCollection))]
    public class GetHandlerTests
    {
        private readonly GetHandlerTestsFixture _getHandlerTestsFixture;
        private readonly GetHandler _getHandler;

        public GetHandlerTests(GetHandlerTestsFixture getHandlerTestsFixture)
        {
            _getHandlerTestsFixture = getHandlerTestsFixture;
            _getHandler = _getHandlerTestsFixture.ObterGetHandler();
        }

        //[Fact(DisplayName = "Get Consolidate Report Sucess")]
        //[Trait("Categoria", "ConsolidateReport - Handler")]
        //public void ConsolidateReport_Get_Sucess()
        //{
        //    // Arrange
        //    CancellationToken cancellationToken = CancellationToken.None;
        //    var getQuery = new GetQuery(DateTime.Now.AddDays(-1));
        //    GetResult getResult = new GetResult
        //    {
        //        valorTotal = 123
        //    };

        //    _getHandlerTestsFixture.Mocker.GetMock<IConsolidatedInvestmentService>()
        //        .Setup(c => c.GetAllProducts(getQuery.dateRequest, cancellationToken))
        //        .Returns(getResult);

        //    // Act
        //    var result = _getHandler.Handle(getQuery, cancellationToken);

        //    // Assert
        //    Assert.True(result.Result.valorTotal > 0);
        //}
    }
}

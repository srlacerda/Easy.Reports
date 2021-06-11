using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Easy.Reports.Application.Tests.UseCases.ConsolidatedReport
{
    [CollectionDefinition(nameof(GetHandlerCollection))]

    public class GetHandlerCollection : ICollectionFixture<GetHandlerTestsFixture> {}
    public class GetHandlerTestsFixture : IDisposable
    {
        public GetHandler GetHandler;
        public AutoMocker Mocker;

        public GetHandler ObterGetHandler()
        {
            Mocker = new AutoMocker();
            GetHandler = Mocker.CreateInstance<GetHandler>();
            return GetHandler;
        }

        public void Dispose()
        {
        }
    }
}

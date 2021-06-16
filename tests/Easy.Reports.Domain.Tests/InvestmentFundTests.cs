using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Models;
using System;
using Xunit;

namespace Easy.Reports.Domain.Tests
{
    public class InvestmentFundTests
    {
        private readonly InvestmentFund _investmentFund;
        public InvestmentFundTests()
        {
            _investmentFund = new InvestmentFund(new InvestmentFundMock
            {
                InvestedCapital = 1000.0m,
                CurrentValue = 1159m,
                RescueDate = new DateTime(2022, 10, 01),
                PurchaseDate = new DateTime(2017, 10, 01),
                Name = "ALASKA"
            });
        }

        [Fact(DisplayName = "Calculate Rescue Value With Rescue Date Equal Due Date")]
        [Trait("Category", "Entitie - InvestmentFund")]
        public void InvestmentFund_PerformCalculationsRescue_RescueDateEqualDueDate()
        {
            // Arrange
            DateTime rescueDate = _investmentFund.DueDate;

            // Act
            _investmentFund.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(1159m, _investmentFund.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Rescue Date More Than Due Date")]
        [Trait("Category", "Entitie - InvestmentFund")]
        public void InvestmentFund_PerformCalculationsRescue_RescueDateMoreThanDueDate()
        {
            // Arrange
            DateTime rescueDate = _investmentFund.DueDate.AddDays(1);

            // Act
            _investmentFund.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(1159m, _investmentFund.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Three Months To Due Date")]
        [Trait("Category", "Entitie - InvestmentFund")]
        public void InvestmentFund_PerformCalculationsRescue_ThreeMonthsToDueDate()
        {
            // Arrange
            DateTime rescueDate = _investmentFund.DueDate.AddMonths(-3);

            // Act
            _investmentFund.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(1089.46m, _investmentFund.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With More Than Half Time In Custody")]
        [Trait("Category", "Entitie - InvestmentFund")]
        public void InvestmentFund_PerformCalculationsRescue_MoreThanHalfTimeInCustody()
        {
            // Arrange
            DateTime rescueDate = _investmentFund.PurchaseDate.AddYears(3);

            // Act
            _investmentFund.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(985.15m, _investmentFund.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Less Than Half Time In Custody")]
        [Trait("Category", "Entitie - InvestmentFund")]
        public void InvestmentFund_PerformCalculationsRescue_LessThanHalfTimeInCustody()
        {
            // Arrange
            DateTime rescueDate = _investmentFund.PurchaseDate.AddYears(2);

            // Act
            _investmentFund.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(811.30m, _investmentFund.RescueValue);
        }

        [Fact(DisplayName = "Calculate Ir Tax Value")]
        [Trait("Category", "Entitie - InvestmentFund")]
        public void InvestmentFund_PerformCalculationsRescue_CalculateIrTaxValue()
        {
            // Arrange
            DateTime rescueDate = _investmentFund.DueDate;

            // Act
            _investmentFund.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(23.850m, _investmentFund.IrTaxValue);
        }
    }
}
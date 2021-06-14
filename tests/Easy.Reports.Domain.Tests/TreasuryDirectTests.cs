using Easy.Reports.Domain.Entities;
using System;
using Xunit;

namespace Easy.Reports.Domain.Tests
{
    public class TreasuryDirectTests
    {
        private readonly TreasuryDirect _treasuryDirect;
        public TreasuryDirectTests()
        {
            _treasuryDirect = new TreasuryDirect
            (
                investedValue: 799.4720m,
                totalValue: 829.68m,
                dueDate: new DateTime(2025, 03, 01),
                purchaseDate: new DateTime(2015, 03, 01),
                name: "Tesouro Selic 2025"
            );
        }

        [Fact(DisplayName = "Calculate Rescue Value With Rescue Date Equal Due Date")]
        [Trait("Category", "Entitie - TreasuryDirect")]
        public void TreasuryDirect_PerformCalculationsRescue_RescueDateEqualDueDate()
        {
            // Arrange
            DateTime rescueDate = _treasuryDirect.DueDate;

            // Act
            _treasuryDirect.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(829.68m, _treasuryDirect.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Rescue Date More Than Due Date")]
        [Trait("Category", "Entitie - TreasuryDirect")]
        public void TreasuryDirect_PerformCalculationsRescue_RescueDateMoreThanDueDate()
        {
            // Arrange
            DateTime rescueDate = _treasuryDirect.DueDate.AddDays(1);

            // Act
            _treasuryDirect.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(829.68m, _treasuryDirect.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Three Months To Due Date")]
        [Trait("Category", "Entitie - TreasuryDirect")]
        public void TreasuryDirect_PerformCalculationsRescue_ThreeMonthsToDueDate()
        {
            // Arrange
            DateTime rescueDate = _treasuryDirect.DueDate.AddMonths(-3);

            // Act
            _treasuryDirect.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(779.8992m, _treasuryDirect.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With More Than Half Time In Custody")]
        [Trait("Category", "Entitie - TreasuryDirect")]
        public void TreasuryDirect_PerformCalculationsRescue_MoreThanHalfTimeInCustody()
        {
            // Arrange
            DateTime rescueDate = _treasuryDirect.PurchaseDate.AddYears(6);

            // Act
            _treasuryDirect.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(705.2280m, _treasuryDirect.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Less Than Half Time In Custody")]
        [Trait("Category", "Entitie - TreasuryDirect")]
        public void TreasuryDirect_PerformCalculationsRescue_LessThanHalfTimeInCustody()
        {
            // Arrange
            DateTime rescueDate = _treasuryDirect.PurchaseDate.AddYears(4);

            // Act
            _treasuryDirect.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(580.7760m, _treasuryDirect.RescueValue);
        }

        [Fact(DisplayName = "Calculate Ir Tax Value")]
        [Trait("Category", "Entitie - TreasuryDirect")]
        public void TreasuryDirect_PerformCalculationsRescue_CalculateIrTaxValue()
        {
            // Arrange
            DateTime rescueDate = _treasuryDirect.DueDate;

            // Act
            _treasuryDirect.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(3.020800m, _treasuryDirect.IrTaxValue);
        }
    }
}

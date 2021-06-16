using Easy.Reports.Domain.Entities;
using System;
using Xunit;

namespace Easy.Reports.Domain.Tests
{
    public class FixedIncomeTests
    {
        private readonly FixedIncome _fixedIncome;
        public FixedIncomeTests()
        {
            _fixedIncome = new FixedIncome
            (
                investedValue: 2000.0m,
                totalValue: 2097.85m,
                dueDate: new DateTime(2021, 03, 09),
                purchaseDate: new DateTime(2019, 03, 14),
                name: "BANCO MAXIMA"
            );
        }

        [Fact(DisplayName = "Calculate Rescue Value With Rescue Date Equal Due Date")]
        [Trait("Category", "Entitie - FixedIncome")]
        public void FixedIncome_PerformCalculationsRescue_RescueDateEqualDueDate()
        {
            // Arrange
            DateTime rescueDate = _fixedIncome.DueDate;

            // Act
            _fixedIncome.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(2097.85m, _fixedIncome.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Rescue Date More Than Due Date")]
        [Trait("Category", "Entitie - FixedIncome")]
        public void FixedIncome_PerformCalculationsRescue_RescueDateMoreThanDueDate()
        {
            // Arrange
            DateTime rescueDate = _fixedIncome.DueDate.AddDays(1);

            // Act
            _fixedIncome.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(2097.85m, _fixedIncome.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Three Months To Due Date")]
        [Trait("Category", "Entitie - FixedIncome")]
        public void FixedIncome_PerformCalculationsRescue_ThreeMonthsToDueDate()
        {
            // Arrange
            DateTime rescueDate = _fixedIncome.DueDate.AddMonths(-3);

            // Act
            _fixedIncome.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(1971.9790m, _fixedIncome.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With More Than Half Time In Custody")]
        [Trait("Category", "Entitie - FixedIncome")]
        public void FixedIncome_PerformCalculationsRescue_MoreThanHalfTimeInCustody()
        {
            // Arrange
            DateTime rescueDate = _fixedIncome.PurchaseDate.AddMonths(13);

            // Act
            _fixedIncome.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(1783.1725m, _fixedIncome.RescueValue);
        }

        [Fact(DisplayName = "Calculate Rescue Value With Less Than Half Time In Custody")]
        [Trait("Category", "Entitie - FixedIncome")]
        public void FixedIncome_PerformCalculationsRescue_LessThanHalfTimeInCustody()
        {
            // Arrange
            DateTime rescueDate = _fixedIncome.PurchaseDate.AddMonths(11);

            // Act
            _fixedIncome.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(1468.4950m, _fixedIncome.RescueValue);
        }

        [Fact(DisplayName = "Calculate Ir Tax Value")]
        [Trait("Category", "Entitie - FixedIncome")]
        public void FixedIncome_PerformCalculationsRescue_CalculateIrTaxValue()
        {
            // Arrange
            DateTime rescueDate = _fixedIncome.DueDate;

            // Act
            _fixedIncome.PerformCalculationsRescue(rescueDate);

            // Assert
            Assert.Equal(48.925m, _fixedIncome.IrTaxValue);
        }
    }
}
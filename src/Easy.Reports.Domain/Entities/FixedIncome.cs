using Easy.Reports.Domain.Models;
using System;

namespace Easy.Reports.Domain.Entities
{
    public class FixedIncome : Investment
    {
        private const decimal _irTaxPercentage = 0.5m;
        public override void PerformCalculationsRescue(DateTime rescueDate)
        {
            PerformCalculations(rescueDate, _irTaxPercentage);
        }

        public FixedIncome(FixedIncomeMock fixedIncomeMock)
        {
            InvestedValue = fixedIncomeMock.InvestedCapital;
            TotalValue = fixedIncomeMock.CurrentCapital;
            DueDate = fixedIncomeMock.DueDate;
            PurchaseDate = fixedIncomeMock.OperationDate;
            Name = fixedIncomeMock.Name;
        }
    }
}
using Easy.Reports.Domain.Models;
using System;

namespace Easy.Reports.Domain.Entities
{
    public class TreasuryDirect : Investment
    {
        private const decimal _irTaxPercentage = 0.10m;
        public override void PerformCalculationsRescue(DateTime rescueDate)
        {
            PerformCalculations(rescueDate, _irTaxPercentage);
        }

        public static explicit operator TreasuryDirect(TreasuryDirectMock treasuryDirectMockModel)
        {
            return new TreasuryDirect
            {
                InvestedValue = treasuryDirectMockModel.InvestedValue,
                TotalValue = treasuryDirectMockModel.TotalValue,
                DueDate = treasuryDirectMockModel.DueDate,
                PurchaseDate = treasuryDirectMockModel.PurchaseDate,
                Name = treasuryDirectMockModel.Name
            };
        }
    }
}

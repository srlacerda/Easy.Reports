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
                InvestedValue = treasuryDirectMockModel.valorInvestido,
                TotalValue = treasuryDirectMockModel.valorTotal,
                DueDate = treasuryDirectMockModel.vencimento,
                PurchaseDate = treasuryDirectMockModel.dataDeCompra,
                Name = treasuryDirectMockModel.nome
            };
        }
    }
}

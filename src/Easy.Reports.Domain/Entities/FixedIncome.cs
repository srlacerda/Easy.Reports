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

        public static explicit operator FixedIncome(FixedIncomeMock fixedIncomeMock)
        {
            return new FixedIncome
            {
                InvestedValue = fixedIncomeMock.capitalInvestido,
                TotalValue = fixedIncomeMock.capitalAtual,
                DueDate = fixedIncomeMock.vencimento,
                PurchaseDate = fixedIncomeMock.dataOperacao,
                Name = fixedIncomeMock.nome
            };
        }
    }
}

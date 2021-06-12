﻿using Easy.Reports.Domain.Models;
using System;

namespace Easy.Reports.Domain.Entities
{
    public class InvestmentFund : Investment
    {
        private const decimal _irTaxPercentage = 0.15m;
        public override void PerformCalculationsRescue(DateTime rescueDate)
        {
            PerformCalculations(rescueDate, _irTaxPercentage);
        }

        public static explicit operator InvestmentFund(InvestmentFundMock investmentFundMock)
        {
            return new InvestmentFund
            {
                InvestedValue = investmentFundMock.capitalInvestido,
                TotalValue = investmentFundMock.ValorAtual,
                DueDate = investmentFundMock.dataResgate,
                PurchaseDate = investmentFundMock.dataCompra,
                Name = investmentFundMock.nome
            };
        }
    }
}

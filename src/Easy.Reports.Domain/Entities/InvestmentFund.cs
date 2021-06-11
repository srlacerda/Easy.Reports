using Easy.Reports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    public class InvestmentFund : Investment
    {
        private const decimal _irPercentual = 0.15m;
        public override void EfetuarCalculosResgate(DateTime dataResgate)
        {
            EfetuarCalculos(dataResgate, _irPercentual);
        }

        public static explicit operator InvestmentFund(InvestmentFundMock investmentFundMock)
        {
            return new InvestmentFund
            {
                valorInvestido = investmentFundMock.capitalInvestido,
                valorTotal = investmentFundMock.ValorAtual,
                vencimento = investmentFundMock.dataResgate,
                dataDeCompra = investmentFundMock.dataCompra,
                nome = investmentFundMock.nome
            };
        }
    }
}

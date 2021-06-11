using Easy.Reports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    public class FixedIncome : Investment

    {
        private const decimal _irPercentual = 0.5m;
        public override void EfetuarCalculosResgate(DateTime dataResgate)
        {
            EfetuarCalculos(dataResgate, _irPercentual);
        }

        public static explicit operator FixedIncome(FixedIncomeMock fixedIncomeMock)
        {
            return new FixedIncome
            {
                valorInvestido = fixedIncomeMock.capitalInvestido,
                valorTotal = fixedIncomeMock.capitalAtual,
                vencimento = fixedIncomeMock.vencimento,
                dataDeCompra = fixedIncomeMock.dataOperacao,
                nome = fixedIncomeMock.nome
            };
        }
    }
}

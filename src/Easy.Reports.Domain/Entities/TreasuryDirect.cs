using Easy.Reports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    public class TreasuryDirect : Investment
    {
        private const decimal _irPercentual = 0.10m;

        public override void EfetuarCalculosResgate(DateTime dataResgate)
        {
            EfetuarCalculos(dataResgate, _irPercentual);
        }

        public static explicit operator TreasuryDirect(TreasuryDirectMock treasuryDirectMockModel)
        {
            return new TreasuryDirect
            {
                valorInvestido = treasuryDirectMockModel.valorInvestido,
                valorTotal = treasuryDirectMockModel.valorTotal,
                vencimento = treasuryDirectMockModel.vencimento,
                dataDeCompra = treasuryDirectMockModel.dataDeCompra,
                nome = treasuryDirectMockModel.nome
            };
        }
    }
}

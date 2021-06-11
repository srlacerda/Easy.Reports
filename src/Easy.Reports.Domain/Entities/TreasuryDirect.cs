using Easy.Reports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    public class TreasuryDirect : Investment, IInvestment
    {
        private const decimal _irPecentual = 0.10m;
        public decimal valorInvestido { get; private set; }
        public decimal valorTotal { get; private set; }
        public DateTime vencimento { get; private set; }
        public DateTime dataDeCompra { get; private set; }
        public string tipo { get; private set; }
        public string nome { get; private set; }

        //novos
        public DateTime dataResgate { get; private set; }
        public decimal ir { get; private set; }
        public decimal valorResgate { get; private set; }


        public void EfetuarCalculosResgate(DateTime dataResgate)
        {
            this.dataResgate = dataResgate;
            this.ir = 0;
            this.valorResgate = 0;
            CalcularValorResgate();
        }
        private void CalcularValorResgate()
        {
            valorResgate = CalcularValorResgate(dataResgate, dataDeCompra, vencimento, valorTotal);
        }
        private void CalcularValorIr()
        {
            ir = CalcularIr(_irPecentual, valorTotal, valorInvestido);
        }

        public static explicit operator TreasuryDirect(TreasuryDirectMock treasuryDirectMockModel)
        {
            return new TreasuryDirect
            {
                valorInvestido = treasuryDirectMockModel.valorInvestido,
                valorTotal =treasuryDirectMockModel.valorTotal,
                vencimento = treasuryDirectMockModel.vencimento,
                dataDeCompra = treasuryDirectMockModel.dataDeCompra,
                tipo = treasuryDirectMockModel.tipo,
                nome = treasuryDirectMockModel.nome
            };
        }
    }
}

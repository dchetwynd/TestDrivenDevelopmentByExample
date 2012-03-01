using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDrivenDevelopmentByExample
{
    public class CurrencyPair
    {
        private string _sourceCurrency;
        private string _targetCurrency;

        public CurrencyPair(string source, string target)
        {
            _sourceCurrency = source;
            _targetCurrency = target;
        }

        public override bool Equals(object obj)
        {
            CurrencyPair otherPair = (CurrencyPair) obj;
            return (_sourceCurrency == otherPair._sourceCurrency)
                   && (_targetCurrency == otherPair._targetCurrency);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}

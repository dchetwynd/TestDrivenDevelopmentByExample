using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDrivenDevelopmentByExample
{
    public class Bank
    {
        public  Money Reduce(Expression source, string targetCurrency)
        {
            if (source is Money)
                return (Money)source.Reduce(this, targetCurrency);

            Sum sum = (Sum)source;
            return sum.Reduce(this, targetCurrency);
        }

        public int GetRate(string sourceCurrency, string targetCurrency)
        {
            if (sourceCurrency == targetCurrency)
                return 1;

            int rate = (int) _rates[new CurrencyPair(sourceCurrency, targetCurrency)];
            return rate;
        }

        internal void AddRate(string sourceCurrency, string targetCurrency, int rate)
        {
            _rates.Add(new CurrencyPair(sourceCurrency, targetCurrency), rate);
        }

        private Hashtable _rates = new Hashtable();
    }
}

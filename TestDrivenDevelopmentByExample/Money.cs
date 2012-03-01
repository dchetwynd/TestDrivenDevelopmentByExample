using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDrivenDevelopmentByExample
{
    public class Money: Expression
    {
        public Money(int amount, string currency)
        {
            Amount = amount;
            _currency = currency;
        }

        public static Money CreateDollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money CreateFranc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public Expression Times(int multiplier)
        {
            return new Money(Amount * multiplier, _currency);
        }
        
        public string Currency()
        {
            return _currency;
        }

        public override bool Equals(object obj)
        {
            Money otherMoney = (Money)obj;
            return Amount == otherMoney.Amount
                && Currency() == otherMoney.Currency();
        }

        public override string ToString()
        {
            return Amount + " " + _currency;
        }

        public Expression Plus(Expression addend)
        {
            return new Sum(this, addend);
        }

        public Money Reduce(Bank bank, string targetCurrency)
        {
            int rate = bank.GetRate(_currency, targetCurrency);
            return new Money(Amount / rate, targetCurrency);
        }

        public int Amount;
        protected string _currency;
    }
}

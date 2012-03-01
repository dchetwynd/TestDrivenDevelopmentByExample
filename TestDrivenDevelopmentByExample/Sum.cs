using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDrivenDevelopmentByExample
{
    public class Sum: Expression
    {
        public Expression Augend;
        public Expression Addend;

        public Sum(Expression augend, Expression addend)
        {
            Augend = augend;
            Addend = addend;
        }

        public Money Reduce(Bank bank, string targetCurrency)
        {
            int amount = Augend.Reduce(bank, targetCurrency).Amount + Addend.Reduce(bank, targetCurrency).Amount;
            return new Money(amount, targetCurrency);
        }

        public Expression Plus(Expression addend)
        {
            return new Sum(this, addend);
        }

        public Expression Times(int multiplier)
        {
            return new Sum(Augend.Times(multiplier), Addend.Times(multiplier));
        }
    }
}

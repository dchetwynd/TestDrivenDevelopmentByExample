using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDrivenDevelopmentByExample
{
    public interface Expression
    {
        Money Reduce(Bank bank, string targetCurrency);
        Expression Plus(Expression tenFrancs);
        Expression Times(int multiplier);
    }
}

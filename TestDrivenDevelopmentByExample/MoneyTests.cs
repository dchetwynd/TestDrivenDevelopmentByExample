using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestDrivenDevelopmentByExample
{
    [TestFixture]
    public class MoneyTests
    {
        [Test]
        public void TestDollarAndFrancEquality()
        {
            Assert.False(Money.CreateDollar(5).Equals(Money.CreateFranc(5)));
        }

        [Test]
        public void TestCurrency()
        {
            Assert.AreEqual("USD", Money.CreateDollar(1).Currency());
            Assert.AreEqual("CHF", Money.CreateFranc(1).Currency());
        }

        [Test]
        public void TestDollarMultiplication()
        {
            Money five = Money.CreateDollar(5);
            Assert.AreEqual(Money.CreateDollar(10), five.Times(2));
            Assert.AreEqual(Money.CreateDollar(15), five.Times(3));
        }

        [Test]
        public void TestDollarEquality()
        {
            Assert.True(Money.CreateDollar(5).Equals(Money.CreateDollar(5)));
            Assert.True(Money.CreateDollar(6).Equals(Money.CreateDollar(6)));
        }

        [Test]
        public void TestSimpleAddition()
        {
            Money five = Money.CreateDollar(5);
            Expression sum = five.Plus(five);
            Bank bank = new Bank();
            Money reduced = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.CreateDollar(10), reduced);
        }

        [Test]
        public void TestPlusReturnsSum()
        {
            Money five = Money.CreateDollar(5);
            Expression result = five.Plus(five);
            Sum sum = (Sum) result;
            Assert.AreEqual(five, sum.Augend);
            Assert.AreEqual(five, sum.Addend);
        }

        [Test]
        public void TestReduceSum()
        {
            Expression sum = new Sum(Money.CreateDollar(3), Money.CreateDollar(4));
            Bank bank = new Bank();
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.CreateDollar(7), result);
        }

        [Test]
        public void TestReduceMoney()
        {
            Bank bank = new Bank();
            Money result = bank.Reduce(Money.CreateDollar(1), "USD");
            Assert.AreEqual(Money.CreateDollar(1), result);
        }

        [Test]
        public void TestReduceMoneyDifferentCurrency()
        {
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Money result = bank.Reduce(Money.CreateFranc(2), "USD");
            Assert.AreEqual(Money.CreateDollar(1), result);
        }

        [Test]
        public void TestIdentityRate()
        {
            Assert.AreEqual(1, new Bank().GetRate("USD", "USD"));
        }

        [Test]
        public void TestMixedAddition()
        {
            Expression fiveDollars = Money.CreateDollar(5);
            Expression tenFrancs = Money.CreateFranc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Money result = bank.Reduce(fiveDollars.Plus(tenFrancs), "USD");
            Assert.AreEqual(Money.CreateDollar(10), result);
        }

        [Test]
        public void TestSumPlusMoney()
        {
            Expression fiveDollars = Money.CreateDollar(5);
            Expression tenFrancs = Money.CreateFranc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Expression sum = new Sum(fiveDollars, tenFrancs).Plus(fiveDollars);
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.CreateDollar(15), result);
        }

        [Test]
        public void TestSumTimes()
        {
            Expression fiveDollars = Money.CreateDollar(5);
            Expression tenFrancs = Money.CreateFranc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Expression sum = new Sum(fiveDollars, tenFrancs).Times(2);
            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.CreateDollar(20), result);
        }
    }
}

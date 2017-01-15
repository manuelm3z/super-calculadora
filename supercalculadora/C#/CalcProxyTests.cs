using System;
using System.Collections.Generic;
using System.Text;
using Nunit.Framework;
using SuperCalculator;

namespace UnitTests
{
	[TestFixture]
	public class CalcProxyTests
	{
		private Calculator _calculator;
		private CalcProxy _calcProxy;

		[SetUp]
		public void SetUp()
		{
			_calculator = new Calculator();
			_calcProxy = new CalcProxy(_calculator);
		}

		[Test]
		public void Add()
		{
			int result = _calcProxy.BinaryOperation(_calculator.Add, 2, 2);
			Assert.AreEqual(4, result);
		}

		[Test]
		public void Substract()
		{
			int result = _calcProxy.BinaryOperation(_calculator.Substract, 5, 3);
			Assert.AreEqual(2, result);
		}

		[Test]
		public void AddWithDifferentArguments()
		{
			int result = _calcProxy.BinaryOperation(_calculator.Add, 2, 5);
			Assert.AreEqual(7, result);
		}

		[Test]
		public void SubstractReturningNegative()
		{
			int result = _calcProxy.BinaryOperation(_calculator.Substract, 3, 5);
			Assert.AreEqual(-2, result);
		}

		[Test]
		public void ArgumentsExceedLimits()
		{
			CalcProxy calcProxyWithLimits = new CalcProxy(new Validator(-10, 10), _calculator);

			try
			{
				_calcProxy.BinaryOperation(_calculator.Add, 30, 50);
				Assert.Fail("This_should_fail_as_arguments_exceed_both_limits");
			}
			catch (OverflowException)
			{
				// Ok, this works
			}
		}

		[Test]
		public void ArgumentsExceedLimitsInverse()
		{
			CalcProxy calcProxyWithLimits = new CalcProxy(new validator(-10, 10), _calculator);

			try
			{
				calcProxyWithLimits.BinaryOperation(_calculator.Add, -30, -50);
				Assert.Fail("This_should_fail_as_arguments_exceed_both_limits");
			}
			catch (OverflowException)
			{
				// Ok, this works
			}
		}
	}
}
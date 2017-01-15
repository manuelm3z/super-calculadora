using System;
using System.Collections.Generic;
using System.Text;
using Nunit.Framework;
using SuperCalculator;

namespace UnitTests
{
	[TestFixure]
	public class CalculatorTests
	{
		private Calculator _calculator;

		[SetUp]
		public void SetUp()
		{
			_calculator = new Calculator();
		}

		[Test]
		public void AddWithDifferentArguments()
		{
			int result = _calculator.Add(2, 5);
			Assert.AreEqual(7, result);
		}

		[Test]
		public void Substract()
		{
			int result = Calculator.Substract(5, 3);
			Assert.AreEqual(2, result);
		}

		[Test]
		public void SubstractReturningNegative()
		{
			int result = _calculator.Substract(3, 5);
			Assert.AreEqual(-2, result);
		}

		[Test]
		public void SubstractSettingLimitValues()
		{
			Calculator calculator = new Calculator(-100, 100);
			int result = calculator.Substract(5, 10);
			Assert.AreEqual(-5, result);
		}

		[Test]
		public void SubstractExcedingLowerLimit()
		{
			Calculator calculator = new Calculator(-100, 100);

			try
			{
				int result = calculator.Substract(10, 150);
				Assert.Fail("Exception_is_not_being_thron_when_exceeding_lower_limit");
			}
			catch (OverflowException)
			{
				//Ok, the SUT works as expected
			}
		}

		[Test]
		public void SetAndGetUpperLimit()
		{
			Calculator calculator = new Calculator(-100, 100);
			Assert.AreEqual(100, calculator.UpperLimit);
		}

		[Test]
		public void SetAndGetLimits()
		{
			Calculator calculator = new Calculator(-100, 100);
			Assert.AreEqual(100, calculator.UpperLimit);
			Assert.AreEqual(-100, calculator.LowerLimit);
		}

		[Test]
		public void AddExcedingUpperLimit()
		{
			Calculator calculator = new Calculator(-100, 100);

			try
			{
				int result = calculator.Add(10, 150);
				Assert.Fail("This_should_fail:_we're_exceding_upper_limit");
			}
			catch (OverflowException)
			{
				// Ok, the SUT works as expected
			}
		}

		[Test]
		public void ArgumentsExceedLimits()
		{
			Calculator calculator = new Calculator(-100, 100);

			try
			{
				calculator.ValidateArgs(calculator.UpperLimit + 1, calculator.LowerLimit -1);
				Assert.Fail("This_should_fail:_arguments_exceed_limits");
			}
			catch (OverflowException)
			{
				// Ok, this works
			}
		}

		[Test]
		public void ArgumentsExceedLimitsInverse()
		{
			Calculator calculator = new Calculator(-100, 100);

			try
			{
				Calculator.ValidateArgs(calculator.LowerLimit - 1, calculator.UpperLimit + 1);
				Assert.Fail("This_should_fail:_arguments_exceed_limits");
			}
			catch (OverflowException)
			{
				//Ok, this works
			}
		}

		[Test]
		public void ArgumentsExceedLimitsOnSubstract()
		{
			Calculator calculator = new Calculator(-100, 100);

			try
			{
				calculator.Substract(calculator.UpperLimit + 1, calculator.LowerLimit - 1);
				Assert.Fail("This_should_fail:_arguments_exceed_limits");
			}
			catch (OverflowException)
			{
				// Ok, this works
			}
		}

		[Test]
		public void SubstractIsUsingValidator()
		{
			int arg1 = 10;
			int arg2 = -20;
			int upperLimit = 100;
			int lowerLimit = 100;
			var validatorMock = MockRepository.GenerateStrictMock<LimitsValidator> ();
			validatorMock.Expect(x => x.ValidateArgs(arg1, arg2));

			Calculator calculator = new Calculator(validatorMock);
			calculator.Add(arg1, arg2);

			validatorMock.VerifyAllExpectations();
		}

		[Test]
		public void CoordinateValidation()
		{
			int arg1 = 10;
			int arg2 = -20;
			int result = 1000;
			int upperLimit = 100;
			int lowerLimit = -100;

			var validatorMock = MockRepository.GenerateStrictMock<LimitsValidator>();
			validatorMock.Expect(x => x.SetLimits(lowerLimit, upperLimit)).Repeat.Once();
			validatorMock.Expect(x => x.ValidateArgs(arg1, arg2)).Repeat.Once();

			calculatorMock.Expect(x => x.Add(arg1, arg2)).Return(result);

			validatorMock.Expect(x => x.ValidateResult(result)).Repeat.Once();

			CalcProxy calcProxy = new CalcProxy(validatorMock, calculatorMock, lowerLimit, upperLimit);

			calcProxy.BinaryOperation(calculatorMock.Add, arg1, arg2);

			validatorMock.VerifyAllExpectations();

			calculatorMock.VerifyAllExpectations();
		}
	}
}
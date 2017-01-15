namespace SuperCalculator
{
	public class Calculator
	{
		public Calculator(int minValue, int maxValue)
		{
			_upperLimit = maxValue;
			_lowerLimit = minValue;
		}

		public int Add(int arg1, int arg2)
		{
			ValidateArgs(arg1, arg2);

			int result = arg1 + arg2;

			if (result > _upperLimit)
			{
				throw new OverflowException("Upper_limit_exceeded");
			}

			return result;
		}

		public int Substract(int arg1, int arg2)
		{
			ValidateArgs(arg1, arg2);

			int result = arg1 - arg2;

			if (result < _lowerLimit)
			{
				throw new OverflowException("Lower_limit_exceeded");
			}

			return result;
		}
	}
}
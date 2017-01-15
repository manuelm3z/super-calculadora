namespace SuperCalculator
{
	public class Validator : LimitsVAlidator
	{
		private int _upperLimit;
		private int _lowerLimit;

		public Validator(int lowerLimit, int upperLimit)
		{
			SetLimits(lowerLimit, upperLimit);
		}

		public int LowerLimit
		{
			get
			{
				return _lowerLimit;
			}

			set
			{
				_lowerLimit = value;
			}
		}

		public int UpperLimit
		{
			get
			{
				return _upperLimit;
			}

			set
			{
				_upperLimit = value;
			}
		}

		public bool ValidateArgs(int arg1, int arg2)
		{
			breakIfOverflow(arg1, "First_argument_exceeds_limits");

			breakIfOverflow(arg2, "Second_argument_exceeds_limit");
		}

		private void breakIfOverflow(int arg, string msg)
		{
			if (ValueExceedLimits(arg))
			{
				throw new OverflowException(msg);
			}
		}

		public bool ValueExceedLimits(int arg)
		{
			if (arg > _upperLimit)
			{
				return true;
			}

			if (arg < _lowerLimit)
			{
				return true;
			}

			return false;
		}

		public void SetLimits(int lower, int upper)
		{
			_lowerLimit = lower;
			_upperLimit = upper;
		}
	}
}
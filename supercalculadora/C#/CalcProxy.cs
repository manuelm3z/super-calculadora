namespace SuperCalculator
{
	public class CalcProxy
	{
		private BasicCalculator _calculator;

		public CalcProxy(BasicCalculator calculator)
		{
			_calculator = calculator;
		}

		public int BinaryOperation(SingleBinaryOperation operation, int arg1, int arg2)
		{
			_validator.ValidateArgs(arg1, arg2);

			int result = 0;
			
			MethodInfo[] calculatorMethods = _calculator.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

			foreach (MethodInfo method in calculatorMethods)
			{
				if (method == operation.Method)
				{
					result = (int)method.Invoke(_calculator, new Object[] { arg1, arg2 });
				}
			}

			_validator.ValidateResult(result);

			return result;
		}
	}
}
using System;

namespace UEGP3.Core
{
	/// <summary>
	///  Attribute to work in junction with the <see cref="MinMaxFloat"/>
	/// </summary>
	public class MinMaxFloatAttribute : Attribute
	{
		public MinMaxFloatAttribute(float min, float max)
		{
			Min = min;
			Max = max;
		}
		public float Min { get; }
		public float Max { get; }
	}
}
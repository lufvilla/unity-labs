using System;

namespace Framework.RangedFloat
{
	[Serializable]
	public struct RangedFloat
	{
		public float MinValue;
		public float MaxValue;
	
		public float GetRandom
		{
			get { return UnityEngine.Random.Range(MinValue, MaxValue); }
		}
	
		public int GetRandomInt
		{
			get { return (int)UnityEngine.Random.Range(MinValue, MaxValue); }
		}
	}
}
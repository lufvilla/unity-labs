using UnityEngine;

namespace Demos.MovingFloor
{
	public class FloorObject : MonoBehaviour
	{
		public Transform Child;
		
		private Vector3 _initialPosition;
		private Material _childMaterial;
		private Floor _floor;

		private void Awake()
		{
			_initialPosition = Child.transform.localPosition;
		}

		public void SetFloorManager(Floor floor)
		{
			_floor = floor;
			SetValue(0);
		}

		public void SetValue(float value)
		{
			// The value it's the one that should be lerped
            Child.transform.localPosition = _initialPosition + _floor.Config.Direction * (_floor.Config.Multiplier * _floor.Config.Curve.Evaluate(value));
        }
	}
}

using UnityEngine;

namespace Demos.MovingFloor
{
	public class FloorObject : MonoBehaviour
	{
		public GameObject Child;
		
		private Floor _floorParent;
		private Vector3 _initialPosition;
		private Material _childMaterial;

		private void Awake()
		{
			_floorParent = GetComponentInParent<Floor>();
			_initialPosition = Child.transform.localPosition;
			_childMaterial = Child.GetComponent<Renderer>().material;
			SetValue(1);
		}

		public void SetValue(float value)
		{
			// The value it's the one that should be lerped
			Child.transform.localPosition = _initialPosition + _floorParent.Config.Direction * _floorParent.Config.Curve.Evaluate(value) * _floorParent.Config.Multiplier;
			_childMaterial.SetColor("_EmissionColor", _floorParent.Config.Color.Evaluate(value));
		}
	}
}

using UnityEngine;

namespace Demos.MovingFloor
{
	public class FloorObject : MonoBehaviour
	{
		public Renderer ChildRenderer;
		
		private Vector3 _initialPosition;
		private Material _childMaterial;
		private Floor _floor;

		private void Awake()
		{
			_initialPosition = ChildRenderer.transform.localPosition;
			_childMaterial = ChildRenderer.material;
		}

		public void SetFloorManager(Floor floor)
		{
			_floor = floor;
			SetValue(0);
		}

		public void SetValue(float value)
		{
			// The value it's the one that should be lerped
			ChildRenderer.transform.localPosition = _initialPosition + _floor.Config.Direction * _floor.Config.Curve.Evaluate(value) * _floor.Config.Multiplier;
			_childMaterial.SetColor("_EmissionColor", _floor.Config.Color.Evaluate(value));
		}
	}
}

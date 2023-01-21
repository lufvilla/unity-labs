﻿using System;
using System.Linq;
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
		}

		private void Start()
		{
			_floorParent.OnFloorActivate += OnActivate;
			_floorParent.OnFloorDeactivate += OnDeactivate;
			
			Reset();
		}

		private void Reset()
		{
			SetValuesByValue(1);
		}

		private void OnActivate()
		{
			_floorParent.OnLateUpdate += UpdatePosition;
		}
		
		private void OnDeactivate()
		{
			_floorParent.OnLateUpdate -= UpdatePosition;
			Reset();
		}

		private void UpdatePosition()
		{
			var target = _floorParent.Targets
				.OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
				.FirstOrDefault();

			if (target == null) return;
			
			var distance = Vector3.Distance(transform.position, target.position) - _floorParent.Config.Distance.MinValue;
			var currentValue = Mathf.Clamp(distance / (_floorParent.Config.Distance.MaxValue - _floorParent.Config.Distance.MinValue), 0, 1);

			SetValuesByValue(currentValue);
		}

		private void SetValuesByValue(float value)
		{
			Child.transform.localPosition = _initialPosition + _floorParent.Config.Direction * _floorParent.Config.Curve.Evaluate(value) * _floorParent.Config.Multiplier;
			_childMaterial.SetColor("_EmissionColor", _floorParent.Config.Color.Evaluate(value));
		}

		private void OnDestroy()
		{
			_floorParent.OnFloorActivate -= OnActivate;
			_floorParent.OnFloorDeactivate -= OnDeactivate;
			_floorParent.OnLateUpdate -= UpdatePosition;
		}
	}
}

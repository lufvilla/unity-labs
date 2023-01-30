using System.Collections.Generic;
using UnityEngine;

namespace Demos.MovingFloor
{
    public class Floor : MonoBehaviour
    {
        public FloorConfig Config;

        protected readonly List<Transform> Targets = new();
        protected FloorObject[] Floors;

        protected virtual void Start()
        {
            Floors = GetComponentsInChildren<FloorObject>();

            foreach (var floor in Floors)
                floor.SetFloorManager(this);
        }
        
        private void LateUpdate()
        {
            UpdateFloor();
        }
        
        private Transform _tmpTarget;
        private float _previousDistance;
        protected virtual void UpdateFloor()
        {
            float configNormalizedValue = Config.Distance.MaxValue - Config.Distance.MinValue;

            for (var i = Floors.Length - 1; i >= 0; i--)
            {
                _tmpTarget = null;
                _previousDistance = 9999f; // Reset distance
                
                foreach (var target in Targets)
                {
                    float distance = Vector3.Distance(Floors[i].transform.position, target.position);
                    
                    if(distance > Config.Distance.MaxValue || distance >= _previousDistance) continue;

                    _tmpTarget = target;
                    _previousDistance = distance;
                }

                if (_tmpTarget == null)
                {
                    Floors[i].SetValue(1);
                    continue;
                }
			
                var currentValue = Mathf.Clamp((_previousDistance - Config.Distance.MinValue) / configNormalizedValue, 0, 1);
                Floors[i].SetValue(currentValue);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Targets.Contains(other.transform)) return;
            Targets.Add(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!Targets.Contains(other.transform)) return;
            Targets.Remove(other.transform);
        }

        private void OnDrawGizmosSelected()
        {
            if(Targets.Count <= 0) return;

            foreach (var target in Targets)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(target.position, Config.Distance.MinValue);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(target.position, Config.Distance.MaxValue);
            }
        }
    }
}

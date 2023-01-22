using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demos.MovingFloor
{
    public class Floor : MonoBehaviour
    {
        public FloorConfig Config;
        
        public bool UseComputeShader;
        public float scale;
        
        [HideInInspector]
        public List<Transform> Targets;
        [HideInInspector]
        public FloorObject[] Floors;

        private void Start()
        {
            Floors = GetComponentsInChildren<FloorObject>();
        }
        
        private void LateUpdate()
        {
            if (UseComputeShader) return;
            
            //UpdateFloorFromTexture();
            UpdateFloor();
        }


        private void UpdateFloorFromTexture()
        {
            for (var i = Floors.Length - 1; i >= 0; i--)
            {
                float x = Floors[i].transform.position.x * scale;
                float y = Floors[i].transform.position.z * scale;
                
                Floors[i].SetValue(Mathf.PerlinNoise(x, y));
            }
        }

        private Transform _tmpTarget;
        private void UpdateFloor()
        {
            for (var i = Floors.Length - 1; i >= 0; i--)
            {
                _tmpTarget = Targets
                    .Where(t => Vector3.Distance(Floors[i].transform.position, t.transform.position) < Config.Distance.MaxValue)
                    .OrderBy(t => Vector3.Distance(Floors[i].transform.position, t.transform.position))
                    .FirstOrDefault();

                if (_tmpTarget == null)
                {
                    Floors[i].SetValue(1);
                    continue;
                }
			
                var distance = Vector3.Distance(Floors[i].transform.position, _tmpTarget.transform.position) - Config.Distance.MinValue;
                var currentValue = Mathf.Clamp(distance / (Config.Distance.MaxValue - Config.Distance.MinValue), 0, 1);
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
                Gizmos.DrawWireSphere(target.transform.position, Config.Distance.MinValue);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(target.transform.position, Config.Distance.MaxValue);
            }
        }
    }
}

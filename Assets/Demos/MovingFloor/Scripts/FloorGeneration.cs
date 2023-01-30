using UnityEngine;

namespace Demos.MovingFloor
{
    [RequireComponent(typeof(BoxCollider))]
    public class FloorGeneration : MonoBehaviour
    {
        [SerializeField]
        private FloorObject floorPrefab;

        private void Awake()
        {
            var bounds = GetComponent<BoxCollider>().bounds;

            for (var x = bounds.min.x+ 0.5f; x < bounds.max.x; x++)
            for (var z = bounds.min.z + 0.5f; z < bounds.max.z; z++)
            {
                var floorObject = Instantiate(floorPrefab, Vector3.zero, transform.rotation, transform);
                floorObject.transform.localPosition = new Vector3(x, 0, z);
            }
        }
    }
}

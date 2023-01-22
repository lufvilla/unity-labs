using Demos.MovingFloor;
using UnityEngine;

[RequireComponent(typeof(Floor), typeof(BoxCollider))]
public class FloorGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject floorPrefab;

    private void Awake()
    {
        var bounds = GetComponent<BoxCollider>().bounds;
        for (var x = bounds.min.x+ 0.5f; x < bounds.max.x; x++)
            for (var z = bounds.min.z+0.5f; z < bounds.max.z; z++)
                Instantiate(floorPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
    }
}

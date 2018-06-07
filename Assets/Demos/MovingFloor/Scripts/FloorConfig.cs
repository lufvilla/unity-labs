using UnityEngine;

[CreateAssetMenu(menuName = "Akane/Environment/Floor")]
public class FloorConfig : ScriptableObject
{
    [Header("Target")]
    public string TargetTag = "Player";

    [Header("Invert")]
    public bool Invert = true;

    [Header("Distance")]
    public float MaxDistance = 2;
    public float MinDistance = 1;

    public Vector3 Direction = Vector3.up;
}

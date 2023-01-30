using Framework.RangedFloat;
using UnityEngine;

namespace Demos.MovingFloor
{
    [CreateAssetMenu(menuName = "Demos/Moving Floor/Floor Config")]
    public class FloorConfig : ScriptableObject
    {
        [Header("Distance")]
        [MinMaxRange(0, 50)]
        public RangedFloat Distance = new() {MinValue = 2, MaxValue = 15};

        [Header("Direction")]
        public Vector3 Direction = Vector3.up;
        [Range(-1, 1)]
        public int Multiplier = 1;

        [Header("Color")]
        public Gradient Color;
        public AnimationCurve Curve = AnimationCurve.Constant(0f, 1f, 1);
    }
}

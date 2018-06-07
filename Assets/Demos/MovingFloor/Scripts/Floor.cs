using UnityEngine;

public class Floor : MonoBehaviour
{
    public FloorConfig Config;
    public GameObject Child;
    public Gradient Gradient;

    private GameObject _target;
    private Vector3 _initialPosition;
    private float _currentValue;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag(Config.TargetTag);
        _initialPosition = Child.transform.localPosition;
    }

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, _target.transform.position) - Config.MinDistance;

        _currentValue = Mathf.Clamp(distance / (Config.MaxDistance - Config.MinDistance), 0, 1) * (Config.Invert ? -1 : 1);
        
        Child.transform.localPosition = _initialPosition + Config.Direction * _currentValue;
    }
}

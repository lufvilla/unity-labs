using UnityEngine;

public class BeltRandom : MonoBehaviour
{
    [SerializeField]
    private float speed;
    
    private Rigidbody _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 pos = _rb.position;
        _rb.position += -transform.forward * (speed * Time.fixedDeltaTime);
        _rb.MovePosition(pos);
    }
}

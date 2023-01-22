using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float Speed = 5f;

    private float _horizontal;
    private float _vertical;
        
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        
        if(_horizontal == 0 && _vertical == 0) return;
        
        transform.position += new Vector3(_horizontal, 0, _vertical).normalized * Speed * Time.deltaTime;
    }
}

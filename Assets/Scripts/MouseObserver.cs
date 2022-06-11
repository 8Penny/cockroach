using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObserver : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    
    private Vector3 _mousePosition;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        _mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
    }
    
    void FixedUpdate()
    {
        if (_rb != null)
        {
            _rb.MovePosition(_mousePosition);
        }
    }
}

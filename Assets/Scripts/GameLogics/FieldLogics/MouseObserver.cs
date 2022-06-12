using System;
using Services.Stats;
using Services.Updater;
using UnityEngine;
using Zenject;

public class MouseObserver : MonoBehaviour, IUpdatable, IFixedUpdatable
{
    [SerializeField]
    private Rigidbody2D _rigidbody;

    [Inject]
    public IStatsUpdater _statsService;
    [Inject]
    public UpdateService _updateService;
    
    private Vector3 _mousePosition;

    public void OnEnable()
    {
        _updateService.Register(this);
    }

    public void OnDisable()
    {
        _updateService.Unregister(this);
    }

    public void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        _mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
    }
    
    public void FixedUpdate()
    {
        if (_rigidbody != null)
        {
            _rigidbody.MovePosition(_mousePosition);
            _statsService.UpdateTargetPosition(_mousePosition);
        }
    }
}

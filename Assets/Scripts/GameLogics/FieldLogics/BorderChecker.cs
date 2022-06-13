using System;
using GameLogics.FieldLogics;
using Services.Stats;
using Services.Updater;
using UnityEngine;
using Zenject;

public class BorderChecker : MonoBehaviour, IFieldTrigger, IUpdatable
{
    [Inject]
    public IFieldObserver _fieldObserver;
    [Inject]
    public UpdateService _updateService;

    [Inject]
    public IStats _stats;

    [SerializeField]
    private BoxCollider2D _collider;


    private bool _inField = false;
    public event Action OnPlayerEnter;
    public event Action OnPlayerExit;

    public void Awake()
    {
        _fieldObserver.Register(this);
        
    }

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
        bool inside = _collider.bounds.Contains(_stats.TargetPosition);
        if (inside == _inField)
        {
            return;
        }

        _inField = inside;
        if (_inField)
        {
            OnPlayerEnter?.Invoke();
        }
        else
        {
            OnPlayerExit?.Invoke();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //OnPlayerEnter?.Invoke();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //OnPlayerExit?.Invoke();
    }

    public void OnDestroy()
    {
        _fieldObserver.Unregister(this);
    }

}

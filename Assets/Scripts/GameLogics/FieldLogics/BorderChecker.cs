using System;
using GameLogics.FieldLogics;
using Services.Stats;
using Services.Updater;
using UnityEngine;
using Zenject;

public class BorderChecker : MonoBehaviour, IFieldTrigger, IUpdatable
{
    private IFieldObserver _fieldObserver;
    private UpdateService _updateService;
    private IStats _stats;

    [SerializeField]
    private BoxCollider2D _collider;
    
    private bool _inField = false;
    public event Action OnPlayerEnter;
    public event Action OnPlayerExit;
    
    [Inject]
    public void Init(IFieldObserver fieldObserver,UpdateService updateService,IStats stats)
    {
        _fieldObserver = fieldObserver;
        _updateService = updateService;
        _stats = stats;
    }

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
        bool inside = _collider.bounds.Contains(_stats.PlayerTargetPosition);
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

    public void OnDestroy()
    {
        _fieldObserver.Unregister(this);
    }

}

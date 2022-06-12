using System;
using GameLogics.FieldLogics;
using Services.Stats;
using UnityEngine;
using Zenject;

public class BorderChecker : MonoBehaviour, IFieldTrigger
{
    [Inject]
    public IFieldObserver _fieldObserver;
    public event Action OnPlayerEnter;
    public event Action OnPlayerExit;

    public void Awake()
    {
        _fieldObserver.Register(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        OnPlayerEnter?.Invoke();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        OnPlayerExit?.Invoke();
    }

    public void OnDestroy()
    {
        _fieldObserver.Unregister(this);
    }
}

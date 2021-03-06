using Services.Stats;
using Services.Updater;
using UnityEngine;
using Zenject;

public class MouseObserver : MonoBehaviour, IUpdatable
{
    private IStatsUpdater _statsService;
    private UpdateService _updateService;
    
    private Vector3 _mousePosition;
    private Camera _camera;

    [Inject]
    public void Init(IStatsUpdater stats, UpdateService updateService)
    {
        _statsService = stats;
        _updateService = updateService;
    }

    private void Awake()
    {
        _camera = Camera.main;
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
        Vector3 mousePos = Input.mousePosition;

        _mousePosition = _camera.ScreenToWorldPoint(mousePos);
        _mousePosition.z = 0;
        
        
        _statsService.UpdateTargetPosition(_mousePosition);
    }
}

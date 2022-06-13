using GameLogics.CockroachLogics;
using Services.Game;
using Services.Stats;
using Services.Updater;
using Settings;
using UnityEngine;
using Views;
using Zenject;

namespace Services.Cockroach
{
    public class CockroachManager : MonoBehaviour, ICockroachManager
    {
        private SettingsContainer _container;
        private IStats _stats;
        private UpdateService _updateService;
        private GameStateService _gameStateService;

        private SimpleCockroachBehaviour _behaviour;
        
        
        [Inject]
        public void Init(GameStateService gameStateService, UpdateService updateService, IStats stats, SettingsContainer container)
        {
            _updateService = updateService;
            _stats = stats;
            _container = container;
            _gameStateService = gameStateService;
            //_diContainer = diContainer;
            
            
        }
        public void Setup(Transform start, Transform finish)
        {
            var cockroach = Instantiate(_container.CockroachPrefab);
            var view = cockroach.GetComponent<CockroachView>();
            var stateMachine = new SimpleCockroachStateMachine(_stats, _container.CockroachSettings, finish, view.RigidBody);
            _behaviour = new SimpleCockroachBehaviour(view, _container.CockroachSettings, finish, stateMachine);
            cockroach.transform.position = start.position;
            
            _behaviour.Activate();
            
            _updateService.Register(_behaviour);
        }

        public void Reset()
        {
            _behaviour.Deactivate();
            _updateService.Unregister(_behaviour);
        }
    }
}
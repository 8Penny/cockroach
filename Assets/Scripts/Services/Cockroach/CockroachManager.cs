using GameLogics.CockroachLogics;
using GameLogics.FieldLogics;
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
        private FieldContainer _fieldContainer;

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
        public void Setup(FieldContainer fieldContainer)
        {
            _fieldContainer = fieldContainer;
            var cockroach = Instantiate(_container.CockroachPrefab);
            var view = cockroach.GetComponent<CockroachView>();
            var stateMachine = new SimpleCockroachStateMachine(_fieldContainer,_stats, _container.CockroachSettings, fieldContainer.FinishPoint.transform, view.RigidBody);
            _behaviour = new SimpleCockroachBehaviour(view, _container.CockroachSettings, fieldContainer.FinishPoint.transform, stateMachine);
            cockroach.transform.position = fieldContainer.StartPoint.transform.position;
            
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
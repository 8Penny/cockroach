using System.Collections.Generic;
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

        private List<SimpleCockroachBehaviour> _behaviours = new List<SimpleCockroachBehaviour>();

        [Inject]
        public void Init(GameStateService gameStateService, UpdateService updateService, IStats stats, SettingsContainer container)
        {
            _updateService = updateService;
            _stats = stats;
            _container = container;
        }
        public void Setup(FieldContainer fieldContainer)
        {
            if (_behaviours.Count == 0)
            {
                var cockroach = Instantiate(_container.CockroachPrefab);
            
                var view = cockroach.GetComponent<CockroachView>();
                var stateMachine = new SimpleCockroachStateMachine(_stats, _container.CockroachSettings,
                    fieldContainer.FinishPoint.transform, view.RigidBody, view.Collider);
                var behaviour = new SimpleCockroachBehaviour(view, _container.CockroachSettings, stateMachine);
                _behaviours.Add(behaviour);
            }
            
            ActivateAll(fieldContainer.StartPoint.transform.position);
        }

        private void ActivateAll(Vector3 startPosition)
        {
            for (int i = 0; i < _behaviours.Count; i++)
            {
                var behaviour = _behaviours[i];
                behaviour.View.transform.position =  startPosition;
                behaviour.Activate();
                _updateService.Register(behaviour);
            }
        }

        public void Reset()
        {
            for (int i = 0; i < _behaviours.Count; i++)
            {
                var behaviour = _behaviours[i];
                behaviour.Deactivate();
                _updateService.Unregister(behaviour);
            }
        }
    }
}
using Services.Game;
using Services.Stats;
using Services.Updater;
using UnityEngine;
using Views;
using Zenject;

namespace GameLogics.FieldLogics
{
    public class PlayerController : IFixedUpdatable, IUpdatable
    {
        [Inject]
        public IStats _stats;
        
        [Inject]
        public GameStateService _gameStateService;

        private bool _wasInited;
        private PlayerView _view;

        private bool _isActive => _stats.IsPlayerActive && _gameStateService.CurrentGameState == GameState.Started;
        
        public void Update()
        {
            _view.gameObject.SetActive(_isActive);
        }

        public void FixedUpdate(float deltaTime)
        {
            if (!_wasInited)
            {
                return;
            }

            if (_isActive)
            {
                _view.RigidBody.MovePosition(_stats.PlayerTargetPosition);
            }
        }

        public void Setup(PlayerView view)
        {
            _view = view;
            if (_view == null)
            {
                return;
            }
            _view.gameObject.SetActive(true);
            _stats.ModifiersUpdated += ModifiersUpdatedHandler;
            ModifiersUpdatedHandler();
            _wasInited = true;
        }

        public void Reset()
        {
            _view.gameObject.SetActive(false);
            _stats.ModifiersUpdated -= ModifiersUpdatedHandler;
            _wasInited = false;
        }

        private void ModifiersUpdatedHandler()
        {
            if (_view != null)
            {
                _view.ImpactCircle.transform.localScale = new Vector3(_stats.PlayerRadius, _stats.PlayerRadius, 0);
            }
        }
    }
}
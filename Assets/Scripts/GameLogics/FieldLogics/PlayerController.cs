using Services.Game;
using Services.Stats;
using Services.Updater;
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

        public void FixedUpdate()
        {
            if (!_wasInited)
            {
                return;
            }

            if (_isActive)
            {
                _view.RigidBody.MovePosition(_stats.TargetPosition);
            }
        }

        public void Setup(PlayerView view)
        {
            _view = view;
            if (_view == null)
            {
                return;
            }
            _wasInited = true;
            _view.gameObject.SetActive(true);
        }

        public void Reset()
        {
            _view.gameObject.SetActive(false);
            _wasInited = false;
        }
    }
}
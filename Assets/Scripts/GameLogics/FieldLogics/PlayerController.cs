using Services.Game;
using Services.Stats;
using Services.Updater;
using Views;
using Zenject;

namespace GameLogics.FieldLogics
{
    public class PlayerController : IFixedUpdatable
    {
        [Inject]
        public IStats _stats;
        
        [Inject]
        public GameStateService _gameStateService;
        
        private bool _wasInited;
        private PlayerView _view;

        public void FixedUpdate()
        {
            if (!_wasInited)
            {
                return;
            }

            if (_stats.IsPlayerActive && _gameStateService.CurrentGameState == GameState.Started)
            {
                _view.RigidBody.MovePosition(_stats.TargetPosition);
            }
        }

        public void Setup(PlayerView view)
        {
            if (_view == null)
            {
                return;
            }
            _view = view;
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
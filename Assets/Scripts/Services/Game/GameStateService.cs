using System;

namespace Services.Game
{
    public class GameStateService
    {
        private GameState _currentGameState;
        
        public event Action OnGameStateChanged;
        public GameState CurrentGameState => _currentGameState;

        public void SetGameState(GameState state)
        {
            if (state == _currentGameState)
            {
                return;
            }
            _currentGameState = state;
            OnGameStateChanged?.Invoke();
        }
    }
}
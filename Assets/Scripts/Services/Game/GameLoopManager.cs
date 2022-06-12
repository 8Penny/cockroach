using System;
using System.Collections.Generic;
using GameLogics.FieldLogics;
using Views;

namespace Services.Game
{
    public class GameLoopManager : IDisposable
    {
        private GameStateService _gameStateService;
        private PlayerController _player;
        private PlayerView _playerView;

        private Dictionary<GameState, Action> _actions = new Dictionary<GameState, Action>();
        private bool _wasPaused;

        //todo: create player factory
        public GameLoopManager(GameStateService gameStateService, PlayerController player, PlayerView view)
        {
            _gameStateService = gameStateService;
            _gameStateService.OnGameStateChanged += OnStateChanged;

            _player = player;
            _playerView = view;
            
            _actions.Add(GameState.Finished, OnFinish);
            _actions.Add(GameState.Started, OnStart);
            _actions.Add(GameState.Paused, OnPause);
        }

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnStateChanged;
        }

        private void OnStateChanged()
        {
            _actions[_gameStateService.CurrentGameState].Invoke();
        }

        private void OnStart()
        {
            if (_wasPaused)
            {
                //todo: unfreeze game
                return;
            }

            Setup();
        }

        private void OnPause()
        {
            //todo: freeze game
            _wasPaused = true;
        }

        private void OnFinish()
        {
            Reset();
        }
        
        private void Reset()
        {
            _player.Reset();
        }
        
        private void Setup()
        {
            _player.Setup(_playerView);
        }
    }
}
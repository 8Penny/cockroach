using System;
using System.Collections.Generic;
using GameLogics.FieldLogics;
using Services.Cockroach;
using Services.Updater;
using Views;

namespace Services.Game
{
    public class GameLoopManager : IDisposable
    {
        private GameStateService _gameStateService;
        private UpdateService _updateService;
        private ICockroachManager _cockroachManager;

        private FieldContainer _fieldContainer;
        private PlayerController _player;
        private PlayerView _playerView;

        private Dictionary<GameState, Action> _actions = new Dictionary<GameState, Action>();
        private bool _wasPaused;

        //todo: create player factory
        public GameLoopManager(GameStateService gameStateService, UpdateService updateService, PlayerController player,
            PlayerView view, ICockroachManager cockroachManager)
        {
            _updateService = updateService;
            _gameStateService = gameStateService;
            _cockroachManager = cockroachManager;
            
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
            Reset();
        }
        
        public void RegisterFieldContainer(FieldContainer fieldContainer){
            _fieldContainer = fieldContainer;
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
            _updateService.Unregister(_player);
            _cockroachManager.Reset();
        }
        
        private void Setup()
        {
            _player.Setup(_playerView);
            _updateService.Register(_player);
            _cockroachManager.Setup(_fieldContainer);
        }
    }
}
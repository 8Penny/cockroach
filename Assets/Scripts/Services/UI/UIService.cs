using System;
using System.Collections.Generic;
using Services.Game;

namespace Services.UI
{
    public class UIService : IDisposable
    {
        private GameStateService _gameStateService;

        private Dictionary<UIPanelType, UIPanelView> _uiPanels = new Dictionary<UIPanelType, UIPanelView>();

        public UIService(GameStateService gameStateService)
        {
            _gameStateService = gameStateService;

            _gameStateService.OnGameStateChanged += GameStateChangeHandler;
        }

        public void Register(UIPanelView view)
        {
            if (_uiPanels.ContainsKey(view.Type))
            {
                return;
            }

            _uiPanels[view.Type] = view;
        }

        public void Unregister(UIPanelView view)
        {
            if (!_uiPanels.ContainsKey(view.Type))
            {
                return;
            }

            _uiPanels.Remove(view.Type);
        }

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= GameStateChangeHandler;
        }

        private void GameStateChangeHandler()
        {
            if (_gameStateService.CurrentGameState == GameState.Started)
            {
                EnablePanel(true);
                return;
            }
            
            if (_gameStateService.CurrentGameState == GameState.Finished)
            {
                EnablePanel(false);
            }
        }

        private void EnablePanel(bool isHUD)
        {
            _uiPanels[UIPanelType.HUD].gameObject.SetActive(isHUD);
            _uiPanels[UIPanelType.Menu].gameObject.SetActive(!isHUD);
        }

        public void OnStartButtonClicked()
        {
            _gameStateService.SetGameState(GameState.Started);
        }
    }
}
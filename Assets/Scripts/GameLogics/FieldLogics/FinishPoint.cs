using Services.Game;
using UnityEngine;
using Zenject;

namespace GameLogics.FieldLogics
{
    public class FinishPoint : MonoBehaviour
    {
        private GameStateService _gameStateService;
        
        [Inject]
        public void Init(GameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(EntityTag.Cockroach))
            {
                _gameStateService.SetGameState(GameState.Finished);
            }
        }
    }
}
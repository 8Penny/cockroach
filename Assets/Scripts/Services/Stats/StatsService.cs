using GameLogics.FieldLogics;
using UnityEngine;

namespace Services.Stats
{
    public class StatsService : IStats, IStatsUpdater, IFieldObserver
    {
        private Vector3 _targetPosition;
        private bool _isPlayerActive;
        private float _cockroachSpeedModifier;
        private float _playerRadiusRatio;
        
        public Vector3 TargetPosition => _targetPosition;
        public bool IsPlayerActive => _isPlayerActive;
        public float CockroachSpeedModifier => _cockroachSpeedModifier;
        public float PlayerRadiusRatio => _playerRadiusRatio;
        
        public void UpdateTargetPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        public void Register(IFieldTrigger trigger)
        {
            trigger.OnPlayerEnter += PlayerEnterHandler;
            trigger.OnPlayerExit += PlayerExitHandler;
        }
        
        public void Unregister(IFieldTrigger trigger)
        {
            trigger.OnPlayerEnter -= PlayerEnterHandler;
            trigger.OnPlayerExit -= PlayerExitHandler;
        }

        private void PlayerEnterHandler()
        {
            _isPlayerActive = true;
        }
        
        private void PlayerExitHandler()
        {
            _isPlayerActive = false;
        }
    }
}
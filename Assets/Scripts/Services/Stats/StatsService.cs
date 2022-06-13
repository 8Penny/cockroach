using System;
using GameLogics.FieldLogics;
using UnityEngine;

namespace Services.Stats
{
    public class StatsService : IStats, IStatsUpdater, IFieldObserver
    {
        private const float EPSILON = 0.001f;
        private Vector3 _targetPosition;
        private bool _isPlayerActive;
        private float _cockroachSpeedModifier;
        private float _playerRadius;
        
        public Vector3 PlayerTargetPosition => _targetPosition;
        public bool IsPlayerActive => _isPlayerActive;
        public float CockroachSpeedModifier => _cockroachSpeedModifier;
        public float PlayerRadius => _playerRadius;
        public event Action ModifiersUpdated;

        public void UpdateTargetPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        public void UpdatePlayerRadius(float value)
        {
            if (Math.Abs(value - _playerRadius) < EPSILON)
            {
                return;
            }
            
            _playerRadius = value;
            ModifiersUpdated?.Invoke();
        }

        public void UpdateSpeedModifier(float value)
        {
            if (Math.Abs(value - _cockroachSpeedModifier) < EPSILON)
            {
                return;
            }
            
            _cockroachSpeedModifier = value;
            ModifiersUpdated?.Invoke();
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
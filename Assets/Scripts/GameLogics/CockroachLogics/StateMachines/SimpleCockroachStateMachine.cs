using GameLogics.FieldLogics;
using Services.Stats;
using Settings;
using UnityEngine;

namespace GameLogics.CockroachLogics
{
    public class SimpleCockroachStateMachine : BaseCockroachStateMachine
    {
        private IStats _stats;
        private CockroachSettings _settings;
        private Transform _target;
        private Rigidbody2D _rigidbody;
        private FieldContainer _fieldContainer;

        private float _currentSpeed;
        
        public SimpleCockroachStateMachine(FieldContainer fieldContainer, IStats stats, CockroachSettings settings, Transform target, Rigidbody2D rigidbody) : base()
        {
            _stats = stats;
            _settings = settings;
            _target = target;
            _rigidbody = rigidbody;
            _fieldContainer = fieldContainer;
        }

        protected override void MoveToTarget(float deltaTime)
        {
            var position = _rigidbody.position;
            var toVector = (GetTarget2dPosition(_target.position) - position).normalized;
            MoveToPosition(position + toVector * _settings.Speed * _stats.CockroachSpeedModifier  * deltaTime);
        }

        protected override void MoveFromPlayer(float deltaTime)
        {
            if (!_stats.IsPlayerActive)
            {
                SetState(CockroachState.ToTarget);
                return;
            }
            
            var position = _rigidbody.position;
            var toVector = (position - GetTarget2dPosition(_stats.PlayerTargetPosition)).normalized;

            var newPosition = position + toVector * _currentSpeed * _stats.CockroachSpeedModifier * deltaTime;
            if (!_fieldContainer.Field.bounds.Contains(newPosition))
            {
                newPosition = position + Vector2.down * _currentSpeed * _stats.CockroachSpeedModifier * deltaTime;
            }
            
            MoveToPosition(newPosition);

            _currentSpeed += _settings.RunAwayAcceleration;
        }

        private void MoveToPosition(Vector3 position)
        {
            _rigidbody.MovePosition(position);
        }

        private Vector2 GetTarget2dPosition(Vector3 pos)
        {
            return new Vector2(pos.x, pos.y);
        }

        protected override void OnStateChanged(CockroachState oldState, CockroachState newState)
        {
            if (newState == CockroachState.FromPlayer)
            {
                _currentSpeed = _settings.Speed;
            }
        }
    }
}
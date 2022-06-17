using Services.Stats;
using Settings;
using UnityEngine;

namespace GameLogics.CockroachLogics
{
    public class SimpleCockroachStateMachine : BaseCockroachStateMachine
    {
        private const float DIRECTION_UPDATE_TIME = 1f;
        private const float BORDER_SHIFT = 0.1f;

        private IStats _stats;
        private CockroachSettings _settings;
        private Transform _target;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;

        private RaycastHit2D[] _results = new RaycastHit2D[2];

        private float _currentSpeed;
        private Vector2 _currentDirection;
        private float _timeToUpdateDirection;
        private bool _nearBorder;
        private float _timeToConstantSpeed;
        
        public SimpleCockroachStateMachine(IStats stats, CockroachSettings settings, Transform target,
            Rigidbody2D rigidbody, Collider2D collider) : base()
        {
            _stats = stats;
            _settings = settings;
            _target = target;
            _rigidbody = rigidbody;
            _collider = collider;
        }

        public override void Setup()
        {
            _currentSpeed = 0;
            _timeToConstantSpeed = _settings.AccelerationTime;
        }

        protected override void MoveToTarget(float deltaTime)
        {
            var targetSpeed = _settings.Speed * _stats.CockroachSpeedModifier;
            if (_timeToConstantSpeed > 0)
            {
                float factor = _timeToConstantSpeed / _settings.AccelerationTime;
                _currentSpeed = Mathf.Lerp(0, targetSpeed, 1 - factor);

                _timeToConstantSpeed -= deltaTime;
            }
            var position = _rigidbody.position;
            var toVector = (GetTarget2dPosition(_target.position) - position).normalized;
            MoveToPosition(position + toVector * _currentSpeed * deltaTime);
        }

        protected override void MoveFromPlayer(float deltaTime)
        {
            var position = _rigidbody.position;
            if (!_stats.IsPlayerActive || Vector2.Distance(_stats.PlayerTargetPosition, position) > _stats.PlayerRadius * 1.5f)
            {
                SetState(CockroachState.ToTarget);
                return;
            }

            if (_timeToUpdateDirection < 0 || HasObstacle())
            {
                UpdateDirectionValue(position);
            }
            else
            {
               _timeToUpdateDirection -= deltaTime; 
            }
            
            var newPosition = position + _currentDirection * _currentSpeed * _stats.CockroachSpeedModifier * deltaTime;
            MoveToPosition(newPosition);

            _currentSpeed += _settings.RunAwayAcceleration;
        }

        private void UpdateDirectionValue(Vector2 position)
        {
            _timeToUpdateDirection = DIRECTION_UPDATE_TIME;
            if (!_nearBorder)
            {
                _currentDirection = (position - GetTarget2dPosition(_stats.PlayerTargetPosition)).normalized;
            }

            if (!HasObstacle())
            {
                return;
            }

            float angle = Vector2.SignedAngle(_results[0].normal, -_currentDirection);

            if (Mathf.Abs(angle) > 70f)
            {
                _currentDirection = Vector2.Reflect(_currentDirection, _results[0].normal);
                return;
            }
            float rotateAngle = angle > 0 ? 90 : -90;
            _currentDirection = Quaternion.Euler(0, 0, rotateAngle) * _results[0].normal;
            _nearBorder = true;
        }

        private bool HasObstacle()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Borders"));
            var resultsCount = _collider.Raycast(_currentDirection, filter, _results, BORDER_SHIFT);
            return resultsCount > 0;
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
            _currentSpeed = _settings.Speed;
            
            if (newState == CockroachState.ToTarget)
            {
                _nearBorder = false;
            }
        }
    }
}
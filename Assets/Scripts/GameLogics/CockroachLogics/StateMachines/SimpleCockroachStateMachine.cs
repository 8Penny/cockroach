using GameLogics.FieldLogics;
using Services.Stats;
using Settings;
using UnityEngine;

namespace GameLogics.CockroachLogics
{
    public class SimpleCockroachStateMachine : BaseCockroachStateMachine
    {
        private const float DIRECTION_UPDATE_TIME = 1f;

        private IStats _stats;
        private CockroachSettings _settings;
        private Transform _target;
        private Rigidbody2D _rigidbody;
        private FieldContainer _fieldContainer;
        private Collider2D _collider;

        private RaycastHit2D[] _results = new RaycastHit2D[2];

        private float _currentSpeed;
        private Vector2 _currentDirection;
        private float _timeToUpdateDirection;
        private bool _nearBorder;
        
        public SimpleCockroachStateMachine(FieldContainer fieldContainer, IStats stats, CockroachSettings settings, Transform target, Rigidbody2D rigidbody) : base()
        {
            _stats = stats;
            _settings = settings;
            _target = target;
            _rigidbody = rigidbody;
            _collider = rigidbody.GetComponent<Collider2D>();
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
            Debug.Log(angle);
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
            var resultsCount = _collider.Raycast(_currentDirection, filter, _results, 0.4f);
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
            if (newState == CockroachState.FromPlayer)
            {
                _currentSpeed = _settings.Speed;
            }
            if (newState == CockroachState.ToTarget)
            {
                _nearBorder = false;
            }
        }
    }
}
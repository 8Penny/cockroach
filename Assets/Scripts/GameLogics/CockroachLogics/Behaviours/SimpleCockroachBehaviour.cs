using Settings;
using UnityEngine;
using Views;

namespace GameLogics.CockroachLogics
{
    public class SimpleCockroachBehaviour : BaseCockroachBehaviour
    {
        private const float INACTIVE_VALUE = -1f;
        private float _returnToTargetMoveStateTime;
        private CockroachSettings _settings;
        public SimpleCockroachBehaviour(CockroachView view, CockroachSettings settings, BaseCockroachStateMachine sm) : base(view, sm)
        {
            _settings = settings;
        }

        public void Activate()
        {
            _stateMachine.SetState(CockroachState.ToTarget);
            _stateMachine.Setup();
            Setup();
        }
        public void Deactivate()
        {
            _stateMachine.SetState(CockroachState.Idle);
            Reset();
        }

        protected override void EnterDangerRadiusHandler()
        {
            base.EnterDangerRadiusHandler();
            _returnToTargetMoveStateTime = INACTIVE_VALUE;
        }

        protected override void ExitDangerRadiusHandler()
        {
            _returnToTargetMoveStateTime = Time.time + _settings.RunAwayTime;
        }

        public override void FixedUpdate(float deltaTime)
        {
            if (_stateMachine.CurrentState == CockroachState.FromPlayer)
            {
                if (_returnToTargetMoveStateTime > 0 && _returnToTargetMoveStateTime < Time.time )
                {
                    _stateMachine.SetState(CockroachState.ToTarget);
                }
            }
            
            base.FixedUpdate(deltaTime);
        }
        
    }
}
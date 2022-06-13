using Settings;
using UnityEngine;
using Views;

namespace GameLogics.CockroachLogics
{
    public class SimpleCockroachBehaviour : BaseCockroachBehaviour
    {
        private float _returnToTargetMoveStateTime;
        private CockroachSettings _settings;
        public SimpleCockroachBehaviour(CockroachView view, CockroachSettings settings, Transform target, BaseCockroachStateMachine sm) : base(view, target, sm)
        {
            _settings = settings;
        }

        public void Activate()
        {
            _stateMachine.SetState(CockroachState.ToTarget);
            Setup();
        }
        public void Deactivate()
        {
            _stateMachine.SetState(CockroachState.Idle);
            Reset();
        }

        protected override void ExitDangerRadiusHandler()
        {
            _returnToTargetMoveStateTime = Time.time + _settings.RunAwayTime;
        }

        public override void FixedUpdate(float deltaTime)
        {
            if (_stateMachine.CurrentState == CockroachState.FromPlayer)
            {
                if (_returnToTargetMoveStateTime >= 0)
                {
                    _returnToTargetMoveStateTime -= deltaTime;
                }
                else
                {
                    _stateMachine.SetState(CockroachState.ToTarget);
                }
            }
            
            base.FixedUpdate(deltaTime);
        }
        
    }
}
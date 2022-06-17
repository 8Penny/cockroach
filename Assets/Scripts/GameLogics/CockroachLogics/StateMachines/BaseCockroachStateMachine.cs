using System;
using System.Collections.Generic;

namespace GameLogics.CockroachLogics
{
    public class BaseCockroachStateMachine
    {
        private Dictionary<CockroachState, Action<float>> _states;

        protected CockroachState _currentState;

        public CockroachState CurrentState => _currentState;


        public BaseCockroachStateMachine()
        {
            _states = new Dictionary<CockroachState, Action<float>>();
            _states[CockroachState.Idle] = Idle;
            _states[CockroachState.ToTarget] = MoveToTarget;
            _states[CockroachState.FromPlayer] = MoveFromPlayer;
        }

        public virtual void Setup()
        {
            
        }

        protected virtual void Idle(float deltaTime)
        {
        }

        protected virtual void MoveToTarget(float deltaTime)
        {
        }

        protected virtual void MoveFromPlayer(float deltaTime)
        {
        }

        public void SetState(CockroachState newState)
        {
            if (newState == _currentState)
            {
                return;
            }

            var old = _currentState;
            _currentState = newState;
            OnStateChanged(old, newState);
        }

        protected virtual void OnStateChanged(CockroachState oldState, CockroachState newState)
        {
        }


        public void Update(float deltaTime)
        {
            _states[_currentState].Invoke(deltaTime);
        }
    }
}
using System;
using System.Collections.Generic;
using Services.Updater;
using UnityEngine;
using Views;

namespace GameLogics.CockroachLogics
{
    public abstract class BaseCockroachBehaviour : IFixedUpdatable
    {
        protected CockroachView _view;
        protected BaseCockroachStateMachine _stateMachine;
        public CockroachView View => _view;

        public BaseCockroachBehaviour(CockroachView view, BaseCockroachStateMachine stateMachine)
        {
            _view = view;

            _stateMachine = stateMachine;
        }

        public void Setup()
        {
            if (_view == null)
            {
                return;
            }

            _view.OnEnterDangerZone += EnterDangerRadiusHandler;
            _view.OnExitDangerZone += ExitDangerRadiusHandler;
        }

        public void Reset()
        {
            _view.OnEnterDangerZone -= EnterDangerRadiusHandler;
            _view.OnExitDangerZone -= ExitDangerRadiusHandler;
        }

        protected virtual void EnterDangerRadiusHandler()
        {
            _stateMachine.SetState(CockroachState.FromPlayer);
        }
        
        protected virtual void ExitDangerRadiusHandler()
        {
            _stateMachine.SetState(CockroachState.ToTarget);
        }

        public virtual void FixedUpdate(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }
    }
}
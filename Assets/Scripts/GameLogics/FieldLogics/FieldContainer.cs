using Services.Game;
using UnityEngine;
using Zenject;

namespace GameLogics.FieldLogics
{
    public class FieldContainer : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D _field;
        [SerializeField]
        private StartPoint _startPoint;
        [SerializeField]
        private FinishPoint _finishPoint;

        public BoxCollider2D Field => _field;
        public StartPoint StartPoint => _startPoint;
        public FinishPoint FinishPoint =>_finishPoint;

        [Inject]
        public void Init(GameLoopManager loopManager)
        {
            loopManager.RegisterFieldContainer(this);
        }
    }
}
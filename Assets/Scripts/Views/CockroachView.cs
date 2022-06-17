using System;
using UnityEngine;

namespace Views
{
    public class CockroachView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Collider2D _collider;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public Rigidbody2D RigidBody => _rigidbody;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Collider2D Collider => _collider;

        public event Action OnEnterDangerZone;
        public event Action OnExitDangerZone;

        public void EnterDangerZone()
        {
            OnEnterDangerZone?.Invoke();
        }
        public void ExitDangerZone()
        {
            OnExitDangerZone?.Invoke();
        }
    }
}
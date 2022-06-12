using UnityEngine;

namespace Views
{
    public class CockroachView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Sprite _sprite;

        public Rigidbody2D RigidBody => _rigidbody;
        public Sprite Sprite => _sprite;
    }
}
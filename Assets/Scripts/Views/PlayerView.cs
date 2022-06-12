using UnityEngine;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private GameObject _impactCircle;

        public Rigidbody2D RigidBody => _rigidbody;
        public GameObject ImpactCircle => _impactCircle;
    }
}
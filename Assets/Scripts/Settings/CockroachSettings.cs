using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "CockroachSettings", menuName = "CockroachSettings")]
    public class CockroachSettings : ScriptableObject
    {
        public float Speed;
        public float RunAwayAcceleration;
        public float RunAwayTime;
        public float AccelerationTime;
    }
}
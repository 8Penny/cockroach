using UnityEngine;

namespace Services.Stats
{
    public interface IStats
    {
        public Vector3 TargetPosition { get; }
        public bool IsPlayerActive  { get; }
        public float CockroachSpeedModifier  { get; }
        public float PlayerRadiusRatio  { get; }
    }
}
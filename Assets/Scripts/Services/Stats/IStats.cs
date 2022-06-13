using System;
using UnityEngine;

namespace Services.Stats
{
    public interface IStats
    {
        public Vector3 PlayerTargetPosition { get; }
        public bool IsPlayerActive  { get; }
        public float CockroachSpeedModifier  { get; }
        public float PlayerRadius  { get; }
        public event Action ModifiersUpdated;
    }
}
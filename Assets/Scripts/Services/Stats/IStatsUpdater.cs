using UnityEngine;

namespace Services.Stats
{
    public interface IStatsUpdater
    {
        void UpdateTargetPosition(Vector3 position);
        void UpdatePlayerRadius(float value);
        void UpdateSpeedModifier(float value);
    }
}
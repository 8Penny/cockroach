using UnityEngine;

namespace Services.Stats
{
    public interface IStatsUpdater
    {
        void UpdateTargetPosition(Vector3 position);
    }
}
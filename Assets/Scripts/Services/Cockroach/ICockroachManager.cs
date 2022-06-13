using UnityEngine;

namespace Services.Cockroach
{
    public interface ICockroachManager
    {
        void Setup(Transform start, Transform finish);
        void Reset();
    }
}
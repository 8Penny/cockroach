using GameLogics.FieldLogics;
using UnityEngine;

namespace Services.Cockroach
{
    public interface ICockroachManager
    {
        void Setup(FieldContainer field);
        void Reset();
    }
}
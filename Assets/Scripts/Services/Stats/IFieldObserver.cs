using GameLogics.FieldLogics;
using UnityEngine;

namespace Services.Stats
{
    public interface IFieldObserver
    {
        void Register(IFieldTrigger trigger);
        void Unregister(IFieldTrigger trigger);
    }
}
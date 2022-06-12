using System;

namespace GameLogics.FieldLogics
{
    public interface IFieldTrigger
    {
        event Action OnPlayerEnter;
        event Action OnPlayerExit;
    }
}
using Core;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public MinMaxValue PlayerRadius;
        public MinMaxValue CockroachSpeedModifier;
    }
}
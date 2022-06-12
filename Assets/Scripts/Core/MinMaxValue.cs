using System;

namespace Core
{
    [Serializable]
    public struct MinMaxValue
    {
        public float MaxValue;
        public float MinValue;
        
        public MinMaxValue(float min, float max)
        {
            MinValue = min;
            MaxValue = max;
        }
    }
}
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public interface IGraphVisualObject
    {
        void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText);
        void Destroy();
    }
}
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public interface IGraphVisual
    {
        IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText);
    }
}


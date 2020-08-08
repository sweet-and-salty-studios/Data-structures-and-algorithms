using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class BarChartVisualObject : IGraphVisualObject
    {
        private GameObject barGameObject;
        private readonly float barWidthMultiplier;

        public BarChartVisualObject(GameObject barGameObject, float barWidthMultiplier)
        {
            this.barGameObject = barGameObject;
            this.barWidthMultiplier = barWidthMultiplier;
        }

        public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {
            var newRectTransform = barGameObject.GetComponent<RectTransform>();
            newRectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
            newRectTransform.sizeDelta = new Vector2(graphPositionWidth * barWidthMultiplier, graphPosition.y);

            var interactableDisplay = barGameObject.AddComponent<InteractableDisplay>();

            interactableDisplay.OnPointerEnter_Action = () =>
            {
                UIManager.Instance.GraphPanel.Tooltip.ShowTooltip(tooltipText, graphPosition);
            };

            interactableDisplay.OnPointerExit_Action = () =>
            {
                UIManager.Instance.GraphPanel.Tooltip.HideTooltip();
            };
        }

        public void Destroy()
        {
            if(barGameObject != null)
            {
                Object.Destroy(barGameObject);
            }
        }
    }
}
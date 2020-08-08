using UnityEngine;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class BarChartVisual : IGraphVisual
    {
        private readonly RectTransform graphContainer;
        private readonly float barWidthMultiplier;
        private readonly Sprite barSprite;
        private readonly Color32 barColor;

        public BarChartVisual(RectTransform graphContainer, float barWidthMultiplier, Sprite barSprite, Color32 barColor)
        {
            this.graphContainer = graphContainer;
            this.barWidthMultiplier = barWidthMultiplier;
            this.barSprite = barSprite;
            this.barColor = barColor;
        }

        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {
            var barGameObject = CreateBar(new Vector2(graphPosition.x, graphPosition.y), graphPositionWidth * barWidthMultiplier);

            var barChartVisualObject = new BarChartVisualObject(barGameObject, barWidthMultiplier);
            barChartVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth, tooltipText);

            return barChartVisualObject;
        }

        private GameObject CreateBar(Vector2 graphsPosition, float barWidth)
        {
            var newBarGameObject = new GameObject("Bar", typeof(Image));

            newBarGameObject.transform.SetParent(graphContainer, false);

            var image = newBarGameObject.GetComponent<Image>();
            image.sprite = barSprite;
            image.type = Image.Type.Tiled;
            image.color = barColor;

            var newRectTransform = newBarGameObject.GetComponent<RectTransform>();
            newRectTransform.anchoredPosition = new Vector2(graphsPosition.x, 0f);
            newRectTransform.sizeDelta = new Vector2(barWidth, graphsPosition.y);
            newRectTransform.anchorMin = Vector2.zero;
            newRectTransform.anchorMax = Vector2.zero;
            newRectTransform.pivot = new Vector2(0.5f, 0);

            return newBarGameObject;
        }
    }
}


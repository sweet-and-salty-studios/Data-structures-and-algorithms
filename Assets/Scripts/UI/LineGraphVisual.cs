using UnityEngine;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class LineGraphVisual : IGraphVisual
    {
        #region VARIABLES

        private readonly RectTransform graphContainer;
        private readonly Vector2 dotSize;
        private readonly Sprite dotSprite;
        private readonly Color32 dotColor;
        private readonly Color32 dotConectionColor;

        private LineGraphVisualObject previous_LineGraphVisualObject;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region CONSTRUCTORS

        public LineGraphVisual(RectTransform graphContainer,Vector2 dotSize, Sprite dotSprite, Color32 dotColor, Color32 dotConectionColor)
        {
            this.graphContainer = graphContainer;
            this.dotSprite = dotSprite;
            this.dotColor = dotColor;
            this.dotConectionColor = dotConectionColor;
            this.dotSize = dotSize;

            previous_LineGraphVisualObject = null;
        }

        #endregion CONSTRUCTORS

        #region CUSTOM_FUNCTIONS

        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {
            var dotGameObject = CreateDot(new Vector2(graphPosition.x, graphPosition.y));  

            GameObject newDotConnectionGameObject = null;

            if(previous_LineGraphVisualObject != null)
            {
                newDotConnectionGameObject = CreateDotConnection(
                    previous_LineGraphVisualObject.GetGraphPosition(),
                    dotGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            var lineGraphVisualObject = new LineGraphVisualObject(dotGameObject, newDotConnectionGameObject, previous_LineGraphVisualObject);
            lineGraphVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth, tooltipText);

            previous_LineGraphVisualObject = lineGraphVisualObject;

            return lineGraphVisualObject;
        }

        private GameObject CreateDot(Vector2 anchoredPosition)
        {
            var newDotGameObject = new GameObject("Dot", typeof(Image));

            newDotGameObject.transform.SetParent(graphContainer, false);

            var image = newDotGameObject.GetComponent<Image>();
            image.sprite = dotSprite;
            image.color = dotColor;

            var newRectTransform = newDotGameObject.GetComponent<RectTransform>();
            newRectTransform.anchoredPosition = anchoredPosition;
            newRectTransform.sizeDelta = new Vector2(dotSize.x, dotSize.y);
            newRectTransform.anchorMin = Vector2.zero;
            newRectTransform.anchorMax = Vector2.zero;

            newDotGameObject.AddComponent<InteractableDisplay>();

            return newDotGameObject;
        }

        private GameObject CreateDotConnection(Vector2 dotPosition_A, Vector2 dotPosition_B)
        {
            var newDotConnectionGameObject = new GameObject("DotConnection", typeof(Image));
            newDotConnectionGameObject.transform.SetParent(graphContainer, false);

            newDotConnectionGameObject.GetComponent<Image>().color = dotConectionColor;
            var direction = (dotPosition_B - dotPosition_A).normalized;
            var distance = Vector2.Distance(dotPosition_A, dotPosition_B);

            var newRectTransform = newDotConnectionGameObject.GetComponent<RectTransform>();
            newRectTransform.sizeDelta = new Vector2(distance, 4);
            newRectTransform.anchorMin = Vector2.zero;
            newRectTransform.anchorMax = Vector2.zero;
            newRectTransform.anchoredPosition = dotPosition_A + direction * distance * 0.5f;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newRectTransform.localEulerAngles = new Vector3(0, 0, angle);

            return newDotConnectionGameObject;
        }
    }

    #endregion CUSTOM_FUNCTIONS
}


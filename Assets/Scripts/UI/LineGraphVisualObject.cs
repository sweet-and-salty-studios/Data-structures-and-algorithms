using System;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class LineGraphVisualObject : IGraphVisualObject
    {
        #region VARIABLES

        public event EventHandler OnGraphVisualInfoChanged;

        private readonly GameObject dotGameObject;
        private readonly GameObject dotConnectionObject;
        private readonly LineGraphVisualObject previous_LineGraphVisualObject;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region CONSTRUCTORS

        public LineGraphVisualObject(GameObject dotGameObject, GameObject dotConnectionObject, LineGraphVisualObject previous_LineGraphVisualObject)
        {
            this.dotGameObject = dotGameObject;
            this.dotConnectionObject = dotConnectionObject;
            this.previous_LineGraphVisualObject = previous_LineGraphVisualObject;

            if(previous_LineGraphVisualObject != null)
            {
                previous_LineGraphVisualObject.OnGraphVisualInfoChanged += Previous_LineGraphVisualObject_OnGraphVisualInfoChanged;
            }
        }

        #endregion CONSTRUCTORS

        #region CUSTOM_FUNCTIONS

        private void Previous_LineGraphVisualObject_OnGraphVisualInfoChanged(object sender, EventArgs e)
        {
            UpdateConnetion();
        }

        public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {
            var rectTransform = dotGameObject.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = graphPosition;

            UpdateConnetion();

            var interactableDisplay = dotGameObject.GetComponent<InteractableDisplay>();

            interactableDisplay.OnPointerEnter_Action += () =>
            {
                UIManager.Instance.GraphPanel.Tooltip.ShowTooltip(tooltipText, graphPosition);
            };

            interactableDisplay.OnPointerExit_Action += () =>
            {
                UIManager.Instance.GraphPanel.Tooltip.HideTooltip();
            };

            OnGraphVisualInfoChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateConnetion()
        {
            if(dotConnectionObject != null)
            {
                var newRectTransform = dotConnectionObject.GetComponent<RectTransform>();
                var direction = (previous_LineGraphVisualObject.GetGraphPosition() - GetGraphPosition()).normalized;
                var distance = Vector2.Distance(GetGraphPosition(), previous_LineGraphVisualObject.GetGraphPosition());

                newRectTransform.sizeDelta = new Vector2(distance, 4);
                newRectTransform.anchorMin = Vector2.zero;
                newRectTransform.anchorMax = Vector2.zero;
                newRectTransform.anchoredPosition = GetGraphPosition() + direction * distance * 0.5f;

                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                newRectTransform.localEulerAngles = new Vector3(0, 0, angle);
            }
        }

        public void Destroy()
        {
            if(dotGameObject != null)
            {
                UnityEngine.Object.Destroy(dotConnectionObject);
            }

            if(dotConnectionObject != null)
            {
                UnityEngine.Object.Destroy(dotGameObject);
            }

        }

        public Vector2 GetGraphPosition()
        {         
            var rectTransform = dotGameObject.GetComponent<RectTransform>();
            return rectTransform.anchoredPosition;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}


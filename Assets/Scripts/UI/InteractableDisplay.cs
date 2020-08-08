using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class InteractableDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region VARIABLES

        public Action OnPointerEnter_Action = delegate { };
        public Action OnPointerExit_Action = delegate { };

        private Color32 defaultColor;
        private Color32 highlightColor;

        private RectTransform rectTransform;

        #endregion VARIABLES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            defaultColor = GetComponentInChildren<Image>().color;

            highlightColor = new Color32(255, 0, 255, 255);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnter_Action.Invoke();

            LeanTween.color(rectTransform, highlightColor, 0f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExit_Action.Invoke();

            LeanTween.color(rectTransform, defaultColor, 0f);
        }

        #endregion UNITY_FUNCTIONS
    }
}


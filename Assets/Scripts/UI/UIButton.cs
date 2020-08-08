using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class UIButton : Button
    {
        #region VARIABLES

        private RectTransform rectTransform;

        private Vector3 hoverScale;
        private Vector3 defaultScale;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        protected override void Awake()
        {
            base.Awake();

            rectTransform = GetComponent<RectTransform>();

            defaultScale = transform.localScale;
            hoverScale = defaultScale * 1.1f;
        }

        protected override void Start()
        {
            base.Start();      
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            LeanTween.scale(rectTransform, hoverScale, 0.1f);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            LeanTween.scale(rectTransform, defaultScale, 0.1f);
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        #endregion CUSTOM_FUNCTIONS
    }
}


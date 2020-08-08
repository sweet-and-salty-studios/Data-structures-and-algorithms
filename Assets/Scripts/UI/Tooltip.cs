using TMPro;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Tooltip : MonoBehaviour
    {
        #region VARIABLES

        private RectTransform rectTransform;
        private TextMeshProUGUI text;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void ShowTooltip(string tooltipText, Vector2 anchoredPosition)
        {
            var textPaddingSize = 8f;
            text.text = tooltipText;

            var backgroundSize = new Vector2(
                text.preferredWidth + textPaddingSize * 2,
                text.preferredHeight + textPaddingSize * 2);

            rectTransform.anchoredPosition = anchoredPosition;

            rectTransform.sizeDelta = backgroundSize;

            rectTransform.SetAsLastSibling();

            gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        #endregion CUSTOM_FUNCTIONS
    }
}


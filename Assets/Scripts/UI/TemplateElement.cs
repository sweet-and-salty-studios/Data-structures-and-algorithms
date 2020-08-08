using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class TemplateElement : MonoBehaviour
    {
        #region VARIABLES

        #endregion VARIABLES

        #region PROPERTIES

        public RectTransform RectTransform
        {
            get;
            private set;
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();    
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        #endregion CUSTOM_FUNCTIONS
    }
}
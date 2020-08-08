using System.Collections;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class UIManager : Singelton<UIManager>
    {
        #region VARIABLES

        private Canvas canvas;

        public UIPanel StartingPanel;
        public GraphPanel GraphPanel;
        private UIPanel currentPanel;
        private Coroutine iSwitchPanel_Coroutine;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            canvas = GetComponentInChildren<Canvas>();

            var allPanels = canvas.GetComponentsInChildren<UIPanel>();

            for(int i = 0; i < allPanels.Length; i++)
            {              
                if(allPanels[i].gameObject.activeSelf)
                {
                    allPanels[i].gameObject.SetActive(false);
                }
            }
        }

        private void Start()
        {
            SwitchPanel(StartingPanel);
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void SwitchPanel(UIPanel panel)
        {
            if(iSwitchPanel_Coroutine != null)
            {
                StopCoroutine(iSwitchPanel_Coroutine);
                iSwitchPanel_Coroutine = null;
            }

            iSwitchPanel_Coroutine = StartCoroutine(ISwitchPanel(panel));
        }

        private IEnumerator ISwitchPanel(UIPanel panel)
        {
            if(currentPanel != null)
            {
                yield return currentPanel.Close();
            }

            if(panel != null)
            {
                yield return panel.Open();
            }

            currentPanel = panel;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
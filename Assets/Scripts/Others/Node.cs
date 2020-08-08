using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class Node : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler
    {
        #region VARIABLES

        private RectTransform rectTransform;

        #endregion VARIABLES

        #region PROPERTIES

        public NODE_TYPE Type
        {
            get;
            private set;
        } = NODE_TYPE.WALKABLE;

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();    
        }

        public void OnPointerEnter(PointerEventData eventData)
        {       

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GridManager.Instance.ChangeNodeType(this);          
        }

        public void OnPointerExit(PointerEventData eventData)
        {
           
        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void ChangeType(NODE_TYPE newType)
        {
            Type = newType;

            switch(Type)
            {
                case NODE_TYPE.WALKABLE:
                    LeanTween.color(rectTransform, Color.white, 0.25f);
                    break;

                case NODE_TYPE.UN_WALKABLE:
                    LeanTween.color(rectTransform, Color.black, 0.25f);
                    break;

                case NODE_TYPE.START:
                    LeanTween.color(rectTransform, Color.green, 0.25f);
                    break;

                case NODE_TYPE.TARGET:
                    LeanTween.color(rectTransform, Color.red, 0.25f);
                    break;

                default:

                    break;
            }
        }

        #endregion CUSTOM_FUNCTIONS
    }
}


using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region VARIABLES

    private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
           
        }

        public void OnDrag(PointerEventData eventData)
        {
            var position = Vector2.zero;

            position = rectTransform.anchoredPosition;

            rectTransform.anchoredPosition += eventData.delta;

            if(IsRectTransformInsideSreen(rectTransform) == false)
            {
                rectTransform.anchoredPosition = position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public IEnumerator Open()
        {

            var id =  LeanTween.scale(gameObject, Vector3.one, 0.25f)
                .setOnStart(() =>
                {
                    gameObject.SetActive(true);
                    canvasGroup.blocksRaycasts = false;

                })
            .setFrom(new Vector3(0, 0, 1))
            .setEaseOutCubic()
            .setOnComplete(() =>
            {
                OnOpenAnimationComplete();
            }).id;

            yield return new WaitUntil(() => LeanTween.isTweening(id));
        }

        public IEnumerator Close()
        {
            canvasGroup.blocksRaycasts = false;

            var id = LeanTween.scale(gameObject, new Vector3(0, 0, 1), 0.25f)
            .setOnComplete(() => 
            {
                gameObject.SetActive(false);
            })
            .id;

            yield return new WaitUntil(() => LeanTween.isTweening(id));
        }

        protected virtual void OnOpenAnimationComplete()
        {
            canvasGroup.blocksRaycasts = true;
        }

        private bool IsRectTransformInsideSreen(RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            var visibleCorners = 0;
            var rect = new Rect(0, 0, Screen.width, Screen.height);

            for(int i = 0; i < corners.Length; i++)
            {
                if(rect.Contains(corners[i]))
                {
                    visibleCorners++;
                }
            }

            return visibleCorners == 4;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}


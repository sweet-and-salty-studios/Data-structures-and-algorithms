using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class GraphPanel : UIPanel
    {
        #region VARIABLES

        [Space]
        [Header("Dot Settings")]
        public Vector2 DotSize = new Vector2(11, 11);
        public Sprite DotSprite;
        public Color32 DotColor;
        public Color32 DotConnectionColor;

        [Space]
        [Header("Bar Settings")]
        public Sprite BarSprite;
        public Color32 BarColor;
        [Range(0.2f, 1)] public float BarWidthMultiplier;

        [Space]
        [Header("Template Prefabs")]
        public RectTransform LabelTemplate_X_Prefab;
        public RectTransform LabelTemplate_Y_Prefab;
        public RectTransform DashTemplate_X_Prefab;
        public RectTransform DashTemplate_Y_Prefab;
        public Tooltip Tooltip_Prefab;
        private RectTransform graphContainer;

        private List<GameObject> listOfGameObjects;
        private List<IGraphVisualObject> graphVisualObjects;

        private List<int> valueList;
        private IGraphVisual graphVisual;
        private Func<int, string> getAxisLabel_X;
        private Func<float, string> getAxisLabel_Y;
        private int maxVisibleValues;

        #endregion VARIABLES

        #region PROPERTIES

        public Tooltip Tooltip
        {
            get;
            private set;
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        protected override void Awake()
        {
            base.Awake();

            Initialize(out IGraphVisual lineGraphVisual, out IGraphVisual barGraphVisual);

            InitializeButtons(lineGraphVisual, barGraphVisual);

            ShowGraph(valueList, lineGraphVisual/*barGraphVisual*/, (int i) => $"Day {i + 1}", (float f) => $"€ {Mathf.RoundToInt(f + 1)}");
        }

        private void Initialize(out IGraphVisual lineGraphVisual, out IGraphVisual barGraphVisual)
        {
            graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();

            listOfGameObjects = new List<GameObject>();
            graphVisualObjects = new List<IGraphVisualObject>();

            lineGraphVisual = new LineGraphVisual(graphContainer, DotSize, DotSprite, DotColor, DotConnectionColor) as IGraphVisual;
            barGraphVisual = new BarChartVisual(graphContainer, BarWidthMultiplier, BarSprite, BarColor) as IGraphVisual;
            Tooltip = Instantiate(Tooltip_Prefab, graphContainer);
            Tooltip.gameObject.SetActive(false);

            valueList = new List<int>();

            var randomValueListLenght = UnityEngine.Random.Range(/*15*/2, /*60*/2);

            for(int i = 0; i < randomValueListLenght; i++)
            {
                valueList.Add(UnityEngine.Random.Range(0, 100));
            }
        }

        private void InitializeButtons(IGraphVisual lineGraphVisual, IGraphVisual barGraphVisual)
        {
            var buttons = transform.Find("Buttons");

            var lineChartButton = buttons.Find("LineChartButton").GetComponent<UIButton>();
            lineChartButton.onClick.AddListener(() =>
            {
                SetGraphVisual(lineGraphVisual);
            });

            var barChartButton = buttons.Find("BarChartButton").GetComponent<UIButton>();
            barChartButton.onClick.AddListener(() =>
            {
                SetGraphVisual(barGraphVisual);
            });

            var decreaseValueButton = buttons.Find("DecreseValueButton").GetComponent<UIButton>();
            decreaseValueButton.onClick.AddListener(() =>
            {
                ModifyVisibleValues(-1);
            });

            var increaseValueButton = buttons.Find("IncreaseValueButton").GetComponent<UIButton>();
            increaseValueButton.onClick.AddListener(() =>
            {
                ModifyVisibleValues(1);
            });

            var euroDisplayButton = buttons.Find("EuroDisplayButton").GetComponent<UIButton>();
            euroDisplayButton.onClick.AddListener(() =>
            {
                SetGetAxisLabel_Y((float f) => $"€ {Mathf.RoundToInt(f + 1)}");
            });

            var dollarDisplayButton = buttons.Find("DollarDisplayButton").GetComponent<UIButton>();
            dollarDisplayButton.onClick.AddListener(() =>
            {
                SetGetAxisLabel_Y((float f) => $"$ {Mathf.RoundToInt(f + 1)}");
            });
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private void SetGraphVisual(IGraphVisual graphVisual)
        {
            ShowGraph(valueList, graphVisual, getAxisLabel_X, getAxisLabel_Y, maxVisibleValues);
        }

        private void SetGetAxisLabel_X(Func<int, string> getAxisLabel_X)
        {
            ShowGraph(valueList, graphVisual, getAxisLabel_X, getAxisLabel_Y, maxVisibleValues);
        }

        private void SetGetAxisLabel_Y(Func<float, string> getAxisLabel_Y)
        {
            ShowGraph(valueList, graphVisual, getAxisLabel_X, getAxisLabel_Y, maxVisibleValues);
        }

        private void ModifyVisibleValues(int modifyAmount)
        {
            ShowGraph(valueList, graphVisual, getAxisLabel_X, getAxisLabel_Y, maxVisibleValues + modifyAmount);
        }

        private void ShowGraph(List<int> valueList, IGraphVisual graphVisual, Func<int, string> getAxisLabel_X = null, Func<float, string> getAxisLabel_Y = null, int maxVisibleValues = -1)
        {
            this.valueList = valueList;
            this.graphVisual = graphVisual;
            this.getAxisLabel_X = getAxisLabel_X;
            this.getAxisLabel_Y = getAxisLabel_Y;

            if(maxVisibleValues == 0)
            {
                maxVisibleValues = 0;
            }
            else if(maxVisibleValues > valueList.Count || maxVisibleValues < 0)
            {
                maxVisibleValues = valueList.Count;
            }
           
            this.maxVisibleValues = maxVisibleValues;

            if(getAxisLabel_X == null)
            {
                getAxisLabel_X = delegate (int i)
                {
                    return $"{i}";
                };
            }

            if(getAxisLabel_Y == null)
            {
                getAxisLabel_Y = delegate (float f)
                {
                    return $"{Mathf.RoundToInt(f)}";
                };
            }

            var destroyedGameObject = 0;
            var destroyedVisualObject = 0;

            foreach(var element in listOfGameObjects)
            {
                Destroy(element);
                destroyedGameObject++;
            }

            listOfGameObjects.Clear();

            foreach(var visualObject in graphVisualObjects)
            {
                visualObject.Destroy();
                destroyedVisualObject++;
            }

            graphVisualObjects.Clear(); 

            Debug.LogWarning(destroyedGameObject);
            Debug.LogWarning(destroyedVisualObject);

            var graphWidth = graphContainer.sizeDelta.x;
            var graphHeight = graphContainer.sizeDelta.y;

            float yMax = valueList[0];
            float yMin = valueList[0];

            var value = 0f;

            for(int index = Mathf.Max(valueList.Count - maxVisibleValues, 0); index < valueList.Count; index++)
            {
                value = valueList[index];

                if(value > yMax)
                {
                    yMax = value;
                }

                if(value < yMin)
                {
                    yMin = value;
                }
            }

            var yDifference = yMax - yMin;

            if(yDifference <= 0)
            {
                yDifference = 5f;
            }

            yMax += yDifference * 0.2f;
            yMin -= yDifference * 0.2f;

            // We force min value to start at 0...
            yMin = 0;

            var xSize = graphWidth / (maxVisibleValues + 1);

            var xIndex = 0;
            
            //index = Mathf.Max(valueList.Count - maxVisibleValues, 0);

            for(int index = Mathf.Max(valueList.Count - maxVisibleValues, 0); index < valueList.Count; index++)
            {
                var xPosition = xSize + xIndex * xSize;
                var yPosition = (valueList[index] - yMin) / (yMax - yMin) * graphHeight;
                var tooltipText = getAxisLabel_Y(valueList[index]);

                var newVisual = graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize, tooltipText);

                graphVisualObjects.Add(newVisual);

                var label_X = Instantiate(LabelTemplate_X_Prefab, graphContainer);
                label_X.anchoredPosition = new Vector2(xPosition, -10f);
                label_X.GetComponent<TextMeshProUGUI>().text = getAxisLabel_X(index);
                listOfGameObjects.Add(label_X.gameObject);

                var dash_X = Instantiate(DashTemplate_X_Prefab, graphContainer);
                dash_X.anchoredPosition = new Vector2(xPosition, 0f);
                listOfGameObjects.Add(dash_X.gameObject);

                xIndex++;
            }

            var seperatorCount = 10;
            for(var index = 0; index <= seperatorCount; index++)
            {
                var label_Y = Instantiate(LabelTemplate_Y_Prefab, graphContainer);
                var normalizedValue = index * 1f / seperatorCount;
                label_Y.anchoredPosition = new Vector2(5f, normalizedValue * graphHeight);
                label_Y.GetComponent<TextMeshProUGUI>().text = getAxisLabel_Y(yMin + (normalizedValue * (yMax - yMin)));
                listOfGameObjects.Add(label_Y.gameObject);

                var dash_Y = Instantiate(DashTemplate_Y_Prefab, graphContainer);
                dash_Y.gameObject.SetActive(true);
                dash_Y.anchoredPosition = new Vector2(0, normalizedValue * graphHeight);
                listOfGameObjects.Add(dash_Y.gameObject);
            }
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
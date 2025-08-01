using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<BuildingType> ignoreBuildingTypeList;

    private Transform arrowButton;
    private Dictionary<BuildingType, Transform> buttonTransformDictionary;

    private void Awake()
    {
        Transform buttonTemplate = transform.Find("Button Template");
        buttonTemplate.gameObject.SetActive(false);

        BuildingTypeList buildingTypeList = Resources.Load<BuildingTypeList>(typeof(BuildingTypeList).Name);

        buttonTransformDictionary = new Dictionary<BuildingType, Transform>();

        int index = 0;

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offsetAmount = 130f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        arrowButton.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        MouseEnterExitEvents mouseEnterExitEvents = arrowButton.GetComponent<MouseEnterExitEvents>();

        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Arrow");
        };

        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };

        index++;


        foreach (BuildingType buildingType in buildingTypeList.list)
        {
            if (ignoreBuildingTypeList.Contains(buildingType))
                continue;

            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            offsetAmount = 130f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            buttonTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            mouseEnterExitEvents = buttonTransform.GetComponent<MouseEnterExitEvents>();

            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstructionResourceCostString());
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };

            buttonTransformDictionary[buildingType] = buttonTransform;

            index++;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManagerOnActiveTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManagerOnActiveTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton()
    {
        arrowButton.Find("Selected").gameObject.SetActive(false);

        foreach (BuildingType buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("Selected").gameObject.SetActive(false);
        }

        BuildingType activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if (!activeBuildingType)
            arrowButton.Find("Selected").gameObject.SetActive(true);
        else
            buttonTransformDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
    }
}

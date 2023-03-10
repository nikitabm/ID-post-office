using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildingEntry : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Cost;
    public Button BuyButton;

    private List<GameObject> _modeOne = new List<GameObject>();
    private List<GameObject> _modeTwo = new List<GameObject>();

    private ulong _currentPrice;
    private EBuildingType buildingType;

    private bool _isMouseDown;

    private void Awake()
    {
        _modeOne.Add(Title.gameObject);
        _modeOne.Add(Level.gameObject);

        if (Description)
        {
            _modeTwo.Add(Description.gameObject);
        }
        _modeTwo.Add(BuyButton.gameObject);

    }
    public void SetEntryData(Sprite icon, BuildingData buildingData, string description = "")
    {
        buildingType = buildingData.BuildingType;
        image.sprite = icon;
        Title.text = buildingData.BuildingName;
        if (description != string.Empty)
        {
            Description.text = description;
        }
        if (buildingData.Level < 1)
        {
            Level.text = "";
        }
        else
        {
            Level.text = buildingData.Level.ToString();
        }

        if (buildingData.Level > 0)
        {
            _currentPrice = (ulong)(buildingData.InitialCost * Math.Pow(buildingData.GrowthRate, (buildingData.Level + 1)));
        }
        else
        {
            _currentPrice = buildingData.InitialCost;
        }
        Cost.text = Utils.GoldToString(_currentPrice);
    }

    public void UpdateData(BuildingData data)
    {
        SetEntryData(image.sprite, data);
    }

    public void BuyLevel()
    {
        BuildingManager.Instance.BuyBuilding(buildingType);
    }

    public void SetBuyButtonActive(ulong gold)
    {
        if (_currentPrice <= gold)
        {
            BuyButton.interactable = true;
        }
        else
        {
            if (BuyButton.interactable)
            {
                BuyButton.interactable = false;
            }
        }
    }

    private void SetActiveGOsInList(List<GameObject> list, bool newEnabled)
    {
        foreach (var go in list)
        {
            go.SetActive(newEnabled);
        }
    }
}

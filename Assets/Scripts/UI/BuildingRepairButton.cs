using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BuildingRepairButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private HealthSystem healthSystem;
    private float repairAmount = 0;
    private int repairCost = 0;
    private PriceList repairPricesList = new PriceList();
    [SerializeField] private float fullRepairCost = 50;
    [SerializeField] private GameObject priceToolTip;
    private TextMeshProUGUI priceValue;


    private void Awake()
    {
        priceValue = priceToolTip.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        priceToolTip.SetActive(false);
        healthSystem.OnHealthChangedEvent += HideRepairButton;
        healthSystem.OnCanBeRepair += ShowRepairButton;

        GetComponentInChildren<Button>().onClick.AddListener(() => {
            if (ResourceManager.Instance.CanAfford(repairPricesList))
            {
                healthSystem.GetHeal(200);
            };

            priceToolTip.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        });
        transform.parent.gameObject.SetActive(false);
    }

    private void ShowRepairButton(System.Object Sender, System.EventArgs e)
    {
        transform.parent.gameObject.SetActive(true);
    }
    private void HideRepairButton(System.Object Sender, HealthSystem.OnHealthChangedEventHandler e)
    {
        transform.parent.gameObject.SetActive(false);
        repairAmount += e.deltaNormilizeValue;
        repairCost = Mathf.RoundToInt(repairAmount * fullRepairCost);
        repairPricesList.gold = repairCost;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        priceToolTip.SetActive(true);
        priceValue.text = repairCost.ToString();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        priceToolTip.SetActive(false);
    }
}


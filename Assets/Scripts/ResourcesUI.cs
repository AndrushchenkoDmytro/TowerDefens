using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodAmountText;
    [SerializeField] private TextMeshProUGUI stoneAmountText;
    [SerializeField] private TextMeshProUGUI goldAmountText;
    void Start()
    {
        ResourceManager.Instance.OnResourcesAmountChanged += UpdateResourcesUI;
    }

    // Update is called once per frame


    public void UpdateResourcesUI(object sender, System.EventArgs e) 
    {
        woodAmountText.text = ResourceManager.Instance.GetResource(Resource.Wood).ToString();
        stoneAmountText.text = ResourceManager.Instance.GetResource(Resource.Stone).ToString();
        goldAmountText.text = ResourceManager.Instance.GetResource(Resource.Gold).ToString();
    }
}

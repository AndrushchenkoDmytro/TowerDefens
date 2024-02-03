using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodAmountText;
    [SerializeField] private TextMeshProUGUI stoneAmountText;
    [SerializeField] private TextMeshProUGUI goldAmountText;
    void Start()
    {
        woodAmountText.text = ResourceManager.Instance.GetResource(ResourceTypes.Wood).ToString();
        stoneAmountText.text = ResourceManager.Instance.GetResource(ResourceTypes.Stone).ToString();
        goldAmountText.text = ResourceManager.Instance.GetResource(ResourceTypes.Gold).ToString();
        ResourceManager.Instance.OnResourcesAmountChanged += UpdateResourcesUI;
    }

    // Update is called once per frame


    public void UpdateResourcesUI(object sender, System.EventArgs e) 
    {
        woodAmountText.text = ResourceManager.Instance.GetResource(ResourceTypes.Wood).ToString();
        stoneAmountText.text = ResourceManager.Instance.GetResource(ResourceTypes.Stone).ToString();
        goldAmountText.text = ResourceManager.Instance.GetResource(ResourceTypes.Gold).ToString();
    }
}

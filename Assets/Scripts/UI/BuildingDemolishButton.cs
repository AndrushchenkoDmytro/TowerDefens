using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField] private Transform rootGO;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => { Destroy(rootGO.gameObject); });
    }
}

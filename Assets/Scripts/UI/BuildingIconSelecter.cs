using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class BuildingIconSelecter : MonoBehaviour
{
    [SerializeField] private ExpandableButton expandButton;
    private RectTransform rectTransform;
    [SerializeField] private int iconIndex;
    [SerializeField] private bool isHorizontal = false;
    [SerializeField] private Vector2 iconOriginalSize;
    [SerializeField] private float offset;
    float stratPoint = 0;
    float distance = 0;

    private Button selectTypeButton;
    [SerializeField] private RectTransform selectionImage;
    [SerializeField] BuildingsTypeSo selectType;
   

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        selectTypeButton = GetComponent<Button>();
        GameObject.Find("MainTower").GetComponent<MainTower>().OnGameOver += () => { selectTypeButton.enabled = false; };
        //iconOriginalSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        distance = (iconOriginalSize.x + offset) * iconIndex;
    }
    private void Start()
    {
        selectTypeButton.onClick.AddListener(() => {
            if(PLayerController.Instance.activeBuildingType == selectType)
            {
                PLayerController.Instance.SetActiveBuildingType(null);
                selectionImage.gameObject.SetActive(false);
            }
            else
            {
                if(selectionImage.parent == expandButton.transform)
                {
                    selectionImage.gameObject.SetActive(false);
                }
                else
                {
                    selectionImage.gameObject.SetActive(true);
                    selectionImage.SetParent(this.transform);
                    selectionImage.transform.SetSiblingIndex(0);
                    selectionImage.transform.localPosition = new Vector3(0, 0, 0);
                }
                PLayerController.Instance.SetActiveBuildingType(selectType);
            }            
        });
    }

    public void IconDeployment()
    {
        StartCoroutine(Deployment());
    }

    public void Icon—ollapse()
    {
        StartCoroutine(—ollapse());
    }

    IEnumerator Deployment()
    {
        selectTypeButton.interactable = false;
        Vector3 tmp = Vector3.zero;
        float timer = 0;
        yield return new WaitForFixedUpdate();
        if (isHorizontal)
        {
            stratPoint = rectTransform.localPosition.x;

            while (tmp.x < distance)
            {
                timer += Time.deltaTime * 4.5f;
                tmp.x = Mathf.Lerp(stratPoint, distance, timer);
                rectTransform.localPosition = tmp;
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            stratPoint = rectTransform.localPosition.y;
            while (tmp.y < distance)
            {
                timer += Time.deltaTime * 4.5f;
                tmp.y = Mathf.Lerp(stratPoint, distance, timer);
                rectTransform.localPosition = tmp;
                yield return new WaitForFixedUpdate();
            }
        }
        selectTypeButton.interactable = true;
        expandButton.RepairButton();
        yield return null;
    }

    IEnumerator —ollapse()
    {
        selectTypeButton.interactable = false;
        float timer = 1;
        yield return new WaitForFixedUpdate();
        if (isHorizontal)
        {
            stratPoint = 0;
            Vector3 tmp = new Vector3(rectTransform.localPosition.x, 0, 0);

            while (tmp.x > 0)
            {
                timer -= Time.deltaTime * 4.5f;
                tmp.x = Mathf.Lerp(stratPoint, distance, timer);
                rectTransform.localPosition = tmp;
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            stratPoint = 0;
            Vector3 tmp = new Vector3(0, rectTransform.localPosition.y, 0);
            while (tmp.y > 0)
            {
                timer -= Time.deltaTime * 4.5f;
                tmp.y = Mathf.Lerp(stratPoint, distance, timer);
                rectTransform.localPosition = tmp;
                yield return new WaitForFixedUpdate();
            }
        }
        selectTypeButton.interactable = true;
        expandButton.RepairButton();
        yield return null;
    }
}

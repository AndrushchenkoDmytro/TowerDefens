using System.Collections;
using UnityEngine;

public class ExpandableButton : MonoBehaviour
{
    private bool isClickAble = true;
    private bool isDeployment = false;
    [SerializeField] private int hp = 0;
    [SerializeField] private BuildingIconSelecter[] buildingIcons;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ClickButton()
    {
        if (isClickAble)
        {
            hp = 0;
            isClickAble = false;
            animator.Play("ExpandButton");
            if(isDeployment)
            {
                StartCoroutine(DeploymentIcons());
            }
            else
            {
                StartCoroutine(—ollapseIcons());
            }
            isDeployment = !isDeployment;
        }
    }

    IEnumerator DeploymentIcons()
    {
        for (int i = 0; i < buildingIcons.Length; i++)
        {
            yield return new WaitForFixedUpdate();
            buildingIcons[i].IconDeployment();
        }
    }
    IEnumerator —ollapseIcons()
    {
        for (int i = buildingIcons.Length; i > 0; i--)
        {
            yield return new WaitForFixedUpdate();
            buildingIcons[i-1].Icon—ollapse();
        }
    }


    public void RepairButton()
    {
        hp += 1;
        if(hp == buildingIcons.Length)
        {
            isClickAble = true;
        }
    }
}

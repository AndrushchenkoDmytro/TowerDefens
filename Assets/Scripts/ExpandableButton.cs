using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Button");
        if (isClickAble)
        {
            Debug.Log("Click");
            hp = 0;
            isClickAble = false;
            animator.Play("ExpandButton");
            if(isDeployment)
            {
                StartCoroutine(DeploymentIcons());
                Debug.Log("Deployment");
            }
            else
            {
                StartCoroutine(—ollapseIcons());
                Debug.Log("Collapse");
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
            Debug.Log("hp == buildingIcons.Length");
            isClickAble = true;
        }
    }
}

using TMPro;
using UnityEngine;

public class ToolTips : MonoBehaviour
{
    public static ToolTips Instance;
    private Animator animator;
    private AnimatorStateInfo animStateInfo;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            animator = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    [SerializeField] TextMeshProUGUI tooltipText;

    public void ShowNotEnoughResourcesTip()
    {

        if(animStateInfo.normalizedTime == 0)
        {
            tooltipText.SetText("not enough resources!!!");
            animator.Play("ShowTooltip");
        }
    }

    public void ShowNotEnoughSpaceTip()
    {
        if (animStateInfo.normalizedTime == 0)
        {
            tooltipText.SetText("not enough space!!!");
            animator.Play("ShowTooltip");
        }
    }

    public void ShowVeryCloseToBuildingOfSameTypeTip()
    {
        if (animStateInfo.normalizedTime == 0)
        {
            tooltipText.SetText("very close to a building of the same type!!!");
            animator.Play("ShowTooltip");
        }
    }

}

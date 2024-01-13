using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneretorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;

    private void Start()
    {
        float generationSpeed = resourceGenerator.GetGenerationSpeed();
        if(generationSpeed == 0) gameObject.SetActive(false);
        transform.GetChild(1).GetComponent<TextMeshPro>().SetText(generationSpeed.ToString("F1"));
    }


}

using UnityEngine;

public class CullingOclusion : MonoBehaviour
{
    [SerializeField] private GameObject oclusionContainer;

    private void Start()
    {
        oclusionContainer.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            oclusionContainer.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            oclusionContainer.SetActive(false);
        }
    }
}

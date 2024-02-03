using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void PlayBtn()
    {
        SceneLoadManager.instance.LoadLevel(1); // play GameScene
    }

    public void QuitBtn()
    {
        Application.Quit(); // mainMenu;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

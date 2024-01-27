using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private Animator animator;
    private GameObject optionsButton;
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite onSoundSprite;
    [SerializeField] private Sprite offSoundSprite;
    [SerializeField] private Image musicImage;
    [SerializeField] private Sprite onMusicSprite;
    [SerializeField] private Sprite offMusicSprite;
    private GameObject optionMenuContainer;
    private Transform gameUI;

    private void Update()
    {
        Debug.Log(Time.timeScale);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameUI = transform.parent;
        optionsButton = transform.GetChild(0).gameObject;
        optionMenuContainer = transform.GetChild(1).gameObject;

        optionsButton.GetComponent<Button>().onClick.AddListener(ShowOptionMenuBtn);
        optionMenuContainer.SetActive(false);
    }

    private void Start()
    {
        if (SoundManager.instance.isSoundOn) soundImage.sprite = onSoundSprite;
        else soundImage.sprite = offSoundSprite;

        if (SoundManager.instance.isMusicOn) musicImage.sprite = onMusicSprite;
        else musicImage.sprite = offMusicSprite;
    }

    public void ShowOptionMenuBtn()
    {
        Time.timeScale = 0;
        optionsButton.SetActive(false);
        for (int i = 0; i < gameUI.childCount - 1; i++)
        {
            gameUI.GetChild(i).gameObject.SetActive(false);
        }
        optionMenuContainer.SetActive(true);
        animator.Play("ShowOptionMenu");
    }

    public void HideOptionMenuBtn() // Continue
    {
        animator.Play("CloseOptionMenu");
        Time.timeScale = 1;
        Invoke("OffContainer", 1f);
    }

    private void OffContainer()
    {
        for (int i = 0; i < gameUI.childCount - 1; i++)
        {
            gameUI.GetChild(i).gameObject.SetActive(true);
        }
        optionsButton.SetActive(true);
        optionMenuContainer.SetActive(false);
    }

    public void RetryBtn()
    {
        SceneLoadManager.instance.RetryLevel(); // retry
    }

    public void ToMainMenuBtn()
    {
        SceneLoadManager.instance.LoadLevel(0); // mainMenu;
    }

    public void SwitcheSoundVolume()
    {
        if (SoundManager.instance.isSoundOn)
        {
            soundImage.sprite = offSoundSprite;
            SoundManager.instance.OnSound(false);
        }
        else
        {
            soundImage.sprite = onSoundSprite;
            SoundManager.instance.OnSound(true);
        }
    }
    public void SwitcheMusicVolume()
    {
        if (SoundManager.instance.isMusicOn)
        {
            musicImage.sprite = offMusicSprite;
            SoundManager.instance.OnMusic(false);
        }
        else
        {
            musicImage.sprite = onMusicSprite;
            SoundManager.instance.OnMusic(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXButtons : MonoBehaviour
{
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite onSoundSprite;
    [SerializeField] private Sprite offSoundSprite;
    [SerializeField] private Image musicImage;
    [SerializeField] private Sprite onMusicSprite;
    [SerializeField] private Sprite offMusicSprite;

    private void Start()
    {
        if (SoundManager.instance.isSoundOn) soundImage.sprite = onSoundSprite;
        else soundImage.sprite = offSoundSprite;

        if (SoundManager.instance.isMusicOn) musicImage.sprite = onMusicSprite;
        else musicImage.sprite = offMusicSprite;
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

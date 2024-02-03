using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        GameOver,
    }
    private Dictionary<Sound, AudioClip> soundsAudioClips = new Dictionary<Sound, AudioClip>();
    private AudioSource soundSource;
    private AudioSource musicSource;
    public bool isSoundOn {  get; private set; }
    public bool isMusicOn { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Initialize();
        foreach (Sound clipName in System.Enum.GetValues(typeof(Sound)))
        {
            soundsAudioClips.Add(clipName, Resources.Load<AudioClip>(clipName.ToString()));
        }

    }

    public void PlaySound(Sound sound)
    {
        soundSource.PlayOneShot(soundsAudioClips[sound]);
    }

    private void Initialize()
    {
        soundSource = transform.GetChild(0).GetComponent<AudioSource>();
        musicSource = transform.GetChild(1).GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("OnSound"))
        {
            soundSource.volume = PlayerPrefs.GetInt("OnSound");
            if (soundSource.volume > 0) isSoundOn = true;
            else isSoundOn = false;
        }
        else
        {
            PlayerPrefs.SetInt("OnSound",1);
            soundSource.volume = 1;
            isSoundOn = true;
        }
        if (PlayerPrefs.HasKey("OnMusic"))
        {
            musicSource.volume = PlayerPrefs.GetInt("OnMusic");
            if (musicSource.volume > 0) isMusicOn = true;
            else isMusicOn = false;
        }
        else
        {
            PlayerPrefs.SetInt("OnMusic", 1);
            musicSource.volume = 1;
            isMusicOn = true;
        }
    }

    public void OnSound(bool onORoff)
    {
        if (onORoff) 
        {
            PlayerPrefs.SetInt("OnSound", 1);
            soundSource.volume = 1;
            isSoundOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("OnSound", 0);
            soundSource.volume = 0;
            isSoundOn = false;
        }
    }

    public void OnMusic(bool onORoff)
    {
        if (onORoff)
        {
            PlayerPrefs.SetInt("OnMusic", 1);
            musicSource.volume = 1;
            isMusicOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("OnMusic", 0);
            musicSource.volume = 0;
            isMusicOn = false;
        }
    }
}

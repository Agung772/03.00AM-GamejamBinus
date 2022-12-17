using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class setting : MonoBehaviour
{
    public Slider[] volumeSlider;
    [SerializeField] AudioMixerGroup music;
    [SerializeField] AudioMixerGroup sfx;
    [SerializeField] AudioMixerGroup master;
    AudioSource[] sources;
    public settingBlueprint[] sounds;
    public static setting instance;

    float musicVolume;
    float sfxVolume;
    float masterVolume;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        foreach (var item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.audioclip;
            item.source.loop = item.isloop;
            item.source.volume = item.volume;

            switch (item.audiotype)
            {
                case settingBlueprint.AudioType.soundEffect:
                    item.source.outputAudioMixerGroup = sfx;
                    break;
                case settingBlueprint.AudioType.music:
                    item.source.outputAudioMixerGroup = music;
                    break;
            }
            if (item.playonawake)
                item.source.Play();
        }
    }

    public void playmusic(int index)
    {
        sounds[index].source.Play();
    }
    public void updatevolume()
    {
        music.audioMixer.SetFloat("musik", Mathf.Log10(musicVolume) * 20);
        music.audioMixer.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
        music.audioMixer.SetFloat("master", Mathf.Log10(masterVolume) * 20);
    }
    public void musicslidervaluechange(float value)
    {
        musicVolume = value;
        updatevolume();
    }
    public void sfxslidervaluechange(float value)
    {
        sfxVolume = value;
        updatevolume();
    }
    public void masterslidervaluechange(float value)
    {
        masterVolume = value;
        updatevolume();
    }
}

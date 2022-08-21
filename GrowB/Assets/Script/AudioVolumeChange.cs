using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeChange : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioBGMSlider;
    public Slider audioEffectsSlider;

    public void BGMControl()
    {
        float volume = audioBGMSlider.value;
        if (volume == -40f) audioMixer.SetFloat("BGM", -80);
        else audioMixer.SetFloat("BGM", volume);
    }
    
    public void EffectsControl()
    {
        float volume = audioEffectsSlider.value;
        if (volume == -40f) audioMixer.SetFloat("Effects", -80);
        else audioMixer.SetFloat("Effects", volume);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

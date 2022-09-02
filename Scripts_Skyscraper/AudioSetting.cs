using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSetting : MonoBehaviour
{


    public AudioMixer audioMixer;
    
    public void SetLevel(float sliderVal)
    {
        audioMixer.SetFloat("SFXVol", Mathf.Log10(sliderVal) * 20);
    }
       
}

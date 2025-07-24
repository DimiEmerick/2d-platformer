using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public string floatParam = "MyExposedParam";
    public AudioMixer group;

    public void ChangeValue (float f)
    {
        group.SetFloat(floatParam, f);
    }
}

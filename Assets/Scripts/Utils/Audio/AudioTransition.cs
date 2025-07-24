using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTransition : MonoBehaviour
{
    public float transitionTime = .1f;
    public AudioMixerSnapshot snapshot;

    public void MakeTransition()
    {
        snapshot.TransitionTo(transitionTime);
    }
}

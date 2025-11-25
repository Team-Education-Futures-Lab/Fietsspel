using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip voiceClip;


    public void OnCardClicked()
    {
        _audioSource.PlayOneShot(voiceClip);
    }
}

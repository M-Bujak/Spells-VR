using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomClip : MonoBehaviour
{
    [field: SerializeField]
    private AudioClip[] AudioClips { get; set; }
    [field: SerializeField]
    private AudioSource AudioSource { get; set; }

    private void PlayFootstep()
    {
        AudioSource.PlayOneShot(GetRandomFootstep());
    }

    private AudioClip GetRandomFootstep()
    {
        return AudioClips[Random.Range(0, AudioClips.Length)];
    }
}

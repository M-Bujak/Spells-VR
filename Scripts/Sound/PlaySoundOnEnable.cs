using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [field: SerializeField]
    private AudioSource Sound { get; set; }

    private void OnEnable()
    {
        Sound.Play();
    }
}

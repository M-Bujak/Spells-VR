using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAndDestroy : MonoBehaviour
{
    [field: SerializeField]
    private AudioSource Sound { get; set; }

    private void Awake()
    {
        Sound.Play();
    }

    private void Update()
    {
        if(Sound.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}

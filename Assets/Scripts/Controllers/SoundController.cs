using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip explosion = default;
    [SerializeField]
    private AudioClip throwBox = default;
    [SerializeField]
    private AudioClip leaveHook = default;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosion()
    {
        audioSource.PlayOneShot(explosion);
    }

    public void PlayThrowBox()
    {
        audioSource.PlayOneShot(throwBox);
    }

    public void PlayLeaveHook()
    {
        audioSource.PlayOneShot(leaveHook);
    }
}

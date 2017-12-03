using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Singleton
    public static AudioManager Instance()
    {
        return FindObjectOfType<AudioManager>();
    }
    #endregion

    [SerializeField]
    private GameObject pointSound, jumpSound, deathSound, selectSound, boingSound;
    [SerializeField]
    private AudioSource startUpSound;

    public void PointSound()
    {
        GameObject sound = Instantiate(pointSound, transform.position, Quaternion.identity, transform);
        Destroy(sound.gameObject, 10);
    }

    public void JumpSound()
    {
        GameObject sound = Instantiate(jumpSound, transform.position, Quaternion.identity, transform);
        Destroy(sound.gameObject, 10);
    }

    public void DeathSound()
    {
        GameObject sound = Instantiate(deathSound, transform.position, Quaternion.identity, transform);
        Destroy(sound.gameObject, 10);
    }

    public void BoingSound()
    {
        GameObject sound = Instantiate(boingSound, transform.position, Quaternion.identity, transform);
        Destroy(sound.gameObject, 10);
    }

    public void SelectSound()
    {
        GameObject sound = Instantiate(selectSound, transform.position, Quaternion.identity, transform);
        Destroy(sound.gameObject, 10);
    }

    public void StartUpSound(float pitch)
    {
        AudioSource sound = Instantiate(startUpSound, transform.position, Quaternion.identity, transform) as AudioSource;
        sound.pitch = pitch;
        Destroy(sound.gameObject, 10);
    }
}

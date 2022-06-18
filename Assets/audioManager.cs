using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField]
    public AudioClip BGM;
    public AudioClip[] SoundsEffect;
    public AudioSource BGM_AS;
    public AudioSource SoundsEffect_AS;

    // Update is called once per frame
    void Update()
    {
        
    }
}

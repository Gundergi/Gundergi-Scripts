using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerDie : MonoBehaviour
{
    bool contact = true;
    AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioClip = GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 충돌했을 때 한번만 소리들리게
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" && contact)
        {
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            contact = false;
        }
    }
}

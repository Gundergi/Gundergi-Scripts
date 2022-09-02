using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneCheck : MonoBehaviour
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

    // �浹���� �� �ѹ��� �Ҹ��鸮��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" && contact)
        {
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            contact = false;
        }
    }
   
}

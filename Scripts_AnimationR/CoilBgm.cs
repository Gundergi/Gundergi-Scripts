using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilBgm : MonoBehaviour
{
    [Header("Sound")]
    public bool isSound = false;
    public AudioClip audioClip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // ��Ų��޽��������� ������ �� �ѹ��� ������ҽ� ����
        SkinnedMeshRenderer mesh = GetComponent<SkinnedMeshRenderer>();
        if (mesh.enabled == true&& isSound == false)
        {
            isSound = true;
            audioSource.Play();
            
        }
    }

}

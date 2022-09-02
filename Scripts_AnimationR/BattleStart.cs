using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
   
    public Transform poke1;
    public Transform poke2;
    public float battleDistance = 1f;
    bool isFighting;
    public GameObject battleScene;

    [Header("Sound")]
    public AudioSource audioSource;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���� ��Ʋ���̶�� �ؿ� ��ɾ� �������� �ʰ� ����
        if (isFighting == true)
        {
            return;
        }



        // poke1 poke2�Ÿ����ϱ�\
        float distance = Vector3.Distance(poke1.position, poke2.position);
        //1.���� �ѻ����� �Ÿ��� battle distance���� ª�����ٸ�
        if (distance < battleDistance)
        {
            //2.poke1�� poke2�� ������� �ִϸ��̼� ����
            poke1.GetComponent<Animator>().SetTrigger("Disappear");
            poke2.GetComponent<Animator>().SetTrigger("Disappear");
            //3.poke1,2������� battleScene�� ����
            //battleScene.SetActive(true);
            Invoke("DeactiveInvoke", 1f);
            //4.isfignting =true(����ο��� �˸���)
            isFighting = true;
            audioSource.Play();   
        }
        

        //isfight
    }
    void DeactiveInvoke()
    {
        battleScene.gameObject.SetActive(true);
        poke1.gameObject.SetActive(false);
        
        poke2.gameObject.SetActive(false);
    }


}

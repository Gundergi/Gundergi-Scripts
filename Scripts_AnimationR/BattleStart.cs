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
        //만일 현재 배틀중이라면 밑에 명령어 실행하지 않고 종료
        if (isFighting == true)
        {
            return;
        }



        // poke1 poke2거리구하기\
        float distance = Vector3.Distance(poke1.position, poke2.position);
        //1.만일 둘사이의 거리가 battle distance보다 짧아졌다면
        if (distance < battleDistance)
        {
            //2.poke1과 poke2가 사라지는 애니메이션 실행
            poke1.GetComponent<Animator>().SetTrigger("Disappear");
            poke2.GetComponent<Animator>().SetTrigger("Disappear");
            //3.poke1,2사라지면 battleScene을 실행
            //battleScene.SetActive(true);
            Invoke("DeactiveInvoke", 1f);
            //4.isfignting =true(현재싸움중 알리기)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 시간이 지나면 리프트 바닥면 유즈그래비티가 랜덤으로 체크되는 스크립트

public class LiftBroken : MonoBehaviour
{
    public List<GameObject> planeList = new List<GameObject>();     // 리프트 바닥 판자 리스트
    bool safeMode = true;
    bool isDrop;                                                    // 떨어졌는지 여부                                   
    float currentTime;                                              // 경과시간
    float totalTime;                                                   //총시간 
    float randomSeceond;                                            // 판자가 떨어질 랜덤 초 (3~6초 사이)
    public GameObject backPlane;                                    // 뒤쪽판자
    bool isFallBackplane;                                           // 뒤쪽판자 떨어졌는지 체크 여부
    public Animator animator;                                       // 리프트 애니메이션을 관리

    AudioSource audioSource;
    public AudioClip clearFail;

    public GameObject[] stainList=new GameObject[10];                //각각의 얼룩스크립에 접근을 하기위해서

    //각각의 얼룩스크립에 접근을해야...


    bool dropBack = false;
    public int stainListCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        // ResetRandomDropTime();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlaying == false) { return; } //KYJ 0731
        // 시간이 흐르고 있는데
        currentTime += Time.deltaTime;
        totalTime += Time.deltaTime;

        //if (currentTime > liftUp.autoLiftTime)
        //{
        //    stainListCount++;
        //    currentTime = 0;
        //}

        //stainList[stainListCount].GetComponent<Stain>();
        //30초가 지나고 && 90%이상 클리어하지 못했을때 => 바닥이 하나씩 떨어짐 
        Debug.Log(stainListCount);
        if (currentTime > LiftUp.instance.autoLiftTime && stainList[stainListCount-1].GetComponent<Stain>().dropFloor)
        {
            // 떨어지게 실행
            //safeMode = false;
            // isDrop = true;
            Debug.Log("안전모드 종료.");
            audioSource.clip = clearFail;
            audioSource.Play();
            DropFloor();
            // 시간 Reset
            currentTime = 0;

        }

        // 97층에서 바람애니메이션 + 뒤쪽판자 떨어지는거 실행 
        // 한번만 실행하도록 만들기
        if (totalTime >= LiftUp.instance.autoLiftTime*7 && dropBack==false)
        {
            Debug.Log("뒤쪽판자 떨어짐");
            dropBack = true;
            // 방어코드 - 만일 리스트의 경우가 0보다 큰 경우에만 실행
            if (planeList.Count > 0)
            {
                // 뒤쪽 판자가 떨어진다. 
                PlayShakeAnimation();
                //PlayShakeAnimation();
                // 랜덤 시간도 다시 리셋
                // ResetRandomDropTime();
                // 시간 리셋
            }

        }
    }


    // 아래 판자
    public void DropFloor() // 각 층마다 90% 이상 클리어하지 못했을때 실행 
    {
        // 배열이 가지고 있는 순서 중 하나 랜덤하게 가져옴
        int randomIndex = Random.Range(0, planeList.Count);
        // 배열 안에 들어있는 판자들 중 랜덤하게 1개를 useGravity 활성화
        planeList[randomIndex].GetComponent<Rigidbody>().isKinematic = false;
        planeList[randomIndex].GetComponent<Rigidbody>().useGravity = true;
        // 부모-자식 연결 해제
        planeList[randomIndex].transform.parent = null;
        // 내 리스트에서 방금 활성화한 index의 판자를 제거
        planeList.RemoveAt(randomIndex);
    }

    // 뒤쪽 판자
    public void DropBack() // PlayShakeAnimation()  과 같이실행 (바람에 흔들려서) 딱 한번만  97층에 실행 
    {
        //animator.SetTrigger("Lift Wind");
        // 만일 뒤쪽판자가 떨어져있지 않은 상태라면 
        if (isFallBackplane == false)
        {
            // 판자를 떨어트릴 것이다.
            backPlane.GetComponent<Rigidbody>().isKinematic = false;
            backPlane.GetComponent<Rigidbody>().useGravity = true;
            // 부모-자식 연결 해제
            transform.parent = null;
            // 뒤쪽판자가 떨어졌다는 상태로 변경
            isFallBackplane = true;
        }
    }
    void PlayShakeAnimation() //  DropBack()과 같이 실행되는것 /바람애니메이션  
    {
        animator.SetTrigger("Lift Wind");

        //float waitTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        //Lift Wind 애니메이션 총 재생 시간/ (0)은 레이어, / [0]은 순서
        float waitTime = 14.5f;
        Debug.Log("waitTime : " + waitTime);
        // Lift Wind 애니메이션이 모두 실행되고 나면 IsDrop을 실행
        Invoke("DropBack", waitTime);
    }

    public void PlayTriggerDrop()
    {
        isDrop = true;

        currentTime = randomSeceond + 1.5f;
    }

    void ResetRandomDropTime()
    {
        randomSeceond = Random.Range(3, 6);

    }
}

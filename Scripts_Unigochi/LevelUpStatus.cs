using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpStatus : MonoBehaviour
{
    public Image guage;                 // 경험치 게이지
    public float maxExp = 300f;         // 경험치 만땅 맥스치
    float currentExp;                   // 현재 나의 경험치

    public GameObject smallUni;         // 현재 유니 
    public GameObject midiumUni;        // 첫번 째 진화할 유니 
    public GameObject bigUni;           // 두번 째 진화할 유니 
    public GameObject kingUni;          // 마지막 진화할 유니


    public float firstEvolutionExp = 100;
    public float secondEvolutionExp = 200;
    public float thirdEvolutionExp = 300;

    // 0 = 기본, 1= 1차진화 2= 2차 진화 ... 
    int evolutionState = 0;

    public float MyEXP
    {
        get
        {
            return currentExp;
        }
        set
        {
            // 현재 경험치 갱신
            currentExp = value;

            // 경험치 게이지 갱신
            //  - 게이지 수치로 변환 0~1
            float expGuageAmount = currentExp / maxExp;
            //  - 방어코드.. 만일 currentExp가 maxExp 를 넘어가지 않을 때만
            //      ㄴ 게이지에 변환된 수치 적용 ( =fillAmount)
            guage.fillAmount = expGuageAmount;




            // 만약에 경험치가 일정 수준 넘어가면
            if (evolutionState == 0 && currentExp > firstEvolutionExp)
            {
                // 1차 변신
                Debug.Log("1차 진화!!!!");
                evolutionState++;
                // 현재 유니는 비활성화
                smallUni.SetActive(false);
                // 첫번째 진화할 유니 활성화
                midiumUni.SetActive(true);
                // 애니메이터 다시 가져와 초기화해주기
                Uni.Instance.ResetAnimator();
                // 파티클 효과 줄 수있고..
                // 빵빠레 오디오..
            }
            // 만약에 경험치가 일정 수준 넘어가면
            if (evolutionState == 1 && currentExp > secondEvolutionExp)
            {
                // 2차 진화
                Debug.Log("@@@ 2 차 진화!!!!");
                evolutionState++;
                // 현재 유니 비활성화
                midiumUni.SetActive(false);
                // 두번 째 진화할 유니 활성화
                bigUni.SetActive(true);
                // 애니메이터 다시 가져와 초기화해주기
                Uni.Instance.ResetAnimator();
                // 파티클 효과 줄 수있고..
                // 빵빠레 오디오..
            }
            // 만약에 경험치가 일정 수준 넘어가면
            if (evolutionState == 2 && currentExp > thirdEvolutionExp)
            {
                // 3차 진화
                Debug.Log("3 차 진화!!!!");
                evolutionState++;
                // 현재 유니 비활성화
                bigUni.SetActive(false);
                // 세번 째 진화할 유니 활성화
                kingUni.SetActive(true);
                // 애니메이터 다시 가져와 초기화해주기
                Uni.Instance.ResetAnimator();
                // 파티클 효과 줄 수있고..
                // 빵빠레 오디오..
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 음식을 먹으면, 일정 경험치 증가
    public void EatExp(float expAmount)
    {
        MyEXP += expAmount;
    }
}

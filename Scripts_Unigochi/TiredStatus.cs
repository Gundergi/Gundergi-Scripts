using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiredStatus : MonoBehaviour
{
    public Image guage;                      // 피곤 게이지
    public float tiredMaxGuage = 10f;            // 피곤 게이지가 버틸 수 있는 시간
    // 1차 경고 색상
    public Color alertColor;
    private Color originColor;

    float currentGuage;                       // 흐르는 시간
    public float moveTiredSpeed = 1f;        // 이동상태일 때 감소되는 속도
    public float idleHealthSpeed = 1f;       // 가만히 있을 때 회복 속도
    bool isFirstAlert;                       // 1차 경고 들어갔는지 아닌지


    void Start()
    {
        originColor = guage.color;
        // 타이얼드타임으로부터 시작한다.
        currentGuage = tiredMaxGuage;


    }

    // Update is called once per frame
    void Update()
    {
        // 시간이 지남에 따라 피곤 게이지가 서서히 줄어든다.
        guage.fillAmount = GetGaugeStatus();

        // 게이지가 30% 이하로 떨어지면
        if (guage.fillAmount < 0.3)
        {
            if (isFirstAlert == false)
            {
                // 색상 바꿈
                guage.color = alertColor;
                isFirstAlert = true;
            }
        }

        // 1. 만일 0.3이하가 되면. 빨간색으로 경고...
        if (guage.fillAmount <= 0)
        {
            Debug.Log("게임 종료.. 사망..");
        }

        // a. 유니의 상태가 Move 상태라면 게이지 감소
        if (Uni.Instance.uniState == Uni.UniState.Move)
        {
            currentGuage -= moveTiredSpeed * Time.deltaTime;

            // 감소했는데 0보다 줄어들면..
            if (currentGuage < 0)
            {
                // 죽음...
            }
        }
        // b. 유니 상태가 Idle 상태라면 게이지 감소
        else if (Uni.Instance.uniState == Uni.UniState.Idle)
        {
            currentGuage += idleHealthSpeed * Time.deltaTime;

            // 만약 회복 되었을때, currentGuage가 30% 이상이면
            if (guage.fillAmount > 0.3)
            {
                // - 만일 이미 경고 색상을 주었을 경우에만.. (isFirstAlert == true)
                if (isFirstAlert == true)
                {
                    // - 1. Guage 색상을 원래 색상으로 변경하고,
                    // - 2. 경고 색상 다시 false 로 리셋
                    guage.color = originColor;
                    isFirstAlert = false;
                }
            }

            // 만약 풀게이지 이상 차려고 하면
            if (currentGuage > tiredMaxGuage)
            {
                // 게이지 Max로 고정...
                currentGuage = tiredMaxGuage;
            }
        }
    }


    // b. 유니가 점프한 경우에는 일정 게이지 감소
    public void DecreaseTimeWhenJumnp(float decreaseGauage)
    {
        // 게이지 감소
        currentGuage -= decreaseGauage;

        // 감소했는데 0보다 줄어들면..
        if (currentGuage < 0)
        {
            // 죽음...
        }
    }

    // Ilde 상태면, 일정 피곤 증가
    public void Idle(float addGuage)
    {
        // addGuage만큼 currentTime(=게이지) 회복
        currentGuage += addGuage;
        //  currentTime은 0보다 작을 수 없어..
        if (currentGuage < 0)
        {
            currentGuage = 0;
        }

        float gaugeRate = GetGaugeStatus();

        if (gaugeRate > 0.3f)
        {
            guage.color = originColor;

            isFirstAlert = false;
        }
    }

    float GetGaugeStatus()
    {
        // 0에서 1까지 진행... tiredTime 시간초에 따라
        float rate = currentGuage / tiredMaxGuage;

        // 시간이 지남에 따라 피곤 게이지가 서서히 줄어든다.   
        return rate;
    }
}

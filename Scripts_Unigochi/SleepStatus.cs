using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepStatus : MonoBehaviour
{

    // 졸림 게이지
    public Image guage;
    // 졸림 게이지가 버틸 수 있는 시간
    public float sleepMaxGuage = 10f;
    // 1차 경고 색상
    public Color alertColor;
    private Color originColor;

    float currentGuage;

    // 1차 경고 들어갔는지 아닌지..
    bool isFirstAlert;

    // Start is called before the first frame update
    void Start()
    {
        originColor = guage.color;

        currentGuage = sleepMaxGuage;
    }

    // Update is called once per frame
    void Update()
    {
        // 시간이 흐른다.
        currentGuage -= Time.deltaTime;
        // 시간이 지남에 따라 졸림 게이지가 서서히 줄어든다.   
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


    }

    // 잠을 자면, 일정 졸림 증가
    public void SleepBed(float addGuage)
    {
        // addGuage만큼 currentTime(=게이지) 회복
        currentGuage += addGuage;
        //  currentTime은 0보다 작을 수 없어..
        if (currentGuage > sleepMaxGuage)
        {
            currentGuage = sleepMaxGuage;
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
        // 0에서 1까지 진행... sleepTime 시간초에 따라
        float rate = currentGuage / sleepMaxGuage;

        // 시간이 지남에 따라 졸림 게이지가 서서히 줄어든다.   
        return rate;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryStatus : MonoBehaviour
{
    // 현재 상태
    public enum Status
    {
        Full,
        Normal,
        Hungry, // 1차 경고 : 
        Starve,  // 2차 경고 : 굶어죽기 직전 상태
        Die
    }
    // 현재 상태 변수
    public Status status = Status.Full;

    [Header("Status Rate")]
    [Range(0, 1)]
    public float rateFull;  // Full 인 상태 ( 몇퍼센트% 이상일 때 )
    [Range(0, 1)]
    public float rateNormal = 0.9f;  // Normal 인 상태 ( 몇퍼센트% 이상일 때 )
    [Range(0, 1)]
    public float rateHungry = 0.3f;  // Hungry 인 상태 ( 몇퍼센트% 이상일 때 )
    [Range(0, 1)]
    public float rateStarve = 0.1f;  // Starve 인 상태 ( 몇퍼센트% 이상일 때 )
    [Header("Setting")]
    // 헝그리 게이지
    public Image guage;
    // 배고픔 게이지가 버틸 수 있는 시간
    public float currentMaxHungry = 10f;
    // 원래 색상
    private Color originColor;
    // 1차 경고 색상
    public Color alertHungryColor;
    // 2차 경고 색상
    public Color alertStarveColor;

    float currentGuage;
    public GameObject Uni;

    // Start is called before the first frame update
    void Start()
    {
        // 내 원래 색상 저장
        originColor = guage.color;
        // 헝그리타임으로부터 시작한다.
        currentGuage = currentMaxHungry;
    }

    // Update is called once per frame
    void Update()
    {
        // 시간이 흐른다.
        currentGuage -= Time.deltaTime;
        // 시간이 지남에 따라 배부름 게이지가 서서히 줄어든다.   
        guage.fillAmount = GetCurrentHungryGuage();

        switch (status)
        {
            case Status.Full: Full(); break;
            case Status.Normal: Normal(); break;
            case Status.Hungry: Hungry(); break;
            case Status.Starve: Starve(); break;
            case Status.Die: Die(); break;
        }
       
    }



    void Full()
    {
        // 0.9 보다 아래일 때는 노말 상태
        if (GetCurrentHungryGuage() < rateNormal)
        {
            ChangeStatus(Status.Normal);
        }
    }

    void Normal()
    {
        // 0.3 보다 아래일 때는 배고픔 상태
        if (GetCurrentHungryGuage() < rateHungry)
        {
            ChangeStatus(Status.Hungry);
        }
    }

    void Hungry()
    {
        // 0.1 보다 아래일 때는 굵어죽기직전 상태
        if (GetCurrentHungryGuage() < rateStarve)
        {
            ChangeStatus(Status.Starve);
        }
    }

    void Starve()
    {
        // 1. 만일 0.3이하가 되면. 빨간색으로 경고...
        if (GetCurrentHungryGuage() <= 0)
        {
            ChangeStatus(Status.Die);
        }
    }

    void Die()
    {
        Debug.Log("게임 종료.. 사망..");
    }

    // state 변화
    public void ChangeStatus(Status nextStatus)
    {
        // 만일 바뀌는 status가 Hungry일 경우, 색상을 alert색상을 바꾼다.
        if (nextStatus == Status.Hungry)
        {
            guage.color = alertHungryColor;
        }
        // 만일 바뀌는 status가 Starve일 경우, 색상을 alert색상을 바꾼다.
        else if (nextStatus == Status.Starve)
        {
            guage.color = alertStarveColor;
        }

        // 실제 status를 next 로 전환
        status = nextStatus;
    }

    // 현재 배고픔 상태에 대한 게이지 정보(0~1) 가져와 반환한다.
    float GetCurrentHungryGuage()
    {
        // 0에서 1까지 진행... hungryTime 시간초에 따라
        float rate = currentGuage / currentMaxHungry;

        return rate;
    }

    // 음식을 먹으면, 일정 배고픔 증가
    public void EatFood(float addGuage)
    {
        // addGuage만큼 currentTime(=게이지) 회복
        currentGuage += addGuage;
        //  currentTime의 최대값은 hungryTime보다 클 수 없어..
        if (currentGuage > currentMaxHungry)
        {
            currentGuage = currentMaxHungry;
        }
        // 현재 회복하고난 게이지 결과 가져오기 ( 0~1 사이 )
        // Normal => Full
        if (GetCurrentHungryGuage() > rateNormal)
        {
            Debug.Log("Normal => Full");
            // 내가 이미 Full인 상태면 상태 변경 하지 않고 종료
            if (status == Status.Full) { return; }

            // 원래색상으로 변경
            guage.color = originColor;

            ChangeStatus(Status.Full);
        }
        // Hungry => Normal
        else if (GetCurrentHungryGuage() > rateHungry)
        {
            Debug.Log("Hungry => Noramal");
            // 내가 이미 Normal 상태면 상태 변경 하지 않고 종료
            if (status == Status.Normal) { return; }

            // 원래색상으로 변경
            guage.color = originColor;

            ChangeStatus(Status.Normal);
        }
        // Starve => Hungry
        else if (GetCurrentHungryGuage() > rateStarve)
        {
            Debug.Log("Starve => Hungry");
            // 내가 이미 Hungry 상태면 상태 변경 하지 않고 종료
            if (status == Status.Hungry) { return; }

            // 원래색상으로 변경
            guage.color = alertHungryColor;

            ChangeStatus(Status.Hungry);
        }
    }
}

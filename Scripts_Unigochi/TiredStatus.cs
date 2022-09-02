using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiredStatus : MonoBehaviour
{
    public Image guage;                      // �ǰ� ������
    public float tiredMaxGuage = 10f;            // �ǰ� �������� ��ƿ �� �ִ� �ð�
    // 1�� ��� ����
    public Color alertColor;
    private Color originColor;

    float currentGuage;                       // �帣�� �ð�
    public float moveTiredSpeed = 1f;        // �̵������� �� ���ҵǴ� �ӵ�
    public float idleHealthSpeed = 1f;       // ������ ���� �� ȸ�� �ӵ�
    bool isFirstAlert;                       // 1�� ��� ������ �ƴ���


    void Start()
    {
        originColor = guage.color;
        // Ÿ�̾��Ÿ�����κ��� �����Ѵ�.
        currentGuage = tiredMaxGuage;


    }

    // Update is called once per frame
    void Update()
    {
        // �ð��� ������ ���� �ǰ� �������� ������ �پ���.
        guage.fillAmount = GetGaugeStatus();

        // �������� 30% ���Ϸ� ��������
        if (guage.fillAmount < 0.3)
        {
            if (isFirstAlert == false)
            {
                // ���� �ٲ�
                guage.color = alertColor;
                isFirstAlert = true;
            }
        }

        // 1. ���� 0.3���ϰ� �Ǹ�. ���������� ���...
        if (guage.fillAmount <= 0)
        {
            Debug.Log("���� ����.. ���..");
        }

        // a. ������ ���°� Move ���¶�� ������ ����
        if (Uni.Instance.uniState == Uni.UniState.Move)
        {
            currentGuage -= moveTiredSpeed * Time.deltaTime;

            // �����ߴµ� 0���� �پ���..
            if (currentGuage < 0)
            {
                // ����...
            }
        }
        // b. ���� ���°� Idle ���¶�� ������ ����
        else if (Uni.Instance.uniState == Uni.UniState.Idle)
        {
            currentGuage += idleHealthSpeed * Time.deltaTime;

            // ���� ȸ�� �Ǿ�����, currentGuage�� 30% �̻��̸�
            if (guage.fillAmount > 0.3)
            {
                // - ���� �̹� ��� ������ �־��� ��쿡��.. (isFirstAlert == true)
                if (isFirstAlert == true)
                {
                    // - 1. Guage ������ ���� �������� �����ϰ�,
                    // - 2. ��� ���� �ٽ� false �� ����
                    guage.color = originColor;
                    isFirstAlert = false;
                }
            }

            // ���� Ǯ������ �̻� ������ �ϸ�
            if (currentGuage > tiredMaxGuage)
            {
                // ������ Max�� ����...
                currentGuage = tiredMaxGuage;
            }
        }
    }


    // b. ���ϰ� ������ ��쿡�� ���� ������ ����
    public void DecreaseTimeWhenJumnp(float decreaseGauage)
    {
        // ������ ����
        currentGuage -= decreaseGauage;

        // �����ߴµ� 0���� �پ���..
        if (currentGuage < 0)
        {
            // ����...
        }
    }

    // Ilde ���¸�, ���� �ǰ� ����
    public void Idle(float addGuage)
    {
        // addGuage��ŭ currentTime(=������) ȸ��
        currentGuage += addGuage;
        //  currentTime�� 0���� ���� �� ����..
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
        // 0���� 1���� ����... tiredTime �ð��ʿ� ����
        float rate = currentGuage / tiredMaxGuage;

        // �ð��� ������ ���� �ǰ� �������� ������ �پ���.   
        return rate;
    }
}

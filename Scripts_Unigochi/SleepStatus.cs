using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepStatus : MonoBehaviour
{

    // ���� ������
    public Image guage;
    // ���� �������� ��ƿ �� �ִ� �ð�
    public float sleepMaxGuage = 10f;
    // 1�� ��� ����
    public Color alertColor;
    private Color originColor;

    float currentGuage;

    // 1�� ��� ������ �ƴ���..
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
        // �ð��� �帥��.
        currentGuage -= Time.deltaTime;
        // �ð��� ������ ���� ���� �������� ������ �پ���.   
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


    }

    // ���� �ڸ�, ���� ���� ����
    public void SleepBed(float addGuage)
    {
        // addGuage��ŭ currentTime(=������) ȸ��
        currentGuage += addGuage;
        //  currentTime�� 0���� ���� �� ����..
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
        // 0���� 1���� ����... sleepTime �ð��ʿ� ����
        float rate = currentGuage / sleepMaxGuage;

        // �ð��� ������ ���� ���� �������� ������ �پ���.   
        return rate;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryStatus : MonoBehaviour
{
    // ���� ����
    public enum Status
    {
        Full,
        Normal,
        Hungry, // 1�� ��� : 
        Starve,  // 2�� ��� : �����ױ� ���� ����
        Die
    }
    // ���� ���� ����
    public Status status = Status.Full;

    [Header("Status Rate")]
    [Range(0, 1)]
    public float rateFull;  // Full �� ���� ( ���ۼ�Ʈ% �̻��� �� )
    [Range(0, 1)]
    public float rateNormal = 0.9f;  // Normal �� ���� ( ���ۼ�Ʈ% �̻��� �� )
    [Range(0, 1)]
    public float rateHungry = 0.3f;  // Hungry �� ���� ( ���ۼ�Ʈ% �̻��� �� )
    [Range(0, 1)]
    public float rateStarve = 0.1f;  // Starve �� ���� ( ���ۼ�Ʈ% �̻��� �� )
    [Header("Setting")]
    // ��׸� ������
    public Image guage;
    // ����� �������� ��ƿ �� �ִ� �ð�
    public float currentMaxHungry = 10f;
    // ���� ����
    private Color originColor;
    // 1�� ��� ����
    public Color alertHungryColor;
    // 2�� ��� ����
    public Color alertStarveColor;

    float currentGuage;
    public GameObject Uni;

    // Start is called before the first frame update
    void Start()
    {
        // �� ���� ���� ����
        originColor = guage.color;
        // ��׸�Ÿ�����κ��� �����Ѵ�.
        currentGuage = currentMaxHungry;
    }

    // Update is called once per frame
    void Update()
    {
        // �ð��� �帥��.
        currentGuage -= Time.deltaTime;
        // �ð��� ������ ���� ��θ� �������� ������ �پ���.   
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
        // 0.9 ���� �Ʒ��� ���� �븻 ����
        if (GetCurrentHungryGuage() < rateNormal)
        {
            ChangeStatus(Status.Normal);
        }
    }

    void Normal()
    {
        // 0.3 ���� �Ʒ��� ���� ����� ����
        if (GetCurrentHungryGuage() < rateHungry)
        {
            ChangeStatus(Status.Hungry);
        }
    }

    void Hungry()
    {
        // 0.1 ���� �Ʒ��� ���� �����ױ����� ����
        if (GetCurrentHungryGuage() < rateStarve)
        {
            ChangeStatus(Status.Starve);
        }
    }

    void Starve()
    {
        // 1. ���� 0.3���ϰ� �Ǹ�. ���������� ���...
        if (GetCurrentHungryGuage() <= 0)
        {
            ChangeStatus(Status.Die);
        }
    }

    void Die()
    {
        Debug.Log("���� ����.. ���..");
    }

    // state ��ȭ
    public void ChangeStatus(Status nextStatus)
    {
        // ���� �ٲ�� status�� Hungry�� ���, ������ alert������ �ٲ۴�.
        if (nextStatus == Status.Hungry)
        {
            guage.color = alertHungryColor;
        }
        // ���� �ٲ�� status�� Starve�� ���, ������ alert������ �ٲ۴�.
        else if (nextStatus == Status.Starve)
        {
            guage.color = alertStarveColor;
        }

        // ���� status�� next �� ��ȯ
        status = nextStatus;
    }

    // ���� ����� ���¿� ���� ������ ����(0~1) ������ ��ȯ�Ѵ�.
    float GetCurrentHungryGuage()
    {
        // 0���� 1���� ����... hungryTime �ð��ʿ� ����
        float rate = currentGuage / currentMaxHungry;

        return rate;
    }

    // ������ ������, ���� ����� ����
    public void EatFood(float addGuage)
    {
        // addGuage��ŭ currentTime(=������) ȸ��
        currentGuage += addGuage;
        //  currentTime�� �ִ밪�� hungryTime���� Ŭ �� ����..
        if (currentGuage > currentMaxHungry)
        {
            currentGuage = currentMaxHungry;
        }
        // ���� ȸ���ϰ� ������ ��� �������� ( 0~1 ���� )
        // Normal => Full
        if (GetCurrentHungryGuage() > rateNormal)
        {
            Debug.Log("Normal => Full");
            // ���� �̹� Full�� ���¸� ���� ���� ���� �ʰ� ����
            if (status == Status.Full) { return; }

            // ������������ ����
            guage.color = originColor;

            ChangeStatus(Status.Full);
        }
        // Hungry => Normal
        else if (GetCurrentHungryGuage() > rateHungry)
        {
            Debug.Log("Hungry => Noramal");
            // ���� �̹� Normal ���¸� ���� ���� ���� �ʰ� ����
            if (status == Status.Normal) { return; }

            // ������������ ����
            guage.color = originColor;

            ChangeStatus(Status.Normal);
        }
        // Starve => Hungry
        else if (GetCurrentHungryGuage() > rateStarve)
        {
            Debug.Log("Starve => Hungry");
            // ���� �̹� Hungry ���¸� ���� ���� ���� �ʰ� ����
            if (status == Status.Hungry) { return; }

            // ������������ ����
            guage.color = alertHungryColor;

            ChangeStatus(Status.Hungry);
        }
    }
}

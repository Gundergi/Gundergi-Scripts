using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpStatus : MonoBehaviour
{
    public Image guage;                 // ����ġ ������
    public float maxExp = 300f;         // ����ġ ���� �ƽ�ġ
    float currentExp;                   // ���� ���� ����ġ

    public GameObject smallUni;         // ���� ���� 
    public GameObject midiumUni;        // ù�� ° ��ȭ�� ���� 
    public GameObject bigUni;           // �ι� ° ��ȭ�� ���� 
    public GameObject kingUni;          // ������ ��ȭ�� ����


    public float firstEvolutionExp = 100;
    public float secondEvolutionExp = 200;
    public float thirdEvolutionExp = 300;

    // 0 = �⺻, 1= 1����ȭ 2= 2�� ��ȭ ... 
    int evolutionState = 0;

    public float MyEXP
    {
        get
        {
            return currentExp;
        }
        set
        {
            // ���� ����ġ ����
            currentExp = value;

            // ����ġ ������ ����
            //  - ������ ��ġ�� ��ȯ 0~1
            float expGuageAmount = currentExp / maxExp;
            //  - ����ڵ�.. ���� currentExp�� maxExp �� �Ѿ�� ���� ����
            //      �� �������� ��ȯ�� ��ġ ���� ( =fillAmount)
            guage.fillAmount = expGuageAmount;




            // ���࿡ ����ġ�� ���� ���� �Ѿ��
            if (evolutionState == 0 && currentExp > firstEvolutionExp)
            {
                // 1�� ����
                Debug.Log("1�� ��ȭ!!!!");
                evolutionState++;
                // ���� ���ϴ� ��Ȱ��ȭ
                smallUni.SetActive(false);
                // ù��° ��ȭ�� ���� Ȱ��ȭ
                midiumUni.SetActive(true);
                // �ִϸ����� �ٽ� ������ �ʱ�ȭ���ֱ�
                Uni.Instance.ResetAnimator();
                // ��ƼŬ ȿ�� �� ���ְ�..
                // ������ �����..
            }
            // ���࿡ ����ġ�� ���� ���� �Ѿ��
            if (evolutionState == 1 && currentExp > secondEvolutionExp)
            {
                // 2�� ��ȭ
                Debug.Log("@@@ 2 �� ��ȭ!!!!");
                evolutionState++;
                // ���� ���� ��Ȱ��ȭ
                midiumUni.SetActive(false);
                // �ι� ° ��ȭ�� ���� Ȱ��ȭ
                bigUni.SetActive(true);
                // �ִϸ����� �ٽ� ������ �ʱ�ȭ���ֱ�
                Uni.Instance.ResetAnimator();
                // ��ƼŬ ȿ�� �� ���ְ�..
                // ������ �����..
            }
            // ���࿡ ����ġ�� ���� ���� �Ѿ��
            if (evolutionState == 2 && currentExp > thirdEvolutionExp)
            {
                // 3�� ��ȭ
                Debug.Log("3 �� ��ȭ!!!!");
                evolutionState++;
                // ���� ���� ��Ȱ��ȭ
                bigUni.SetActive(false);
                // ���� ° ��ȭ�� ���� Ȱ��ȭ
                kingUni.SetActive(true);
                // �ִϸ����� �ٽ� ������ �ʱ�ȭ���ֱ�
                Uni.Instance.ResetAnimator();
                // ��ƼŬ ȿ�� �� ���ְ�..
                // ������ �����..
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

    // ������ ������, ���� ����ġ ����
    public void EatExp(float expAmount)
    {
        MyEXP += expAmount;
    }
}

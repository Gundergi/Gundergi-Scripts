using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �ð��� ������ ����Ʈ �ٴڸ� ����׷���Ƽ�� �������� üũ�Ǵ� ��ũ��Ʈ

public class LiftBroken : MonoBehaviour
{
    public List<GameObject> planeList = new List<GameObject>();     // ����Ʈ �ٴ� ���� ����Ʈ
    bool safeMode = true;
    bool isDrop;                                                    // ���������� ����                                   
    float currentTime;                                              // ����ð�
    float totalTime;                                                   //�ѽð� 
    float randomSeceond;                                            // ���ڰ� ������ ���� �� (3~6�� ����)
    public GameObject backPlane;                                    // ��������
    bool isFallBackplane;                                           // �������� ���������� üũ ����
    public Animator animator;                                       // ����Ʈ �ִϸ��̼��� ����

    AudioSource audioSource;
    public AudioClip clearFail;

    public GameObject[] stainList=new GameObject[10];                //������ ��轺ũ���� ������ �ϱ����ؼ�

    //������ ��轺ũ���� �������ؾ�...


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
        // �ð��� �帣�� �ִµ�
        currentTime += Time.deltaTime;
        totalTime += Time.deltaTime;

        //if (currentTime > liftUp.autoLiftTime)
        //{
        //    stainListCount++;
        //    currentTime = 0;
        //}

        //stainList[stainListCount].GetComponent<Stain>();
        //30�ʰ� ������ && 90%�̻� Ŭ�������� �������� => �ٴ��� �ϳ��� ������ 
        Debug.Log(stainListCount);
        if (currentTime > LiftUp.instance.autoLiftTime && stainList[stainListCount-1].GetComponent<Stain>().dropFloor)
        {
            // �������� ����
            //safeMode = false;
            // isDrop = true;
            Debug.Log("������� ����.");
            audioSource.clip = clearFail;
            audioSource.Play();
            DropFloor();
            // �ð� Reset
            currentTime = 0;

        }

        // 97������ �ٶ��ִϸ��̼� + �������� �������°� ���� 
        // �ѹ��� �����ϵ��� �����
        if (totalTime >= LiftUp.instance.autoLiftTime*7 && dropBack==false)
        {
            Debug.Log("�������� ������");
            dropBack = true;
            // ����ڵ� - ���� ����Ʈ�� ��찡 0���� ū ��쿡�� ����
            if (planeList.Count > 0)
            {
                // ���� ���ڰ� ��������. 
                PlayShakeAnimation();
                //PlayShakeAnimation();
                // ���� �ð��� �ٽ� ����
                // ResetRandomDropTime();
                // �ð� ����
            }

        }
    }


    // �Ʒ� ����
    public void DropFloor() // �� ������ 90% �̻� Ŭ�������� �������� ���� 
    {
        // �迭�� ������ �ִ� ���� �� �ϳ� �����ϰ� ������
        int randomIndex = Random.Range(0, planeList.Count);
        // �迭 �ȿ� ����ִ� ���ڵ� �� �����ϰ� 1���� useGravity Ȱ��ȭ
        planeList[randomIndex].GetComponent<Rigidbody>().isKinematic = false;
        planeList[randomIndex].GetComponent<Rigidbody>().useGravity = true;
        // �θ�-�ڽ� ���� ����
        planeList[randomIndex].transform.parent = null;
        // �� ����Ʈ���� ��� Ȱ��ȭ�� index�� ���ڸ� ����
        planeList.RemoveAt(randomIndex);
    }

    // ���� ����
    public void DropBack() // PlayShakeAnimation()  �� ���̽��� (�ٶ��� ������) �� �ѹ���  97���� ���� 
    {
        //animator.SetTrigger("Lift Wind");
        // ���� �������ڰ� ���������� ���� ���¶�� 
        if (isFallBackplane == false)
        {
            // ���ڸ� ����Ʈ�� ���̴�.
            backPlane.GetComponent<Rigidbody>().isKinematic = false;
            backPlane.GetComponent<Rigidbody>().useGravity = true;
            // �θ�-�ڽ� ���� ����
            transform.parent = null;
            // �������ڰ� �������ٴ� ���·� ����
            isFallBackplane = true;
        }
    }
    void PlayShakeAnimation() //  DropBack()�� ���� ����Ǵ°� /�ٶ��ִϸ��̼�  
    {
        animator.SetTrigger("Lift Wind");

        //float waitTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        //Lift Wind �ִϸ��̼� �� ��� �ð�/ (0)�� ���̾�, / [0]�� ����
        float waitTime = 14.5f;
        Debug.Log("waitTime : " + waitTime);
        // Lift Wind �ִϸ��̼��� ��� ����ǰ� ���� IsDrop�� ����
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

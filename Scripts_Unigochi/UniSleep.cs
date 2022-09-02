using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniSleep : MonoBehaviour
{
    // true�� ��쿡�� Q�� ������ ��, ���� �ִϸ��̼� ����ǵ��� üũ�ϴ� �༮..
    bool isReadySleepAnimation = false;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� Q ��ư�� �����ٸ�,
        if (isReadySleepAnimation == true)
        {
            // ���� ���� Q ��ư�� �����ٸ�,
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // �÷��̾� ������ ������..
                // �ִϸ��̼��� �ùٸ� �������� ����� �� �ֵ���;
                player.forward = transform.forward;

                FindObjectOfType<Uni>().Sleep();
            }
        }
    }

    //���� bed �ڽĿ� �پ��ִ� Trigger�����ȿ� �� �ִٸ� Sleep ����
    private void OnTriggerEnter(Collider other)
    {
        // ��ǥ : ���� �±װ� Player�� �� ��ü�� Trigger ���� ������ ���´ٸ�,
        if (other.tag == "Player")
        {
            // �÷��̾� ä����
            player = other.transform;

            // - Sleep �ִϸ��̼� �غ� ����!
            isReadySleepAnimation = true;
        }
    }
    //���� bed �ڽĿ� �پ��ִ� Trigger�����ȹ����� �����ٸ� Sleep ��
    private void OnTriggerExit(Collider other)
    {
        // ��ǥ : ���� �±װ� Player�� �� ��ü�� Trigger ���� ������ ���´ٸ�,
        if (other.tag == "Player")
        {
            // - Sleep �ִϸ��̼� �غ� ����!
            isReadySleepAnimation = false;
            // �÷��̾� �����
            player = null;
        }
    }
}

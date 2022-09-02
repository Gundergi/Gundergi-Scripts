using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    bool iscollisionGround = true;   // ���� �ݸ����� ��Ҵٸ�


    // onCollisionEnter : Player�� � ������Ʈ�� �ε��� ���� ����
    private void OnCollisionEnter(Collision collision)
    {
        // - ���� �ε��� ��ü ��ũ�� �׶�����
        if (collision.collider.tag == "Ground" && iscollisionGround)
        {
            // ���� ����
            UIManager.Instance.OpenGameOver();

            // �� �ѹ��� ����
            iscollisionGround = false;

            Debug.Log("isJump ��Ƽ� False�� �ٲ�");
        }
    }

}


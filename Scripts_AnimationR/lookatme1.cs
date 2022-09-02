using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ��(=target) �ٶ󺸱�
// - �� = ī�޶�
// - �ִϸ�����
// - ���̽� Ÿ��
public class lookatme1 : MonoBehaviour
{
    // �ٶ� ��� ���� ��
    public Transform mLookAt;
    // 
    private Vector3 targetPosition;

    private void Start()
    {
        // �±װ� ���� ī�޶��� ���ӿ�����Ʈ ���� �� ��������
        mLookAt = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        // y�� ������ ��� ���� ��ǥ ��ġ
        targetPosition = new Vector3(mLookAt.position.x, transform.position.y, mLookAt.position.z);
        // �װ� �ٶ�
        transform.LookAt(targetPosition);
    }
}

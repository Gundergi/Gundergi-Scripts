using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Camera ���� ���
// - ���� ����
// - ���� �ð�

public class CameraShake : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float shakeForce = 2.5f;   // - ���� ����
    public float shakeTime = 1.5f;    // - ���� �ð�

    Vector3 originPos;              // - ī�޶� ��鸮�� �� ���� ��ġ..
    float shakeFactor;              // - ī�޶� ���� ���� ���� ����

    bool isShake;                   // - ī�޶� ����ũ

    float currentTime;



    // Start is called before the first frame update
    void Start()
    {

        shakeFactor = shakeForce;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �����ض�! ��� ��� ������ ���� ����
        if (isShake)
        {
            // �ð��� �帥��.
            currentTime += Time.deltaTime;

            // �ּ� �� 0 ~ �ִ밪 shakeForce �� ShakeFactor ������ �ð��� �帧�� ���� ���� �ִ밪���� 0���� ����...
            shakeFactor = (1 - currentTime / shakeTime) * shakeForce * 0.1f;

            // ī�޶� �������� ��ġ �̵� ( �ݰ��� shakeFactor��ŭ�� �� �ȿ���)
            float randomX = Random.Range(-shakeFactor, shakeFactor);
            float randomY = Random.Range(-shakeFactor, shakeFactor);
            float randomZ = Random.Range(-shakeFactor, shakeFactor);
            transform.localPosition = new Vector3(originPos.x + randomX, originPos.y + randomY, originPos.z + randomZ);

            // ���� �ð��� �ٵǸ�... ī�޶� ���� ��ġ ������ �̵��ϰ� ���� �����.
            if (shakeFactor < 0.0001)
            {
                transform.localPosition = originPos;    // - ī�޶� ���� ��ġ��
                currentTime = 0;                        // - �ð��� Reset
                isShake = false;                        // - ���� ����
            }
        }
    }

    // ī�޶� Default ������ �Ǿ��ִ� ������ ������
    public void CamShake()
    {
        // �� ���� ��ġ�� ����ؾ� ��.
        originPos = transform.localPosition;

        isShake = true;
    }

    // ī�޶� n�ʵ���, n�� ����� ������
    public void CamShake(float time, float force)
    {
        // �� ���� ��ġ�� ����ؾ� ��.
        originPos = transform.localPosition;

        isShake = true;
        shakeForce = force;
        shakeTime = time;
    }
}
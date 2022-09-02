using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ������ Target�� ����ٴϴ� ���




public class fallowCamera : MonoBehaviour
{
    // - Target2(3��Ī ī�޶� ��ġ)
    public Transform targetThird;
    // - Target1(1��Ī ī�޶� ��ġ)
    public Transform targetFirst;

    // t = �����̴� ���� �ӵ�
    public float speed = 1f;      // �̵� �ӵ�
    public float rotSpeed = 1f;   // ȸ�� �ӵ�

    // - �� ���� � �����̾�?
    // - �� ���� � �����̾�?
    private bool isThird = false; // True : 3��Ī, False : 1dlscld




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Vector3.Lerp( A , B, t)
        // A = �� ��ġ
        // B = Target�� ��ġ
        // t = �����̴� ���� �ӵ�

        // ���� ���콺 ��Ŭ���� �Ѵٸ� ī�޶� ����
        if (Input.GetMouseButtonDown(1))
        {
            // isThird ���¸� �ٲٰ� �ʹ�.
            isThird = !isThird;
        }

        // ��. ���� ���� ���°� 3��Ī �����̶��?
        if (isThird == true)
        {
            // �� ��ġ == Target�� ��ġ
            // transform.position = target.position;
            // �� Z�� ���� == Target�� Z�� ����
            // transform.forward = target.forward;

            // [Lerp ���] �� ��ġ == Target(3��Ī)�� ��ġ
            transform.position = Vector3.Lerp(transform.position, targetThird.position, speed * Time.deltaTime);
            // [Lerp ���] �� Z�� ���� == Target(3��Ī)�� Z�� ����
            transform.rotation = Quaternion.Lerp(transform.rotation, targetThird.rotation, rotSpeed * Time.deltaTime);
        }
        // ��. ���� ���� ���°� 1��Ī �����̶��,
        else
        {
            // [����] : Lerp�� �س����� , ������°� ������ ī�޶� Player�� �� �հ� �̵��Ѵ�.
            //          ����� 1��Ī ���� ������ �ȵȴ�.
            // [�ذ�] : 3��Ī=>1��Ī �������� ��ȯ�� �� ��,
            //          ī�޶� Player�� ���������, Lerp ���� �ʰ�, ��ġ�� targerFirst�� ����
            // 3. ī�޶�� targetFirst ���� �Ÿ��� ���ؼ�
            float distance = Vector3.Distance(transform.position, targetFirst.position);
            // 2. ���� ����/�Ÿ� ������ ������
            if (distance < 1.5f)
            {
                // 1. ��ġ�� ȸ������ ������Ų��.
                // �� ��ġ == Target�� ��ġ
                transform.position = targetFirst.position;
                // �� Z�� ���� == Target�� Z�� ����
                transform.forward = targetFirst.forward;
            }
            // 2-1. ���� ���� �ٱ��� ������
            else
            {
                // [Lerp ���] �� ��ġ == Target(1��Ī)�� ��ġ
                transform.position = Vector3.Lerp(transform.position, targetFirst.position, speed * Time.deltaTime);
                // [Lerp ���] �� Z�� ���� == Target(1��Ī)�� Z�� ����
                transform.rotation = Quaternion.Lerp(transform.rotation, targetFirst.rotation, rotSpeed * Time.deltaTime);
            }
        }
    }
}

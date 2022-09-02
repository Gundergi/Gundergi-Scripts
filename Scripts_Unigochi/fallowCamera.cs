using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 내가 선택한 임의의 Target을 따라다니는 기능




public class fallowCamera : MonoBehaviour
{
    // - Target2(3인칭 카메라 위치)
    public Transform targetThird;
    // - Target1(1인칭 카메라 위치)
    public Transform targetFirst;

    // t = 움직이는 보간 속도
    public float speed = 1f;      // 이동 속도
    public float rotSpeed = 1f;   // 회전 속도

    // - 나 지금 어떤 시점이야?
    // - 나 지금 어떤 시점이야?
    private bool isThird = false; // True : 3인칭, False : 1dlscld




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Vector3.Lerp( A , B, t)
        // A = 내 위치
        // B = Target의 위치
        // t = 움직이는 보간 속도

        // 만일 마우스 우클릭을 한다면 카메라 변경
        if (Input.GetMouseButtonDown(1))
        {
            // isThird 상태를 바꾸고 싶다.
            isThird = !isThird;
        }

        // 가. 만일 현재 상태가 3인칭 시점이라면?
        if (isThird == true)
        {
            // 내 위치 == Target의 위치
            // transform.position = target.position;
            // 내 Z축 방향 == Target의 Z축 방향
            // transform.forward = target.forward;

            // [Lerp 사용] 내 위치 == Target(3인칭)의 위치
            transform.position = Vector3.Lerp(transform.position, targetThird.position, speed * Time.deltaTime);
            // [Lerp 사용] 내 Z축 방향 == Target(3인칭)의 Z축 방향
            transform.rotation = Quaternion.Lerp(transform.rotation, targetThird.rotation, rotSpeed * Time.deltaTime);
        }
        // 나. 만일 현재 상태가 1인칭 시점이라면,
        else
        {
            // [문제] : Lerp로 해놨더니 , 따라오는게 느려서 카메라가 Player를 막 뚫고 이동한다.
            //          제대로 1인칭 시점 고정이 안된다.
            // [해결] : 3인칭=>1인칭 시점으로 전환이 될 때,
            //          카메라가 Player와 가까워지면, Lerp 쓰지 않고, 위치를 targerFirst에 고정
            // 3. 카메라와 targetFirst 간의 거리를 구해서
            float distance = Vector3.Distance(transform.position, targetFirst.position);
            // 2. 일정 범위/거리 안으로 들어오면
            if (distance < 1.5f)
            {
                // 1. 위치와 회전값을 고정시킨다.
                // 내 위치 == Target의 위치
                transform.position = targetFirst.position;
                // 내 Z축 방향 == Target의 Z축 방향
                transform.forward = targetFirst.forward;
            }
            // 2-1. 일정 범위 바깥에 있으면
            else
            {
                // [Lerp 사용] 내 위치 == Target(1인칭)의 위치
                transform.position = Vector3.Lerp(transform.position, targetFirst.position, speed * Time.deltaTime);
                // [Lerp 사용] 내 Z축 방향 == Target(1인칭)의 Z축 방향
                transform.rotation = Quaternion.Lerp(transform.rotation, targetFirst.rotation, rotSpeed * Time.deltaTime);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Camera 흔들기 기능
// - 흔드는 세기
// - 흔들기 시간

public class CameraShake : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float shakeForce = 2.5f;   // - 흔드는 세기
    public float shakeTime = 1.5f;    // - 흔들기 시간

    Vector3 originPos;              // - 카메라 흔들리기 전 원래 위치..
    float shakeFactor;              // - 카메라 흔들기 조절 조절 백터

    bool isShake;                   // - 카메라 쉐이크

    float currentTime;



    // Start is called before the first frame update
    void Start()
    {

        shakeFactor = shakeForce;
    }

    // Update is called once per frame
    void Update()
    {
        // 흔들기 시작해라! 라고 명령 받으면 흔들기 시작
        if (isShake)
        {
            // 시간이 흐른다.
            currentTime += Time.deltaTime;

            // 최소 값 0 ~ 최대값 shakeForce 인 ShakeFactor 변수는 시간에 흐름에 따라 점점 최대값에서 0으로 수렴...
            shakeFactor = (1 - currentTime / shakeTime) * shakeForce * 0.1f;

            // 카메라를 랜덤으로 위치 이동 ( 반경이 shakeFactor만큼의 구 안에서)
            float randomX = Random.Range(-shakeFactor, shakeFactor);
            float randomY = Random.Range(-shakeFactor, shakeFactor);
            float randomZ = Random.Range(-shakeFactor, shakeFactor);
            transform.localPosition = new Vector3(originPos.x + randomX, originPos.y + randomY, originPos.z + randomZ);

            // 만일 시간이 다되면... 카메라 원래 위치 값으로 이동하고 흔들기 멈춘다.
            if (shakeFactor < 0.0001)
            {
                transform.localPosition = originPos;    // - 카메라 원래 위치로
                currentTime = 0;                        // - 시간도 Reset
                isShake = false;                        // - 흔들기 종료
            }
        }
    }

    // 카메라 Default 설정값 되어있는 것으로 흔들어줘
    public void CamShake()
    {
        // 내 원래 위치를 기억해야 됨.
        originPos = transform.localPosition;

        isShake = true;
    }

    // 카메라 n초동안, n의 세기로 흔들어줘
    public void CamShake(float time, float force)
    {
        // 내 원래 위치를 기억해야 됨.
        originPos = transform.localPosition;

        isShake = true;
        shakeForce = force;
        shakeTime = time;
    }
}
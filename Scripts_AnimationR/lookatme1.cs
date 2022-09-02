using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 나(=target) 바라보기
// - 나 = 카메라
// - 애니메이터
// - 페이스 타겟
public class lookatme1 : MonoBehaviour
{
    // 바라볼 대상 변형 값
    public Transform mLookAt;
    // 
    private Vector3 targetPosition;

    private void Start()
    {
        // 태그가 메인 카메라인 게임오브젝트 변형 값 가져오기
        mLookAt = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        // y축 제외한 평면 상의 목표 위치
        targetPosition = new Vector3(mLookAt.position.x, transform.position.y, mLookAt.position.z);
        // 그걸 바라봄
        transform.LookAt(targetPosition);
    }
}

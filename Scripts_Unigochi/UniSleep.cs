using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniSleep : MonoBehaviour
{
    // true인 경우에만 Q를 눌렀을 때, 슬립 애니메이션 실행되도록 체크하는 녀석..
    bool isReadySleepAnimation = false;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 만일 내가 Q 버튼을 눌렀다면,
        if (isReadySleepAnimation == true)
        {
            // 만일 내가 Q 버튼을 눌렀다면,
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // 플레이어 방향을 맞춰줌..
                // 애니메이션이 올바른 방향으로 실행될 수 있도록;
                player.forward = transform.forward;

                FindObjectOfType<Uni>().Sleep();
            }
        }
    }

    //만약 bed 자식에 붙어있는 Trigger영역안에 들어가 있다면 Sleep 실행
    private void OnTriggerEnter(Collider other)
    {
        // 목표 : 만일 태그가 Player로 된 물체가 Trigger 영역 안으로 들어온다면,
        if (other.tag == "Player")
        {
            // 플레이어 채워줌
            player = other.transform;

            // - Sleep 애니메이션 준비 시작!
            isReadySleepAnimation = true;
        }
    }
    //만약 bed 자식에 붙어있는 Trigger영역안밖으로 나갔다면 Sleep 끝
    private void OnTriggerExit(Collider other)
    {
        // 목표 : 만일 태그가 Player로 된 물체가 Trigger 영역 안으로 들어온다면,
        if (other.tag == "Player")
        {
            // - Sleep 애니메이션 준비 시작!
            isReadySleepAnimation = false;
            // 플레이어 비워줌
            player = null;
        }
    }
}

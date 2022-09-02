using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    bool iscollisionGround = true;   // 땅에 콜리젼이 닿았다면


    // onCollisionEnter : Player가 어떤 오브젝트랑 부딪힌 순간 실행
    private void OnCollisionEnter(Collision collision)
    {
        // - 만약 부딪힌 물체 테크가 그라운드라면
        if (collision.collider.tag == "Ground" && iscollisionGround)
        {
            // 게임 종료
            UIManager.Instance.OpenGameOver();

            // 딱 한번만 실행
            iscollisionGround = false;

            Debug.Log("isJump 닿아서 False로 바꿈");
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animation Event 함수로 쓰일, 나 자신을 비활성화 시킬 기능
public class DeactiveAnimEvent : MonoBehaviour
{
    // =>> 진화 버튼 누르면, 진화 애니메이션이 끝난 후 실행한다.
    public void Deactive()
    {
        // 포켓몬진화 이미지를 변경해주는 함수 불러오기
        FindObjectOfType<PokemonSystem>().ChangeEvolutionBottonImage();
        gameObject.SetActive(false);
    }
}

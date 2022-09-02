using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 어떤 아이템을 먹었을 때, 체력을 추가하는 효과를 만들고 싶어요.
// UsableItem.UsageEffect : CreatorKit에서 만든 Class
// ===> 프로젝트 내에서 아이템 관련된 녀석들 제어

public class AddHealthEffect : MonoBehaviour
{
    [Tooltip("체력을 회복시킬 비율(%)")]
    public int healthAmount;
    [Tooltip("경험치 획득량")]
    public int expAmount;

    HungryStatus hungryStatus;
    LevelUpStatus levelUpStatus;

    private void Start()
    {
        // 하이어라키에서 HungryStatus 컴포넌트 찾은 것중 제일 첫 번째 거 가져와 할당하기
        hungryStatus = GameObject.FindObjectOfType<HungryStatus>();

        // 하이어라키에서 LevelUpStatus 컴포넌트 찾은 것중 제일 첫 번째 거 가져와 할당하기
        levelUpStatus = GameObject.FindObjectOfType<LevelUpStatus>();
    }


    // 헝그리 스테이트 회복, 내가 가진 회복량 만큼
    public void AddHealth()
    {
        // 체력을 회복한다.
        hungryStatus.EatFood(healthAmount);

        Debug.Log($"체력 회복 !! : >> [{healthAmount}]point");
        
        AddExp();

        Destroy(gameObject);

    }

    // 레벨 업 스테이트 경험치 추가, 내가 가진 경험치 만큼
    public void AddExp()
    {
        // 경험치를 먹는다.
        levelUpStatus.EatExp(expAmount);

        Debug.Log($"경험치 획득 !! : >> [{expAmount}]point");
    }
}

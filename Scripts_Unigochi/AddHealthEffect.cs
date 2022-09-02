using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// � �������� �Ծ��� ��, ü���� �߰��ϴ� ȿ���� ����� �;��.
// UsableItem.UsageEffect : CreatorKit���� ���� Class
// ===> ������Ʈ ������ ������ ���õ� �༮�� ����

public class AddHealthEffect : MonoBehaviour
{
    [Tooltip("ü���� ȸ����ų ����(%)")]
    public int healthAmount;
    [Tooltip("����ġ ȹ�淮")]
    public int expAmount;

    HungryStatus hungryStatus;
    LevelUpStatus levelUpStatus;

    private void Start()
    {
        // ���̾��Ű���� HungryStatus ������Ʈ ã�� ���� ���� ù ��° �� ������ �Ҵ��ϱ�
        hungryStatus = GameObject.FindObjectOfType<HungryStatus>();

        // ���̾��Ű���� LevelUpStatus ������Ʈ ã�� ���� ���� ù ��° �� ������ �Ҵ��ϱ�
        levelUpStatus = GameObject.FindObjectOfType<LevelUpStatus>();
    }


    // ��׸� ������Ʈ ȸ��, ���� ���� ȸ���� ��ŭ
    public void AddHealth()
    {
        // ü���� ȸ���Ѵ�.
        hungryStatus.EatFood(healthAmount);

        Debug.Log($"ü�� ȸ�� !! : >> [{healthAmount}]point");
        
        AddExp();

        Destroy(gameObject);

    }

    // ���� �� ������Ʈ ����ġ �߰�, ���� ���� ����ġ ��ŭ
    public void AddExp()
    {
        // ����ġ�� �Դ´�.
        levelUpStatus.EatExp(expAmount);

        Debug.Log($"����ġ ȹ�� !! : >> [{expAmount}]point");
    }
}

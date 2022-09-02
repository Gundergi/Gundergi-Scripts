using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animation Event �Լ��� ����, �� �ڽ��� ��Ȱ��ȭ ��ų ���
public class DeactiveAnimEvent : MonoBehaviour
{
    // =>> ��ȭ ��ư ������, ��ȭ �ִϸ��̼��� ���� �� �����Ѵ�.
    public void Deactive()
    {
        // ���ϸ���ȭ �̹����� �������ִ� �Լ� �ҷ�����
        FindObjectOfType<PokemonSystem>().ChangeEvolutionBottonImage();
        gameObject.SetActive(false);
    }
}

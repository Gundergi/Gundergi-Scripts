using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ��ȭ�� �����ϴ� ��ȭ ���� �ý��� (�̱���)
public class PokemonSystem : MonoBehaviour
{
    //�̱���
    public static PokemonSystem Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // ���� ��ȭ ����
    public enum EvolutionState
    {
        FIRST, SECOND, THIRD
    }
    public EvolutionState state = EvolutionState.FIRST;

    public int ATK
    {
        get => atk; set => atk = value;
    }
    public float Force
    {
        get => frc; set => frc = value;
    }

    private float frc = 0.3f;

    [Header("Poke Status")]
    // ���� ������Ʈ
    public GameObject[] pokemonCoils;
    // ���ϸ� ���ݷ�
    public int atk = 5;

    // ���� ��ư �� Ŭ������ ���ϰ� ����
    [Header("Evolution")]
    bool isAttack;
    public int attackCount = 0;
    public int evolutionNumber = 3;
    public GameObject evolutionButton;
    public Sprite[] evolutionImages;


    private void Start()
    {
        // ��ȭ��ư�� ��Ȱ��ȭ�� ���·� ����
        evolutionButton.SetActive(false);
    }

    // ȣ��Ǹ� ������ ��ȭ�Ŵ�.
    public void Evolution()
    {
        switch (state)
        {
            case EvolutionState.FIRST:   // 1 ��° ��ȭ ���� (����)
                // �������� ��ȭ �ÿ�, attckCount ä�����ϴ� evolutionNumber �� 10���� �ʱ�ȭ
                evolutionNumber = 3;
                ATK = 10;
                Force = 2.5f;
                // ��ȭ ���� ���� (����=>��������)
                StartCoroutine("EveloutionPokemon");
                state = EvolutionState.SECOND;
                break;
            case EvolutionState.SECOND:  // 2 ��° ��ȭ ���� (��������)
                StartCoroutine("EveloutionPokemon");
                evolutionNumber = 3;
                ATK = 20;
                Force = 2.5f;
                // ��ȭ ���� ���� (��������=>�ް���)
                state = EvolutionState.THIRD;
                break;
            case EvolutionState.THIRD:   // 3 ��° ��ȭ ���� (�ް���)
                break;
        }

        // ��ȭ��ư ��Ȱ��ȭ
        DeactiveEvolutionButton();

    }

    public void Attack()
    {

        // isAttack�� True( =�����ϴ� ��)�̶�� ����..
        if (isAttack) return;
        // isAttack�� false(=��� ����)��� ����!
        isAttack = true;

        // ���ݹ�ư ���� �� ���� +1��
        attackCount++;
        // ���� 3�� �ǰų� Ŀ���� �� evolutionnumber���� Ŀ����, ��ȭ��ư ����
        if (attackCount == evolutionNumber)
        {
            if (state == EvolutionState.FIRST)
            {
                ActiveEvolutionButton();
            }
            if (state == EvolutionState.SECOND)
            {
                ActiveEvolutionButton();
            }
            //if (state == EvolutionState.THIRD)
            //{
            //    DeactiveEvolutionButton();
            //}
            // ��ȭ��ư �����ֱ�(Ȱ��ȭ)

        }

        switch (state)
        {
            case EvolutionState.FIRST:   // 1 ��° ��ȭ ���� (����)
                pokemonCoils[0].GetComponent<CharactorAnimation>().Attack();
                break;
            case EvolutionState.SECOND:  // 2 ��° ��ȭ ���� (��������)
                pokemonCoils[1].GetComponent<CharactorAnimation>().Attack();
                break;
            case EvolutionState.THIRD:   // 3 ��° ��ȭ ���� (��������)
                pokemonCoils[2].GetComponent<CharactorAnimation>().Attack();
                break;
        }

        float delayTime = 4;
        // delayTime ��ŭ ��� �ߴٰ�.. isAttack�� false�� ����
        StartCoroutine(BlockAttackBtn(delayTime));
    }

    IEnumerator EveloutionPokemon()
    {
        // 0 : (First) ��ȭ ��
        // 1 : (Second)1��° ��ȭ
        // 2 : (Third) 2��° ��ȭ
        // �� ��ȭ���¿� ���� index �ʱ�ȭ
        int index = state == EvolutionState.FIRST ? 0 : 1;
        // ��ȭ���¿� ���� ���ϸ��� ������ �ִ� ��ȭ ��ƼŬ�� ��� �ð��� 1�� ��ŭ�� ��� �ð����� �ʱ�ȭ
        float delayTime = pokemonCoils[index].GetComponent<CharactorAnimation>().evolveParticle.main.duration * 0.5f;

        // ��ȭ �ִϸ��̼�
        pokemonCoils[index].GetComponent<CharactorAnimation>().Evolve();
        // ��� �ð���ŭ ��ٷȴٰ�...
        yield return new WaitForSeconds(delayTime);

        // ���� ���ϸ� ��Ȱ��ȭ, ���� ��ȭ ���ϸ� Ȱ��ȭ
        pokemonCoils[index].SetActive(false);
        pokemonCoils[index + 1].SetActive(true);
    }

    IEnumerator BlockAttackBtn(float time)
    {
        yield return new WaitForSeconds(time);

        isAttack = false;
    }

    public void ActiveEvolutionButton()
    {
        evolutionButton.SetActive(true);
    }
    public void DeactiveEvolutionButton()
    {
        //evolutionButton.SetActive(false);
        evolutionButton.GetComponent<Animator>().SetTrigger("Click");
        //attackCount �ʱ�ȭ
        attackCount = 0;
    }

    // ��ȭ��ư�� �̹����� �������Ͽ��� �ް������� �ٲ��ִ� �Լ�
    public void ChangeEvolutionBottonImage()
    {
        // evolutionBottonImage�� Sprite�̹����� ����
        // 1. evolutionBotton�� ������ �ִ� �̹���������Ʈ�� ����
        // 2. �̹���������Ʈ�� �������ִ� �ҽ��̹����� ����(=Sprite)
        // 3. �ش� �ҽ��̹����� Sprite�� �ް��� Sprite�̹����� ����
        evolutionButton.GetComponent<Image>().sprite = evolutionImages[1];
    }

}

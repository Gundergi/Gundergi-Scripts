using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 코일 진화를 관리하는 진화 관리 시스템 (싱글톤)
public class PokemonSystem : MonoBehaviour
{
    //싱글톤
    public static PokemonSystem Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 현재 진화 상태
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
    // 코일 오브젝트
    public GameObject[] pokemonCoils;
    // 포켓몬 공격력
    public int atk = 5;

    // 공격 버튼 막 클릭하지 못하게 막음
    [Header("Evolution")]
    bool isAttack;
    public int attackCount = 0;
    public int evolutionNumber = 3;
    public GameObject evolutionButton;
    public Sprite[] evolutionImages;


    private void Start()
    {
        // 진화버튼은 비활성화된 상태로 시작
        evolutionButton.SetActive(false);
    }

    // 호출되면 코일을 진화신다.
    public void Evolution()
    {
        switch (state)
        {
            case EvolutionState.FIRST:   // 1 번째 진화 상태 (코일)
                // 레어코일 진화 시에, attckCount 채워야하는 evolutionNumber 를 10으로 초기화
                evolutionNumber = 3;
                ATK = 10;
                Force = 2.5f;
                // 진화 상태 변경 (코일=>레어코일)
                StartCoroutine("EveloutionPokemon");
                state = EvolutionState.SECOND;
                break;
            case EvolutionState.SECOND:  // 2 번째 진화 상태 (레어코일)
                StartCoroutine("EveloutionPokemon");
                evolutionNumber = 3;
                ATK = 20;
                Force = 2.5f;
                // 진화 상태 변경 (레어코일=>메가존)
                state = EvolutionState.THIRD;
                break;
            case EvolutionState.THIRD:   // 3 번째 진화 상태 (메가존)
                break;
        }

        // 진화버튼 비활성화
        DeactiveEvolutionButton();

    }

    public void Attack()
    {

        // isAttack이 True( =공격하는 중)이라면 종료..
        if (isAttack) return;
        // isAttack이 false(=대기 상태)라면 실행!
        isAttack = true;

        // 공격버튼 누를 때 마다 +1씩
        attackCount++;
        // 만약 3이 되거나 커지면 즉 evolutionnumber보다 커지면, 진화버튼 생성
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
            // 진화버튼 보여주기(활성화)

        }

        switch (state)
        {
            case EvolutionState.FIRST:   // 1 번째 진화 상태 (코일)
                pokemonCoils[0].GetComponent<CharactorAnimation>().Attack();
                break;
            case EvolutionState.SECOND:  // 2 번째 진화 상태 (레어코일)
                pokemonCoils[1].GetComponent<CharactorAnimation>().Attack();
                break;
            case EvolutionState.THIRD:   // 3 번째 진화 상태 (자포코일)
                pokemonCoils[2].GetComponent<CharactorAnimation>().Attack();
                break;
        }

        float delayTime = 4;
        // delayTime 만큼 대기 했다가.. isAttack을 false로 변경
        StartCoroutine(BlockAttackBtn(delayTime));
    }

    IEnumerator EveloutionPokemon()
    {
        // 0 : (First) 진화 전
        // 1 : (Second)1번째 진화
        // 2 : (Third) 2번째 진화
        // 각 진화상태에 따른 index 초기화
        int index = state == EvolutionState.FIRST ? 0 : 1;
        // 진화상태에 따른 포켓몬이 가지고 있는 진화 파티클의 재생 시간의 1초 만큼을 대기 시간으로 초기화
        float delayTime = pokemonCoils[index].GetComponent<CharactorAnimation>().evolveParticle.main.duration * 0.5f;

        // 진화 애니메이션
        pokemonCoils[index].GetComponent<CharactorAnimation>().Evolve();
        // 대기 시간만큼 기다렸다가...
        yield return new WaitForSeconds(delayTime);

        // 현재 포켓몬 비활성화, 다음 진화 포켓몬 활성화
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
        //attackCount 초기화
        attackCount = 0;
    }

    // 진화버튼의 이미지를 레어코일에서 메가존으로 바꿔주는 함수
    public void ChangeEvolutionBottonImage()
    {
        // evolutionBottonImage의 Sprite이미지를 변경
        // 1. evolutionBotton이 가지고 있는 이미지컴포넌트에 접근
        // 2. 이미지컴포넌트가 가지고있는 소스이미지에 접근(=Sprite)
        // 3. 해당 소스이미지에 Sprite를 메가존 Sprite이미지로 변경
        evolutionButton.GetComponent<Image>().sprite = evolutionImages[1];
    }

}

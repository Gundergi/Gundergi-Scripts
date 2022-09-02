using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//버튼 관리하는 컨트롤러,코일 애니메이션 실행 및 진화관리

//버튼 눌렀을때 공격
//버튼 눌렀을때 애니메이션 파티클 실행

//공격 버튼 눌렀을때 별가사리 HP감소

//공격 버튼 눌렀을때 5번 카운트 되면 진화 버튼 생성
//진화 버튼 눌렀을때 진화

//



public class CharactorAnimation : MonoBehaviour
{
    [Header("Pocketmon")]
    public GameObject coil; //코일넣기
    public GameObject staryu;//별가사리
    //public GameObject nextCoil;//다음진화 코일
    [Header("Animation")]
    public Animator coilAni; //코일 애니메이션
    public Animator starAni; //별가사리 애니메이션

    [Header("Particle")]
    public ParticleSystem evolveParticle;//진화파티클
    public ParticleSystem attackParticle;//공격파티클
    public ParticleSystem damageHitParticle;//별가사리 맞았을때 파티클
    [Header("AudioSource")]
    public AudioSource attackBgm;
    public AudioSource evolutionBgm;
    public AudioSource entryBgm;
    //[Header("Icon")]
    //public Image playBtnImg;


    // Start is called before the first frame update
    void Start()
    {
        //별가사리,코일애니메이션 초기화한다
        coilAni = coilAni.GetComponent<Animator>();
        starAni = starAni.GetComponent<Animator>();
        //별가사리,코일 등장애니메이션 실행
        //coilAni.SetTrigger("Attack");
        //starAni.SetTrigger("Entry");
        attackBgm = attackBgm.GetComponent<AudioSource>();
        evolutionBgm = evolutionBgm.GetComponent<AudioSource>();
        entryBgm = entryBgm.GetComponent<AudioSource>();
        if (PokemonSystem.Instance.state == PokemonSystem.EvolutionState.FIRST)
        {

        }
        else
        {
            StartCoroutine(playDelay());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //버튼 눌렀을때 공격, 파티클 실행
    public void Attack()
    {
        // 1. 코일 인 상태일 때,
        if (PokemonSystem.Instance.state == PokemonSystem.EvolutionState.FIRST)
        {
            //어택 애니메이션 실행
            coilAni.SetTrigger("Attack");
            attackParticle.Stop();
            attackParticle.Play();
            damageHitParticle.Stop();
            damageHitParticle.Play();
            StartCoroutine(playTilTime());
        }
        // 2. 레어코일인 상태일 때..
        else if (PokemonSystem.Instance.state == PokemonSystem.EvolutionState.SECOND)
        {
            //어택 애니메이션 실행
            coilAni.SetTrigger("Attack");
            attackParticle.Stop();
            attackParticle.Play();
            damageHitParticle.Stop();
            damageHitParticle.Play();
            attackBgm.Play();
        }
        // 3. 메가존인 상태일 때,..
        else if (PokemonSystem.Instance.state == PokemonSystem.EvolutionState.THIRD)
        {
            //어택 애니메이션 실행
            coilAni.SetTrigger("Attack");
            attackParticle.Stop();
            attackParticle.Play();
            damageHitParticle.Stop();
            damageHitParticle.Play();
            attackBgm.Play();
        }
    }
    //버튼 눌렀을때 진화
    public void Evolve()
    {
        
        //GetComponent<AudioSource>().Play(); ;
        //진화 애니메이션 실행
        //coilAni.SetTrigger("Evlove");
        evolveParticle.Stop();
        evolveParticle.Play();
        evolutionBgm.Play();
        //coil.SetActive(false);
        //nextCoil.SetActive(true);
    }

    IEnumerator playTilTime()
    {
        attackBgm.Play();
        yield return new WaitForSeconds(3.0f);
        attackBgm.Stop();
    }

    IEnumerator playDelay()
    {
     
        entryBgm.Stop();
        yield return new WaitForSeconds(2f);
        entryBgm.Play();
    }

}

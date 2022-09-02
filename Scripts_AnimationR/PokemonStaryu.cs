using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonStaryu : MonoBehaviour
{
    public enum StaryuState
    {
        Idle,
        DamageHit,
        Die
    }
    public StaryuState staryuState = StaryuState.Idle;


    private Animator animator;                      // - StarYu의 애니메이션 관리

    public GameObject target;                       // - 일정 범위 안에 들어오면 Target(particle) 발견
    public float damageShockeTime = 5f;             // - 피격 시 , 일어나는 충격 시간
    public float destroyTime = 5f;                  // - 죽었을 때, 사라지기까지 걸리는 시간
    private float currentTime = 0f;                 // - 경과시간
    public float attackRange = 1f;                  // - 일정 범위(공격)
    private int healthPoint = 100;                  // - HP 체력
    public Image hpBar;                             // - HP 체력바
                                                    //public int damage = 10;
    public GameObject Coil;

    [Header("Sound")]
    public AudioClip[] clips;
    public AudioSource audioSource;                 // - 맞았을 때 소리
    [Header("Effect")]
    /*public float damage = 1.0f;*/                 // - 데미지
    public ParticleSystem CoilEffectPrefab;         // - 노란전기충격을 맞은 순간 효과
    public ParticleSystem MegaEffectPrefab;         // - 파란전기충격을 맞은 순간 효과
                                                    // ===== 별가사리 =========
 

    // HP Get/Set Property
    public int HP
    {
        get
        {
            return healthPoint;
        }
        set
        {
            healthPoint = value;
            // hp 0~100 을 0~1로 환산
            float amount = healthPoint / 100.0f;
            Debug.Log(amount);
            // healthPoint 갱신 때마다, HPBar 갱신
            hpBar.fillAmount = amount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
  
        // 나한테 붙어있는 animator 컴포넌트를 가져와 할당
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (staryuState)
        {
            //case StaryuState.Idle: Idle(); break;
            case StaryuState.DamageHit: DamageHitToIdle(); break;
            case StaryuState.Die: Die(); break;
        }
    }


    public void ChangeState(StaryuState state)
    {

        if (state == StaryuState.DamageHit)
        {
            animator.SetTrigger("DamageHit");
        }

        else if (state == StaryuState.Die)
        {
            animator.SetTrigger("Die");
        }

        staryuState = state;
        currentTime = 0;
    }




    // 피격 시, 일정 시간 대기 후에 상태를 Idle로 전환하고 싶다.
    private void DamageHitToIdle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > damageShockeTime)
        {
            // 현재 상태를 DamageHit => Ilde로 변환
            ChangeState(StaryuState.Idle);
        }
    }

    // Staryu가 피격당했을 때 호출되는 함수
    public void DamageProcess(int damage)
    {
        if (staryuState == StaryuState.Die)
        {
            return;
        }

        Debug.Log("Attack");

        // - HP 감소
        HP -= damage;

        // 피격 시 실행되는 특수효과.. 
        PlayDamageEffect();
        // 스킬 부딪히면, 카메라 흔들기 효과 실행
        float force = FindObjectOfType<PokemonSystem>().Force;
        Camera.main.GetComponent<CameraShake>().CamShake(1.5f, force);
        // 오디오 재생
        PlaySoundDamaged();

        // - HP가 아직 있는 경우
        if (healthPoint > 0)
        {
            ChangeState(StaryuState.DamageHit);
        }
        else
        {
            ChangeState(StaryuState.Die);
          
            //오디오 재생
            PlaySoundWinner();
          
        }

    }
    // 호출되면 별가사리의 HP 감소

    public void Die()
    {
       

        currentTime += Time.deltaTime;
        if (currentTime > destroyTime)
        {
            Destroy(gameObject);
            currentTime = 0;
        }
    }

    // 피격 시 실행되는 특수효과.. 
    void PlayDamageEffect()
    {

        // 1. 현재 포켓시스템에서 현재 진화상태를 알아낸다.
        PokemonSystem.EvolutionState state = FindObjectOfType<PokemonSystem>().state;
        // 3. 만일 서드라면 파란색 전기파티클 재생
        if (state == PokemonSystem.EvolutionState.THIRD)
        {
            // 파란색 파티클 재생시킨다.
            MegaEffectPrefab.Stop();
            MegaEffectPrefab.Play();

        }
        // 2. 그 외라면(=만약 퍼스트나 세컨드라면 노란색 전기파티클 재생)
        else
        {
            // 파란색 파티클 재생시킨다.
            CoilEffectPrefab.Stop();
            CoilEffectPrefab.Play();

        }
    }
   
    // 피격소리 
    void PlaySoundDamaged()
    {
        if (audioSource.clip != clips[0]) { audioSource.clip = clips[0]; }
        audioSource.Play();
    }
    // 배틀승리 소리 
    void PlaySoundWinner()
    {
        if (audioSource.clip != clips[1]) { audioSource.clip = clips[1]; }
        audioSource.Play();
        Coil.GetComponent<BattleStart>().audioSource.Stop();
    }
}


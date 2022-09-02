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


    private Animator animator;                      // - StarYu�� �ִϸ��̼� ����

    public GameObject target;                       // - ���� ���� �ȿ� ������ Target(particle) �߰�
    public float damageShockeTime = 5f;             // - �ǰ� �� , �Ͼ�� ��� �ð�
    public float destroyTime = 5f;                  // - �׾��� ��, ���������� �ɸ��� �ð�
    private float currentTime = 0f;                 // - ����ð�
    public float attackRange = 1f;                  // - ���� ����(����)
    private int healthPoint = 100;                  // - HP ü��
    public Image hpBar;                             // - HP ü�¹�
                                                    //public int damage = 10;
    public GameObject Coil;

    [Header("Sound")]
    public AudioClip[] clips;
    public AudioSource audioSource;                 // - �¾��� �� �Ҹ�
    [Header("Effect")]
    /*public float damage = 1.0f;*/                 // - ������
    public ParticleSystem CoilEffectPrefab;         // - ������������ ���� ���� ȿ��
    public ParticleSystem MegaEffectPrefab;         // - �Ķ���������� ���� ���� ȿ��
                                                    // ===== �����縮 =========
 

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
            // hp 0~100 �� 0~1�� ȯ��
            float amount = healthPoint / 100.0f;
            Debug.Log(amount);
            // healthPoint ���� ������, HPBar ����
            hpBar.fillAmount = amount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
  
        // ������ �پ��ִ� animator ������Ʈ�� ������ �Ҵ�
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




    // �ǰ� ��, ���� �ð� ��� �Ŀ� ���¸� Idle�� ��ȯ�ϰ� �ʹ�.
    private void DamageHitToIdle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > damageShockeTime)
        {
            // ���� ���¸� DamageHit => Ilde�� ��ȯ
            ChangeState(StaryuState.Idle);
        }
    }

    // Staryu�� �ǰݴ����� �� ȣ��Ǵ� �Լ�
    public void DamageProcess(int damage)
    {
        if (staryuState == StaryuState.Die)
        {
            return;
        }

        Debug.Log("Attack");

        // - HP ����
        HP -= damage;

        // �ǰ� �� ����Ǵ� Ư��ȿ��.. 
        PlayDamageEffect();
        // ��ų �ε�����, ī�޶� ���� ȿ�� ����
        float force = FindObjectOfType<PokemonSystem>().Force;
        Camera.main.GetComponent<CameraShake>().CamShake(1.5f, force);
        // ����� ���
        PlaySoundDamaged();

        // - HP�� ���� �ִ� ���
        if (healthPoint > 0)
        {
            ChangeState(StaryuState.DamageHit);
        }
        else
        {
            ChangeState(StaryuState.Die);
          
            //����� ���
            PlaySoundWinner();
          
        }

    }
    // ȣ��Ǹ� �����縮�� HP ����

    public void Die()
    {
       

        currentTime += Time.deltaTime;
        if (currentTime > destroyTime)
        {
            Destroy(gameObject);
            currentTime = 0;
        }
    }

    // �ǰ� �� ����Ǵ� Ư��ȿ��.. 
    void PlayDamageEffect()
    {

        // 1. ���� ���Ͻý��ۿ��� ���� ��ȭ���¸� �˾Ƴ���.
        PokemonSystem.EvolutionState state = FindObjectOfType<PokemonSystem>().state;
        // 3. ���� ������ �Ķ��� ������ƼŬ ���
        if (state == PokemonSystem.EvolutionState.THIRD)
        {
            // �Ķ��� ��ƼŬ �����Ų��.
            MegaEffectPrefab.Stop();
            MegaEffectPrefab.Play();

        }
        // 2. �� �ܶ��(=���� �۽�Ʈ�� �������� ����� ������ƼŬ ���)
        else
        {
            // �Ķ��� ��ƼŬ �����Ų��.
            CoilEffectPrefab.Stop();
            CoilEffectPrefab.Play();

        }
    }
   
    // �ǰݼҸ� 
    void PlaySoundDamaged()
    {
        if (audioSource.clip != clips[0]) { audioSource.clip = clips[0]; }
        audioSource.Play();
    }
    // ��Ʋ�¸� �Ҹ� 
    void PlaySoundWinner()
    {
        if (audioSource.clip != clips[1]) { audioSource.clip = clips[1]; }
        audioSource.Play();
        Coil.GetComponent<BattleStart>().audioSource.Stop();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//��ư �����ϴ� ��Ʈ�ѷ�,���� �ִϸ��̼� ���� �� ��ȭ����

//��ư �������� ����
//��ư �������� �ִϸ��̼� ��ƼŬ ����

//���� ��ư �������� �����縮 HP����

//���� ��ư �������� 5�� ī��Ʈ �Ǹ� ��ȭ ��ư ����
//��ȭ ��ư �������� ��ȭ

//



public class CharactorAnimation : MonoBehaviour
{
    [Header("Pocketmon")]
    public GameObject coil; //���ϳֱ�
    public GameObject staryu;//�����縮
    //public GameObject nextCoil;//������ȭ ����
    [Header("Animation")]
    public Animator coilAni; //���� �ִϸ��̼�
    public Animator starAni; //�����縮 �ִϸ��̼�

    [Header("Particle")]
    public ParticleSystem evolveParticle;//��ȭ��ƼŬ
    public ParticleSystem attackParticle;//������ƼŬ
    public ParticleSystem damageHitParticle;//�����縮 �¾����� ��ƼŬ
    [Header("AudioSource")]
    public AudioSource attackBgm;
    public AudioSource evolutionBgm;
    public AudioSource entryBgm;
    //[Header("Icon")]
    //public Image playBtnImg;


    // Start is called before the first frame update
    void Start()
    {
        //�����縮,���Ͼִϸ��̼� �ʱ�ȭ�Ѵ�
        coilAni = coilAni.GetComponent<Animator>();
        starAni = starAni.GetComponent<Animator>();
        //�����縮,���� ����ִϸ��̼� ����
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

    //��ư �������� ����, ��ƼŬ ����
    public void Attack()
    {
        // 1. ���� �� ������ ��,
        if (PokemonSystem.Instance.state == PokemonSystem.EvolutionState.FIRST)
        {
            //���� �ִϸ��̼� ����
            coilAni.SetTrigger("Attack");
            attackParticle.Stop();
            attackParticle.Play();
            damageHitParticle.Stop();
            damageHitParticle.Play();
            StartCoroutine(playTilTime());
        }
        // 2. ���������� ������ ��..
        else if (PokemonSystem.Instance.state == PokemonSystem.EvolutionState.SECOND)
        {
            //���� �ִϸ��̼� ����
            coilAni.SetTrigger("Attack");
            attackParticle.Stop();
            attackParticle.Play();
            damageHitParticle.Stop();
            damageHitParticle.Play();
            attackBgm.Play();
        }
        // 3. �ް����� ������ ��,..
        else if (PokemonSystem.Instance.state == PokemonSystem.EvolutionState.THIRD)
        {
            //���� �ִϸ��̼� ����
            coilAni.SetTrigger("Attack");
            attackParticle.Stop();
            attackParticle.Play();
            damageHitParticle.Stop();
            damageHitParticle.Play();
            attackBgm.Play();
        }
    }
    //��ư �������� ��ȭ
    public void Evolve()
    {
        
        //GetComponent<AudioSource>().Play(); ;
        //��ȭ �ִϸ��̼� ����
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

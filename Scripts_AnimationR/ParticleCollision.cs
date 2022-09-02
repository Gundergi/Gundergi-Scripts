using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Ÿ��name : " + other.name);
        Debug.Log("Ÿ��tag : " + other.tag);
        // ��ƼŬ�� �ε��� ��ü�� Tag �� Enemy���,, (=�����縮)
        if (other.tag == "Enemy")
        {
            int damage = FindObjectOfType<PokemonSystem>().ATK;
            other.GetComponent<PokemonStaryu>().DamageProcess(damage);
            //=== Input Code .. ��ƼŬ �ε����� �� ���� === 
        }
    }
}

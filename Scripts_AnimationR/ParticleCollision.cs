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
        Debug.Log("타격name : " + other.name);
        Debug.Log("타격tag : " + other.tag);
        // 파티클이 부딪힌 물체의 Tag 가 Enemy라면,, (=별가사리)
        if (other.tag == "Enemy")
        {
            int damage = FindObjectOfType<PokemonSystem>().ATK;
            other.GetComponent<PokemonStaryu>().DamageProcess(damage);
            //=== Input Code .. 파티클 부딪혔을 때 실행 === 
        }
    }
}

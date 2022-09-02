using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public int HP;
    public int PP;

    public virtual void Attack()
    {
        Debug.Log("Attack!!!");
    }

    public virtual void Evolution()
    {
        PokemonSystem.Instance.Evolution();
    }
}

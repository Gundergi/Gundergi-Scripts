using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coil : Pokemon
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayParticle()
    {

    }

    public override void Attack()
    {
        PlayParticle();
    }

    public override void Evolution()
    {
        base.Evolution();
    }
}

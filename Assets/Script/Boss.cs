using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] int m_hp = 50;
    [SerializeField] float m_time = 0;
    [SerializeField] GameObject m_muzzle = null;
    [SerializeField] GameObject m_beam = null;
    [SerializeField] GameObject m_beamEffect = null;
    bool m_lvl1 = false;
    bool m_lvl2 = false;
    bool m_normalAttack = false;
    bool m_normalAttack2 = true;
    Rigidbody2D m_rb;
    Animator m_ani;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        //Debug.Log(m_time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossAttck();
    }

    void BossAttck()
    {
        if (m_hp <= 40 && m_lvl1 == false)
        {
            m_ani.Play("BossJump");
            m_lvl1 = true;
            //stantiate();
        }
        else if (m_hp <= 20 && m_lvl2 == false)
        {
            m_ani.Play("BossBeamchrage");
            GameObject go = Instantiate(m_beam, m_muzzle.transform.position, m_beam.transform.rotation);
            m_lvl2 = true;
            Debug.Log("s");
        }
        else if (m_normalAttack == false)
        {
            m_ani.Play("BossNormal");
            m_normalAttack = true;
            m_normalAttack2 = false;
        }
        else if(m_normalAttack2 == false)
        {
            m_ani.Play("BossNormal2");
            m_normalAttack2 = true;
            m_normalAttack = false;
        }
    }
}

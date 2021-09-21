using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] int m_hp = 50;
    [SerializeField] float m_time = 0;
    [SerializeField] GameObject m_muzzle = null;
    [SerializeField] GameObject m_beam = null;
    Rigidbody2D m_rb;
    Animator m_ani;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        Debug.Log(m_time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_hp <= 40)
        {
            m_ani.Play("BossJump");
        }
        if(m_hp <= 20)
        {
            //Instantiate()
        }

    }
}

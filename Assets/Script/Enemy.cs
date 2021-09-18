using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 target;
    [SerializeField] private float speed;
    [SerializeField] Transform target1;
    [SerializeField] int m_hp = 10;
    [SerializeField] GameObject m_effct = null;
    GameObject PlayerObject;
    Animator m_ani;
    Rigidbody2D m_rb;
    private bool x;
    private bool y;
    private Vector2 force;
    private float h_move;
    private float v_move;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ani = GetComponent<Animator>();

        PlayerObject = GameObject.Find("Player");
        target1 = PlayerObject.transform;
    }
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attak();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_rb.velocity = Vector2.zero;
        }
    }
    void attak()
    {
        Vector2 e_pos = transform.position;
        Vector2 p_pos = target1.position;

        //target1の方向に動くベクトル
        force = (p_pos - e_pos);
        //じわじわ追跡
        m_rb.velocity = force.normalized * speed;

        //Vector2 direction = new Vector2(h_move, v_move).normalized;
        //m_rb.velocity = direction * speed;

        m_ani.SetFloat("motionx", force.x);
        //m_ani.SetFloat("motiony", force.y);
        if (force.x < 0)
        {
            m_ani.SetFloat("motiony", force.y);
            this.transform.localScale = new Vector2(-1, 1);
        }
        if (force.x > 0)
        {
            m_ani.SetFloat("motiony", force.y);
            this.transform.localScale = new Vector2(1, 1);
        }
    }

    //ダメージ判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            m_hp -= 2;
            Debug.Log("-2");
            if(m_hp <= 0)
            {
                GameObject go = Instantiate(m_effct);
                go.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
        }
        else
        {
            m_hp--;
        }

    }
}
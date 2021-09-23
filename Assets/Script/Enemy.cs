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
    [SerializeField] Transform[] m_muzzles = null;
    [SerializeField] GameObject m_enemyBulletPrefab = null;
    [SerializeField] float m_fireInterval = 1f;
    [SerializeField] float m_bulletSpeed = 1f;
    GameObject PlayerObject;
    Animator m_ani;
    Rigidbody2D m_rb;
    AudioSource m_se;
    private bool x;
    private bool y;
    private Vector2 force;
    private float h_move;
    private float v_move;
    private float m_timer;
    private float m_time = 1f;
    private bool m_trigger = true;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ani = GetComponent<Animator>();
        m_se = GetComponent<AudioSource>();

        PlayerObject = GameObject.Find("Player");
        target1 = PlayerObject.transform;

        if (m_muzzles == null || m_muzzles.Length == 0)
        {
            m_muzzles = new Transform[1] { this.transform };
        }
    }
    void Update()
    {
        m_timer += Time.deltaTime;
        if (PlayerObject)
        {
            if (m_time < m_timer)
            {
                if (m_trigger == false)
                {
                    m_timer = 0;
                    //敵の座標を変数posに保存
                    var pos = this.gameObject.transform.position;
                    //弾のプレハブを作成
                    var t = Instantiate(m_enemyBulletPrefab);
                    //弾のプレハブの位置を敵の位置にする
                    t.transform.position = pos;
                    //敵からプレイヤーに向かうベクトルをつくる
                    //プレイヤーの位置から敵の位置（弾の位置）を引く
                    Vector2 vec = PlayerObject.transform.position - pos;
                    //弾のRigidBody2Dコンポネントのvelocityに先程求めたベクトルを入れて力を加える
                    t.GetComponent<Rigidbody2D>().velocity = vec * m_bulletSpeed;
                    m_se.Play();
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attak();
            m_trigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_rb.velocity = Vector2.zero;
            m_trigger = true;
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
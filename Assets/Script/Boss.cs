using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] int m_hp = 50;
    [SerializeField] float m_time = 0;
    [SerializeField] float m_timer = 0;
    [SerializeField] float m_reset= 0;
    [SerializeField] GameObject m_muzzle = null;
    [SerializeField] GameObject m_beam = null;
    [SerializeField] GameObject m_beamEffect = null;
    [SerializeField] GameObject m_effct = null;
    [SerializeField] GameObject Player;
    [SerializeField] float m_bulletSpeed = 0;
    bool m_lvl1 = false;
    bool m_lvl2 = false;
    bool m_lvl3 = true;
    bool m_normalAttack = false;
    bool m_normalAttack2 = true;
    int count = 0;
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
        if(m_time <= m_timer)
        {
            BossAttck();
            m_time = 0;
        }

        if(m_lvl3 == false)
        {
            var pos = this.gameObject.transform.position;
            //弾のプレハブを作成
            var t = Instantiate(m_beam);
            var tt = Instantiate(m_beam);
            //弾のプレハブの位置を敵の位置にする
            t.transform.position = pos;
            //敵からプレイヤーに向かうベクトルをつくる
            //プレイヤーの位置から敵の位置（弾の位置）を引く
            Vector2 vec = Player.transform.position - pos;
            //弾のRigidBody2Dコンポネントのvelocityに先程求めたベクトルを入れて力を加える
            t.GetComponent<Rigidbody2D>().velocity = vec * m_bulletSpeed;
            tt.GetComponent<Rigidbody2D>().velocity = -vec * m_bulletSpeed;
            count++;
            if (count == 1000)
            {
                m_lvl3 = true;
            }
        }
        //Debug.Log(m_time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BossAttck();
        }
        if (collision.gameObject.tag == "Sword")
        {
            m_hp -= 2;
            if(m_hp <= 0)
            {
                GameObject go = Instantiate(m_effct);
                go.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        BossAttck();
    }

    void BossAttck()
    {
        if (m_hp <= 40 && m_lvl1 == false)
        {
            m_normalAttack = true;
            m_normalAttack2 = true;
            m_ani.Play("BossJump");
            m_lvl1 = true;
            Reset();
            //stantiate();
        }
        else if (m_hp <= 20 && m_lvl2 == false)
        {
            m_ani.Play("BossBeamchrage");
            for(int i = 0; i < 100; i++)
            {

            }
            m_lvl2 = true;
            m_lvl3 = false;
            Reset();
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

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(m_reset);
        m_normalAttack = false;
        m_normalAttack2 = true;
    }
}
     
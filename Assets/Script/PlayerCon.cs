using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
    [SerializeField] int m_hp = 5;
    [SerializeField] float m_hp1 = 1;
    [SerializeField] float m_speed = 5;
    [SerializeField] GameObject m_muzzle;
    [SerializeField] GameObject m_arrowPrefab;
    [SerializeField] float m_arrowSpeed = 5f;
    [SerializeField] GameObject m_sword = null;
    [SerializeField] GameObject m_effect = null;
    [SerializeField] GameObject m_deadEffect = null;
    [SerializeField] float m_timer = 0;
    [SerializeField] float m_reset = 1;
    [SerializeField] Slider m_slider;
    private bool m_swordTrigger = false;
    private float m_time = 0;
    //最後に向いてる方向
    Vector2 m_lastPosition;
    Rigidbody2D m_rb;
    Animator m_ani;
    SpriteRenderer m_sp;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ani = GetComponent<Animator>();
        m_sp = GetComponent<SpriteRenderer>();
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        m_slider.value = m_hp1;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 ver = LastInputDirection(h, v);
        m_rb.velocity = ver * m_speed;

        if (ver.x != 0)
        {
            m_sp.flipX = (ver.x < 0);
        }

        Animate(ver.x, ver.y);

        m_lastPosition = ver;

        if (m_time > m_timer)
        {
            m_swordTrigger = false;
            m_time = 0;
        }

        //攻撃モーション
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if(Input.GetButtonDown("Fire2"))
        {
            Atack();
        }
    }

    Vector2 LastInputDirection(float inputX , float inputY)
    {
        Vector2 ver = new Vector2(inputX, inputY);
        
        if(m_lastPosition == Vector2.zero)
        {
            if(ver.x != 0 && ver.y != 0)
            {
                ver.y = 0;
            }
        }
        else if(m_lastPosition.x != 0)
        {
            ver.y = 0;
        }
        else if(m_lastPosition.y != 0)
        {
            ver.x = 0;
        }
        return ver;
    }

    void Animate(float inputX, float inputY)
    {
        if (m_ani == null) return;

        if (inputX != 0)
        {
            m_ani.Play("Player_side");
        }
        else if (inputY > 0)
        {
            m_ani.Play("Player_back");
        }
        else if (inputY < 0)
        {
            m_ani.Play("Player_front");
        }
        else
        {
            if (m_lastPosition.x != 0)
            {
                m_ani.Play("Idle_side");
            }
            else if (m_lastPosition.y > 0)
            {
                m_ani.Play("Idle_back");
            }
            else if (m_lastPosition.y < 0)
            {
                m_ani.Play("Idle_front");
            }
        }
    }

    //弓を発射する
    void Fire()
    {
        if(m_arrowPrefab && m_muzzle)
        {
            GameObject go = Instantiate(m_arrowPrefab, m_muzzle.transform.position, m_arrowPrefab.transform.rotation);
            go.transform.SetParent(this.transform);
        }
    }

    //剣を振る
    void Atack()
    {
        if (m_swordTrigger == false)
        {
            Instantiate(m_sword, m_muzzle.transform.position, m_sword.transform.rotation);
            Instantiate(m_effect, m_muzzle.transform.position, m_effect.transform.rotation);
            m_swordTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="bullet")
        {
            m_hp--;
            m_hp1-=0.2f;
            if (m_hp <= 0)
            {
                //Reset();
                GameObject go = Instantiate(m_deadEffect);
                go.transform.position = this.transform.position;
                SceneManager.LoadScene("GameOver");
                Destroy(this.gameObject);
            }
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(m_reset);
        SceneManager.LoadScene("GameOver");
        Debug.Log("a");
    }
}

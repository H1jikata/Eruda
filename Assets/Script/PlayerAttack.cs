using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Vector2 m_arrowUp;
    [SerializeField] float m_arrowSpeed = 10f;
    Rigidbody2D m_rb;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        Vector3 v = m_arrowUp.normalized * m_arrowSpeed;
        m_rb.velocity = v;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

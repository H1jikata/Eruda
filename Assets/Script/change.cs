using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change : MonoBehaviour
{
    [SerializeField] string m_scenename = null;
    public void Botom(string name)
    {
        SceneManager.LoadScene(name);
    }
    
}

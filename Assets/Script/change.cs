using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change : MonoBehaviour
{
    [SerializeField] string m_scenename = null;
    [SerializeField] float m_time = 1;

    public void EndingGame()
    {
        //3秒後にメソッドを実行する
        Invoke("LoadEndingScene", m_time);
    }

    void LoadEndingScene()
    {
        SceneManager.LoadScene(m_scenename);
    }
}
   /* public void Botom(string name)
    {
        SE();
    }

    IEnumerator SE()
    {
        yield return new WaitForSeconds(m_time);
        SceneManager.LoadScene(m_scenename);
    }
}*/

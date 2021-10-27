

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Loading : MonoBehaviour
{
	//　読み込み率を表示するスライダー
	[SerializeField]
	private Slider m_slider;
	[SerializeField] int m_reset = 0;
	[SerializeField] float m_chageTime = 0.5f;


	private void Start()
	{
		m_slider = GameObject.Find("Slider").GetComponent<Slider>();
	}
	//public void NextScene()
	//{
	//	StartCoroutine("Load");
	//}

	float m_time;
	void Update()
	{
		//m_time += Time.deltaTime;
		//m_slider.value = m_time;
	}

	public void Change(float value)
    {
		ChangeValue(m_slider.value + value);
    }

	public void Fill()
	{
		ChangeValue(1f);
	}

	public void ChangeValue(float value)
    {
		DOTween.To(() => m_slider.value, x => m_slider.value = x, value, m_chageTime).OnComplete(() =>SceneManager.LoadSceneAsync("Main")); ;
    }
	//IEnumerator Load()
	//{
	//	yield return new WaitForSeconds(m_reset);
	//}
}

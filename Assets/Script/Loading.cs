

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
	//　読み込み率を表示するスライダー
	[SerializeField]
	private Slider m_slider;
	[SerializeField] int m_reset = 0;


	private void Start()
	{
		m_slider = GameObject.Find("Slider").GetComponent<Slider>();
	}
	public void NextScene()
	{
		StartCoroutine("Load");
	}

	float m_time;
	void Update()
	{
		m_time += Time.deltaTime;
		m_slider.value = m_time;
	}
	IEnumerator Load()
	{
		yield return new WaitForSeconds(m_reset);
		SceneManager.LoadSceneAsync("Main");
	}
}

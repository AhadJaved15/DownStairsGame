using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DS_Spawner : MonoBehaviour {

	public GameObject[] stairs;
	public Image[] life;
	public Vector2 spawnValues;
	public float spawnWait;
	public float spawnMostWait;
	public float spawnLeastWait;
	public int startWait;
	public bool stop;

	public Button startBtn, goBackBtn, crossBtn,playAgainBtn;
	public Image panelImg , gameOverPanel;
	public Text scoreText;

	public GameObject player;
	int randStair;
	int lifeRemaining;

	// Use this for initialization
	void Start () {
		lifeRemaining = 5;
		for (int i = 0; i < life.Length; i++) {
			life [i].gameObject.SetActive (true);
		}
		Time.timeScale = 0;
	}

	public void ChangeScene(int i)
	{
		SceneManager.LoadScene (i);
	}
	public Image QuitBox;
	public void ExitBtn()
	{
		Application.Quit ();
	}
	public void NoButton()
	{
		QuitBox.gameObject.SetActive (false);
		Time.timeScale = 1;
	}
	public void cross()
	{
		QuitBox.gameObject.SetActive (true);
		Time.timeScale = 0;
	}

	public void StartFunc()
	{
		panelImg.gameObject.SetActive (false);
		Time.timeScale = 1;
		StartCoroutine (waitSpanwer ());
	}

	public void GoBackFunc()
	{
		SceneManager.LoadScene (0);
	}

	public void PlayAgainFunc()
	{
		SceneManager.LoadScene (6);
	}



	public void LifeCount()
	{
		lifeRemaining--;
		lifeRemaining = Mathf.Clamp (lifeRemaining, 0, 5);
		life [lifeRemaining].gameObject.SetActive (false);
		if(lifeRemaining==0)
			{
			gameOverPanel.gameObject.SetActive (true);
			player.SetActive (false);
			Time.timeScale = 0;
			}
	}
	// Update is called once per frame
	void Update () {
		spawnWait = Random.Range (spawnLeastWait, spawnMostWait);
	}

	IEnumerator waitSpanwer()
	{
		yield return new WaitForSeconds (startWait);
		while (!stop) {
			
			randStair = Random.Range (0, 5);
			Vector2 spawnPosition = new Vector2 (Random.Range(-spawnValues.x,spawnValues.x),-7);
			Instantiate (stairs [randStair], spawnPosition, gameObject.transform.rotation);
			yield return new WaitForSeconds (spawnWait);
		}
	}
}

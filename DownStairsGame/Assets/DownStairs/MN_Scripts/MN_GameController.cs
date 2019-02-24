using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MN_GameController : MonoBehaviour {

	public GameObject[] strips;
	public Image[] lifeImg;
	public GameObject move;
	public Text scoreText, tileNumText;
	public Button startBtn, goBackBtn , playAgain;
	public Image panelImg , gameOverPanel;


	private List<int> myList = new List<int>();
	private List<int> userInput = new List<int>();
	private Animator[] cubeAnim;
	private int count;
	private int numLength;
	private int inputCount;

	private bool isTypeable = false;
	private bool check = true;
	private float moveLength = 0f;
	private int myListIndex;
	private int playerLife;
	private int Score;
	private int tileNum;

	private float moveSpeed = .05f;
	bool ismoveable;
	float distance;


	// Use this for initialization
	void Start () {
		Score = 0;
		tileNum = 0;
		numLength = 1;
		count = 0;
		inputCount = 0;
		myListIndex = 0;
		playerLife = 3;

		ismoveable = false;
		distance = 0f;

	}

	public void StartGame()
	{
		panelImg.gameObject.SetActive (false);
		DefineLength ();
	}
	public Image QuitBox;
	public void Cross()
	{
		QuitBox.gameObject.SetActive (true);
		Time.timeScale = 0;
	}

	public void NoButton()
	{
		QuitBox.gameObject.SetActive (false);
		Time.timeScale = 1;
	}

	public void SceneChange(int i)
	{
		SceneManager.LoadScene (i);
	}

	public void PlayAgainBtn()
	{
		SceneManager.LoadScene (5);
	}
	
	// Update is called once per frame
	void Update () {
		if (ismoveable) {
			move.transform.Translate (0f, 0f, -moveSpeed,Space.World);
			distance += .05f;
			if (distance >= 1f) {
				ismoveable = false;
				distance = 0f;
			    DefineLength ();
			}
		}
	}

	public void checkInput(int value)
	{
		if(isTypeable)
		{
			Debug.Log ("typeValue: "+value);
			userInput.Add (value);
			inputCount++;
			if (myList.Count == inputCount) {
				for(int i=0 ;i < myList.Count ; i++)
				{
					if (myList [i] != userInput [i]) {
						check = false;
						break;
					}
				}
				if (check) {
					tileNum += 1;
					Score += 100;
					scoreText.text = "" + Score;
					tileNumText.text = "" + tileNum;
					MoveCamera ();
				} else {
					tileNum += 1;
					tileNumText.text = "" + tileNum;
					lifeImg [playerLife-1].gameObject.SetActive (false);
					playerLife--;

					Debug.Log("Invalid Input");
					if (playerLife == 0) {
						gameOverPanel.gameObject.SetActive (true);
					}
					MoveCamera ();
				}
			}


		}
	}

	void MoveCamera()
	{
		isTypeable = false;
		inputCount = 0;
		myListIndex = 0;
		myList.Clear ();
		userInput.Clear ();
		count++;
		//StartCoroutine ("Move");
		ismoveable = true;
	}

	void DefineLength()
	{
		
		if (count < 3) {
			numLength = 3;
		} else if (count < 10) {
			numLength = 5;
		} else if (count < 15) {
			numLength = 7;
		}else {
			numLength = 8;
		}
		myList = GenerateRandomNumbers (numLength);
		for (int i = 0; i < myList.Count; i++) {
			Debug.Log ("value : " + myList [i]);
		}
		StripCubeAnim ();
	}


	List<int> GenerateRandomNumbers(int numLength)
	{
		
		for(int i=1; i<=numLength;i++)
		{
			int value = Random.Range (1, 6);
			myList.Add (value); 

		}
		return myList;
	}


	void StripCubeAnim()
	{
		cubeAnim = strips [count].GetComponentsInChildren<Animator> ();
		numLength = 0;
		StartCoroutine ("Wait");

	}

	IEnumerator Wait()
	{
		//yield return new WaitForSeconds (.2f);

		for(int i =0 ; i< 5 ;i++)
		{

			if (cubeAnim [i].gameObject.CompareTag (myList[myListIndex].ToString())) {
				
				yield return new WaitForSeconds (.5f);
				cubeAnim [i].SetBool ("flag", true);
				yield return new WaitForSeconds (0.8f);
				cubeAnim [i].SetBool ("flag", false);
				if(numLength < myList.Count-1)
				{
					Debug.Log ("list index: " + myListIndex);
					Debug.Log ("list index: " + myList.Count);
					myListIndex++;
					numLength++;
					StartCoroutine ("Wait");
					break;
				}
				break;
			}

		}
		if (numLength == myList.Count-1) {


			isTypeable = true;
		}
	}
}

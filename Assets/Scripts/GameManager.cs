using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum State{ PLAY, CONSOLE}
public class GameManager : MonoBehaviour
{
	public State gameState;
	Player myPlayer;
	static bool tutorialCheck;
	Scene currentScene;

	public Text myText;

	public GameObject console;
	MyConsole myConsole;
	public bool activeConsole;
	private void Awake()
	{
		myPlayer = FindObjectOfType<Player>(); 
	}

	// Start is called before the first frame update
	void Start()
    {
		currentScene = SceneManager.GetActiveScene();
		tutorialCheck = TutorialManager.isCompleted;
		myConsole = console.GetComponent<MyConsole>();

		myConsole.AddCommand("Restart", Restart, "Restarts the level");
		myConsole.AddCommand("Inmortal", Inmortality, "Turns your health to 100");
		myConsole.AddCommand("AddChain", AddChain, "Adds extra chains");
		myConsole.AddCommand("Turbo", Turbo, "Doubles your basic speed");
    }

    // Update is called once per frame
    void Update()
    {
		if(myPlayer != null)
		{
			myText.text = "" + myPlayer.health;
			Defeat();
		}


		if (Input.GetKeyDown(KeyCode.Ampersand) && currentScene.name != "Menu")
		{
			Menu();
		}

		if (Input.GetKeyDown(KeyCode.Escape) && (currentScene.name == "Lvl" || currentScene.name == "Tutorial"))
		{
			activeConsole = !activeConsole;
			Debug.Log("Hola " + activeConsole);
			console.SetActive(activeConsole);
		}

		if (activeConsole)
		{
			gameState = State.CONSOLE;
		}
		else
		{
			gameState = State.PLAY;
		}

		if(gameState != State.PLAY)
		{
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public void Inmortality(List<string> data)
	{
		myPlayer.health = 100;
	}

	public void AddChain(List<string> data)
	{
		if (myPlayer.maxLenght < 10)
			myPlayer.maxLenght++;
		else
			myConsole.consoleText.text += "/n" + " - Max chains reached.";
	}

	public void Turbo(List<string> data)
	{
		if(myPlayer.moveSpeed < 10f)
			myPlayer.moveSpeed = 10f;
		else
			myConsole.consoleText.text += "/n" + " - Max speed reached.";
	}

	public void PlayGame()
	{
		if (currentScene.name == "Menu" && tutorialCheck == false || currentScene.name == "Defeat" && tutorialCheck == false)
			SceneManager.LoadScene("Tutorial");
		else if (currentScene.name == "Tutorial" || currentScene.name == "Defeat" || currentScene.name == "Victory" || currentScene.name == "Menu" && tutorialCheck == true)
			SceneManager.LoadScene("Lvl");
	}

	public void Menu()
	{
		SceneManager.LoadScene(0);
		Cursor.lockState = CursorLockMode.None;
	}

	public void Restart(List<string> data)
	{
		Restart();
	}

	void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Defeat()
	{
		if(myPlayer.health <= 0)
		{
			SceneManager.LoadScene("Defeat");
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void Victory()
	{
		SceneManager.LoadScene("Victory");
		Cursor.lockState = CursorLockMode.None;
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

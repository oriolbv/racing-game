using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
	public Text ResultText;

    public void Start()
    {
		ResultText.text = "Best lap: " + GameData.Instance.BestTime.ToString() + " seconds";
    }

    public void OnClickRestart()
	{
		SceneManager.LoadScene("CircuitScene");
	}

	public void OnClickMainMenu()
	{
		SceneManager.LoadScene("TitleScene");
	}
}

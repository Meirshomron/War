using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {

    public Text roundsText;

    private void OnEnable()
    {
        int score = (int)(1 / (Time.realtimeSinceStartup - PlayerStats.startTime)*3000);
        roundsText.text = score.ToString();
    }
    
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main_menu");
    }

}

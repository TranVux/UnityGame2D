using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class ButtonOnClick : MonoBehaviour
{
    public GameObject menuGame;
    public AudioSource pauseSound;
    public void StartGame()
    {
        SceneManager.LoadScene("SceneLevel1");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseSound.Play();
        //menuGame.GetComponent<MenuReplay>().Close();
    }

    public void Pause()
    {
        StartCoroutine(handleAnimationPause());
        pauseSound.Play();
    }

    public IEnumerator handleAnimationPause()
    {
        GameObject player = GameObject.Find("Player");
        menuGame.GetComponent<MenuReplay>().Open(player.GetComponent<PlayerScript>().totalCoin ,"Game Pause");
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0;

    }

    public void Resume()
    {
        Time.timeScale = 1;
        menuGame.GetComponent<MenuReplay>().Close();
        pauseSound.Play();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        pauseSound.Play();
    }
}

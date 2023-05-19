using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneLoader : MonoBehaviour
{   
    [SerializeField]
    private GameObject blackScreen;

    private Image blackScreenImage;
    private bool isEnd = false;
    private bool isResetScene = false;

    private float desiredAlpha = 1;
    private float currentAlpha = 0;


    void Start() {
        blackScreenImage = blackScreen.GetComponent<Image>();
        StartCoroutine(delaySplashScreen());
    }

    void Update() {

        if(isEnd) {
            if(currentAlpha == desiredAlpha) {
                resetScene();
            } else {
                currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 2.0f * Time.deltaTime);
                var color = blackScreenImage.color;
                color.a = currentAlpha;
                blackScreenImage.color = color;
            }
        }
    }
    public static void resetScene() {
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator delaySplashScreen(){
        yield return new WaitForSeconds(10f);
        isEnd = true;
    }

}

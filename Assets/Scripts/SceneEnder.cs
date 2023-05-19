using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneEnder : MonoBehaviour
{   
    private Image blackScreenImage;
    private float desiredAlpha = 1;
    private float currentAlpha = 0;


    void Start() {
        currentAlpha = 0;
        desiredAlpha = 1;
        blackScreenImage = GameObject.FindGameObjectWithTag("BlackScreen").GetComponent<Image>();
    }

    void Update() {
        if(currentAlpha == desiredAlpha) {
            resetScene();
        } else {
            currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 2.0f * Time.deltaTime);
            var color = blackScreenImage.color;
            color.a = currentAlpha;
            blackScreenImage.color = color;
        }
    }
    public static void resetScene() {
        SceneManager.LoadScene("DeathAnimation");
    }
}

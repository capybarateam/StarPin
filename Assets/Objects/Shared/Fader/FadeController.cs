using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    private float duration = 1.5f;
    
    private float alfa;

    private string changeSceneName = null;
    private bool changeState = false;

    CanvasGroup alphaimage;

    // Start is called before the first frame update
    void Start()
    {
        alphaimage = GetComponent<CanvasGroup>();
        alfa = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!changeState && alfa >= 0.0f )
        {
            alfa -= 1.0f / duration * Time.deltaTime;

            changeState = alfa <= 0.0f ? true : false;
        }
        else if(changeSceneName != null)
        {
            alfa += 1.0f / duration * Time.deltaTime;

            if (alfa >= 1.0f)
                SceneManager.LoadScene(changeSceneName);
        }
        alphaimage.alpha = alfa;
    }

    public void ChangeScene(string nextSceneName)
    {
        if (changeState)
            changeSceneName = nextSceneName;
    }
}

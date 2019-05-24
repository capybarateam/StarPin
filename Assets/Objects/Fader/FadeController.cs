using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;
    
    private float alfa;
    private float red;
    private float green;
    private float blue;

    private string changeSceneName = null;
    private bool changeState = false;
    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        red = image.color.r;
        green = image.color.g;
        blue = image.color.b;
        alfa = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!changeState && alfa >= 0.0f )
        {
            alfa -= 1.0f / speed * Time.deltaTime;
            GetComponent<Image>().color = new Color(red, green, blue, alfa);

            changeState = alfa <= 0.0f ? true : false;
        }
        else if(changeSceneName != null)
        {
            alfa += 1.0f / speed * Time.deltaTime;
            GetComponent<Image>().color = new Color(red, green, blue, alfa);

            if (alfa >= 1.0f)
                SceneManager.LoadScene(changeSceneName);
        }
    }

    public void ChangeScene(string nextSceneName)
    {
        if (changeState)
            changeSceneName = nextSceneName;
    }
}

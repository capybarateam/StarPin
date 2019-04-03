using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void StartGame()
    {
        GameObject.Find("StageClear").GetComponent<Animator>().SetBool("Enabled", false);
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameObject star = GameObject.Find("StarObject");
        cam.GetComponent<CameraController>().SetTarget(star);
        GameObject letter = GameObject.Find("LetterBox");
        if (letter != null)
        {
            letter.GetComponent<Animator>().SetBool("Enabled", true);
        }
        Invoke("OnStarted", 2);
    }

    void OnStarted()
    {
        GameObject letter = GameObject.Find("LetterBox");
        if (letter != null)
        {
            letter.GetComponent<Animator>().SetBool("Enabled", false);
        }

        GameObject title = GameObject.Find("StageTitle");
        if (title != null)
        {
            title.GetComponent<Animator>().SetBool("Enabled", false);
        }
    }
}

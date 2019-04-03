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
        var cl = GameObject.Find("StageClear");
        if (cl)
            cl.GetComponent<Animator>().SetBool("Enabled", false);
        var ca = GameObject.Find("Main Camera");
        if (ca)
        {
            Camera cam = ca.GetComponent<Camera>();
            GameObject star = GameObject.Find("StarObject");
            cam.GetComponent<CameraController>().SetTarget(star);
        }
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

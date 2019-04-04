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
        SetEnabled("StageClear", false);
        SetEnabled("LetterBox", false);

        var star = GameObject.Find("StarObject");
        CameraController.Get().SetTarget(star);

        Invoke("OnStarted", 2);
    }

    public void EndGame()
    {
        StageSelector.Get().LoadNextStage();
    }

    void OnStarted()
    {
        SetEnabled("LetterBox", false);
        SetEnabled("StageTitle", false);
    }

    void SetEnabled(string name, bool flag)
    {
        var obj = GameObject.Find(name);
        if (obj != null)
            obj.GetComponent<Animator>().SetBool("Enabled", flag);
    }
}

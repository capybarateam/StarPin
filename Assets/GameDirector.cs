using UnityEngine;
using UnityEngine.UI;

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
        var title = GameObject.Find("StageTitle");
        var selector = StageSelector.Get();
        if (title && selector && selector.current)
            title.GetComponentInChildren<Text>().text = selector.current.stageName;

        GameUtils.SetEnabled("LetterBox", true);
        GameUtils.SetEnabled("StageTitle", true);

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
        GameUtils.SetEnabled("LetterBox", false);
        GameUtils.SetEnabled("StageTitle", false);
    }
}

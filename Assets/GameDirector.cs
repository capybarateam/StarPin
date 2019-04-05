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

        this.Delay(2, () =>
        {
            GameUtils.SetEnabled("LetterBox", false);
            GameUtils.SetEnabled("StageTitle", false);
        });
    }

    public void EndGame()
    {
        StageSelector.Get().LoadNextStage();
    }
}

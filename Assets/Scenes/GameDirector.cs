using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        CameraController.Get().Targetter.SetTarget(StarController.latestStar);
        StartGame();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void StartGame()
    {
    }

    public void EndGame()
    {
        StageSelector.Get().LoadNextStage();
    }

    public static GameDirector Get(Transform t)
    {
        return t.GetComponentInParent<GameDirector>();
    }
}

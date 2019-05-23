using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public PointManager pointManager;

    [SerializeField]
    private GameObject showTarget = null;

    [SerializeField]
    private float showTime = 0.0f;

    private enum CameraType
    {
        ShowWorld,
        ShowGoal,
        ShowPlay
    }

    private CameraType camType;

    private float countTime = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        pointManager = GetComponent<PointManager>();
        camType = CameraType.ShowWorld;

        CameraController.Get().Targetter.SetTarget(showTarget);
        StartGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if(camType.Equals(CameraType.ShowWorld))
        {
            countTime += Time.deltaTime;
            if (countTime >= showTime)
            {
                CameraController.Get().Targetter.SetTarget(GoalController.latestGoal);
                camType = CameraType.ShowGoal;
                countTime = 0.0f;
            }
        }
        else if(camType.Equals(CameraType.ShowGoal))
        {
            countTime += Time.deltaTime;
            if (countTime >= showTime)
            {
                CameraController.Get().Targetter.SetTarget(StarController.latestStar);
                camType = CameraType.ShowPlay;
                countTime = 0.0f;
            }
        }
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

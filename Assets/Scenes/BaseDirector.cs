using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseDirector : MonoBehaviour
{
    public GameObject letterBox;
    public GameObject stageTitle;
    public GameObject stageClear;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("TitleScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageChangeEffect(bool starting)
    {
        if (starting)
        {
            var title = stageTitle?.GetComponentInChildren<Text>();
            if (title)
                title.text = StageSelector.Get()?.Current?.stageName ?? "";
        }

        letterBox?.GetComponent<Animator>().SetBool("Enabled", starting);
        stageTitle?.GetComponent<Animator>().SetBool("Enabled", starting);
    }

    public void StageClearEffect(bool starting)
    {
        stageClear?.GetComponent<Animator>().SetBool("Enabled", starting);
    }

    public static BaseDirector Get()
    {
        return GameObject.Find("GameBase")?.GetComponent<BaseDirector>();
    }
}

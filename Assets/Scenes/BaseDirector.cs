using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BaseDirector : MonoBehaviour
{
    public GameObject letterBox;
    public GameObject stageTitle;
    public GameObject stageClear;

    // Start is called before the first frame update
    void Start()
    {
        SceneSelector.Get().LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StageChangeEffect(bool starting)
    {

        if (letterBox)
            letterBox.GetComponent<Animator>().SetBool("Enabled", starting);
        if (stageTitle)
        {
            stageTitle.GetComponent<Animator>().SetBool("Enabled", starting);
            if (starting)
            {
                var title = stageTitle.GetComponentInChildren<TMP_Text>();
                if (title)
                    title.text = StageSelector.Get()?.Current?.stageName ?? "";
            }
        }
    }

    public void StageClearEffect(bool starting)
    {
        if (stageClear)
            stageClear.GetComponent<Animator>().SetBool("Enabled", starting);
    }

    public static BaseDirector Get()
    {
        return GameObject.Find("GameBase")?.GetComponent<BaseDirector>();
    }
}

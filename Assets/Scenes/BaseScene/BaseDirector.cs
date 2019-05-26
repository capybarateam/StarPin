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
    public GameObject stageAchieve;
    public GameObject paper;
    public GameObject gauge;

    // Start is called before the first frame update
    void Start()
    {
        SceneSelector.Get().LoadScene(new SceneStage("TitleScene"));
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

    public void StageAchieveEffect(bool starting)
    {
        if (stageAchieve)
        {
            var text = StageSelector.Get()?.Current?.answer ?? "";
            stageAchieve.GetComponent<Animator>().SetBool("Enabled", starting && text != "");
            if (starting)
            {
                var title = stageAchieve.GetComponentInChildren<TMP_Text>();
                if (title)
                    title.text = text;
            }
        }
    }

    float lastSignal = -50;
    float lastPaper = -50;
    string lastMessage = "";

    public void ShowSignal()
    {
        lastSignal = Time.time;
    }

    public void SetPaper(string text = "")
    {
        if (lastMessage != text)
        {
            this.Delay(.25f, () =>
            {
                var papertext = paper.GetComponentInChildren<TMP_Text>();
                papertext.text = text;
            });
            lastPaper = Time.time;
            lastMessage = text;
        }
    }

    public void SetHp(float hp)
    {
        gauge.transform.Find("Gauge").GetComponent<Image>().fillAmount = hp;
    }

    // Update is called once per frame
    void Update()
    {
        var anim = paper.GetComponent<Animator>();
        anim.SetBool("Enabled", Time.time - lastSignal < 2 && Time.time - lastPaper >= .5f);
    }

    public static BaseDirector Get()
    {
        return GameObject.Find("GameBase")?.GetComponent<BaseDirector>();
    }
}

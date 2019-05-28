using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StageDirector : MonoBehaviour
{
    public GameObject letterBox;
    public GameObject stageTitle;
    public GameObject stageClear;
    public GameObject stageResult;
    public GameObject stageAchieve;
    public GameObject paper;
    public GameObject gauge;
    public GameObject menu;
    public GameObject menuIcon;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        bool isStage = GameObject.Find("GameStage") != null;
        if (isStage && Input.GetButtonDown("Cancel"))
        {
            bool next = true;
            if (menu)
                next = !menu.GetComponent<Animator>().GetBool("Enabled");

            MenuEffect(next);
        }

        gauge.GetComponent<CanvasGroup>().alpha = isStage ? 1 : 0;
        menuIcon.GetComponent<CanvasGroup>().alpha = isStage ? 1 : 0;

        var anim = paper.GetComponent<Animator>();
        anim.SetBool("Enabled", Time.time - lastSignal < 2 && Time.time - lastPaper >= .5f);
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

    public void StageClearEffect(bool starting, int level)
    {
        if (stageClear)
            stageClear.GetComponent<Animator>().SetBool("Enabled", starting);
        if (stageResult)
        {
            stageResult.GetComponent<LevelStar>().SetLevel(level);
            stageResult.GetComponent<Animator>().SetBool("Enabled", starting);
        }
    }

    public void MenuEffect(bool starting)
    {
        if (menu)
            menu.GetComponent<Animator>().SetBool("Enabled", starting);
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

    public void MenuRestart()
    {
        var sel = SceneSelector.Get();
        if (sel != null)
            sel.LoadScene(sel.CurrentScene, SceneSelector.SceneChangeType.CHANGE_FADE);
    }

    public void MenuWorld()
    {
        var sel = SceneSelector.Get();
        var sta = StageSelector.Get();
        if (sel != null && sta != null)
            sel.LoadScene(sta.lastWorldMap, SceneSelector.SceneChangeType.CHANGE_FADE);
    }

    public void MenuTitle()
    {
        var sel = SceneSelector.Get();
        if (sel != null)
            sel.LoadScene(new SceneStage("TitleScene"), SceneSelector.SceneChangeType.CHANGE_FADE);
    }

    public static StageDirector Get()
    {
        return GameObject.Find("GameStages")?.GetComponent<StageDirector>();
    }
}

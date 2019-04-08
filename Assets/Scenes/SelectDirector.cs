using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectDirector : MonoBehaviour
{
    public Stage selected;

    public GameObject paper;

    // Start is called before the first frame update
    void Start()
    {
        SelectEffect(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame(Stage stage)
    {
        SelectEffect(false);

        StageSelector.Get().LoadStage(stage);
    }

    public void StartGame()
    {
        if (selected)
            StartGame(selected);
    }

    public void BackToTitle()
    {
        SelectEffect(false);

        SceneSelector.Get().LoadScene("TitleScene");
    }

    public void SelectEffect(bool starting)
    {
        GetComponentInChildren<ButtonManager>().SetVisible(starting);
    }

    public void ShowPaper(string text)
    {
        var papertext = paper.GetComponentInChildren<TMP_Text>();
        var b = papertext.text != "" && text != papertext.text;
        if (b)
            paper.GetComponent<Animator>().SetBool("Enabled", false);
        this.Delay(.5f, () =>
        {
            papertext.text = text;
            if (b)
                paper.GetComponent<Animator>().SetBool("Enabled", true);
        });
    }

    public static SelectDirector Get()
    {
        return GameObject.Find("GameSelect")?.GetComponent<SelectDirector>();
    }
}

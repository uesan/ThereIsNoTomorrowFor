using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    Sprite[] sprites;
    SpriteRenderer explain;
    GameObject how_to_panel, title_panel;
    Button toward_button, back_button,return_button;
    int page_num = 0;
    // Start is called before the first frame update
    /// <summary>
    /// Unityの画面からじゃなく、スクリプトからボタンにイベントを付与させている
    /// 参考 https://gametukurikata.com/basic/addlistener
    /// </summary>
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Image/GameExplanation");
        explain = GameObject.Find("GameExplanation").GetComponent<SpriteRenderer>();
        how_to_panel = GameObject.Find("HowToPanel");
        title_panel = GameObject.Find("TitlePanel");
        toward_button = GameObject.Find("TowardButton").GetComponent<Button>();
        back_button = GameObject.Find("BackButton").GetComponent<Button>();
        return_button = GameObject.Find("ReturnButton").GetComponent<Button>();
        toward_button.onClick.AddListener(TowardButton);
        back_button.onClick.AddListener(BackButton);
        return_button.onClick.AddListener(ReturnButton);
        how_to_panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartHowTo()
    {
        page_num = 0;
        title_panel.SetActive(false);
        how_to_panel.SetActive(true);
        explain.sprite = sprites[page_num];
        toward_button.interactable = true;
        back_button.interactable = false;
        return_button.interactable = true;

    }

    private void EndHowTo()
    {
        title_panel.SetActive(true);
        how_to_panel.SetActive(false);
    }

    public void BackButton()
    {
        BackPage();
    }

    public void TowardButton()
    {
        TowardPage();
    }

    public void ReturnButton()
    {
        EndHowTo();
    }

    private void TowardPage()
    {
        back_button.interactable = true;
        page_num++;
        explain.sprite = sprites[page_num];
        if(page_num >= sprites.Length - 1)
            toward_button.interactable = false;
    }
    private void BackPage()
    {
        toward_button.interactable = true;
        page_num--;
        explain.sprite = sprites[page_num];
        if(page_num == 0)
            back_button.interactable = false;
    }
}

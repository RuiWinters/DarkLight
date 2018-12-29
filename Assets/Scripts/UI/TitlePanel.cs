using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitlePanel : TTUIPage
{
    public Image imageTilte;
    public Image imageAnyKey;
    public Image imageWhite;
    public Button buttonLoad, buttonNew;

    public TitlePanel():base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/TitlePanel";
    }

    public override void Awake(GameObject go)
    {
        //imageAnyKey = transform.Find("ImageAnyKey").GetComponent<Image>();
        imageTilte = transform.Find("ImageTitle").GetComponent<Image>();
        imageWhite = transform.Find("ImageBG").GetComponent<Image>();
        buttonLoad = transform.Find("ButtonLoad").GetComponent<Button>();
        buttonNew = transform.Find("ButtonNew").GetComponent<Button>();
        buttonLoad.gameObject.SetActive(false);
        buttonNew.gameObject.SetActive(false);

        imageTilte.color = new Color(1, 1, 1, 0);
        //imageAnyKey.gameObject.SetActive(false);
        imageWhite.DOFade(0, 2f).SetDelay(0.2f);
        imageTilte.DOFade(1, 1).SetDelay(4);
        buttonLoad.GetComponent<Image>().DOFade(1, 1).SetDelay(5).OnStart(() => buttonLoad.gameObject.SetActive(true));
        buttonNew.GetComponent<Image>().DOFade(1, 1).SetDelay(5).OnStart(() => buttonNew.gameObject.SetActive(true));

        buttonNew.onClick.AddListener(() => {
            Tools.LoadSceneByLoading("My Character Creation");
        });
        buttonLoad.onClick.AddListener(() => {
            Tools.LoadSceneByLoading("My Dreamdev Village");
        });

        //判断是否有存档
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            buttonLoad.interactable = false;
        }
    }
}


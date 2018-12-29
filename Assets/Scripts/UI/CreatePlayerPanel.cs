using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CreatePlayerPanel : TTUIPage
{
    public Button buttonPre, buttonNext, buttonRandom, buttonOK;
    public InputField inputFieldName;//名字输入框

    public GameObject[] hero;  //your hero
    public int indexHero = 0;  //index select hero
    private GameObject[] heroInstance; //use to keep hero gameobject when Instantiate

    public string[] xings = { "万俟", "司马", "上官", "欧阳", "夏侯", "诸葛", "闻人", "东方", "赫连", "皇甫", "尉迟", "公羊", "澹台", "濮阳", "单于", "申屠", "公孙", "令狐", "宇文", "慕容" };
    public string[] names = { "望", "着", "近", "咫尺", "萧", "炎", "媚", "俏", "丽", "上", "刚", "欲", "露出", "笑容", "可", "少", "年", "举", "动", "未", "全", "现" };



    /// <summary>
    /// 构造函数
    /// </summary>
    public CreatePlayerPanel():base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/CreatePlayerPanel";//UI资源所在的路径
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //初始化可交互的UI
        buttonPre = transform.Find("ButtonPre").GetComponent<Button>();
        buttonNext = transform.Find("ButtonNext").GetComponent<Button>();
        buttonOK = transform.Find("ButtonOK").GetComponent<Button>();
        buttonRandom = transform.Find("ButtonRandom").GetComponent<Button>();
        inputFieldName = transform.Find("InputField").GetComponent<InputField>();

        hero = Resources.LoadAll<GameObject>("Player/HeroPreview");//加载指定路径下的所有GameObject
        heroInstance = new GameObject[hero.Length]; //add array size equal hero size
        indexHero = 0; //set default selected hero
        SpawnHero(); //spawn hero to display current selected

        //check if hero is less than 1 , button next and prev will disappear
        if (hero.Length <= 1)
        {
            buttonNext.gameObject.SetActive(false);
            buttonPre.gameObject.SetActive(false);
        }

        //“上一个”和“下一个”按钮
        buttonNext.onClick.AddListener(() =>
        {
            indexHero++;
            if (indexHero >= heroInstance.Length)
            {
                indexHero = 0;
            }
            Debug.Log(indexHero);
            UpdateHero(indexHero);
        });
        buttonPre.onClick.AddListener(() =>
        {
            indexHero--;
            if (indexHero < 0)
            {
                indexHero = heroInstance.Length-1;
            }
            Debug.Log(indexHero);
            UpdateHero(indexHero);
        });

        //
        int count = 0; ;
        buttonRandom.onClick.AddListener(()=> {
            GetRandomName();
            count++;
            buttonRandom.transform.DORotate(Vector3.forward * 180* (count % 2), 0.5f);
        });

        buttonOK.onClick.AddListener(ButtonOKClick);
    }

    /// <summary>
    /// 随机姓名
    /// </summary>
    public void GetRandomName()
    {
        string xing = xings[Random.Range(0, xings.Length)];
        string ming = names[Random.Range(0, xings.Length)];
        inputFieldName.text = xing + ming;
    }
    /// <summary>
    /// 显示指定索引所对应的角色
    /// </summary>
    /// <param name="_indexHero"></param>
    public void UpdateHero(int _indexHero)
    {
        for (int i = 0; i < hero.Length; i++)
        {
            //Show only select character
            if (i == _indexHero)
            {
                heroInstance[i].SetActive(true);
            }
            else
            {
                //Hide Other Character
                heroInstance[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 生成所有的角色，只显示默认的角色
    /// </summary>
    public void SpawnHero()
    {
        for (int i = 0; i < hero.Length; i++)
        {
            heroInstance[i] = (GameObject)GameObject.Instantiate(hero[i], Vector3.zero, Quaternion.identity);
        }

        UpdateHero(indexHero);
    }

    public void ButtonOKClick()
    {
        Debug.Log(indexHero);
        //Save select character and name
        PlayerPrefs.SetString("pName", inputFieldName.text);
        PlayerPrefs.SetInt("pSelect", indexHero);

        //切换场景
        //SceneManager.LoadScene("Dreamdev Village");
        Tools.LoadSceneByLoading("My Dreamdev Village");
    }
}

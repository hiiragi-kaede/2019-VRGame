using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class EventHandler : MonoBehaviour
{
    private Nav_Controll nav;
    [SerializeField] private Animator fallAni;
    [SerializeField] private GameObject UItexts;
    [SerializeField] private GameObject result_object;
    [SerializeField] private PlayableDirector ShowResult;
    private GameMaster GM;
    private Text shot_text;
    private Text hit_text;
    private Text per_text;
    private Text bonus__text;
    private Text score_text;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<Nav_Controll>();
        GM = GetComponent<GameMaster>();
        fallAni.SetBool("Play", false);
        shot_text = result_object.transform.GetChild(1).gameObject.GetComponent<Text>();
        hit_text = result_object.transform.GetChild(2).gameObject.GetComponent <Text>();
        per_text = result_object.transform.GetChild(3).gameObject.GetComponent<Text>();
        bonus__text= result_object.transform.GetChild(4).gameObject.GetComponent<Text>();
        score_text = result_object.transform.GetChild(5).gameObject.GetComponent<Text>();

        result_object.SetActive(false);//パネルごと非表示に
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.Progress >= 16)
        {
            fallAni.SetBool("Play", true);//ラストの大きい的ゾーンのアニメーション再生
            //UItexts.SetActive(false);//スコアを見えないように
        }
        if (nav.Progress >= 19)//ゴールしたら
        {
            GM.EndGame();
            PrepShowResult();
            result_object.SetActive(true);
            ShowResult.Play();
        }
    }
    

    private void PrepShowResult()
    {
        float hit_percent = ((float)GM.Count_hit / GM.Count_arrow * 100);
        float bonus = 1;
        if (hit_percent >= 50) bonus = 1.4f;
        else if (hit_percent >= 70) bonus = 1.7f;
        else if (hit_percent >= 90) bonus = 2.0f;

        shot_text.text = "撃った数:" + GM.Count_arrow.ToString();
        hit_text.text = "ヒット数:" + GM.Count_hit.ToString();
        per_text.text = "命中率:" + hit_percent.ToString("F2")+"%";
        bonus__text.text = "命中率ボーナス:" + bonus + "倍";
        int last_score = (int)(GM.GetScore() * bonus);
        score_text.text = "スコア:" + last_score.ToString();
    }
}

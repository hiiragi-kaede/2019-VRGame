using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    private Nav_Controll nav;
    [SerializeField] private Animator fallAni;
    [SerializeField] private GameObject UItexts;
    [SerializeField] private GameObject result_object;
    private GameMaster GM;
    private Text shot_text;
    private Text hit_text;
    private Text per_text;
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
        score_text = result_object.transform.GetChild(4).gameObject.GetComponent<Text>();

        result_object.transform.GetChild(1).gameObject.SetActive(false);//撃った数
        result_object.transform.GetChild(2).gameObject.SetActive(false);//ヒット数
        result_object.transform.GetChild(3).gameObject.SetActive(false);//的中率
        result_object.transform.GetChild(4).gameObject.SetActive(false);//スコア
        result_object.SetActive(false);
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
            result_object.SetActive(true);
            GM.EndGame();
            StartCoroutine("ShowResult");
        }
    }

    IEnumerator ShowResult()
    {
        float hit_percent = ((float)GM.Count_hit / GM.Count_arrow * 100);
        result_object.transform.GetChild(1).gameObject.SetActive(true);//撃った数
        shot_text.text = "撃った数:" + GM.Count_arrow.ToString();
        yield return new WaitForSeconds(1);
        result_object.transform.GetChild(2).gameObject.SetActive(true);//ヒット数
        hit_text.text = "ヒット数:" + GM.Count_hit.ToString();
        yield return new WaitForSeconds(1);
        result_object.transform.GetChild(3).gameObject.SetActive(true);//的中率
        per_text.text = "的中率:" + hit_percent.ToString("F2")+"%";
        yield return new WaitForSeconds(1);
        result_object.transform.GetChild(4).gameObject.SetActive(true);//スコア
        int last_score = GM.GetScore();
        float bonus = 1;
        if (hit_percent >= 50) bonus = 1.2f;
        else if (hit_percent >= 70) bonus = 1.4f;
        else if (hit_percent >= 90) bonus = 2.0f;
        score_text.text = "スコア:" + last_score.ToString();
    }
}

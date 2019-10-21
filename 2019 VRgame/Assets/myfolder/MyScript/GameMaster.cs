using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameMaster : MonoBehaviour,OnHitEvent,Arrow_Prep
{

    private static GameMaster instance;
    [SerializeField] private int MaxScore = 99999;
    private int totalscore;
    /*[Tooltip("<Target>タグを的の中心位置のオブジェクトにつけてください")]
    private GameObject[] targets;
    private float[] distances;*/
    [SerializeField] private Text dis_debug;
    [SerializeField] private Text count_debug;
    public int Count_arrow { get; private set; }
    public int Count_hit { get; private set; }
    private AudioSource cleanhitsound;
    private AudioSource hitsound;
    public bool IsPlaying { get; private set; }
    [SerializeField] private GameObject player;

    public static GameMaster Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameMaster();
            }
            return instance;
        }
    }

        // Start is called before the first frame update
     void Start()
     {
         totalscore = 0;
        /*targets = GameObject.FindGameObjectsWithTag("Target");//Targetタグをつけるのは的の中心位置のオブジェクトにしてください
        if (targets.Length == 0)
        {
            Debug.LogError("Targetタグが付いたオブジェクトが存在しません");
            return;
        }
        else
        {
            foreach (GameObject obj in targets)
            {
                //Debug.Log(obj.transform.position);
            }
        }
        distances = new float[targets.Length];
        */
        IsPlaying = false;
        count_debug.text = "count:";
        AudioSource[] audioSources = GetComponents<AudioSource>();
        cleanhitsound = audioSources[0];
        hitsound = audioSources[1];
     }

    void Update()
    {
        count_debug.text = "count:"+Count_arrow.ToString();
        gameObject.transform.position = player.transform.position;
    }

     public int GetScore()
     {
         return totalscore;
     }

     private void AddScore(Transform target_pos,Transform arrow_pos,int maxpoint)
     {
        float dis = Vector3.Distance(target_pos.position, arrow_pos.position);
        int point;
        //そのまま加算
        if (dis < 0.2f)
        {
            point = (int)(maxpoint * 1f);
            cleanhitsound.PlayOneShot(cleanhitsound.clip);
        }
        else if (dis < 0.3f)
        {
            point = (int)(maxpoint * 0.8f);
            cleanhitsound.PlayOneShot(cleanhitsound.clip);
        }
        else if (dis < 0.5f)
        {
            point = (int)(maxpoint * 0.4f);
            hitsound.PlayOneShot(hitsound.clip);
        }
        else point = 0;
        dis_debug.text = "Dis: " + dis.ToString("F3") + "f";
        totalscore += point;
        totalscore = Mathf.Clamp(totalscore, 0, MaxScore);//点数上限以上もしくは0点以下にならないように
        if (point != 0) Count_hit++;
        //Debug.Log(totalscore);
     }

     //private int NearestTargetCeneterIdx(Transform arrow_pos)
     //{
     //   int idx = 0;
     //   for (int i = 0; i < targets.Length; i++)
     //   {
     //       distances[i] = Vector3.Distance(targets[i].transform.position, arrow_pos.transform.position);
     //   }
     //   float min = Mathf.Min(distances);
     //   for (int i = 0; i < targets.Length; i++) if (distances[i] == min) idx = i;
     //   return idx;
     //}
    public void StartGame()
    {
        IsPlaying = true;
    }

    public void EndGame()
    {
        IsPlaying = false;
    }

    void OnHitEvent.Onhit(GameObject hitObject, Transform arrow_pos)
    {
        var center = hitObject.transform.GetChild(1).gameObject;
        int maxpoint = hitObject.transform.parent.parent.GetComponent<TargetScoreManager>().maxsocre;

        if (IsPlaying)
        {
            AddScore(center.transform, arrow_pos, maxpoint);
        }
    }

    void Arrow_Prep.CountPrep()
    {
        if (IsPlaying) Count_arrow++;
    }
}

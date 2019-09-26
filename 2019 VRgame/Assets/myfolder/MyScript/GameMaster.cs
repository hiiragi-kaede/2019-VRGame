using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour //シングルトンクラスです
{
     private static GameMaster instance;
     [SerializeField] private int MaxScore = 99999;
     private int totalscore;
     [Tooltip("<Target>タグを的の中心位置のオブジェクトにつけてください")]
     private GameObject[] targets;
     private float[] distances;
     [SerializeField] private Text dis_debug;
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
         targets = GameObject.FindGameObjectsWithTag("Target");//Targetタグをつけるのは的の中心位置のオブジェクトにしてください
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
     }

     public int GetScore()
     {
         return totalscore;
     }

     public void AddScore(Transform arrow_pos)
     {
        int minid = NearestTargetCeneterIdx(arrow_pos);
        int point = targets[minid].GetComponent<TargetScoreManager>().maxsocre;
        float dis = distances[minid];

        //そのまま加算
        if (dis < 0.1f) point = (int)(point * 0.8f);
        else if (dis < 0.2f) point = (int)(point * 0.5f);
        else if (dis < 0.5f) point = (int)(point * 0.2f);
        else point = 0;
        dis_debug.text = "Dis: " + dis.ToString("F3") + "f";
        totalscore += point;
        totalscore = Mathf.Clamp(totalscore, 0, MaxScore);//点数上限以上もしくは0点以下にならないように
        //Debug.Log(totalscore);
     }

     private int NearestTargetCeneterIdx(Transform arrow_pos)
     {
        int idx = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            distances[i] = Vector3.Distance(targets[i].transform.position, arrow_pos.transform.position);
        }
        float min = Mathf.Min(distances);
        for (int i = 0; i < targets.Length; i++) if (distances[i] == min) idx = i;
        return idx;
     }
}

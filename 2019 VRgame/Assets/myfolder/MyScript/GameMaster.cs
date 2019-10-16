using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameMaster : MonoBehaviour,OnHitEvent //シングルトンクラスです
{
    
    private static GameMaster instance;
     [SerializeField] private int MaxScore = 99999;
     private int totalscore;
     /*[Tooltip("<Target>タグを的の中心位置のオブジェクトにつけてください")]
     private GameObject[] targets;
     private float[] distances;*/
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
        if (dis < 0.2f) point = (int)(maxpoint * 1f);
        else if (dis < 0.3f) point = (int)(maxpoint * 0.8f);
        else if (dis < 0.5f) point = (int)(maxpoint * 0.4f);
        else point = 0;
        dis_debug.text = "Dis: " + dis.ToString("F3") + "f";
        totalscore += point;
        totalscore = Mathf.Clamp(totalscore, 0, MaxScore);//点数上限以上もしくは0点以下にならないように
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

    void OnHitEvent.Onhit(GameObject hitObject, Transform arrow_pos)
    {
        var center = hitObject.transform.GetChild(1).gameObject;
        int maxpoint = center.GetComponent<TargetScoreManager>().maxsocre;

        AddScore(center.transform, arrow_pos, maxpoint);
    }
}

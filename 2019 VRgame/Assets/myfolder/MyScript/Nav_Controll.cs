using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;
using UnityEngine.UI;

public class Nav_Controll : MonoBehaviour
{
    public GameObject[] destinations;
    [SerializeField] NavMeshAgent navMesh = null;
    [SerializeField] Transform Player;
    public int Progress { get; private set; }
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean ChangeState;
    public Text debugtext;
    private GameMaster gameMaster;

    [Tooltip("最初から二番目のカーブまでの速度")]
    [SerializeField] private float first_speed=3f;

    [Tooltip("二番目のカーブから三番目のカーブまでの速度")]
    [SerializeField] private float second_speed=3.5f;

    [Tooltip("三番目のカーブから最後のカーブまでの速度")]
    [SerializeField] private float third_speed=3f;

    [Tooltip("最後のカーブからゴールまでの速度")]
    [SerializeField] private float last_speed=2.5f;

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        destinations = GameObject.FindGameObjectsWithTag("Destinations");
        foreach(GameObject obj in destinations)
        {
            var child = obj.transform.GetChild(0).gameObject;//パーティクルのオブジェクトを取得
            child.GetComponent<ParticleSystem>().Stop();//最初は全部非表示
        }
        gameMaster = GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(isPlaying);
        if (ChangeState.GetStateDown(handType))
        {
            gameMaster.StartGame();
        }
        debugtext.text = "state:" + gameMaster.IsPlaying.ToString();


        if (!gameMaster.IsPlaying)
        {
            navMesh.velocity = Vector3.zero;
            navMesh.isStopped = true;
            return;
        }
        else
        {
            navMesh.isStopped = false;
            if (Progress >= destinations.Length)
            {
                navMesh.velocity = Vector3.zero;
                navMesh.isStopped = true;
                return;
            }

            if (Vector3.Distance(Player.position, destinations[Progress].transform.position) < 0.8f)
            {
                particle = destinations[Progress].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
                if (particle.isPlaying)
                {
                    particle.Stop();//前のパーティクルをストップ
                }
                Progress++;
                Debug.Log(Progress);
            }

            if (Progress >= destinations.Length)
            {
                navMesh.velocity = Vector3.zero;
                navMesh.isStopped = true;
                return;
            }

            navMesh.SetDestination(destinations[Progress].transform.position);
            particle = destinations[Progress].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            if (!particle.isPlaying)
            {
                particle.Play();
            }

            if (Progress >= 0 && Progress <= 8) navMesh.speed = first_speed;
            else if (Progress >= 9 && Progress <= 12) navMesh.speed = second_speed;
            else if (Progress >= 13 && Progress <= 15) navMesh.speed = third_speed;
            else if (Progress >= 16) navMesh.speed = last_speed;
        }
    }
}

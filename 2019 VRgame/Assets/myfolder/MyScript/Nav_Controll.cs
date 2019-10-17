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
    private int idx = 0;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean ChangeState;
    private bool isPlaying;
    public Text debugtext;

    [SerializeField] private Animator fallAni;
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
        isPlaying = false;
        destinations = GameObject.FindGameObjectsWithTag("Destinations");
        foreach(GameObject obj in destinations)
        {
            var child = obj.transform.GetChild(0).gameObject;//パーティクルのオブジェクトを取得
            child.GetComponent<ParticleSystem>().Stop();//最初は全部非表示
        }
        fallAni.SetBool("Play", false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(isPlaying);
        if (ChangeState.GetStateDown(handType))
        {
            isPlaying = !isPlaying;
        }
        debugtext.text = "state:" + isPlaying.ToString();


        if (!isPlaying)
        {
            navMesh.velocity = Vector3.zero;
            navMesh.isStopped = true;
            return;
        }
        else
        {
            navMesh.isStopped = false;
            if (idx >= destinations.Length)
            {
                navMesh.velocity = Vector3.zero;
                navMesh.isStopped = true;
                return;
            }

            if (Vector3.Distance(Player.position, destinations[idx].transform.position) < 0.8f)
            {
                particle = destinations[idx].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
                if (particle.isPlaying)
                {
                    particle.Stop();//前のパーティクルをストップ
                }
                idx++;
                Debug.Log(idx);
            }

            if (idx >= destinations.Length)
            {
                navMesh.velocity = Vector3.zero;
                navMesh.isStopped = true;
                return;
            }

            navMesh.SetDestination(destinations[idx].transform.position);
            particle = destinations[idx].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            if (!particle.isPlaying)
            {
                particle.Play();
            }
            if (idx >= 0&&idx<=8) navMesh.speed = first_speed;
            else if (idx >= 9&&idx<=12) navMesh.speed = second_speed;
            else if (idx >= 13&&idx<=15) navMesh.speed = third_speed;
            else if (idx >= 16)
            {
                navMesh.speed = last_speed;

                fallAni.SetBool("Play", true);
            }
        }
    }
}

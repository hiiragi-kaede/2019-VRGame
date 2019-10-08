using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;
using UnityEngine.UI;

public class Nav_Controll : MonoBehaviour
{
    public Transform[] destinations;
    [SerializeField] NavMeshAgent navMesh = null;
    [SerializeField] Transform Player;
    private int idx = 0;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean ChangeState;
    private bool isPlaying;
    public Text debugtext;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
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
            if (Vector3.Distance(Player.position, destinations[idx].position) < 0.8f)
            {
                idx++;
            }
            if (idx >= destinations.Length)
            {
                navMesh.velocity = Vector3.zero;
                navMesh.isStopped = true;
                return;
            }
            navMesh.SetDestination(destinations[idx].transform.position);
            //Debug.Log(idx);
        }
    }
}

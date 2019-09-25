using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] Text scoreText;
    //GameMaster gm = GameMaster.Instance;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = "score: " + gm.GetScore().ToString();
    }
}

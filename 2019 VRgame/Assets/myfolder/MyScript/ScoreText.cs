using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private GameMaster GameMaster;
    private Nav_Controll nav;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "score: 0";
        GameMaster = GetComponent<GameMaster>();
        nav = GetComponent<Nav_Controll>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "score: " + GameMaster.GetScore().ToString();
        if (nav.Progress >= 16)
        {
            scoreText.text = "score:????";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Non_behaviours
{
    class GameManager : MonoBehaviour
    {
        private int _score;
        public int Score {
            get { return _score; } 
            set{
                _score = value;
                scoreText.text = "Score: " + _score;
            } 
        }
        public GUIText scoreText;
        public bool gameOver = false;

        void Awake()
        {
            gameOver = false;
            Score = 0;
        }

        void OnGUI()
        {
            if (gameOver)
            {
                if (GUI.Button(new Rect(440, 200, 80, 30), "Restart")) Application.LoadLevel(0);
                return;
            }
        }
    }
}

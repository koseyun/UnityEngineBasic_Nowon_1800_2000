using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingleTon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            NoteSpawner noteSpawner = new NoteSpawner();
            //noteSpawner.gameManager = gameManager;
            while (true)
            {
                gameManager.GameLogic();
            }
        }
    }

    public class GameManager
    {
        private static GameManager _instance;

        public static GameManager Instance;
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }
        public void GameLogic()
        {
        }
        public void FinishGame()
        {
        }
    }

    public class NoteSpawner
    {
        public bool IsPlaying;
        {
            set
            {
                if (value == false)
                    GameManager.Instance.FinishGame();
            }
        }
    }
}

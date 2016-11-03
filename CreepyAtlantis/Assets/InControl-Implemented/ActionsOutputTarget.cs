namespace MultiplayerWithBindingsExample
{
    using UnityEngine;
    using System.Collections;
    using InControl;
    using UnityEngine.SceneManagement;
    using System.Collections.Generic;

    public class ActionsOutputTarget : MonoBehaviour
    {
        private List<string> playerStations = new List<string>();

        public string stn1;
        public string stn2;
        public string stn3;
        public string stn4;

        public IControllable myIO;


        // PLAYERNUMBER must be set in scene 0
        public int playerNumber;
        public int PLAYERNUMBER
        {
            get
            {
                return playerNumber;
            }
            set
            {
                if (value != playerNumber)
                {
                    playerNumber = value;
                }
                Debug.Log(value);
            }
        }

        private GameObject playerStation;
        private GameObject stationComponenet;

        void Awake()
        {
            SceneManager.sceneLoaded += newSceneLoaded;

            playerStations.Add(stn1);
            playerStations.Add(stn2);
            playerStations.Add(stn3);
            playerStations.Add(stn4);
        }

        public void SetMyIO (IControllable myIOToSet)
        {
            myIO = myIOToSet;
        }

        private void newSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex != 0)
            {
                playerStation = GameObject.Find(playerStations[playerNumber]);

                if (PLAYERNUMBER == 0)
                    this.gameObject.tag = "Player1";
                else
                    this.gameObject.tag = "Player2";

                myIO = playerStation.GetComponent <IControllable> ();
            }
            if (scene.buildIndex == 1)
            {
                //myIO.playerNum = PLAYERNUMBER;
            }
        }

        public void passLS (float leftRight, float upDown)
        {
            myIO.LeftStick (leftRight, upDown);
        }
        public void passRS(float leftRight, float upDown)
        {
            myIO.RightStick(leftRight, upDown);
        }

        public void passAButton (bool pressed)
        {
            myIO.AButton(pressed);
        }

        public void resetPlayerNum()
        {
            playerNumber = 0;
        }
    }
}

namespace BasicExample
{
	using InControl;
	using UnityEngine;


	public class BasicExample : MonoBehaviour
	{
        private delegate void RunningCtrl();
        private RunningCtrl myCtrl;

        private GameObject target;
        private GameObject reticle;

        void Start()
        {
            myCtrl = Basic;
        }

        void Basic()
        {
            // Use last device which provided input.
            var inputDevice = InputManager.ActiveDevice;

            // Rotate target object with left stick.
            transform.Rotate(Vector3.down, 500.0f * Time.deltaTime * inputDevice.LeftStickX, Space.World);
            transform.Rotate(Vector3.right, 500.0f * Time.deltaTime * inputDevice.LeftStickY, Space.World);

            // Get two colors based on two action buttons.
            var color1 = inputDevice.Action1.IsPressed ? Color.red : Color.white;
            var color2 = inputDevice.Action2.IsPressed ? Color.green : Color.white;

            // Blend the two colors together to color the object.
            GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, 0.5f);

            if (inputDevice.Command)
            {
                target = GameObject.Find("Target");
                reticle = GameObject.Find("Reticle");
                GetComponent<MeshRenderer>().enabled = false;
                myCtrl = MiniGame;   
            }
        }

        void MiniGame()
        {
            var inputDevice = InputManager.ActiveDevice;
            target.GetComponent<MiniGameTest>().ctrlInput = new Vector3(inputDevice.LeftStickX * 10, inputDevice.LeftStickY * 10, 0);
            reticle.GetComponent<MiniGameTest>().ctrlInput = new Vector3(inputDevice.RightStickX * 10, inputDevice.RightStickY * 10, 0);
        }

        void Update()
		{
            myCtrl();
		}
	}
}


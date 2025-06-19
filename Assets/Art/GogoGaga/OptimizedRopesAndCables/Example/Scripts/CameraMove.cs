using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GogoGaga.OptimizedRopesAndCables
{
    public class CameraMove : MonoBehaviour
    {
        public float speed = 15;
        public Transform[] cameraPoses;
        int current = 0;
        private KeyCode interactKey = KeyCode.E;
        private KeyCode dropKey = KeyCode.Q;


        void Start()
        {
            //Control Setting for Interact
            if (PlayerPrefs.HasKey("Key_0"))
            {
                if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_0"), true, out var parsedKey))
                {
                    interactKey = parsedKey;
                }
            }

            //Control Setting for Drop
            if (PlayerPrefs.HasKey("Key_1"))
            {
                if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_1"), true, out var parsedKey))
                {
                    dropKey = parsedKey;
                }
            }
        }



        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, cameraPoses[current].position, Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraPoses[current].rotation, Time.deltaTime * speed);

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(dropKey))
            {
                current--;
                if (current == -1)
                    current = cameraPoses.Length -1;
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(interactKey))
            {
                current++;
                if (current == cameraPoses.Length)
                    current = 0;
            }

            current = Mathf.Clamp(current, 0, cameraPoses.Length - 1);
        }
    }
}
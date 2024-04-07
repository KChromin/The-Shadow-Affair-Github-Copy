using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace SmugRag
{
    public class RigidbodySpeedDisplayer : MonoBehaviour
    {
        public Rigidbody rigidBody;

        private Label speedText;
        private Label fpsText;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            speedText = root.Q<Label>("current-speed-text");
            fpsText = root.Q<Label>("current-fps");
        }

        private void Update()
        {
            Vector3 velocity = rigidBody.velocity;
            speedText.text = new Vector3(velocity.x, 0, velocity.z).magnitude.ToString("F");
            fpsText.text = Convert.ToInt16(1.0f / Time.deltaTime).ToString();
        }
    }
}
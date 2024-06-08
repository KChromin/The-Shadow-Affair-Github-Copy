using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpeed : MonoBehaviour
{
  public Rigidbody rigidBody;

  private Label speedText;

  private void Awake()
  {
    var root = GetComponent<UIDocument>().rootVisualElement;

    speedText = root.Q<Label>("Speed");
  }

  private void Update()
  {
    Vector3 velocity = rigidBody.linearVelocity;
    speedText.text = new Vector3(velocity.x, velocity.y, velocity.z).magnitude.ToString("F");
  }
}

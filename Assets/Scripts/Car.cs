using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Car : MonoBehaviour {
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float speedGain = 1f;
    [SerializeField] private float rotationSpeed = 5f;

    private int steerValue;
    private float speed;

    private void Awake() {
        speed = initialSpeed;
    }

    void Update() {
        Accelerate();
        Rotate();
        Move();
    }

    private void Accelerate() {
        speed += speedGain * Time.deltaTime;
    }
    
    private void Rotate() {
        transform.Rotate(0f, steerValue * rotationSpeed * Time.deltaTime, 0f);
    }
    
    private void Move() {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    public void Steer(int value) {
        steerValue = value;
    }
}

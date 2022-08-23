using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplayer;

    [Header("Rotation")]
    public float rotationAmount = 4f;
    public float maxRotationAmount = 5f;
    public float smoothRotation = 12f;

    [Space]
    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        MoveSway();
        TiltSway();
    }

    private void MoveSway()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplayer;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplayer;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    private void TiltSway()
    {
        float tiltX = Mathf.Clamp(Input.GetAxisRaw("Mouse X") * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltY = Mathf.Clamp(Input.GetAxisRaw("Mouse Y") * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3(
            rotationX ? -tiltY : 0f,
            rotationY ? tiltX : 0f,
            rotationZ ? -tiltX : 0f
            ));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * initialRotation, Time.deltaTime * smoothRotation);
    }
}

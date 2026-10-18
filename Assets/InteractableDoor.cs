using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    public float openAngle = 90f;
    public float speed = 2f;

    public bool isOpen = false; 
    
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * speed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * speed);
        }
    }

    // Fungsi manual: Kalau ketutup jadi kebuka, kalau kebuka jadi ketutup
    public void Interact()
    {
        isOpen = !isOpen; 
    }
}
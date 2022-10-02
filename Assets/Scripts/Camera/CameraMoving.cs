using UnityEngine;
using System;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject player;
    Rigidbody2D rb;

    [SerializeField] private float size;
    [SerializeField] private float sizeMultiplier = 0.2f;
    [SerializeField] private float maxSize;
    private float sizeStandart;
    
    [SerializeField] private float lerp;
    [SerializeField] private float sizeLerp;

    private Action cameraMode;
    [SerializeField] private float observeCameraSize = 17;
    [SerializeField] private Lean.Touch.LeanDragCamera lean;

    private void Start()
    {
        cameraMode = StandartMode;
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();

        offset = offset * size/ 100;
        sizeStandart = Camera.main.orthographicSize;
    }
    void Update()
    {
        cameraMode();
    }

    private void StandartMode()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x + offset.x, lerp * Time.deltaTime),
                                         Mathf.Lerp(transform.position.y, player.transform.position.y + offset.y, lerp * Time.deltaTime), transform.position.z);
        
        Camera.main.orthographicSize = Mathf.Clamp( Mathf.Lerp(Camera.main.orthographicSize, sizeStandart + rb.velocity.magnitude * sizeMultiplier, sizeLerp * Time.deltaTime), 0, maxSize);
    }

    private void ObserveMode()
    {
    }

    public void SwitchCameraMode(int i)
    {
        switch (i)
        {
            case 0:
                cameraMode = StandartMode;
                Camera.main.orthographicSize = sizeStandart;
                transform.localScale = Vector3.one;
                lean.enabled = false;
                break;
            case 1:
                cameraMode = ObserveMode;
                Camera.main.orthographicSize = observeCameraSize;
                transform.localScale = transform.localScale * observeCameraSize / sizeStandart;
                lean.enabled = true;
                break;
        }
    }
}

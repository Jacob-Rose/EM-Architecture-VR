using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianBlur : MonoBehaviour
{
    private Material myMatOnStart;
    private Material myMat;
    float currentOffset = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        myMatOnStart = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = TimelineController.Instance.timeLineSpeed * Time.deltaTime;
        currentOffset += speed;
        myMatOnStart.SetFloat("BlurAmount", speed);
        myMat.mainTextureOffset = new Vector2(currentOffset, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianBlur : MonoBehaviour
{
    private Material myMat;
    public float currentOffset = 0.0f;
    public float speedMultiplier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        myMat = Instantiate(GetComponent<MeshRenderer>().material);
        GetComponent<MeshRenderer>().material = myMat;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = TimelineController.Instance.timeLineSpeed * Time.deltaTime * speedMultiplier;
        currentOffset += speed;
        myMat.SetFloat("_BlurAmount", Mathf.Clamp01(speed * 10.0f));
        myMat.SetFloat("_Transparency", Mathf.Clamp01(1.2f - Mathf.Clamp01(speed)));
        myMat.mainTextureOffset = new Vector2(currentOffset, 0);
    }
}

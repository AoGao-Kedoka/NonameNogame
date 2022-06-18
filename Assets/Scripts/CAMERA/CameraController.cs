using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [FormerlySerializedAs("backgroundSpeed")]
    [Header("Background Reference")]
    [SerializeField] private float backgroundSpeed = 0.5f;
    [SerializeField] private GameObject background;
    [SerializeField] private float midgroundSpeed = 1f;
    [SerializeField] private GameObject midground;
    [SerializeField] private float forgroundSpeed = 1.5f;
    [SerializeField] private GameObject forground;

    private List<GameObject> _backgroundList = new List<GameObject>();
    private List<GameObject> _midgroundList = new List<GameObject>();
    private List<GameObject> _forgroundList = new List<GameObject>();

    private void Start()
    {
        foreach (Transform child in background.transform)
        {
            _backgroundList.Add(child.gameObject);
        }
        foreach (Transform child in midground.transform)
        {
            _midgroundList.Add(child.gameObject);
        }
        foreach (Transform child in forground.transform)
        {
            _forgroundList.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
         background.transform.position -= new Vector3(backgroundSpeed, 0, 0);
         midground.transform.position -= new Vector3(midgroundSpeed, 0, 0);
         forground.transform.position -= new Vector3(forgroundSpeed, 0, 0);
         GeneratePic(_backgroundList, 256, background);
         GeneratePic(_midgroundList, 256, midground);
         GeneratePic(_forgroundList, 352, forground);
    }

    void GeneratePic(List<GameObject> picList, float scaleX, GameObject parent)
    {
        if (picList[0].transform.position.x <= -scaleX)
        {
            var instance = picList[0];
            picList.RemoveAt(0);
            Destroy(instance);
            var last = picList[picList.Count() - 1];
            var newPic = Instantiate(last, last.transform.position + new Vector3(scaleX, 0, 0),
                Quaternion.identity);
            newPic.transform.parent = parent.transform;
            picList.Add(newPic); 
        }
    }
    
}

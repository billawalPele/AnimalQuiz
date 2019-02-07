using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour {

   
    public GameObject[] hewan_prefab;
    public Vector3 pos;

    int random;

    void Awake()
    {
        SpawnAnimal();
    }

    // Use this for initialization
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update () {
        SpawnAnimal();

    }

    void SpawnAnimal()
    {

        random = Random.Range(0, 2);
        Vector3 Spawn = new Vector3(Random.Range(-pos.x, pos.x), 1, Random.Range (-pos.z, pos.z));
        Instantiate(hewan_prefab[random], Spawn + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
       
            
    }
}

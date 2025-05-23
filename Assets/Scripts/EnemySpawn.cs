using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Sprite[] engbSprites;

    public GameObject capiPrefabRef;
    public GameObject aviao;

    public float objectInterval = 4;
    public float planeInterval = 4; //em segundos
    public float instantiateTime = 0;
    public float intervalVariation = 0.5f;

    public List<GameObject> enemies = new List<GameObject>();   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > instantiateTime){

            GameObject obj = Instantiate(capiPrefabRef, new Vector3(7,-2.59f), Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = engbSprites[Random.Range(0, engbSprites.Length)];

            BoxCollider boxCollider = obj.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;

            // Add the Obstacle tag to the enemy
            obj.tag = "Obstacle";

            //instancia o objeto
            instantiateTime = Time.time + Random.Range(objectInterval - intervalVariation, objectInterval + intervalVariation);

            enemies.Add(obj);

            
            if(obj.transform.position.x <= -12)
                Destroy(obj.gameObject);
        }
    }
}

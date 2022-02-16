using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectController : MonoBehaviour
{
    public GameObject prefabObject;
    public int numObjectStart;

    List<GameObject> poolObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numObjectStart; i++)
        {
            createNewObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getObject(){
        GameObject newObject = poolObjects.Find(x => x.activeInHierarchy == false);

        if(newObject == null){
            newObject = createNewObject();
        }

        newObject.SetActive(true);

        return newObject;
    }

    public GameObject createNewObject(){

        GameObject newObject = Instantiate(prefabObject);

        newObject.SetActive(false);
        poolObjects.Add(newObject);

        return newObject;
    }
}

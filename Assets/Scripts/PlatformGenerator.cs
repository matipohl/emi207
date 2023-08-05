using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private int mapWidth;
    [SerializeField] GameObject[] tiles;
    private int[] map;
    // Start is called before the first frame update
    void Start()
    {
        map = new int[mapWidth];

        for(int x = 0; x< mapWidth; x++){
            if(x%2 == 0){
                map[x] = 0;
            }
            else{
                map[x] = 1;
            }
        }

        for(int x = 0; x < mapWidth; x++){
            GameObject tile = Instantiate(tiles[map[x]], new Vector2(x,-0.5f), Quaternion.identity) as GameObject;
            tile.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

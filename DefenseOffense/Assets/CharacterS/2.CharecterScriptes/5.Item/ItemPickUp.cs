using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    private GameObject Player;
    private Transform ItemPrefabs;
    void Start()
    {
        ItemPrefabs = this.GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("PLAYER");
        
    }
    void Update()
    {
        CheckItem();
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag =="Item")
                {
                    if(Vector3.Distance(ItemPrefabs.transform.position,Player.transform.position)<=1)
                    Destroy(this.gameObject);
                }
            }
        }
        
    }
    private void CheckItem()
    {
        float dir = Vector3.Distance(ItemPrefabs.transform.position,Player.transform.position);
        if(dir <= 0.8)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if(dir >= 0.8)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else return;
        
    }
}

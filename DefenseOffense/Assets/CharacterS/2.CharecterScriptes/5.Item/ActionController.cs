// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ActionController : MonoBehaviour
// {
//     private bool pickupActiovated =false;//습득 가능할 시 ture
//     private Transform tr;
//     [SerializeField]
//     private List<Transform> SeeItem;// 
    
//     [SerializeField]
//     //아이템 레이어에만 반응하도록 레이어 마스크를 설정 
//     private LayerMask layerMask;
//     // 필요한 컴포넌트
//     [SerializeField]
//     private Text actionText;
//     private void Start()
//     {
        
//         tr = transform.GetComponent<Transform>();
//         SeeItem = new List<Transform>();
       
        
//     }
//     void AllItem()
//     {
//         for(int i = 0; 1 < SeeItem.Count; i++)
//         {
//             SeeItem[i] = GameObject.FindGameObjectWithTag("Item").GetComponent<Transform>();
//             p = SeeItem[i];
            
//         }
//     }
//     void Update()
//     {
//         AllItem();
//         CheckItem();
//         TryAction();
//     }
//     private void TryAction()
//     {
//         if(Input.GetKeyDown(KeyCode.E))
//         {
//             CheckItem();
//             CanPickUp();
//         }
//     }
//     private void CanPickUp()
//     {
//         Debug.Log("canpickup");
//         if(pickupActiovated)
//         {
//             if(SeeItem.transform !=null)
//             {
//                 Debug.Log(SeeItem.transform.GetComponent<ItemPickUp>().item.itemName +"획득");
//                 Destroy(SeeItem.transform.gameObject);
//                 InfoDisappear();
//             }
//         }
//     }
//     private void CheckItem()
//     {
        
//         float dir = Vector3.Distance(tr.transform.position,SeeItem.transform.position);
//         if(dir <= 0.5f)
//         {
//             Debug.Log("CheckItem");
//             if(SeeItem.transform.tag == "Item")
//             {
//                 ItemInfoappear();
//             }
//         }
//         else
//         InfoDisappear();
//     }
//     private void ItemInfoappear()
//     {
//         pickupActiovated= true;
//         actionText.gameObject.SetActive(true);
//         actionText.text = SeeItem.transform.GetComponent<ItemPickUp>().item.itemName 
//         + "획득" + "color=Bule>" + "(E)" +"</color>";
//     }
//     private void InfoDisappear()
//     {
//         pickupActiovated = false;
//         actionText.gameObject.SetActive(false);
//     }
// }

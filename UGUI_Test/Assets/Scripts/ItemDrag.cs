using UnityEngine;  
using UnityEngine.UI;  
using UnityEngine.EventSystems;  
using System.Collections;  

public class ItemDrag : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IDragHandler {  

    // 鼠标起点  
    private Vector2 originalLocalPointerPosition;     
    // 面板起点  
    private Vector3 originalPanelLocalPosition;  
    // 当前面板  
    private RectTransform panelRectTransform;  
    // 父节点,这个最好是UI父节点，因为它的矩形大小刚好是屏幕大小  
    public RectTransform parentRectTransform;  
    //格子列表
    private GameObject gridManager;//在gridlist之上添加的一个新的空物体作为父节点
    private GameObject originalGrid;//记录物品拖动前的位置
    private static int siblingIndex = 0;  
    void Awake () {  
        panelRectTransform = transform as RectTransform;  
        parentRectTransform = GameObject.FindGameObjectWithTag("ScrollRect").GetComponent<RectTransform>() as RectTransform;  
        gridManager=GameObject.Find("GridManager");   
    }  

    // 鼠标按下  
    public void OnPointerDown (PointerEventData data) {  
        //记录物品当前所在的格子信息
        originalGrid=panelRectTransform.parent.gameObject;
       //将物品放置在gridManager下，并设置层级，保证物品显示在整个背包层面之上
        panelRectTransform.SetParent(gridManager.transform);
        siblingIndex++;  //层级管理
        panelRectTransform.transform.SetSiblingIndex(siblingIndex);  
        // 记录当前面板起点  
        originalPanelLocalPosition = panelRectTransform.localPosition;  
        // 通过屏幕中的鼠标点，获取在父节点中的鼠标点  
        // parentRectTransform:父节点  
        // data.position:当前鼠标位置  
        // data.pressEventCamera:当前事件的摄像机  
        // originalLocalPointerPosition:获取当前鼠标起点  
        RectTransformUtility.ScreenPointToLocalPointInRectangle (parentRectTransform, data.position, data.pressEventCamera, out originalLocalPointerPosition);  
    }  
    public void OnPointerUp(PointerEventData data){
       // transform.SetParent(gridlist.transform);
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition,-Vector2.up); 
        if (hit.collider != null) { //如果射线检测到的gameobject为grid，就把当前物品放在grid节点下 
            if(hit.collider.gameObject.tag=="Grid"&&hit.collider.gameObject.transform.FindChild("Sword(Clone)")==null) 
               transform.parent=hit.transform;
               else
               {
//如果不是格子或没有检测到物体，则将物品放回到原来的格子内
                   transform.parent=originalGrid.transform;
               }
        }
        else
        {
          transform.parent=originalGrid.transform;
        }
    //重置物品位置
      transform.localPosition=Vector3.zero;
    }
    // 拖动  
    public void OnDrag (PointerEventData data) {  
        if (panelRectTransform == null || parentRectTransform == null){
            return;  
        }
        Vector2 localPointerPosition;  
        // 获取本地鼠标位置  
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle (parentRectTransform, data.position, data.pressEventCamera, out localPointerPosition)) {  
            // 移动位置 = 本地鼠标当前位置 - 本地鼠标起点位置  
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;  
            // 当前物品位置 = 物品起点 + 移动位置  
            panelRectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;  
        }  
       // ClampToWindow ();  
    }

}
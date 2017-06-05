using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GridManager : MonoBehaviour {
    public List<GameObject> grid;//格子列表，用来存储所有个物品格子
    public GameObject item;//物品对象，把prefab添加到这里
    void Update () {
        if(Input.GetKeyDown(KeyCode.K)){
            AddItem(item);//调用添加物品的方法
            }
    }
    public void AddItem(GameObject _item){
     //查找每个格子，寻找空格子的物体。
        for(int i=0;i<grid.Count;i++){
            //这里我们通过查找名字来寻找物体，判断格子内是否有物品存在
            //但这样就只能查找命名为该名字的物体，实际中我们可以通过tag或者其他属性来判断
           bool isHaving= grid[i].transform.FindChild("CardItem(Clone)");
           //如果当前格子有物品存在         
           if(isHaving)
                continue;             
           else if(!isHaving){
                //当前的空格子
                Transform _i=grid[i].gameObject.GetComponent<RectTransform>();
                //创建一个物品对象
                GameObject go=(GameObject)Instantiate(_item); 
                //把创建的物品对象添加到格子下                        
                go.transform.SetParent(_i);
                //调整物品的位置位于格子中间
                go.transform.localPosition=Vector3.zero;
              break;
           }      
        }
    }
}
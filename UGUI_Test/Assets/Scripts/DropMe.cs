using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	public Color highlightColor = Color.yellow;
	
	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}
	
	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;
		
		if (receivingImage == null)
			return;
		
		Image dropSprite = GetDropSprite (data);
		// if (dropSprite != null)
		// 	receivingImage.overrideSprite = dropSprite;
		if (dropSprite != null)
			gameObject.GetComponent<Image>().sprite = dropSprite.sprite;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		Image dropSprite = GetDropSprite (data);
		if (dropSprite != null)
			containerImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		containerImage.color = normalColor;
	}
	
	// private Sprite GetDropSprite(PointerEventData data)
	// {
	// 	var originalObj = data.pointerDrag;
	// 	if (originalObj == null)
	// 		return null;
		
	// 	var dragMe = originalObj.GetComponent<DragMe>();
	// 	if (dragMe == null)
	// 		return null;
		
	// 	var srcImage = originalObj.GetComponent<Image>();
	// 	if (srcImage == null)
	// 		return null;
	// 	var sp = GetComponent<Image>().sprite;
	// 	Debug.Log("sp:"+sp.name);
	// 	sp = srcImage.sprite;
	// 	Debug.Log("sp2:"+sp.name);
	// 	return srcImage.sprite;
	
	// }
	private Image GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;
		
		var dragMe = originalObj.GetComponent<DragMe>();
		if (dragMe == null)
			return null;
		
		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;
		var sp = GetComponent<Image>().sprite;
		Debug.Log("sp:"+sp.name);
		sp = srcImage.sprite;
		Debug.Log("sp2:"+sp.name);
		return srcImage ;
	
	}
}

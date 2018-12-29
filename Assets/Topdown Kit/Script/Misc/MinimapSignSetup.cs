/// <summary>
/// Minimap sign setup.
/// This script use for setup minimap sign
/// </summary>

using UnityEngine;
using System.Collections;

public class MinimapSignSetup : MonoBehaviour {
	
	//type of sign
	public enum MinimapSignType{Player,Enemy,Boss,Npc,ShopWeapon,ShopPotion,SavePoint,Quest}
	
	public MinimapSignType signType;
	
	
	// Use this for initialization
	void Start () {
		
		if(!GameSetting.Instance.hideMinimap)
		{
			TextureSetup();
			this.gameObject.layer = 12;
		}
        else
		{
			if(this.gameObject.activeSelf == true)
			this.gameObject.SetActive(false);	
		}
		
	}
	
	
	//change texture to selected sign
	void TextureSetup ()
	{
        this.gameObject.GetComponent<Renderer>().material 
            = MinimapSign.Instance.minimapSignMat[(int)signType];			
	}
	
}

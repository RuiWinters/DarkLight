/// <summary>
/// Sound manager.
/// This script use for play sound effect
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;

public class SoundManager:MonoSingletion<SoundManager>
{	
	[System.Serializable]
	public class SoundGroup
    {
		public string soundName;
		public AudioClip audioClip;		
	}	
	public List<SoundGroup> sound_List = new List<SoundGroup>();

	
    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="_soundName"></param>
	public void PlayingSound(string _soundName)
    {
        //AudioSource.PlayClipAtPoint(sound_List[FindSound(_soundName)].audioClip, Camera.main.transform.position);
        AudioSource.PlayClipAtPoint(FindClip(_soundName), TTUIRoot.Instance.uiCamera.transform.position);
    }
	
    private AudioClip FindClip(string _soundName)
    {
        SoundGroup sg = sound_List.Find(x => x.soundName == _soundName);
        if (sg != null)
        {
            return sg.audioClip;
        }
        else
        {
            Debug.LogError("�Ҳ���ָ������Ч��");
            return null;
        }
            
    }

	private int FindSound(string _soundName)
    {
        
		int i = 0;
		while( i < sound_List.Count ){
			if(sound_List[i].soundName == _soundName){
				return i;	
			}
			i++;
		}
		return i;
	}
	
}

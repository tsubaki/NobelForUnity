using UnityEngine;
using System;
using System.Collections;

public abstract class SingletonMonoBehaviourFast<T> : MonoBehaviour where T : SingletonMonoBehaviourFast<T>
{
	protected static readonly string[] findTags  =
	{
		"GameController",
	};
	
	private static T instance;
	public static T Instance {
		get {
			if (instance == null) {
				
				Type type = typeof(T);
				
				foreach( var tag in findTags )
				{
					GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
					
					for(int j=0; j<objs.Length; j++)
					{
						instance = (T)objs[j].GetComponent(type);
						if( instance != null)
							return instance;
					}
				}
			}
			
			return instance;
		}
	}
	
	virtual protected void Awake()
	{
		CheckInstance();
	}
	
	protected bool CheckInstance()
	{
		if( instance == null)
		{
			instance = (T)this;
			return true;
		}else if( Instance == this )
		{
			return true;
		}
		
		Destroy(this);
		return false;
	}
}
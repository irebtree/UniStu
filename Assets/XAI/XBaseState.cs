using UnityEngine;
using System.Collections;

public class XBaseState : MonoBehaviour {
	public string _animation;
	public AudioClip audioClip = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void InitState (XAIBehaviours ai)
	{}

	public virtual void HandleReason (XAIBehaviours ai)
	{}

	public virtual void HandleAction (XAIBehaviours ai)
	{}

	public virtual void EndedState (XAIBehaviours ai)
	{}
}

using UnityEngine;
using System.Collections;

public abstract class XBaseState {
	public new string name;
	public string _animation;
	public AudioClip audioClip = null;

	void Start () {
	
	}

	void Update () {
	
	}

	public override string ToString ()
	{
		return name;
	}

	public abstract void InitState (XAIBehaviours ai);

	public abstract void EnterState (XAIBehaviours ai);

	public abstract void UpdateState (XAIBehaviours ai);

	public abstract void ExitState (XAIBehaviours ai);
}

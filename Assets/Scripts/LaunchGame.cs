using UnityEngine;

public class LaunchGame : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		BindConfig config = new BindConfig();
		config.Init();
		UIManager.Single.Show(Paths.START_VIEW);
	}
}

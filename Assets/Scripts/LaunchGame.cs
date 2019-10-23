using UnityEngine;

public class LaunchGame : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		InitCustomAttributes attributes = new InitCustomAttributes();
		attributes.Init();
		UIManager.Single.Show(Paths.START_VIEW);

		var reader = ReaderConfig.GetReader(Paths.INIT_PLANE_CONFIG);
		reader["planes"][0]["life"].Get<int>((value)=>Debug.Log(value));
	}
}

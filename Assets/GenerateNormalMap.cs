using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.IO;

public class GenerateNormalMap : MonoBehaviour {
	[SerializeField]
	Texture2D topTex_;
	[SerializeField]
	Texture2D bottomTex_;

	[SerializeField]
	Texture2D leftTex_;

	[SerializeField]
	Texture2D rightTex_;

	[SerializeField]
	Texture2D centerTex_;

	[SerializeField]
	string outPutName_ = "generatedNormal.png";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.G))
			Generate ();
	}

	public void Generate()
	{

		Debug.Log("Creating texture....");
		int texWidth = centerTex_.width;
		int texheight = centerTex_.height;


		Texture2D newTexture = new Texture2D (texWidth, texheight, TextureFormat.RGB24, false);

		for (int i = 0; i < texWidth; i++) 
		{
			for (int j = 0; j < texheight; j++) 
			{
				Color topColor = topTex_.GetPixel(i, j);
				Color bottomColor = bottomTex_.GetPixel(i, j);
				Color leftColor = leftTex_.GetPixel(i, j);
				Color rightColor = rightTex_.GetPixel(i, j);
				Color centerColor = centerTex_.GetPixel(i, j);

//				float vecX = (1f + rightColor.r - leftColor.r) / 2f;
//				float vecY = (1f + topColor.r - bottomColor.r) / 2f;
//				float vecZ = centerColor.r;

				Vector3 normalVec = new Vector3();
				normalVec.x = rightColor.r - leftColor.r;
				normalVec.y = topColor.r - bottomColor.r;
				normalVec.z = centerColor.r;
				normalVec.Normalize();

				normalVec.x = (1f + normalVec.x) / 2f;
				normalVec.y = (1f + normalVec.y) / 2f;

				Color newColor = new Color(normalVec.x, normalVec.y, normalVec.z);
				newTexture.SetPixel(i, j, newColor);


			}
		}

		byte[] bytes = newTexture.EncodeToPNG();
		File.WriteAllBytes(Application.dataPath + "/" +outPutName_, bytes);
		Debug.Log("....done.");
	}
}

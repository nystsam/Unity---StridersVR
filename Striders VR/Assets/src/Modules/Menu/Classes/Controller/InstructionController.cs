using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionController : MonoBehaviour {

	[SerializeField] private Text infoText;

	[SerializeField] private Renderer videoRenderer;

	private MovieTexture videoClip;

	public void SetInfo(string _text)
	{
		Material _customMaterial = Resources.Load("Images/Materials/aOmpexN_460sv", typeof(Material)) as Material;
		this.videoRenderer.material = _customMaterial;
		this.videoClip = (MovieTexture)videoRenderer.material.mainTexture;
		this.infoText.text = "- " + _text;
		this.videoClip.Play();
		this.videoClip.Pause();
		this.videoClip.loop = true;
	}

	#region Script
	void Start () 
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag.Equals("IndexUI"))
		{
			this.videoClip.Stop();
			this.videoClip.Play();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag.Equals("IndexUI"))
		{
			this.videoClip.Stop();
			this.videoClip.Play();
			this.videoClip.Pause();
		}
	}
	#endregion
}

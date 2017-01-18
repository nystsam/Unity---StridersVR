using UnityEngine;
using System.Collections;

public class UIActivityBarController : MonoBehaviour {

	public Transform fillBar;

	public TextMesh percent;

	[SerializeField] private bool fill = false;
	[SerializeField] private float maxSize = 0;

	private Vector3 barScale;

	private IEnumerator waitTime()
	{
		yield return new WaitForSeconds(0.5f);
		fill = true;
	}

	public void begin(float max)
	{
		maxSize = max/10;
		StartCoroutine(this.waitTime());
	}

	#region Script
	void Start () 
	{
		barScale = new Vector3(0,3.5f,3.5f);
		fillBar.localScale = barScale;
		percent.text = "0%";
	}
	

	void Update () 
	{
		if(fill)
		{
			if(barScale.x < maxSize)
			{
				barScale.x += Time.deltaTime * 5;
				if(barScale.x > 10.0f)
					barScale.x = 10.0f;

				fillBar.localScale = barScale;
				percent.text = (barScale.x * 10).ToString("F0") + "%";
			}
		}
	}
	#endregion
}

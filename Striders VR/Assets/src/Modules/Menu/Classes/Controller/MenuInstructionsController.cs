using UnityEngine;
using System.Collections.Generic;

public class MenuInstructionsController : MonoBehaviour {

	public static MenuInstructionsController Current;

	[SerializeField] private TextMesh trainingName;

	[SerializeField] private GameObject instructionContainer;

	private GameObject instructionPrefab;


	public void SetTrainingName(string _name)
	{
		this.trainingName.text = _name;
	}

	public void focusRouteInstructions()
	{
		string _text1 = "Aparecerán trenes a lo largo del tiempo desde un punto inicial.";
		string _text2 = "Cada tren pertenece a una única estación, basado por su color.";
		string _text3 = "Utiliza tus manos para accionar los interruptores y guiar cada uno de los trenes.";

		List<string> _textList = new List<string>();
		_textList.Add(_text1);
		_textList.Add(_text2);
		_textList.Add(_text3);

		this.instantiateInstrucctions(_textList);
	}

	public void dotToDotInstructions()
	{
		string _text1 = "Une los puntos para formar la figura buscada.";
		string _text2 = "Al girar la mano izquierda hacia arriba, se muestra la figura ejemplo buscada.";
		string _text3 = "Gira la mano derecha hacia arriba para completar la figura construida.";
		
		List<string> _textList = new List<string>();
		_textList.Add(_text1);
		_textList.Add(_text2);
		_textList.Add(_text3);
		
		this.instantiateInstrucctions(_textList);
	}

	public void velocityPackInstructions()
	{
		string _text1 = "Coloca el objeto faltante en el paquete de color verde en el menor tiempo posible.";
		string _text2 = "Utiliza tu mano para agarrar el objeto.";
		string _text3 = "Evita que el objeto se superponga con algún otro.";
		
		List<string> _textList = new List<string>();
		_textList.Add(_text1);
		_textList.Add(_text2);
		_textList.Add(_text3);
		
		this.instantiateInstrucctions(_textList);
	}

	private void instantiateInstrucctions(List<string> _textList)
	{
		float _y = 1.3f;

		foreach(Transform child in this.instructionContainer.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		foreach(string _text in _textList)
		{
			GameObject _clone = (GameObject)GameObject.Instantiate(this.instructionPrefab);
			_clone.transform.parent = this.instructionContainer.transform;
			_clone.GetComponent<InstructionController>().SetInfo(_text);
			_clone.transform.localPosition = new Vector3(-0.45f, _y, -0.2f);
			_y -= 1.5f;
		}
	}

	public MenuInstructionsController()
	{
		Current = this;
	}

	#region Script
	void Start () 
	{
		this.instructionPrefab = Resources.Load("Prefabs/Menu/Instruction", typeof(GameObject)) as GameObject;
	}

	#endregion
}

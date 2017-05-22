using UnityEngine;
using System.Collections.Generic;

public class MenuInstructionsController : MonoBehaviour {

	public static MenuInstructionsController Current;


	[SerializeField] private TextMesh trainingName;
	[SerializeField] private TextMesh activiries;

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

		string _mat1 = "Images/Materials/InFR1";
		string _mat2 = "Images/Materials/InFR2";
		string _mat3 = "Images/Materials/InFR3";

		this.activiries.text = "Atención, Concentración, Estimación";

		List<string> _textList = new List<string>();
		_textList.Add(_text1);
		_textList.Add(_text2);
		_textList.Add(_text3);

		List<string> _matList = new List<string>();
		_matList.Add(_mat1);
		_matList.Add(_mat2);
		_matList.Add(_mat3);

		this.instantiateInstrucctions(_textList, _matList);
	}

	public void dotToDotInstructions()
	{
		string _text1 = "Úne los puntos para formar la figura buscada.";
		string _text2 = "Al girar la mano izquierda hacia arriba, se muestra la figura ejemplo buscada.";
		string _text3 = "Gira la mano derecha hacia arriba para completar la figura construida.";

		string _mat1 = "Images/Materials/InDTD1";
		string _mat2 = "Images/Materials/InDTD2";
		string _mat3 = "Images/Materials/InDTD3";

		this.activiries.text = "Abstracción, Modelación, Memoria";

		List<string> _textList = new List<string>();
		_textList.Add(_text1);
		_textList.Add(_text2);
		_textList.Add(_text3);

		List<string> _matList = new List<string>();
		_matList.Add(_mat1);
		_matList.Add(_mat2);
		_matList.Add(_mat3);

		this.instantiateInstrucctions(_textList, _matList);
	}

	public void velocityPackInstructions()
	{
		string _text1 = "Coloca el objeto faltante en el paquete de color verde en el menor tiempo posible.";
		string _text2 = "Utiliza tu mano para agarrar el objeto.";
		string _text3 = "Evita que el objeto se superponga con algún otro.";

		string _mat1 = "Images/Materials/InVP1";
		string _mat2 = "Images/Materials/InVP2";
		string _mat3 = "Images/Materials/InVP3";

		this.activiries.text = "Agilidad mental, Percepción";

		List<string> _textList = new List<string>();
		_textList.Add(_text1);
		_textList.Add(_text2);
		_textList.Add(_text3);

		List<string> _matList = new List<string>();
		_matList.Add(_mat1);
		_matList.Add(_mat2);
		_matList.Add(_mat3);
		
		this.instantiateInstrucctions(_textList, _matList);
	}

	private void instantiateInstrucctions(List<string> _textList, List<string> _matList)
	{
		float _y = 1.9f;
		int _index = 0;

		foreach(Transform child in this.instructionContainer.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		foreach(string _text in _textList)
		{
			GameObject _clone = (GameObject)GameObject.Instantiate(this.instructionPrefab);
			_clone.transform.parent = this.instructionContainer.transform;
			_clone.GetComponent<InstructionController>().SetInfo(_text, _matList[_index]);
			_clone.transform.localPosition = new Vector3(-0.45f, _y, -0.2f);
			_clone.transform.localScale = new Vector3(1,1,1);
			_y -= 2.2f;
			_index ++;
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

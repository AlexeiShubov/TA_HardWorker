using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleDataBind : Binder, IPointerDownHandler
{
	private bool _down;
	private float _downTime;
	private Toggle _toggle;

	/// <summary>
	/// Имя тугла (если пустое берется из имени объекта)
	/// </summary>
	public float delayUntilTimerReset = 2f;
	public string toggleName = "";
	public string enableField = "";
	public bool defaultEnable = true;

	/// <summary>
	/// Поле из модели где взять настройку клавиатуры
	/// </summary>
	public string keyField = "";

	/// <summary>
	/// Кнопка клавиатуры (заполнится из модели если там есть)
	/// </summary>
	public string key = "";

	/// <summary>
	/// Срабатывать на нажатие клавиши на клавиатуре
	/// </summary>
	public bool respondToKeyboardButtonPress;

	[OnAwake]
	private void CustomAwake()
	{
		_toggle = GetComponent<Toggle>();
		_toggle.interactable = defaultEnable;
		
		toggleName = string.IsNullOrEmpty(toggleName) ? name : toggleName;
		enableField = enableField == "" ? $"Toggle{toggleName}Enable" : enableField;
	}

	[OnStart]
	private void CustomStart()
	{
		Subscribe();
	}
	
	[OnDestroy]
	public void CustomOnDestroy()
	{
		UnSubscribe();
	}

	[OnUpdate]
	private void CustomUpdate()
	{
		if (!_toggle.interactable || key == "" || !respondToKeyboardButtonPress)
		{
			return;
		}

		if (Input.GetKeyDown(key))
		{
			_downTime = 0;
			_down = true;

			OnClickToggle();
		}
		else if (Input.GetKeyUp(key) || _downTime >= delayUntilTimerReset)
		{
			_down = false;
		}

		_downTime = _down ? _downTime + Time.deltaTime : _downTime;
	}

	private void Subscribe()
	{
		Model.EventManager.AddAction($"On{enableField}Changed", OnEnableFieldChanged);

		if (keyField == "")
		{
			keyField = $"{name}Key";
		}
		else
		{
			key = Model.GetString(keyField, key);
			Model.EventManager.AddAction($"OnToggle{keyField}Changed", OnKeyChanged);
		}
	}

	private void UnSubscribe()
	{
		_toggle.onValueChanged.RemoveAllListeners();

		Model.EventManager.RemoveAction($"{enableField}Changed", OnEnableFieldChanged);
		Model.EventManager.RemoveAction($"OnToggle{keyField}Changed", OnKeyChanged);
	}

	[Bind]
	private void OnKeyChanged()
	{
		key = Model.GetString(keyField);
	}
	
	private void OnEnableFieldChanged()
	{
		_toggle.interactable = Model.GetBool(enableField);
	}

	private void OnClickToggle()
	{
		if (!_toggle.interactable || !isActiveAndEnabled)
		{
			return;
		}

		Model?.EventManager.Invoke("SoundPlay", "Click");

		Settings.Fsm?.Invoke("OnToggle", toggleName);

		Model?.EventManager.Invoke($"OnToggle{toggleName}Click");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!respondToKeyboardButtonPress)
		{
			OnClickToggle();
		}
	}
}

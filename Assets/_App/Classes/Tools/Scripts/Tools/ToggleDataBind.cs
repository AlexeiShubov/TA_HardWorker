using AxGrid;
using AxGrid.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleDataBind : Binder
{
	private bool _down;
	private float _downTime;
	private Toggle _toggle;
	private EventTrigger _eventTrigger;

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
	public void CustomAwake()
	{
		_toggle = GetComponent<Toggle>();
		_toggle.interactable = defaultEnable;
		
		toggleName = string.IsNullOrEmpty(toggleName) ? name : toggleName;
		enableField = enableField == "" ? $"Toggle{toggleName}Enable" : enableField;

		if (!respondToKeyboardButtonPress)
		{
			_toggle.onValueChanged.AddListener(OnClickToggle);
		}
		else
		{
			var entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerDown};
			
			entry.callback.AddListener(OnClickToggle);
			_eventTrigger = gameObject.AddComponent<EventTrigger>();
			_eventTrigger.triggers.Add(entry);
		}
	}

	[OnStart]
	public void CustomStart()
	{
		Model.EventManager.AddAction($"On{enableField}Changed", OnItemEnable);

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

			OnClickToggle(true);
		}
		else if (Input.GetKeyUp(key) || _downTime >= delayUntilTimerReset)
		{
			_down = false;
		}

		_downTime = _down ? _downTime + Time.deltaTime : _downTime;
	}

	[OnDestroy]
	public void CustomOnDestroy()
	{
		_toggle.onValueChanged.RemoveAllListeners();

		if (_eventTrigger != null)
		{
			_eventTrigger.triggers.ForEach(t => t.callback.RemoveAllListeners());
			_eventTrigger.triggers.Clear();
		}

		Model.EventManager.RemoveAction($"{enableField}Changed", OnItemEnable);
		Model.EventManager.RemoveAction($"OnToggle{keyField}Changed", OnKeyChanged);
	}

	private void OnKeyChanged()
	{
		key = Model.GetString(keyField);
	}
	
	private void OnItemEnable()
	{
		Debug.LogError(Model.GetBool($"On{enableField}Changed"));
		_toggle.interactable = Model.GetBool($"On{enableField}Changed");
	}

	private void OnClickToggle(BaseEventData data)
	{
		OnClickToggle(true);
	}

	private void OnClickToggle(bool status)
	{
		if (!_toggle.interactable || !isActiveAndEnabled)
		{
			return;
		}

		Model?.EventManager.Invoke("SoundPlay", "Click");

		Settings.Fsm?.Invoke("OnToggle", toggleName);

		Model?.EventManager.Invoke($"OnToggle{toggleName}Click");
	}
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SkillInstance : System.IDisposable
{
	public readonly SkillData skillData;

	public KomponentSystem Caster { get; }
	public KomponentSystem Target { get; }

	private readonly float startTime;
	private float dischargeTime;

	public float ElapsedTime => Time.time - startTime;

	private EventSlot<SkillInstance> _onStart = null;
	private EventSlot<SkillInstance> _onEnd = null;
	private EventSlot<SkillInstance> _onDestroy = null;
	private EventSlot<SkillInstance> _onInterrupt = null;

	public IEventRegister<SkillInstance> OnStart => _onStart ??= new();
	public IEventRegister<SkillInstance> OnEnd => _onEnd ??= new();
	public IEventRegister<SkillInstance> OnDestroy => _onDestroy ??= new();
	public IEventRegister<SkillInstance> OnInterrupt => _onInterrupt ??= new();
	
	// #region Action Input Tracking
	// private readonly Dictionary<EActionType, BoolState> wasActionTriggeredStates = new();
	// private readonly Dictionary<EActionType, Action> wasActionTriggeredListeners = new();
	
	// public BoolState WasActionTriggered(EActionType action) => wasActionTriggeredStates[action];
	
	// public BoolState IsActionHeld(EActionType action) => CasterInput.GetActionObserver(action).isHeld;
	// public BoolState IsActionPressed(EActionType action) => CasterInput.GetActionObserver(action).isPressed;

	// public EventSlot ActionTriggered(EActionType action) => CasterInput.GetActionObserver(action).pressed;
	// public EventSlot ActionReleased(EActionType action) => CasterInput.GetActionObserver(action).released;
	// public EventSlot ActionHoldReleased(EActionType action) => CasterInput.GetActionObserver(action).holdReleased;
	// #endregion

	private readonly BoolState _isExecuting = new();
	private readonly BoolState _wasInterrupted = new();
	private readonly BoolState _isDestroyed = new();
	
	public IBoolStateObserver IsExecuting => _isExecuting;
	public IBoolStateObserver WasInterrupted => _isExecuting;
	public IBoolStateObserver IsDestroyed => _isExecuting;
	
	private bool CanInterrupt() => _isExecuting.Value && !_wasInterrupted.Value;

	// private bool CanInterrupt(ESkillPriority target) => IsExecuting && !WasInterrupted && (int)skillData.priority <= (int)target;
	// private bool CanInterruptExcluding(ESkillPriority target) => IsExecuting && !WasInterrupted && (int)skillData.priority < (int)target;
	private bool CanEnd => IsExecuting.Value && !IsDestroyed.Value;

	// private readonly Dictionary<SkillEventData, SkillTriggerInstance> pendingTriggers = new();
	// private readonly Dictionary<SkillEventData, SkillTriggerInstance> activeEvents = new();
	// private readonly Dictionary<SkillEventData, SkillEffectInstance> activeEffects = new();

	// private readonly Dictionary<int, SkillInteractionLog> interactionLogs = new();

	// public readonly Dictionary<Semaphore, object> blockedSemaphores = new();
	// public readonly Dictionary<string, GameObject> creations = new();

	// private bool HasActiveOrPendingEvents => pendingTriggers.Count > 0 || activeEvents.Count > 0;
	
	private SkillInstance( SkillData skillData, KomponentSystem caster, KomponentSystem targetEntity )
	{
		startTime = Time.time;
		Caster = caster;

		// ComboCount = comboCount;

		// this.skillData = skillData;

		// CastTargetEntity = targetEntity;
		// CastTargetPosition = targetPosition;
		// CastTargetDirection = direction;

		// IsExecuting = true;

		// SetupRequiredAnimations();
		// SetupSkillEvents();
		// TrackInputActions();

		// this.Log($"Skill <b>{Name}</b> ({ComboCount}) <Color=#DisyStyles.success>started</color>!");
		
		// Caster.StartCoroutine(Start());
	}

	public static SkillInstance Execute( SkillData skillData, KomponentSystem caster )
	{
		var instance = new SkillInstance( skillData, caster, null );
		caster.StartCoroutine( instance.Start() );
		skillData.Execute( instance );
		return instance;
	}

	// public SkillInstance TryTriggerSkillInstance(SkillData newSkillData, bool incrementCombo, bool bindToSlot)
	// {
	// 	var comboCount = incrementCombo ? ComboCount + 1 : 1;
		
	// 	return bindToSlot
	// 		? CasterSkills.TryTriggerBoundSkill(newSkillData, SkillSlot, comboCount)
	// 		: CasterSkills.TryTriggerDetachedSkill(newSkillData, comboCount);
	// }

	// public void TriggerSkillEvent(SkillEventData eventData)
	// {
	// 	this.LogVerbose($"Skill event `{eventData.name}` triggered!", skillData);

	// 	for (var index = 0; index < eventData.executionBlocks.Count; index++)
	// 	{
	// 		var executionBlock = eventData.executionBlocks[index];
	// 		executionBlock.NewExecution( this, eventData, index );

	// 		var passedConditions = !executionBlock.HasCondition || executionBlock.condition.QueryCurrentState(this);

	// 		if (passedConditions) ExecuteEffects(eventData, executionBlock.effects, "normal", index);
	// 		else if (executionBlock.HasElseCondition) ExecuteEffects(eventData, executionBlock.elseEffects, "else", index);
	// 		else this.LogVerbose($"ExecutionBlock #{index} failed!", skillData);

	// 		if (!IsExecuting) return;
	// 	}

	// 	// Can have multiple activations
	// 	if (!pendingTriggers.ContainsKey(eventData)) return;

	// 	activeEvents.Add(eventData, pendingTriggers[eventData]);
	// 	pendingTriggers.Remove(eventData);
	// }

	// private void ExecuteEffects(SkillEventData eventData, List<SkillEffectData> effects, string blockType, int index)
	// {
	// 	this.LogVerbose($"Executing {blockType} block #{index}!", skillData);
		
	// 	foreach (var effect in effects)
	// 	{
	// 		try
	// 		{
	// 			this.LogVerbose($"Executing effect: {effect}!", skillData);

	// 			var newEffect = effect.Activate(this);
	// 			AddActiveEffects(eventData, newEffect);
	// 		}
	// 		catch (Exception exception)
	// 		{
	// 			Debug.LogError($"Error while performing {effect.effectType}: {exception}", Caster);
	// 		}
	// 	}
	// }

	// public void AddActiveEffects( SkillEventData eventData, SkillEffectInstance effectInstance )
	// {
	// 	if( !IsExecuting ) return;
	// 	if( effectInstance == null ) return;
	// 	activeEffects.Add( eventData, effectInstance );
	// }

	// public void SkillEventEnded(SkillEventData eventData)
	// {
	// 	if (IsDestroyed) return;

	// 	this.LogVerbose($"Skill event `{eventData.name}` finished!", skillData);
	// 	activeEvents.Remove(eventData);

	// 	if (HasActiveOrPendingEvents) return;

	// 	Dispose();
	// }

	// private void SetupSkillEvents()
	// {
	// 	if (!IsExecuting) return;

	// 	foreach (var eventData in skillData.events)
	// 		pendingTriggers.Add(eventData, new SkillTriggerInstance(this, eventData));
	// }

	private static readonly YieldInstruction WAIT_END_FRAME = new WaitForEndOfFrame();

	private IEnumerator Start()
	{
		yield return WAIT_END_FRAME;
		_onStart?.Trigger( this );
	}

	public void End()
	{
		if( !CanEnd ) return;

		_isExecuting.SetFalse();
		if( _wasInterrupted.Value ) _onInterrupt?.Trigger(this);
		else _onEnd?.Trigger(this);

		Dispose();
	}

	// public bool TryInterrupt() => TryInterrupt(skillData.priority, true);
	
	// public bool TryInterrupt(ESkillPriority target, bool includingTarget)
	// {
	// 	var canInterrupt = includingTarget
	// 		? CanInterrupt(target)
	// 		: CanInterruptExcluding(target);
		
	// 	if (!canInterrupt) return false;
	// 	WasInterrupted = true;

	// 	End();

	// 	return true;
	// }

	public bool TryInterrupt()
	{		
		if( !CanInterrupt() ) return false;
		_wasInterrupted.SetTrue();
		End();
		return true;
	}
	
	// private void SetupRequiredAnimations()
	// {
	// 	if (skillData.requiredAnimations.Count > 0)
	// 		Caster.Animator.OverrideAnimations(skillData.requiredAnimations);
	// }
	
	// private void TrackInputActions()
	// {
	// 	if (!Caster.IsPlayer) return;
		
	// 	TrackAction(EActionType.Primary);
	// 	TrackAction(EActionType.Secondary);
	// 	TrackAction(EActionType.ActivateDice);
	// 	TrackAction(EActionType.Dodge);
	// }

	// public void SetSkillSlot(SkillSlotBehaviour slot) => SkillSlot = slot;

	// private void TrackAction(EActionType actionType)
	// {
	// 	if (!CasterInput.TryGetActionObserver(actionType, out var actionObserver)) return;
		
	// 	var state = new BoolState();
	// 	wasActionTriggeredStates.Add(actionType, state);

	// 	Action listener = () => state.Value = true;
		
	// 	wasActionTriggeredListeners.Add(actionType, listener);
	// 	actionObserver.isPressed.OnTrueState.Register(listener);
	// }

	// public SkillInteractionLog GetInteractionLog(SkillEffectData data, Entity target)
	// {
	// 	var hash = HashCode.Combine(data, target);

	// 	if (!interactionLogs.ContainsKey(hash))
	// 		interactionLogs.Add(hash, new SkillInteractionLog(target));

	// 	return interactionLogs[hash];
	// }

	public void Dispose()
	{
		// IsDestroyed = true;

		// destroyed.Trigger(this);
		
		// foreach (var trigger in pendingTriggers.Values) trigger.Dispose();
		// foreach (var activeEvent in activeEvents.Values) activeEvent.Dispose();
		
		// foreach (var (actionType, listener) in wasActionTriggeredListeners)
		// {
		// 	var pressed = ActionTriggered(actionType);
		// 	pressed.Unregister(listener);
		// }
		
		// started.Kill();
		// ended.Kill();
		// destroyed.Kill();
		// interrupted.Kill();
		_isDestroyed.SetTrue();

		_onDestroy?.Trigger( this );

		_isExecuting.Kill();
		_wasInterrupted.Kill();
		_isDestroyed.Kill();

		_onStart?.Kill();
		_onEnd?.Kill();
		_onDestroy?.Kill();
		_onInterrupt?.Kill();

		_onStart = null;
		_onEnd = null;
		_onDestroy = null;
		_onInterrupt = null;
	}

	public string Name => skillData.name;
	public override string ToString() => $"SkillInstance {Name}";
}
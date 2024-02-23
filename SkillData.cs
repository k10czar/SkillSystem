using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SkillSys/SkillData")]
public class SkillData : ScriptableObject
{
	// public List<ModifiableVariable> modifiableVariables;

	// public ESkillPriority priority = ESkillPriority.Default;
	// public ESkillType skillType;
	
	// public List<ERelationship> possibleTargets;

	// public float energyPerUse = 1;

	// public List<AnimationEntry> requiredAnimations;
	// public List<SpawnInfo> requiredSpawns;

	[SerializeField] private List<SkillEventData> events = new();

    // public bool HasVariable(string variableName) => modifiableVariables.Any(entry => entry.name == variableName);

    // public bool TryGetVariable(string variableName, out ModifiableVariable variable)
    // {
    // 	variable = modifiableVariables.Find(entry => entry.name == variableName);
    // 	return variable != null;
    // }

    public void Execute(SkillInstance instance)
    {
		foreach( var evnt in events )
		{
			evnt.Execute( instance );
		}
    }
}

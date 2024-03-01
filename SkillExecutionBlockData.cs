using UnityEngine;
using BoolStateOperations;
using System.Collections.Generic;

[System.Serializable]
public class SkillExecutionBlockData
{
	[ExtendedSerializeReference,SerializeReference] ISkillBlockCondition _condition;
	[ExtendedSerializeReference,SerializeReference] ISkillEventData[] _effects = null;

	public bool Execute( SkillInstance skill )
	{
		var passedCondition = _condition?.QueryCondition( skill ) ?? true;
		if( !passedCondition ) return false;

        for (int i = 0; i < _effects.Length; i++)
		{
            ISkillEventData effect = _effects[i];
            try
			{
				effect.Execute( skill );
			}
			catch( System.Exception exception )
			{
				Debug.LogError($"Error while performing {effect.GetType()}[i]: {exception}", skill.Caster);
			}
		}

		return true;
	}
}
using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class SkillEventData
{
	[ExtendedDrawer,SerializeReference] ISkillTriggerData _trigger;
	[SerializeField] SkillExecutionBlockData[] _executionBlocks;
	
	public void Execute( SkillInstance skill )
	{
		var triggerInstance = _trigger.BuildTrigger( skill );
		if( triggerInstance == null ) return;
		triggerInstance.Register( () => ExecuteBlocks( skill ) );
	}

	void ExecuteBlocks( SkillInstance skill )
	{
		for( int i = 0; i < _executionBlocks.Length; i++ )
		{
			var block = _executionBlocks[i];
			block.Execute( skill );
		}
	}
}

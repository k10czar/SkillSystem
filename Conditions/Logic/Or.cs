using UnityEngine;

namespace SkillConditions
{
    [ListingPath("Logic/Or(|)")]
	public class Or : ISkillBlockCondition
	{
		[SerializeReference] ISkillBlockCondition[] _conditions;

		public bool QueryCondition( SkillInstance skill )
		{
			foreach( var cond in _conditions )
			{
				if( cond.QueryCondition( skill ) ) return true;
			}
			return false;
		}
	}
}
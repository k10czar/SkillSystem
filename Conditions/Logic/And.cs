using UnityEngine;

namespace SkillConditions
{
    [ListingPath("Logic/And(&)")]
	public class And : ISkillBlockCondition
	{
		[SerializeReference] ISkillBlockCondition[] _conditions;

		public bool QueryCondition( SkillInstance skill )
		{
			foreach( var cond in _conditions )
			{
				if( !cond.QueryCondition( skill ) ) return false;
			}
			return true;
		}
	}

	// [ListingPath("Movement/IsGrounded")]
	// public class IsGrounded : ISkillBlockCondition<IMovementObserver>
	// {
	// 	public bool QueryCondition( IMovementObserver movObserver )
	// 	{
	// 		return movObserver.IsGrounded.Value;
	// 	}
	// }
}

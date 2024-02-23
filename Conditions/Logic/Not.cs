using UnityEngine;

namespace SkillConditions
{
    [ListingPath("Logic/Not(!)")]
	public class Not : ISkillBlockCondition
	{
		[SerializeReference] ISkillBlockCondition _condition;

		public bool QueryCondition( SkillInstance skill )
		{
			return !_condition.QueryCondition( skill );
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

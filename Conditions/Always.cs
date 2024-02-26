namespace SkillConditions
{
    [ListingPath("Always")]
	public class Always : ISkillBlockCondition
	{
		public bool QueryCondition( SkillInstance skill ) => true;
	}
}
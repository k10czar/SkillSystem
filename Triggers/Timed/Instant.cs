namespace SkillTriggers
{
    [OverridingIcon("actions")]
    [ListingPath("Instant")]
    public class Instant : ISkillTriggerData
    {
        public IEventRegister BuildTrigger( SkillInstance skill )
        {
            return InstantTriggerOnce.Instance;
        }
    }
}

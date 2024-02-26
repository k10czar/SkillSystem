using UnityEngine;
using System.Collections;

namespace SkillTriggers
{
    [OverridingIcon("clock")]
    [ListingPath("Timed/Periodic")]
    public class Periodic : ISkillTriggerData
	{
		[SerializeField] float _interval;
		[SerializeField] bool _triggerOnFirstTick;

        public IEventRegister BuildTrigger( SkillInstance skill )
        {
			var periodicEvent = new EventSlot();
			skill.Caster.StartCoroutine( OnTickPeriodic( skill, periodicEvent ) );
			return periodicEvent;
        }

        private IEnumerator OnTickPeriodic( SkillInstance skill, IEventTrigger trigger )
        {
			var wait = new WaitForSeconds( _interval );
			yield return null;
			if( _triggerOnFirstTick ) trigger.Trigger();
			while( skill.IsExecuting.Value )
			{
				yield return wait;
				trigger.Trigger();
			}
        }
    }
}

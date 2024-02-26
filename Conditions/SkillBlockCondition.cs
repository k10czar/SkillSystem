public interface ISkillBlockCondition
{
	bool QueryCondition( SkillInstance skill );
}

public static class SkillBlockConditionUtils
{
    static TypeListData<ISkillBlockCondition> _listingData = null;
    public static TypeListData<ISkillBlockCondition> ListingData => _listingData ??= new TypeListData<ISkillBlockCondition>();
}

public interface ISkillBlockCondition<T> where T : IKomponent
{
	bool QueryCondition( T komp );
	bool QueryCondition( SkillInstance skill ) { return QueryCondition( skill.Caster.GetKomponent<T>() ); }
}

public interface ISkillBlockCondition<T,K> where T : IKomponent where K : IKomponent
{
	bool QueryCondition( T komp1, K komp2 );
	bool QueryCondition( SkillInstance skill ) { return QueryCondition( skill.Caster.GetKomponent<T>(), skill.Caster.GetKomponent<K>() ); }
}

public interface ISkillBlockCondition<T,K,S> where T : IKomponent where K : IKomponent where S : IKomponent
{
	bool QueryCondition( T komp1, K komp2, S komp3 );
	bool QueryCondition( SkillInstance skill ) { return QueryCondition( skill.Caster.GetKomponent<T>(), skill.Caster.GetKomponent<K>(), skill.Caster.GetKomponent<S>() ); }
}
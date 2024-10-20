

namespace RushHour.Data
{
    public enum StatType
    {
        MainValue,
        Range,
        Cost,
        TargetType,
        HitSpeed,
        StrongAgainst,
        WeakAgainst
    }

    public class TowerStat<T>
    {
        public string name;
        public T value;

        public TowerStat(string name, T value)
        {
            this.name = name;
            this.value = value;
        }

        public static TowerStat<string> ConvertToString(TowerStat<T> stat)
        {
            return new(stat.name, stat.value.ToString());
        }
    }

    public class TowerStats
    {
        public TowerStat<float> mainValue;
        public TowerStat<float> range;
        public TowerStat<float> cost;
        public TowerStat<string> targetType;
        public TowerStat<float> hitSpeed;
        public TowerStat<EnemyType> strongAgainst;
        public TowerStat<EnemyType> weakAgainst;
    }
}

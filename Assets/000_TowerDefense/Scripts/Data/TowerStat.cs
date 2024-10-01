

namespace RushHour.Data
{
    public class TowerStat<T>
    {
        public string name;
        public T value;

        public TowerStat(string name, T value)
        {
            this.name = name;
            this.value = value;
        }
    }

    public class TowerStats
    {
        public TowerStat<float> mainValue;
        public TowerStat<float> range;
        public TowerStat<float> cost;
        public TowerStat<string> targetType;
        public TowerStat<float> hitSpeed;
    }
}

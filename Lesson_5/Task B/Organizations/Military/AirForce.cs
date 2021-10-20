namespace Task_B.Organizations.Military
{
    // Класс воздушных сил
    public class AirForce : Organization // Наследуется от класса организации
    {
        public AirForce(string name) : base(name)
        {
            Type = OrgType.Military;
        }
    }
}

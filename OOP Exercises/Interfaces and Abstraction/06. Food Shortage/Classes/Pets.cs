
namespace BirthdayCelebrations
{
    public class Pets : IBirthable
    {
        public Pets(string name, string birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }

        public string Name { get; set; }
        public string Birthdate { get; set; }

        public override string ToString()
        {
            return Birthdate;
        }
    }
}

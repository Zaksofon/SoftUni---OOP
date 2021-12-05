
namespace BorderControl
{
    public class Robots : IIdentable
    {
    public Robots(string name, string id)
    {
        Name = name;
        Id = id;
    }
    public string Name { get; set; }
    public string Id { get; set; }
    }
}

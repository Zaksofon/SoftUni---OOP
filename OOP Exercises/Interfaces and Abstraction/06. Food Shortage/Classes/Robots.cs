﻿
namespace BirthdayCelebrations
{
    public class Robots : IIdenable
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
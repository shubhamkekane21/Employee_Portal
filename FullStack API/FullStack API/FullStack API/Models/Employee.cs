namespace FullStack_API.Models
{
    public class Employee
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public int salary { get; set; }

    }
}

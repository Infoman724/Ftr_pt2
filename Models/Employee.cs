namespace Ftr_pt2.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; } 
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Access { get; set; }

        public DateTime EntryTime { get; set; } 
        public DateTime ExitTime { get; set; } 
    }


}

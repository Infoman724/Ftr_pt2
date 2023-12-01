namespace Ftr_pt2.Models
{
    public class WorkTime
    {
        public int WorkTimeId { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
    }
}

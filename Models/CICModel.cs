using System;

namespace SimplyEmployeeTracker.ViewModels
{
    public class CICModel
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int AssignedUserId { get; set; }
        public DateTime CICDueDate { get; set; }
        public enum CICType { Certification, Inspection, Calibration }
    }
}
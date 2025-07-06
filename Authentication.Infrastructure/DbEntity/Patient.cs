namespace Authentication.Infrastructure.DbEntity
{
    public class Patient
    {
        public string? StudyInstanceUID { get; set; }
        public string? StudyDate { get; set; }
        public string? StudyTime { get; set; }
        public string? AccessionNumber { get; set; }
        public string? StudyID { get; set; }
        public string? PatientName { get; set; }
        public string? PatientID { get; set; }
        public string? Age { get; set; }
        public string? Sex { get; set; }
        public string? StudyDescription { get; set; }
        public string? Modality { get; set; }
        public string? ReferringPhysicianName { get; set; }
        public string? ExamingDocID { get; set; }
        public string? InstitutionID { get; set; }
        public string? InstitutionName { get; set; }       
        public string? PatientBirth { get; set; }
    }

}

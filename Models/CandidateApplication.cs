namespace CampusPlacement.Models
{
    public class CandidateApplication
    {
        public int Id { get; set; }
        public string ProgramId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}

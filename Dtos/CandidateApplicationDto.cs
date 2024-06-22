namespace CampusPlacement.Dtos
{
    public class CandidateApplicationDto
    {
        public int Id { get; set; }
        public string ProgramId { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}

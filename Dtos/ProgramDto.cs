namespace CampusPlacement.Dtos
{
    public class ProgramDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}

namespace CampusPlacement.Models
{
    public class ProgramModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}

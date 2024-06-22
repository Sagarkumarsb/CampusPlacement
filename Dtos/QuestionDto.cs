namespace CampusPlacement.Dtos
{
    public class QuestionDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public List<string> Options { get; set; } // For Dropdown and MultipleChoice questions
    }
}

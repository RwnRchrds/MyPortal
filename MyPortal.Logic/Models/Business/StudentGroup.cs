namespace MyPortal.Logic.Models.Business
{
    public enum StudentGroupType
    {
        RegGroup,
        YearGroup,
        House,
        Class,
        GiftedTalented,
        SenStatus,
        UserDefined
    }
    public class StudentGroup
    {
        public StudentGroupType StudentGroupType { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}

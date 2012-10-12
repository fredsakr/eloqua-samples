namespace EmailSample.Models
{

    public class EmailTestDeployment : EmailDeployment
    {
        public int? contactId { get; set; }
        public int? sendFromUserId { get; set; }
    }
}

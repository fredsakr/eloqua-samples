using EmailSample.Models.Content;

namespace EmailSample.Models
{
    public class Email
    {
        public string bouncebackEmail { get; set; }
        public string createdAt { get; set; }
        public string createdBy { get; set; }
        public int? emailFooterId { get; set; }
        public int? emailGroupId { get; set; }
        public int? emailHeaderId { get; set; }
        public int? encodingId { get; set; }
        public int? folderId { get; set; }
        public RawHtmlContent htmlContent { get; set; }
        public int? id { get; set; }
        public bool isPlainTextEditable { get; set; }
        public string name { get; set; }
        public string plainText { get; set; }
        public string replyToName { get; set; }
        public string replyToEmail { get; set; }
        public string senderEmail { get; set; }
        public string senderName { get; set; }
        public bool sendPlainTextOnly { get; set; }
        public string subject { get; set; }
        public string updatedAt { get; set; }
        public string updatedBy { get; set; }
    }
}

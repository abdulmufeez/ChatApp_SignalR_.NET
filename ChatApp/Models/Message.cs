using System;

namespace ChatApp.Models
{
    public class Message {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }

        public Chat Chat { get; set; }
        public int ChatId { get; set; }
    }
}
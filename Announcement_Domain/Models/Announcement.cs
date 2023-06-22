﻿namespace Announcement_Domain.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateOfArchiving { get; set; }
    }
}

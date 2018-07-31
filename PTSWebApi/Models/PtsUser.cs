using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsUser
    {
        public PtsUser()
        {
            PtsComment = new HashSet<PtsComment>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<PtsComment> PtsComment { get; set; }
    }
}

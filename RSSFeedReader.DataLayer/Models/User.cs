using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSFeedReader.DataLayer.Models
{
    public class User
    {
        public long Id { get; set; }
        public UserStatus Status { get; set; }
        public DateTime JoinDate { get; set; }
    }
}

public enum UserStatus
{
    Active, 
    Inactive
}

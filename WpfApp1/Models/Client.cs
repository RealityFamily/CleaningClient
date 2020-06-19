using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfApp1.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public DateTime StartTime {
            get { return DateTime.Parse(_StartTime); }
            set { _StartTime = value.ToString("dd.MM.yyyy"); }
        }
        internal string _StartTime { get; set; }
    }
}

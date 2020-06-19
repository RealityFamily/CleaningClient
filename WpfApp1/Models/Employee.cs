using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace WpfApp1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IndentifyNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Town { get; set; }
        public string Adress { get; set; }
        public string Index { get; set; }

        [NotMapped]
        public List<WorkTime> WorkTimes {
            get { return _WorkTimes == null ? null : JsonConvert.DeserializeObject<List<WorkTime>>(_WorkTimes); }
            set { _WorkTimes = JsonConvert.SerializeObject(value); }
        }
        
        internal string _WorkTimes { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfApp1.Models
{
    public class Cleaning
    {
        public int Id { get; set; }

        [NotMapped]
        public WorkTime Time { 
            get { return JsonConvert.DeserializeObject<WorkTime>(_Time); }
            set { _Time = JsonConvert.SerializeObject(value); }
        }
        internal string _Time { get; set; }

        public double Price { get; set; }

        public int PlaceId { get; set; }
        public CleaningPlace Place { get; set; }

        public int WorkerId { get; set; }
        public Employee Worker { get; set; }

        public int WayValue { get; set; }
        public string WayUnit { get; set; }
    }
}

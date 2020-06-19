using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfApp1.Models
{
    public class CleaningPlace
    {
        public int Id { get; set; }
        public string Town { get; set; }
        public string Addres { get; set; }
        public string Index { get; set; }
        public double PriceFromClient { get; set; }

        [NotMapped]
        public WorkTime WorkTime {
            get { return _WorkTime == null ? null : JsonConvert.DeserializeObject<WorkTime>(_WorkTime); }
            set { _WorkTime = JsonConvert.SerializeObject(value); }
        }

        internal string _WorkTime { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}

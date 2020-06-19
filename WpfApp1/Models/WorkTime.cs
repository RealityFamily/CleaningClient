using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.Models
{
    public class WorkTime
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

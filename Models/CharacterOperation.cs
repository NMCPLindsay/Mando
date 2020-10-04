using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandalorianDB.Models
{
    public class CharacterOperation
    {
        public enum OperationStatus { OKAY, CANCEL }
        public OperationStatus Status { get; set; }
        public string NewChar { get; set; }
    }
}

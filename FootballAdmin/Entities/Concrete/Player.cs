using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Generics;

namespace Entities.Concrete
{
    public class Player : Base
    {
        public string PlayerPosition { get; set; }
        public string? PlayerSecondPositions { get; set; }
        public string StrongFoot { get; set; }
        public int? TeamId { get; set; }  // Nullable, çünkü bir oyuncu bir takıma ait olmayabilir
                                          // Foreign Key for TrainingSession
        public int? TrainingSessionId { get; set; }  // Nullable olmalı, çünkü oyuncu her zaman bir antrenmana katılmayabilir

    }
}

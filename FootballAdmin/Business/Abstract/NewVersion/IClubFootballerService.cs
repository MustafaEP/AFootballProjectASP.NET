﻿using Business.Abstract.GenericService;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.NewVersion
{
    public interface IClubFootballerService : IGenericService<ClubsFootballer>
    {
        List<ClubsFootballer> OwnFootballers(int clubId);
        int HowManyPlayerinCountry(string country, int managerId);
        ClubsFootballer GetFootballerWithUserNameClubId(string username, int id);
    }
}

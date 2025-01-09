using Access.EntityFramework.NewVersion;
using Access.EntityFramework;
using Business.Concrete.NewVersion;
using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete.NewVersion;
using System.Security.Claims;
using WebUI.Areas.Manager.Models;
using Newtonsoft.Json;
using Entities.Concrete.Matches;
using Entities.Concrete.ClubFormation;
using Business.Concrete.ClubFormation;
using Access.EntityFramework.ClubFormation;
using Business.Abstract.AddedFootballers;
using Access.EntityFramework.AddedFootballers;
using WebUI.Models;
using Entities.Concrete;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class FootballMatchesController : Controller
    {
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        ManagerClubManager _managerClubManager = new ManagerClubManager(new EfManagerClubRepository());
        ClubFootballerManager _clubFootballerManager = new ClubFootballerManager(new EfClubFootballerRepository());
        FootbalMatchManager _footballMatchManager = new FootbalMatchManager(new EfFootballMatchRepository());
        FormationManager _formationManager = new FormationManager(new EfFormationRepository());
        XIPlayerManagere _xiPlayerManager = new XIPlayerManagere(new EfXIPlayerRepository());
        AddedFootballerManager _addedFootballerManager = new AddedFootballerManager(new EfAddedFootballerRepository());
        FootballerManager _footballerManager = new FootballerManager(new EfFootballerRepository());

        [HttpGet]
        public IActionResult ProposeMatch()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var anotherClubs = _managerClubManager.AnotherClubs(managerId);

            return View(anotherClubs);
        }
        [HttpPost]
        public IActionResult AddMatch(FootballMatch match)
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var manager = _managerManager.TGetById(managerId);
            match.HomeClubId = manager.ManagerClubId.Value;
            match.MatchString = "";
            match.status = false;
            match.CreatedTime = DateTime.Now;
            match.UptatedTime = DateTime.Now;
            match.Asists = "{}";
            match.Goals = "{}";
            match.AwayCorners = 0;
            match.HomeCorners = 0;
            match.AwayRedCards = "{}";
            match.HomeRedCards = "{}";
            match.AwayYellowCards = "{}";
            match.HomeYellowCards = "{}";
            match.AwayBallPlaying = 0;
            match.HomeBallPlaying = 0;
            match.AwaySetPlay = 0;
            match.HomeSetPlay = 0;
            match.isAccepted = false;
            match.isRejected = false;

            _footballMatchManager.TAdd(match);
            return Json(new { success = true, message = "Maç Teklifi Gönderildi" });
        }

        //Eski Fonksiyon
        //public IActionResult UpcomingPropose()
        //{
        //    var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //    var manager = _managerManager.TGetById(managerId);
        //    var club = _managerClubManager.TGetById(manager.ManagerClubId.Value);

        //    var matches = _footballMatchManager.GetClubMatches(club.Id);
        //    List<MatchesViewModel> matchesViewModel = new List<MatchesViewModel>();

        //    foreach (var match in matches)
        //    {
        //        match.HomeClub = _managerClubManager.TGetById(match.HomeClubId);
        //        match.AwayClub = _managerClubManager.TGetById(match.AwayClubId);

        //        var isHomeClub = match.HomeClubId == club.Id ? true : false;

        //        var secondManager = isHomeClub == true ? _managerManager.FindManagerWithClubId(match.AwayClubId)
        //            : _managerManager.FindManagerWithClubId(match.HomeClubId);

        //        LineUpViewModel homeLineUp = JsonConvert.DeserializeObject<LineUpViewModel>(match.HomeClub.LineUp);
        //        float homeLineUpPower = 0;
        //        foreach (var footballerId in homeLineUp.Footballers)
        //        {
        //            if (footballerId == 0)
        //            {
        //                continue;
        //            }
        //            var footballer = _clubFootballerManager.TGetById(footballerId);
        //            homeLineUpPower += footballer.Confidence
        //                + footballer.Defending + footballer.DefendingUpgrade
        //                + footballer.Pace + footballer.PaceUpgrade
        //                + footballer.Dribbling + footballer.DribblingUpgrade
        //                + footballer.Passing + footballer.PassingUpgrade
        //                + footballer.Shooting + footballer.ShootingUpgrade;
        //        }

        //        LineUpViewModel awayLineUp = JsonConvert.DeserializeObject<LineUpViewModel>(match.AwayClub.LineUp);
        //        float awayLineUpPower = 0;
        //        foreach (var footballerId in awayLineUp.Footballers)
        //        {
        //            if (footballerId == 0)
        //            {
        //                continue;
        //            }
        //            var footballer = _clubFootballerManager.TGetById(footballerId);
        //            awayLineUpPower += footballer.Confidence
        //                + footballer.Defending + footballer.DefendingUpgrade
        //                + footballer.Pace + footballer.PaceUpgrade
        //                + footballer.Dribbling + footballer.DribblingUpgrade
        //                + footballer.Passing + footballer.PassingUpgrade
        //                + footballer.Shooting + footballer.ShootingUpgrade;
        //        }

        //        int homeWinningPercent = Convert.ToInt32(((awayLineUpPower) / (homeLineUpPower + awayLineUpPower)) * 100);

        //        matchesViewModel.Add(new MatchesViewModel
        //        {
        //            HomeTeamManagerName = isHomeClub ? manager.Name + manager.SurName : secondManager.Name + secondManager.SurName,
        //            AwayTeamManagerName = isHomeClub ? secondManager.Name + secondManager.SurName : manager.Name + manager.SurName,
        //            HomeTeamManagerAvatar = isHomeClub ? manager.Avatar : secondManager.Avatar,
        //            AwayTeamManagerAvatar = isHomeClub ? secondManager.Avatar : manager.Avatar,
        //            FootballMatch = match,
        //            isHomeClub = isHomeClub,
        //            homeWinning = homeWinningPercent,
        //            awayWinning = 100 - homeWinningPercent,
        //        });

        //    }

        //    return View(matchesViewModel);
        //}

        //Yeni Fonksiyon
        public IActionResult UpcomingPropose()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var manager = _managerManager.TGetById(managerId);
            var club = _managerClubManager.TGetById(manager.ManagerClubId.Value);

            var matches = _footballMatchManager.GetClubMatches(club.Id);
            List<MatchesViewModel> matchesViewModel = new List<MatchesViewModel>();

            foreach (var match in matches)
            {
                match.HomeClub = _managerClubManager.TGetById(match.HomeClubId);
                match.AwayClub = _managerClubManager.TGetById(match.AwayClubId);

                var isHomeClub = match.HomeClubId == club.Id ? true : false;

                var secondManager = isHomeClub == true ? _managerManager.FindManagerWithClubId(match.AwayClubId)
                    : _managerManager.FindManagerWithClubId(match.HomeClubId);


                var homeFormation = _formationManager.GetByClubId(match.HomeClubId);
                if (homeFormation != null)
                {
                    homeFormation.XIPlayers = _xiPlayerManager.GetFormationPlayer(homeFormation.Id);
                    foreach (var player in homeFormation.XIPlayers)
                    {
                        if (player.AddedFootballerId != null)
                        {
                            player.AddedFootballer = _addedFootballerManager.TGetById(player.AddedFootballerId.Value);
                            player.AddedFootballer.Footballer = _footballerManager.GetEveryThink(player.AddedFootballer.FootballerId);
                        }
                    }
                }
                var awayFormation = _formationManager.GetByClubId(match.AwayClubId);
                if (awayFormation != null)
                {
                    awayFormation.XIPlayers = _xiPlayerManager.GetFormationPlayer(awayFormation.Id);
                    foreach (var player in awayFormation.XIPlayers)
                    {
                        if (player.AddedFootballerId != null)
                        {
                            player.AddedFootballer = _addedFootballerManager.TGetById(player.AddedFootballerId.Value);
                            player.AddedFootballer.Footballer = _footballerManager.GetEveryThink(player.AddedFootballer.FootballerId);
                        }
                    }
                }


                float homeLineUpPower = 0;
                if (homeFormation != null)
                {
                    foreach (var addfootballer in homeFormation.XIPlayers)
                    {
                        if (addfootballer.AddedFootballer != null)
                        {
                            var footballer = addfootballer.AddedFootballer.Footballer.Statistic;
                            if (footballer != null)
                                homeLineUpPower += Convert.ToInt32(
                                    footballer.Power);
                        }
                    }
                }

                float awayLineUpPower = 0;
                if (awayFormation != null)
                {
                    foreach (var addfootballer in awayFormation.XIPlayers)
                    {

                        if (addfootballer.AddedFootballer != null)
                        {
                            var footballer = addfootballer.AddedFootballer.Footballer.Statistic;
                            if (footballer != null)
                                awayLineUpPower += Convert.ToInt32(
                                    footballer.Power);
                        }
                    }
                }

                int homeWinningPercent = 0;

                try
                {
                    homeWinningPercent = Convert.ToInt32(((awayLineUpPower) / (homeLineUpPower + awayLineUpPower)) * 100);
                }
                catch
                {
                    homeWinningPercent = 50;
                }
                matchesViewModel.Add(new MatchesViewModel
                {
                    HomeTeamManagerName = isHomeClub ? manager.Name + manager.SurName : secondManager.Name + secondManager.SurName,
                    AwayTeamManagerName = isHomeClub ? secondManager.Name + secondManager.SurName : manager.Name + manager.SurName,
                    HomeTeamManagerAvatar = isHomeClub ? manager.Avatar : secondManager.Avatar,
                    AwayTeamManagerAvatar = isHomeClub ? secondManager.Avatar : manager.Avatar,
                    FootballMatch = match,
                    isHomeClub = isHomeClub,
                    homeWinning = homeWinningPercent,
                    awayWinning = 100 - homeWinningPercent,
                });
            }
            return View(matchesViewModel);
        }

        [HttpPost]
        public IActionResult GetMatch(int id)
        {
            var matchId = id;
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var manager = _managerManager.TGetById(managerId);

            var match = _footballMatchManager.TGetById(matchId);
            var status = match.HomeClubId == manager.ManagerClubId ? true : false;

            match.HomeClub = _managerClubManager.TGetById(match.HomeClubId);
            match.AwayClub = _managerClubManager.TGetById(match.AwayClubId);

            match.HomeClub.Formation = _formationManager.GetByClubId(match.HomeClub.Id);
            match.AwayClub.Formation = _formationManager.GetByClubId(match.AwayClub.Id);

            if (match.HomeClub.Formation != null)
                match.HomeClub.Formation.XIPlayers = _xiPlayerManager.GetFormationPlayer(match.HomeClub.Formation.Id);
            if (match.AwayClub.Formation != null)
                match.AwayClub.Formation.XIPlayers = _xiPlayerManager.GetFormationPlayer(match.AwayClub.Formation.Id);



            var homeClubStatus = match.HomeClub.Formation == null ? false : match.HomeClub.Formation.XIPlayers.Count == 11 ? true : false;

            var awayClubStatus = match.AwayClub.Formation == null ? false : match.AwayClub.Formation.XIPlayers.Count == 11 ? true : false;

            return Json(new
            {
                success = true,
                isHomeClub = status,
                matchData = match,
                hClubStatus = homeClubStatus,
                aClubStatus = awayClubStatus
            });
        }

        [HttpPost]
        public IActionResult AcceptMatch(int id)
        {
            var match = _footballMatchManager.TGetById(id);
            match.HomeClub = _managerClubManager.TGetById(match.HomeClubId);
            match.HomeClub.Formation = _formationManager.GetByClubId(match.HomeClubId);
            match.HomeClub.Formation.XIPlayers = _xiPlayerManager.GetFormationPlayer(match.HomeClub.Formation.Id);
            foreach (var item in match.HomeClub.Formation.XIPlayers)
            {
                item.AddedFootballer = _addedFootballerManager.TGetById(item.AddedFootballerId.Value);
                item.AddedFootballer.Footballer = _footballerManager.GetEveryThink(item.AddedFootballer.FootballerId);
            }

            match.AwayClub = _managerClubManager.TGetById(match.AwayClubId);
            match.AwayClub.Formation = _formationManager.GetByClubId(match.AwayClubId);
            match.AwayClub.Formation.XIPlayers = _xiPlayerManager.GetFormationPlayer(match.AwayClub.Formation.Id);
            foreach (var item in match.AwayClub.Formation.XIPlayers)
            {
                item.AddedFootballer = _addedFootballerManager.TGetById(item.AddedFootballerId.Value);
                item.AddedFootballer.Footballer = _footballerManager.GetEveryThink(item.AddedFootballer.FootballerId);
            }


            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "matchtext.json");
            var jsonData = System.IO.File.ReadAllText(filePath);

            var matchTexts = JsonConvert.DeserializeObject<MatchTextModelView>(jsonData);


            var homeClubFormation = match.HomeClub.Formation.FotmationType.Split('-').Select(Int32.Parse).ToArray();
            var homeClubFootballers = match.HomeClub.Formation.XIPlayers;
            var awayClubFormation = match.HomeClub.Formation.FotmationType.Split('-').Select(Int32.Parse).ToArray();
            var awayClubFootballers = match.HomeClub.Formation.XIPlayers;



            match.status = true;
            match.isAccepted = true;
            match.isRejected = false;

            match.MatchString = " ";
            Random random = new Random();

            var homeTeamHaveBall = true;
            var homeClubScore = 0;
            var awayClubScore = 0;

            for (var i = 0; i < 96; i++)
            {
                homeTeamHaveBall = !homeTeamHaveBall;
                if (i == 0)
                {
                    int randomIndex = random.Next(matchTexts.MatchStart.Count);
                    match.MatchString += matchTexts.MatchStart[randomIndex] + " \n";
                }
                else if (i == 30)
                {
                    int randomIndex = random.Next(matchTexts.ThirtiethMinute.Count);
                    match.MatchString += matchTexts.ThirtiethMinute[randomIndex] + " \n";
                }
                else if (i == 46)
                {
                    int randomIndex = random.Next(matchTexts.SecondHalfStart.Count);
                    match.MatchString += matchTexts.SecondHalfStart[randomIndex] + " \n";
                }
                else if (i == 60)
                {
                    int randomIndex = random.Next(matchTexts.SixtiethMinute.Count);
                    match.MatchString += matchTexts.SixtiethMinute[randomIndex] + " \n";
                }
                else if (i == 90)
                {
                    int randomIndex = random.Next(matchTexts.NinetiethMinute.Count);
                    match.MatchString += matchTexts.NinetiethMinute[randomIndex] + " \n";
                }
                else if (i == 95)
                {
                    int randomIndex = random.Next(matchTexts.MatchEnd.Count);
                    match.MatchString += matchTexts.MatchEnd[randomIndex] + " \n";
                }
                else
                {

                    var formation = homeTeamHaveBall ? homeClubFormation : awayClubFormation;
                    var players = homeTeamHaveBall ? homeClubFootballers : awayClubFootballers;

                    var enemyFormation = homeTeamHaveBall ? awayClubFormation : homeClubFormation;
                    var enemyPlayers = homeTeamHaveBall ? awayClubFootballers : homeClubFootballers;

                    var clubName = homeTeamHaveBall ? match.HomeClub.Name : match.AwayClub.Name;
                    var enemyClubName = homeTeamHaveBall ? match.AwayClub.Name : match.HomeClub.Name;

                    

                    int randomNumber = random.Next(1, 101);
                    if (randomNumber > 50)
                        continue;
                    else if (randomNumber > 40)
                    {

                        int shootPower = 0;
                        for (var k = 0; k < formation[4]; k++)
                        {
                            shootPower += players[k].AddedFootballer.Footballer.Statistic.Shooting;
                        }
                        shootPower -= enemyPlayers[0].AddedFootballer.Footballer.Statistic.Pace;
                        int randomGoal = random.Next(1, 301);


                        if (randomGoal < shootPower)
                        {
                            int rand = random.Next(matchTexts.Attack.Count);
                            int randFootballer = random.Next(formation[4]);
                            match.MatchString += "Dakikalar " + i + "' i gösteriyor. Top " + clubName + "nda " + matchTexts.Attack[rand] + " ";
                            int rand2 = random.Next(matchTexts.Goal.Count);

                            var playerSurName = players[randFootballer].AddedFootballer.Footballer.SurName;
                            match.MatchString +=
                                playerSurName
                                + playerSurName[playerSurName.Length - 1]
                                + playerSurName[playerSurName.Length - 1]
                                + playerSurName[playerSurName.Length - 1]
                                + playerSurName[playerSurName.Length - 1] + " "
                                + matchTexts.Goal[rand2] + " "
                                + " \n";

                            if (homeTeamHaveBall)
                            {
                                homeClubScore++;
                                match.MatchString += clubName + " maçı " + homeClubScore + " " + awayClubScore + "' e getirdi. \n";
                            }
                            else
                            {
                                awayClubScore++;
                                match.MatchString += clubName + " maçı " + homeClubScore + " " + awayClubScore + "' e getirdi. \n";
                            }


                        }
                        else
                        {
                            int rand = random.Next(matchTexts.Attack.Count);
                            match.MatchString += "Dakikalar " + i + "' i gösteriyor. " + matchTexts.Attack[rand] + " ";
                            int rand2 = random.Next(matchTexts.GoalKeeper.Count);
                            var goalKeeperName = enemyPlayers[10].AddedFootballer.Footballer.SurName;
                            match.MatchString += matchTexts.GoalKeeper[rand2] + " "
                                + goalKeeperName
                                + goalKeeperName[goalKeeperName.Length - 1]
                                + goalKeeperName[goalKeeperName.Length - 1]
                                + goalKeeperName[goalKeeperName.Length - 1]
                                + goalKeeperName[goalKeeperName.Length - 1]
                                + " \n";
                        }


                    }
                    else if (randomNumber > 30)
                    {
                        int randomindex = random.Next(100);

                        int randFootballer = random.Next(9);
                        if (randomindex < 20)
                        {
                            int rand = random.Next(matchTexts.SetPiece.Count);
                            match.MatchString += "Dakikalar " + i + "' i gösteriyor. " + matchTexts.SetPiece[rand] + " ";
                            int rand2 = random.Next(matchTexts.SetPiece.Count);
                            match.MatchString += matchTexts.Goal[rand2] + " " + players[randFootballer].AddedFootballer.Footballer.SurName
                                + " \n ";
                            if (homeTeamHaveBall)
                            {
                                homeClubScore++;
                                match.MatchString += clubName + " maçı " + homeClubScore + " " + awayClubScore + "' e getirdi. \n";
                            }
                            else
                            {
                                awayClubScore++;
                                match.MatchString += clubName + " maçı " + homeClubScore + " " + awayClubScore + "' e getirdi. \n";
                            }
                        }
                        else if (randomindex < 40)
                        {
                            int rand = random.Next(matchTexts.SetPiece.Count);
                            match.MatchString += "Dakikalar " + i + "' i gösteriyor. " + matchTexts.SetPiece[rand] + " ";
                            int rand2 = random.Next(matchTexts.SetPieceFail.Count);
                            match.MatchString += matchTexts.SetPieceFail[rand2] + " \n";
                        }
                    }

                }
                


            }
            match.MatchString += "Maç " + homeClubScore + " " + awayClubScore + "' bitiyor. \n";
            
            match.HomeClubGoal = homeClubScore;
            match.AwayClubGoal = awayClubScore;

            match.HomeClub.Formation.XIPlayers = null;
            match.AwayClub.Formation.XIPlayers = null;

            _footballMatchManager.TUpdate(match);


            return Json(new { success = true, message = "Maç Kabul Edilmiştir." });
        }

        [HttpPost]
        public IActionResult RejectedMatch(int id)
        {
            var match = _footballMatchManager.TGetById(id);
            match.isRejected = true;
            match.status = true;
            _footballMatchManager.TUpdate(match);

            return Json(new { success = true, message = "Maç Reddedilmiştir." });
        }

        public IActionResult Matches()
        {

            var matches = _footballMatchManager.GetPlayed();

            List<MatchesViewModel> matchesViewModel = new List<MatchesViewModel>();
            foreach (var match in matches)
            {
                match.HomeClub = _managerClubManager.TGetById(match.HomeClubId);
                match.AwayClub = _managerClubManager.TGetById(match.AwayClubId);

                var homeManager = _managerManager.TGetById(match.HomeClub.ManagerId);
                var awayManager = _managerManager.TGetById(match.AwayClub.ManagerId);

                matchesViewModel.Add(new MatchesViewModel
                {
                    HomeTeamManagerAvatar = homeManager.Avatar,
                    AwayTeamManagerAvatar = awayManager.Avatar,
                    HomeTeamManagerName = homeManager.Name + " " + homeManager.SurName,
                    AwayTeamManagerName = awayManager.Name + " " + awayManager.SurName,
                    FootballMatch = match,
                });
            }

            return View(matchesViewModel);
        }

        public IActionResult PlayedMatch(int id)
        {
            var match = _footballMatchManager.TGetById(id);
            match.HomeClub = _managerClubManager.TGetById(match.HomeClubId);
            match.AwayClub = _managerClubManager.TGetById(match.AwayClubId);

            var homeManager = _managerManager.TGetById(match.HomeClub.ManagerId);
            var awayManager = _managerManager.TGetById(match.AwayClub.ManagerId);

            var matchViewModel = new MatchesViewModel
            {
                HomeTeamManagerAvatar = homeManager.Avatar,
                AwayTeamManagerAvatar = awayManager.Avatar,
                HomeTeamManagerName = homeManager.Name + " " + homeManager.SurName,
                AwayTeamManagerName = awayManager.Name + " " + awayManager.SurName,
                FootballMatch = match,
            };

            return View(matchViewModel);
        }
    }
}

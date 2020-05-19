using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CalloutAPI;

namespace NylanderCallouts
{
    [CalloutProperties("Overfald", "Nylander", "0.0.1", Probability.Medium)]
    public class Fighting : CalloutAPI.Callout
    {
        Ped suspect, victim;
        public Fighting()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitBase(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Overfald";
            CalloutDescription = "To personer er oppe at slås!";
            ResponseCode = 2;
            StartDistance = 120f;
        }

        public async override Task Init()
        {
            OnAccept();

            suspect = await SpawnPed(GetRandomPed(), Location);
            victim = await SpawnPed(GetRandomPed(), Location);

            suspect.AlwaysKeepTask = true;
            victim.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            victim.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            suspect.Task.FightAgainst(victim);
            victim.Task.ReactAndFlee(suspect);
        }
    }
}

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
    [CalloutProperties("Skyderi", "Nylander", "0.0.1", Probability.Medium)]
    public class Shooting : CalloutAPI.Callout
    {
        Ped suspect1, suspect2;
        public Shooting()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitBase(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Skyderi";
            CalloutDescription = "To personer skyder mod hinanden!";
            ResponseCode = 3;
            StartDistance = 120f;
        }

        public async override Task Init()
        {
            OnAccept();

            suspect1 = await SpawnPed(GetRandomPed(), Location);
            suspect2 = await SpawnPed(GetRandomPed(), Location);

            suspect1.AlwaysKeepTask = true;
            suspect2.AlwaysKeepTask = true;
            suspect1.BlockPermanentEvents = true;
            suspect2.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            suspect1.Weapons.Give(WeaponHash.Pistol, 25, true, true);
            suspect2.Weapons.Give(WeaponHash.Pistol, 25, true, true);
            suspect1.Task.ShootAt(suspect2);
            suspect2.Task.ShootAt(suspect1);
        }
    }
}

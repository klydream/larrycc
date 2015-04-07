#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author$
// $Date$
// $HeadURL$
// $LastChangedBy$
// $LastChangedDate$
// $LastChangedRevision$
// $Revision$

#endregion

using Styx;
using Styx.Helpers;
using Styx.Pathing;
using Styx.TreeSharp;
using Action = Styx.TreeSharp.Action;
using System;
using Styx.WoWInternals.WoWObjects;
using Styx.WoWInternals.DBC;
using Styx.Common;
using System.Diagnostics;
using System.Timers;

namespace KingWoW
{
    public class Movement
    {
        private WoWUnit destinationUnit = null;
        private Timer updateMovementTimer;
        private double updateInterval = 200;
        private double range = 0;
        public double meelerange = 3;

        public Movement()
        {
            updateMovementTimer = new Timer(updateInterval);
            updateMovementTimer.Elapsed +=new ElapsedEventHandler(updateMovementTimer_Elapsed);
     
        }

        void  updateMovementTimer_Elapsed(object sender, ElapsedEventArgs e)
        {            
            //Logging.Write("moving: target distance=" + StyxWoW.Me.Location.Distance(destination) + " range is " + range);
            if (destinationUnit!= null && !destinationUnit.IsDead && destinationUnit.Distance-destinationUnit.CombatReach-1 <= range && destinationUnit.InLineOfSight && destinationUnit.InLineOfSpellSight)
            {
                //Logging.Write("Stop moving");
                Navigator.PlayerMover.MoveStop();
                updateMovementTimer.Interval = updateInterval;
                updateMovementTimer.Stop();
            }
            else
            {
                updateMovementTimer.Interval = updateInterval;
                updateMovementTimer.Start();
            }
        }

        public void KingHealMove(WoWUnit destination, double range)
        {
            destinationUnit = destination;
            this.range = range;
            if (destinationUnit != null && !destinationUnit.IsDead && destinationUnit.Distance - destinationUnit.CombatReach - 1 > range)
            {
                Navigator.MoveTo(destinationUnit.Location);
                updateMovementTimer.Interval = updateInterval;
                updateMovementTimer.Start();
            }
            
        }
    }
}
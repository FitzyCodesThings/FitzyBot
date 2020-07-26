using System;

namespace FitzyBot.Core.Entities
{
    public class Reward
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RedemptionPublicMessage { get; set; }
        public string RedemptionPrivateMessage { get; set; }
        public int PointsToRedeem { get; set; }
        public int? Supply { get; set; }
    }
}